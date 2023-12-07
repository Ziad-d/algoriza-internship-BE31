using Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace Service
{
    public class ImageService : IImageService
    {
        private new List<string> _allowedExtensions = new List<string> { ".png", ".jpg", ".webp" };
        private long _maxAllowedImageSize = 2097152;
        private readonly IWebHostEnvironment _environment;
        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string?> ValidateImage(IFormFile image)
        {
            if (image is null)
            {
                return "\\images\\No_Image.png";
            }

            if (!_allowedExtensions.Contains(Path.GetExtension(image.FileName).ToLower()))
                return null;

            if (image.Length > _maxAllowedImageSize)
                return null;

            string imgExtension = Path.GetExtension(image.FileName);
            Guid imgGuid = Guid.NewGuid();
            string imgName = imgGuid + imgExtension;
            string imgPath = "/images/" + imgName;
            string imgFullPath = _environment.WebRootPath + imgPath;
            FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
            await image.CopyToAsync(imgFileStream);
            imgFileStream.Dispose();
            return imgPath;
        }

        public void DeleteImage(string? imageUrl)
        {
            if (imageUrl != "\\images\\No_Image.png" && imageUrl is not null)
            {
                string oldImgFullPath = _environment.WebRootPath + imageUrl;
                File.Delete(oldImgFullPath);
            }
        }

        public string GenerateUrl(string imageUrl)
        {
            return _environment.WebRootPath + imageUrl;
            //var request = _httpContextAccessor.HttpContext.Request;
            //return $"{request.Scheme}://{request.Host.Host}{imageUrl}";

            /*if (imageUrl is null)
            {
                return null;
            }

            if (imageUrl.Split(":")[0] == "https")
            {
                return imageUrl;
            }
            else
            {
                return $"https://{request.Host.Host}{imageUrl}";
            }*/
        }
    }
}
