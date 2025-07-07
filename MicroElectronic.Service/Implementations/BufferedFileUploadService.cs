using MicroElectronic.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Service.Implementations
{
    public class BufferedFileUploadService : IBufferedFileUploadService
    {
        public async Task<string> UploadFile(IFormFile? file)
        {
            Guid guid = Guid.NewGuid();
            try
            {
                if (file?.Length > 0)
                {
                    var extension = Path.GetExtension(file.FileName).ToString();
                    var path = Directory.GetParent(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)).Parent.Parent.FullName + @"\wwwroot\images";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var fullPath = Path.Combine(path, guid.ToString() + extension);
                    using (var sssss = file.OpenReadStream())
                    {
                        byte[] buffer = new byte[16 * 1024];
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int read;
                            while ((read = sssss.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, read);
                            }
                            ;
                            File.WriteAllBytes(fullPath, ms.ToArray());
                        }
                    }
                    //using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                    //{
                    //    await file.CopyToAsync(fileStream);
                    //}
                    return $"images/{guid.ToString() + extension}";
                }
                else
                {
                    return "/images/default.png";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File copy failed", ex);
            }
        }
    }
}
