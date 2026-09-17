using ASHOP.DAL.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services.File
{
    public interface IFileService
    {
        Task<Result<FileUploadResult>> UploadFile(IFormFile file);

        Task<Result<bool>> Delete(string publicId);



    }
}
