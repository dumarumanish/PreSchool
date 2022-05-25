using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Files
{
    /// <summary>
    /// All the external file of the application
    /// </summary>
    public class File : CommonProperties
    {

        /// <summary>
        /// Name of the file on upload
        /// </summary>
        public string OrginalFileName { get; set; }

        /// <summary>
        /// Name of the current file name 
        /// </summary>
        public string CurrentFileName { get; set; }

        public string FileExtension { get; set; }
        public string FileContentType { get; set; }

        /// <summary>
        /// Name of the entity of which this file is corresponded
        /// eg: Product, Customer
        /// </summary>
        public string EntityName { get; set; }


        /// <summary>
        /// Determine if the file is image or not, for performance increment
        /// </summary>
        public bool IsImage { get; set; }

        /// <summary>
        /// Gets or sets the of file path
        /// Format: File/{EntityName}/{CurrentFileName}
        /// </summary>
        public string Path { get; set; }
 
    }
}
