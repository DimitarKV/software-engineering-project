using CloudinaryDotNet.Actions;

namespace HotelReservations.Helpers.Cloudinary;

public interface IImageUploader
{
    Task<ImageUploadResult> UploadImageAsync(string name, IFormFile file);
}