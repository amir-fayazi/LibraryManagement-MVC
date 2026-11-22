using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace Library.Web.Utilities;
public static class ImageStorage
{
    public static string? Save(IFormFile? file, string? currentPath, string folder, ModelStateDictionary errors)
    {
        if (file is null || file.Length == 0)
            return !string.IsNullOrWhiteSpace(currentPath) ? currentPath : $"/images/{folder}/default.svg";

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (file.Length > 2 * 1024 * 1024 || !new[] { ".jpg", ".jpeg", ".png", ".webp" }.Contains(extension))
        {
            errors.AddModelError("ImageFile", "تصویر باید JPG، PNG یا WEBP و حداکثر ۲ مگابایت باشد.");
            return null;
        }
        using var stream = file.OpenReadStream();
        var header = new byte[12];
        var read = stream.Read(header, 0, header.Length);
        var valid = (extension is ".jpg" or ".jpeg") && read >= 3 && header[0] == 0xff && header[1] == 0xd8 && header[2] == 0xff
            || extension == ".png" && read >= 8 && header.AsSpan(0, 8).SequenceEqual(new byte[] {137,80,78,71,13,10,26,10})
            || extension == ".webp" && read >= 12 && System.Text.Encoding.ASCII.GetString(header, 0, 4) == "RIFF" && System.Text.Encoding.ASCII.GetString(header, 8, 4) == "WEBP";
        if (!valid)
        {
            errors.AddModelError("ImageFile", "فایل انتخاب‌شده تصویر معتبر نیست.");
            return null;
        }
        var fileName = Path.GetRandomFileName() + extension;
        var directory = Path.Combine(AppServices.WebRootPath, "images", folder);
        Directory.CreateDirectory(directory);
        using var target = File.Create(Path.Combine(directory, fileName));
        stream.Position = 0;
        stream.CopyTo(target);
        return $"/images/{folder}/{fileName}";
    }
}
