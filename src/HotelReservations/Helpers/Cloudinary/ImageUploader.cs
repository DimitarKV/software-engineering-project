using CloudinaryDotNet.Actions;

namespace HotelReservations.Helpers.Cloudinary;

public class ImageUploader : IImageUploader
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    public ImageUploader(CloudinaryDotNet.Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<ImageUploadResult> UploadImageAsync(string name, IFormFile file)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.Name + Guid.NewGuid(), file.OpenReadStream()),
            PublicId = name.Replace(" ", "")
        };
        return await _cloudinary.UploadAsync(uploadParams);
    }
}