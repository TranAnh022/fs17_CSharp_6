namespace MediaPlayer.Core.Entity
{
    public class Playtrack
    {
        public Playtrack(string name)
        {
            PlayTrackId = Guid.NewGuid();
            PlayTrackName = name;
        }

        public Guid PlayTrackId { get; set; }
        public string PlayTrackName { get; set; }

        public List<MediaFile> MediaFiles { get; set; }

        public override string ToString()
        {
            string mediaFilesInfo = MediaFiles != null && MediaFiles.Any()
                ? string.Join(", ", MediaFiles)
                : "";

            return $"Name: {PlayTrackName}, MediaFiles: [{mediaFilesInfo}]";
        }
    }
}