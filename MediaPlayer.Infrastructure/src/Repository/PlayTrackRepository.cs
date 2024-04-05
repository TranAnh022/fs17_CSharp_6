
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.RepositoryAbstraction;
using MediaPlayer.Infrastructure.src.Data;

namespace MediaPlayer.Infrastructure.src.Repository
{
    public class PlayTrackRepository : IPlayTrackRepository
    {
        private List<Playtrack> _playTracks;
        private HashSet<User> _users;
        private List<MediaFile> _medias;

        public PlayTrackRepository(Database database)
        {
            _playTracks = database._playtracks;
            _users = database._users;
            _medias = database._mediaFiles;

        }

        public Playtrack AddMediaToPlayTrack(Guid playTrackId, Guid userId, Guid mediaId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            var media = _medias.FirstOrDefault(m => m.MediaFileId == mediaId);
            var playTrack = _playTracks.FirstOrDefault(p => p.PlayTrackId == playTrackId);

            if (user != null && playTrack != null)
            {
                var userPlayTrack = user.Playtracks.FirstOrDefault(p => p.PlayTrackId == playTrackId);
                if (userPlayTrack != null)
                {
                    if (userPlayTrack.MediaFiles == null)
                    {
                        userPlayTrack.MediaFiles = new List<MediaFile>();
                    }
                    userPlayTrack.MediaFiles.Add(media);
                    return userPlayTrack;
                }
            }
            return null;
        }


        public Playtrack CreatePlayTrack(Playtrack playtrack, Guid userId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                if (user.Playtracks == null)
                {
                    user.Playtracks = new List<Playtrack>();
                }
                user.Playtracks.Add(playtrack);
                _playTracks.Add(playtrack);
                return playtrack;
            }
            return null;
        }

        public Playtrack RemoveMediaToPlayTrack(Guid playTrackId, Guid userId, Guid mediaId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            var playTrack = _playTracks.FirstOrDefault(p => p.PlayTrackId == playTrackId);

            if (user != null && playTrack != null)
            {
                var userPlayTrack = user.Playtracks.FirstOrDefault(p => p.PlayTrackId == playTrackId);
                if (userPlayTrack != null)
                {
                    var mediaToRemove = userPlayTrack.MediaFiles.FirstOrDefault(m => m.MediaFileId == mediaId);
                    if (mediaToRemove != null)
                    {
                        userPlayTrack.MediaFiles.Remove(mediaToRemove);
                        return userPlayTrack;
                    }
                }
            }
            return null;
        }
    }
}