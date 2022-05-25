using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Models
{
    public class EmailAttachment
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
    }
}
