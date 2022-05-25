using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Services.Files.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Files
{
    public class FileService : IFileService
    {
        private readonly IApplicationDbContext _context;
        private readonly IHostingEnvironmentService _hostingEnvironment;

        public FileService(IApplicationDbContext context,
            IHostingEnvironmentService hostingEnvironment
            )
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }



        /// <summary>
        /// Insert new file
        /// </summary>
        /// <param name="file"></param>
        /// <returns>id of the newly created file</returns>
        public async Task<int> InsertFile(InsertFileCommand file)
        {
            if (file == null || file.File == null)
                throw new BadRequestException("File not included");
            if (string.IsNullOrWhiteSpace(file.EntityName))
                throw new BadRequestException("Entity name is not defined, Contact administrator");
            try
            {
                var fileExtension = Path.GetExtension(file.File.FileName);
                var fileName = $"{file.File.FileName.SafeSubstring(15)}{DateTime.Now.Ticks}{fileExtension}";

                string folderName = $"Files/{file.EntityName}";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string folderPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var localFilePath = Path.Combine(folderPath, fileName);

              

                var newFile = new Data.Entities.Files.File
                {
                    FileContentType = file.File.ContentType,
                    IsDeleted = false,
                    FileExtension = fileExtension,
                    CurrentFileName = fileName,
                    EntityName = file.EntityName,
                    OrginalFileName = file.File.FileName,
                    Path = folderName + "/" + fileName,
                    IsImage = file.File.IsImage(),
                };

                _context.Files.Add(newFile);


                var stream = file.File.OpenReadStream();
                using (var fileStream = new FileStream(localFilePath, FileMode.Create))
                {
                    stream.CopyTo(fileStream);
                }
                if ((await _context.SaveChangesAsync()) > 0)
                    return newFile.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            throw new Exception("Cannot save the file, Contact Administrator");

        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        public async Task<UserUploadFileDto> InsertUserUploadFile(InsertUserUploadFile file)
        {
            if (file == null || file.file == null)
                throw new BadRequestException("File not included");

            var fileId = await InsertFile(new InsertFileCommand
            {
                File = file.file,
                EntityName = "UserUpload"
            });


            var uploadedFile = await _context.Files
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == fileId);

            if (uploadedFile == null)
                throw new NotFoundException("File is not uploaded.");

            return new UserUploadFileDto
            {
                FileId = uploadedFile.Id,
                FilePath = uploadedFile.Path
            };
        }

        public async Task<bool> DeleteFile(int id)
        {
            var file = await _context.Files
                .FirstOrDefaultAsync(s => s.Id == id);

            if (file == null)
                throw new NotFoundException("File not found");


            file.IsDeleted = true;
     
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<FileDetail> GetBinaryFile(int id)
        {
            var file = await _context.Files
                .FirstOrDefaultAsync(d => d.Id == id);

            if (file == null)
                throw new NotFoundException("No file found");

            string webRootPath = _hostingEnvironment.WebRootPath;
            string filePath = Path.Combine(webRootPath, file.Path);

            if (!File.Exists(filePath))
                throw new NotFoundException("File not found");

            return new FileDetail
            {
                ContentType = file.FileContentType,
                FileContents = File.ReadAllBytes(filePath),
                FileName = file.OrginalFileName,

            };
        }

       
    }
}
