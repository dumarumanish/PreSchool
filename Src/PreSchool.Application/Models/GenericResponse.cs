using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Models
{
    public class GenericResponse <T> where T : class
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public List<string> ErrorMessages { get; set; }
        public GenericResponse()
        {
            ErrorMessages = new List<string>();
        }
    }
}
