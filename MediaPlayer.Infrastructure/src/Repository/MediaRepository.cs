using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Core.RepositoryAbstraction;
using MediaPlayer.Infrastructure.src.Data;
using MediaPlayer.Service.src.DTO;

namespace MediaPlayer.Infrastructure.src.Repository
{
    public class MediaRepository : IMediaFileRepository
    {
        private List<MediaFile> _medias;

        public MediaRepository(Database database)
        {
            _medias = database._mediaFiles;
        }

        public MediaFile ChangeMediaStatus(Guid id, MediaStatus status)
        {
            var mediaFile = _medias.FirstOrDefault(m => m.MediaFileId == id);
            mediaFile.Status = status;
            return mediaFile;
        }

        public MediaFile GetMediaFileById(Guid mediaFileId)
        {
            return _medias.FirstOrDefault(m => m.MediaFileId == mediaFileId);
        }

        public MediaFile CreateNewMediaFile(MediaFile mediaFile)
        {
            _medias.Add(mediaFile);
            return mediaFile;
        }

        public IEnumerable<MediaFile> GetMedias()
        {
            return _medias.ToList();
        }

        public void DeleteMediaFile(Guid id)
        {
            var deletedFile = _medias.FirstOrDefault(m => m.MediaFileId == id);
            if (deletedFile is not null)
            {
                _medias.Remove(deletedFile);
                Console.WriteLine("Media file deleted successfully.");
                return;
            };
            Console.WriteLine($"Media file with ID {id} not found.");
            return;
        }

        public MediaFile? UpdateMediaFile(Guid id, MediaFileDto mediaFile)
        {
            var updatedMedia = _medias.FirstOrDefault(m => m.MediaFileId == id);
            if (updatedMedia is not null)
            {
                updatedMedia.FileName = mediaFile.FileName;
                updatedMedia.Type = mediaFile.Type;

                Console.WriteLine("Media file updated successfully.");
                return updatedMedia;
            };
            Console.WriteLine($"Media file with ID {id} not found.");
            return null;
        }

        public void UpdateMediaFile(MediaFile mediaFile)
        {
            var existingMediaFile = _medias.FirstOrDefault(m => m.MediaFileId == mediaFile.MediaFileId);
            if (existingMediaFile != null)
            {
                existingMediaFile.SoundEffect = mediaFile.SoundEffect;
            }
            else
            {
                Console.WriteLine("Media file not found.");
            }
        }

    }
}