﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Models
{
    public class FileDetail
    {
        public byte[] FileContents { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
