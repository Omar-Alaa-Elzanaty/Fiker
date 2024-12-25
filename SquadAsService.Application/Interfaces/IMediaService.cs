using Fiker.Domain.Dtos;

namespace Fiker.Application.Interfaces
{
    public interface IMediaService
    {
        Task<string> Save(MediaFile media);

        void Delete(string url);

        Task<string> Update(string? oldUrl, MediaFile newMedia);

        string GetUrl(string? url);
    }
}
