using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Stores.Models.Dtos
{
    public class StoreImageTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

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
