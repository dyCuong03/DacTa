using CloudinaryDotNet.Actions;

namespace SmartDevice.Service;
public interface IImageService
{
    Task<ImageUploadResult> AddImageAsync(IFormFile file);
    Task<DeletionResult> DeleteImageAsync(string publicId);
}