using Microsoft.AspNetCore.Http;

namespace Domain.Services
{
    public interface IImageService
    {
        Task<string> ValidateImage(IFormFile imageFile);
        void DeleteImage(string imageUrl);
        string GenerateUrl(string imageUrl);
    }
}
