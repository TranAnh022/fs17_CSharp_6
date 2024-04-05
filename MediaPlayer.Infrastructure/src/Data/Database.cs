using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;

namespace MediaPlayer.Infrastructure.src.Data
{
    public class Database
    {
        public HashSet<User> _users;
        public List<MediaFile> _mediaFiles;
        public List<Playtrack> _playtracks;
        public User _currentUser;

        private static Database _instance;

        private Database()
        {
            _mediaFiles = [
                new MediaFile("video1",MediaType.Video),
                new MediaFile("video2",MediaType.Video),
                new MediaFile("video3",MediaType.Video),
                new MediaFile("audio1",MediaType.Audio),
                new MediaFile("audio2",MediaType.Audio),
                new MediaFile("audio3",MediaType.Audio),
            ];

            _users = [
                new User("Ben",UserType.Memeber),
                new User("Luke",UserType.Memeber),
                new User("Max",UserType.Memeber),
                new User("Admin",UserType.Admin),

            ];

            _playtracks = new List<Playtrack>();
            
        }

        public static Database GetDatabase()
        {
            if (_instance is null)
            {
                _instance = new Database();
            }
            return _instance;
        }

    }


}