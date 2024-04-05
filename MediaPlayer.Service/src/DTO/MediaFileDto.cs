using MediaPlayer.Core.Enum;

namespace MediaPlayer.Service.src.DTO
{
    public class MediaFileDto
    {
        public MediaFileDto(string name, MediaType type)
        {
            FileName = name;
            Type = type;
        }

        public string FileName { get; set; }
        public MediaType Type { get; set; }

    }
}