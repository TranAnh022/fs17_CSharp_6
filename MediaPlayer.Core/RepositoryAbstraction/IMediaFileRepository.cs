using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Service.src.DTO;

namespace MediaPlayer.Core.RepositoryAbstraction
{
    public interface IMediaFileRepository
    {
        public IEnumerable<MediaFile> GetMedias();

        public MediaFile CreateNewMediaFile(MediaFile mediaFile);

        public void DeleteMediaFile(Guid id);

        public MediaFile UpdateMediaFile(Guid id, MediaFileDto mediaFile);

        public MediaFile ChangeMediaStatus(Guid id, MediaStatus status);

        public MediaFile GetMediaFileById(Guid mediaFileId);
        public void UpdateMediaFile(MediaFile mediaFile);
    }
}