using PreSchool.Application.Services.Files;
using PreSchool.Application.Services.Files.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeUser]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<UserUploadFileDto> InsertUserUploadFile([FromForm]InsertUserUploadFile file)
        {

            return await _fileService.InsertUserUploadFile(file);
        }


















    }
}