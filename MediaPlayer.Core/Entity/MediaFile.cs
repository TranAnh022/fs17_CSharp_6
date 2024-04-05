using MediaPlayer.Core.Enum;

namespace MediaPlayer.Core.Entity
{
    public class MediaFile
    {
        public MediaFile(string fileName, MediaType mediaType, Guid id = default)
        {
            MediaFileId = id == default ? Guid.NewGuid() : id;
            FileName = fileName;
            Type = mediaType;
            Status = MediaStatus.Stopped;
        }

        public Guid MediaFileId { get; set; }
        public string FileName { get; set; }
        public MediaType Type { get; set; }

        public MediaStatus Status { get; set; }

        public override string ToString()
        {
            return $"{FileName} - {Type} - {MediaFileId}";
        }
    }
}