using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Core.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Utils;

namespace MediaPlayer.Service.src.ServiceImplemention
{
    public class MediaFileService
    {
        private IMediaFileRepository _mediaFileRepository;

        public MediaFileService(IMediaFileRepository mediaFileRepository)
        {
            _mediaFileRepository = mediaFileRepository;
        }

        public bool IsAdmin(User user)
        {
            if (user.UserType == UserType.Admin) return true;
            return false;
        }

        public IEnumerable<MediaFile> GetAllMediaFile()
        {
            return _mediaFileRepository.GetMedias();
        }

        public void ChangeMediaStatus(Guid id, MediaStatus status)
        {
            _mediaFileRepository.ChangeMediaStatus(id, status);
        }

        public MediaFile CreateNewMediaFile(string mediaName, MediaType mediaFile, User userAdmin)
        {
            if (IsAdmin(userAdmin))
            {
                if (!string.IsNullOrEmpty(mediaName))
                {
                    var mediaFactory = new MediaFactory();
                    var media = mediaFactory.CreateMedia(mediaName, mediaFile);
                    Console.WriteLine("New media file created successfully.");
                    return _mediaFileRepository.CreateNewMediaFile(media);
                }
                Console.WriteLine("File name cannot be null or empty. Cannot create media file.");
            }
            else
            {
                Console.WriteLine("Only admin can create new media file");
            }
            return null;
        }

        public void DeleteMediaFile(Guid id, User userAdmin)
        {
            if (IsAdmin(userAdmin))
            {
                if (_mediaFileRepository.GetMedias().Any(m => m.MediaFileId == id))
                {
                    _mediaFileRepository.DeleteMediaFile(id);
                    return;
                }
                Console.WriteLine($"Can not find the media with id : {id}");
                return;
            }
            Console.WriteLine("Only admin can delete media file");
        }

        public MediaFile UpdateMediaFile(Guid id, MediaFileDto mediaFile, User userAdmin)
        {
            if (IsAdmin(userAdmin))
            {
                return _mediaFileRepository.UpdateMediaFile(id, mediaFile);
            }
            Console.WriteLine("Only admin can update media file");
            return null;
        }


    }
}