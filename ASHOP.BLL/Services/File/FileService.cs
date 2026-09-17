using ASHOP.DAL.DTO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ASHOP.BLL.Services.File
{
    public class FileService : IFileService
    {
        private static readonly string[] _allowedExtensions =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp",
            ".svg"
        };

        private const long _MaxFileSize = 5 * 1024 * 1024;
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;

        public FileService( IConfiguration configuration)
        {
            _configuration = configuration;
            var account = new Account(
            _configuration["CloudinarySettings:CloudName"],
            _configuration["CloudinarySettings:APIKey"],
            _configuration["CloudinarySettings:APISecret"]
        );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<Result<FileUploadResult>> UploadFile(IFormFile file)
        {
          
                if (file == null || file.Length == 0)
                {
                    return Result<FileUploadResult>.Fail(" no File  was provided.");
                }

                var extension = Path
                    .GetExtension(file.FileName)
                    .ToLower();

                if (!_allowedExtensions.Contains(extension))
                {
                    return Result<FileUploadResult>.Fail(
                        $"File type {extension} is not allowed."
                    );
                }

                if (file.Length > _MaxFileSize)
                {
                    return Result<FileUploadResult>.Fail(
                        $"File size exceeds the maximum limit of {_MaxFileSize / (1024 * 1024)} MB."
                    );
                }


            using (var stream = file.OpenReadStream())
            {
                var uploadparam = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "AShop"
                };
                var uploadResults = await _cloudinary.UploadAsync(uploadparam);

                if(uploadResults.Error != null)
                {
                    return Result<FileUploadResult>.Fail(uploadResults.Error.Message);

                }

                return Result<FileUploadResult>.Ok(new FileUploadResult
                {
                    Url = uploadResults.SecureUrl.ToString(),
                    PublicId = uploadResults.PublicId
                });


            }
           
           
        }

        public async Task<Result<bool>> Delete(string publicId)
        {
            var deleteparam = new  DeletionParams(publicId);
            var result= await _cloudinary.DestroyAsync(deleteparam);
            if (result.Result != "ok")
            {                return Result<bool>.Fail("Failed to delete image from cloudinary.");


            }
            return Result<bool>.Ok(true, "Image deleted successfully.");

        }
    }
}