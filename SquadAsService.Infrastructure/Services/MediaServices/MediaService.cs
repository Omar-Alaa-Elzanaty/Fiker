using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SquadAsService.Application.Interfaces;
using SquadAsService.Domain.Dtos;

namespace SquadAsService.Infrastructure.Services.MediaServices
{
    public class MediaService : IMediaService
    {
        private readonly IConfiguration _configuration;

        public MediaService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Delete(string url)
        {
            var imageNameToDelete = Path.GetFileNameWithoutExtension(url);
            var ext = Path.GetExtension(url);
            var oldImagePath = $@"{_configuration["ImageSavePath"]}\Images\{imageNameToDelete}{ext}";

            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }
        }

        public string GetUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return null!;

            return _configuration["ImageSavePath"]!.ToString() + @"/" + url;
        }

        public async Task<string> Save(MediaFile media)
        {
            var extension = Path.GetExtension(media.FileName).ToLower();

            if (extension != ".png")
            {
                throw new Exception("Only PNG files are allowed.");
            }

            var uniqueFileName = Guid.NewGuid().ToString() + extension;

            var uploadsFolder = Path.Combine("wwwroot", "Images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(media.Base64));

            return uniqueFileName;
        }

        public async Task<string?> Update(string? oldUrl, MediaFile newMedia)
        {
            if (oldUrl == null && newMedia == null)
            {
                return null;
            }

            if (newMedia == null)
            {
                return oldUrl;
            }

            if (oldUrl == null)
            {
                return await Save(newMedia);
            }

            Delete(oldUrl);
            return await Save(newMedia)!;
        }
    }
}
