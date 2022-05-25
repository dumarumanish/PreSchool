using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application
{
    public static class StringExtension
    {
        public static string SafeSubstring(this string orig, int length)
        {
            return orig.Substring(0, orig.Length >= length ? length : orig.Length);
        }
        public static string ReplaceParameters<T>(this string text, T parameter) where T : class
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;
            foreach (var prop in typeof(T).GetProperties())
            {
                text = text.Replace($"{{{prop.Name}}}", prop.GetValue(parameter)?.ToString(), true, null);
            }

            return text;
        }
    }
}
