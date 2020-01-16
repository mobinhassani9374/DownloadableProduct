using DownloadableProduct.Utillity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;

namespace DownloadableProduct.UI.Helpers
{
    public class FileHelper
    {
        private readonly IHostingEnvironment _env;
        public FileHelper(IHostingEnvironment env)
        {
            _env = env;
        }
        public ServiceResult<string> Upload(IFormFile formFile)
        {
            var result = new ServiceResult<string>(true);

            if (formFile == null)
                result.AddError("ImageIsNull");
            else
            {
                var extension = System.IO.Path.GetExtension(formFile.FileName);
                var fileName = $"{Guid.NewGuid()}{extension}";

                var path = System.IO.Path.Combine(_env.WebRootPath, "Files", fileName);

                var fileStream = new System.IO.FileStream(path,
                    System.IO.FileMode.Create);

                formFile.CopyTo(fileStream);

                fileStream.Close();

                result.Data = fileName;
            }

            return result;
        }
    }
}
