using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Core.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;


namespace MediaPlayer.Service.src.ServiceImplemention
{
    public class MediaFileService
    {
        private IMediaFileRepository _mediaFileRepository;
        private AuthorizationService _authorizationService;

        public MediaFileService(IMediaFileRepository mediaFileRepository, AuthorizationService authorizationService)
        {
            _mediaFileRepository = mediaFileRepository;
            _authorizationService = authorizationService;
        }


        public IEnumerable<MediaFile> GetAllMediaFile()
        {
            return _mediaFileRepository.GetMedias();
        }

        public void ChangeMediaStatus(Guid id, MediaStatus status)
        {
            _mediaFileRepository.ChangeMediaStatus(id, status);
        }

        public MediaFile CreateNewMediaFile(string mediaName, MediaType mediaFile)
        {
            if (_authorizationService.IsAdmin())
            {
                if (!string.IsNullOrEmpty(mediaName))
                {
                    var media = new MediaFile(mediaName, mediaFile);
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

        public void DeleteMediaFile(Guid id)
        {
            if (_authorizationService.IsAdmin())
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

        public MediaFile UpdateMediaFile(Guid id, MediaFileDto mediaFile)
        {
            if (_authorizationService.IsAdmin())
            {
                return _mediaFileRepository.UpdateMediaFile(id, mediaFile);
            }
            Console.WriteLine("Only admin can update media file");
            return null;
        }

        public void UpdateMediaFileSoundEffect(Guid mediaFileId, SoundType newSoundEffect)
        {
            var mediaFile = _mediaFileRepository.GetMediaFileById(mediaFileId);

            if (mediaFile != null && mediaFile.Type == MediaType.Audio)
            {
                mediaFile.SoundEffect = newSoundEffect;
                _mediaFileRepository.UpdateMediaFile(mediaFile);
            }
            else
            {
                Console.WriteLine("Media file not found or not an audio file.");
            }
        }

    }
}