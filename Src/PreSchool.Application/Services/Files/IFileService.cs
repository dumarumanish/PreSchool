using PreSchool.Application.Models;
using PreSchool.Application.Services.Files.Models;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Files
{
    public interface IFileService
    {
        Task<bool> DeleteFile(int id);
        Task<FileDetail> GetBinaryFile(int id);
        Task<int> InsertFile(InsertFileCommand file);
        Task<UserUploadFileDto> InsertUserUploadFile(InsertUserUploadFile file);
    }
}