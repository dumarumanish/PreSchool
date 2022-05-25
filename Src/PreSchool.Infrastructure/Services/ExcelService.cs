using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace PreSchool.Infrastructure.Services
{
    public class ExcelService : IExcelService
    {

        /// <summary>
        /// Common Code for the Export
        /// It creates Workbook, Sheet, Generate Header Cells and returns HttpResponseMessage
        /// </summary>
        /// <typeparam name="T">Generic Class Type</typeparam>
        /// <param name="exportData">Data to be exported</param>
        /// <param name="fileName">Export File Name</param>
        /// <param name="sheetName">First Sheet Name</param>
        /// <returns></returns>
        public FileDetail Export<T>(IList<T> exportData, string fileName,
            bool appendDateTimeInFileName = false,
            string sheetName = ExcelUtility.DEFAULT_SHEET_NAME)
        {

            List<string> _headers = new List<string>();
            List<string> _type = new List<string>();
            IWorkbook workbook;
            ISheet sheet;

            fileName = appendDateTimeInFileName
                ? $"{fileName}_{DateTime.Now.ToString(ExcelUtility.DEFAULT_FILE_DATETIME)}"
                : fileName;

            #region Generation of Workbook, Sheet and General Configuration
            workbook = new XSSFWorkbook();
            sheet = workbook.CreateSheet(sheetName);

            var headerStyle = workbook.CreateCellStyle();
            var headerFont = workbook.CreateFont();
            headerFont.IsBold = true;
            headerStyle.SetFont(headerFont);
            #endregion


            #region Read generic list and convert to datatabale
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            #region Reading property name to generate cell header
            foreach (PropertyDescriptor prop in properties)
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                _type.Add(type.Name);
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                string name = Regex.Replace(prop.Name, "([A-Z])", " $1").Trim(); //space seperated name by caps for header
                _headers.Add(name);
            }
            #endregion

            #region Generating Datatable from List
            foreach (T item in exportData)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            #endregion

            #endregion



            #region Generating Header Cells
            var header = sheet.CreateRow(0);
            for (var i = 0; i < _headers.Count; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(_headers[i]);
                cell.CellStyle = headerStyle;
                // It's heavy, it slows down your Excel if you have large data                
                sheet.AutoSizeColumn(i);
            }
            #endregion

            #region Generating SheetRow based on datatype
            IRow sheetRow = null;

            for (int i = 0; i < table.Rows.Count; i++)
            {

                sheetRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    // TODO: Below commented code is for Wrapping and Alignment of cell
                    // Row1.CellStyle = CellCentertTopAlignment;
                    // Row1.CellStyle.WrapText = true;
                    // ICellStyle CellCentertTopAlignment = _workbook.CreateCellStyle();
                    // CellCentertTopAlignment = _workbook.CreateCellStyle();
                    // CellCentertTopAlignment.Alignment = HorizontalAlignment.Center;

                    ICell Row1 = sheetRow.CreateCell(j);
                    string cellvalue = Convert.ToString(table.Rows[i][j]);

                    // TODO: move it to switch case

                    if (string.IsNullOrWhiteSpace(cellvalue))
                    {
                        Row1.SetCellValue(string.Empty);
                    }
                    else if (_type[j].ToLower() == ExcelUtility.STRING)
                    {
                        Row1.SetCellValue(cellvalue);
                    }
                    else if (_type[j].ToLower() == ExcelUtility.INT32)
                    {
                        Row1.SetCellValue(Convert.ToInt32(table.Rows[i][j]));
                    }
                    else if (_type[j].ToLower() == ExcelUtility.DOUBLE)
                    {
                        Row1.SetCellValue(Convert.ToDouble(table.Rows[i][j]));
                    }
                    else if (_type[j].ToLower() == ExcelUtility.DATETIME)
                    {
                        Row1.SetCellValue(Convert.ToDateTime
                             (table.Rows[i][j]).ToString(ExcelUtility.DATETIME_FORMAT));
                    }
                    else if (_type[j].ToLower() == ExcelUtility.BOOLEAN)
                    {
                        Row1.SetCellValue(Convert.ToBoolean(table.Rows[i][j]));
                    }
                    else if (_type[j].ToLower() == ExcelUtility.DECIMAL)
                    {
                        Row1.SetCellValue(Convert.ToDouble(table.Rows[i][j]));
                    }
                    else
                    {
                        Row1.SetCellValue(string.Empty);
                    }
                }
            }
            #endregion

            #region Generating and Returning Stream for Excel
            using (var memoryStream = new MemoryStream())
            {
                workbook.Write(memoryStream);

                return new FileDetail
                {
                    ContentType = ExcelUtility.EXCEL_MEDIA_TYPE,
                    FileContents = memoryStream.ToArray(),
                    FileName = $"{fileName}.xlsx"
                };
            }
            #endregion
        }



        public GenericResponse<List<T>> Import<T>(IFormFile file)
        {
            var response = new GenericResponse<List<T>>()
            {
                IsSuccess = false
            };

            List<T> outputList = new List<T>();

            if (file == null || file.Length == 0)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add("File is empty");
                return response;
            }

            string sFileExtension = Path.GetExtension(file.FileName).ToLower();

            ISheet sheet;
            IFormulaEvaluator evaluator;
            DataFormatter formatter = new DataFormatter();


            using (var stream = file.OpenReadStream())

            {
                stream.Position = 0;

                if (sFileExtension == ".xls")

                {
                    HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  

                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    evaluator = hssfwb.GetCreationHelper().CreateFormulaEvaluator();

                }

                else
                {
                    XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  

                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    evaluator = hssfwb.GetCreationHelper().CreateFormulaEvaluator();

                }


                try
                {
                    if (sheet != null)
                    {
                        IRow firstRow = sheet.GetRow(0);

                        // The number of the last cell in a row is the total number of columns
                        int cellCount = firstRow.LastCellNum;

                        // the label of the last column
                        int rowCount = sheet.LastRowNum;

                        for (int i = 1; i <= rowCount; ++i)
                        {
                            // Get the data of the row 
                            IRow row = sheet.GetRow(i);

                            if (row == null) continue; // rows without data are null by default
                            {
                                T model = Activator.CreateInstance<T>();

                                for (int j = row.FirstCellNum; j < cellCount; ++j)
                                {
                                    // Get columnName, Remove white spaces
                                    var columnName = firstRow.GetCell(j).ToString().Replace(" ", "");
                                    if (row.GetCell(j) != null)
                                    {
                                        var rowTemp = row.GetCell(j);

                                        string value = null;

                                        if (rowTemp.CellType == CellType.Numeric)
                                        {
                                            short format = rowTemp.CellStyle.DataFormat;

                                            if (format == 14 || format == 31 || format == 57 || format == 58 || format == 20)
                                                value = rowTemp.DateCellValue.ToString("yyyy-MM-dd");

                                            else
                                                value = rowTemp.NumericCellValue.ToString();

                                        }
                                        else
                                            value = formatter.FormatCellValue(rowTemp, evaluator);

                                        // Assign
                                        foreach (System.Reflection.PropertyInfo item in typeof(T).GetProperties())
                                        {
                                            //var column = item.GetCustomAttributes(true).First(x => x is ColumnAttribute) as ColumnAttribute;
                                            if (item.Name == columnName)
                                            {
                                                try
                                                {

                                                    item.SetValue(model, Convert.ChangeType(value, item.PropertyType));
                                                }
                                                catch
                                                {
                                                    // Return which cell have invalid values
                                                    response.ErrorMessages.Add($"Invalid data in row {i} on column \"{firstRow.GetCell(j)}\"");
                                                }
                                                break;

                                            }

                                        }

                                    }

                                }

                                outputList.Add(model);

                            }

                        }
                    }
                }
                catch (Exception)
                {
                    response.ErrorMessages.Add($"Invalid file");
                }

                finally

                {

                    stream.Close();

                }
                response.IsSuccess = response.ErrorMessages.Count == 0;

                if (response.IsSuccess)
                    response.Data = outputList;

                return response;
            }
        }
    }


    public class ExcelUtility
    {
        public const string DEFAULT_SHEET_NAME = "Sheet1";
        public const string DEFAULT_FILE_DATETIME = "yyyyMMdd_HHmm";
        public const string DATETIME_FORMAT = "dd/MM/yyyy hh:mm:ss";
        public const string EXCEL_MEDIA_TYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string DISPOSITION_TYPE_ATTACHMENT = "attachment";


        #region DataType available for Excel Export
        public const string STRING = "string";
        public const string INT32 = "int32";
        public const string DOUBLE = "double";
        public const string DATETIME = "datetime";
        public const string BOOLEAN = "boolean";
        public const string DECIMAL = "decimal";
        #endregion
    }
}
