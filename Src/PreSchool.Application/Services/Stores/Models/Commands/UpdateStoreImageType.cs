using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Stores.Models.Commands
{
    public class UpdateStoreImageType
    {
        public int Id { get; set; }

        /// <summary>
        /// Size of the image in kilo Byte, 0 for unlimited
        /// </summary>
        public int SizeLimitInKB { get; set; }

        /// <summary>
        /// Width of the image
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the image
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Allow multiple image of same type
        /// </summary>
        public bool AllowMultiple { get; set; }
        public int DisplayOrder { get; set; }
    }
}
