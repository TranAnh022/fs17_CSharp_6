using MediaPlayer.Core.Abstraction;
using MediaPlayer.Core.Enum;

namespace MediaPlayer.Core.Entity
{
    public class MediaFile
    {
        public MediaFile(string fileName, MediaType mediaType )
        {
            MediaFileId =  Guid.NewGuid() ;
            FileName = fileName;
            Type = mediaType;
            Status = MediaStatus.Stopped;
            if (mediaType == MediaType.Audio)
            {
                SoundEffect = SoundType.Echo;
            }
        }

        public Guid MediaFileId { get; set; }
        public string FileName { get; set; }
        public MediaType Type { get; set; }
        public SoundType? SoundEffect { get; set; }
        public MediaStatus Status { get; set; }


        public override string ToString()
        {
            if (Type == MediaType.Audio)
            {
                return $"{FileName} - {Type} - Sound: {SoundEffect}";
            }
            else
            {
                return $"{FileName} - {Type}";
            }
        }
    }
}