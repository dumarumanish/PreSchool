//using Swashbuckle.AspNetCore.SwaggerGen;
//using System.Collections.Generic;

//namespace PreSchool.Filters
//{
//    public class SwaggerAcceptLanguageFilter : IOperationFilter
//    {
//        public void Apply(Operation operation, OperationFilterContext context)
//        {
//            if (operation.Parameters == null)
//                operation.Parameters = new List<IParameter>();

//            operation.Parameters.Add(new NonBodyParameter
//            {
//                Name = "Accept-Language",
//                In = "header",
//                Type = "string",
//                Default = "en",
//                Required = false // set to false if this is optional
//            });
//        }
//    }
//}