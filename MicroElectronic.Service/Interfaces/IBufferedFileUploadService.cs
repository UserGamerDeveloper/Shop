using Microsoft.AspNetCore.Http;

namespace MicroElectronic.Service.Interfaces
{
    public interface IBufferedFileUploadService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
