using MediaPlayer.Core.Entity;
using MediaPlayer.Core.RepositoryAbstraction;


namespace MediaPlayer.Service.src.ServiceImplemention
{
    public class PlayTrackService
    {
        private IPlayTrackRepository _playTrackRepository;

        public PlayTrackService(IPlayTrackRepository playTrackRepository)
        {
            _playTrackRepository = playTrackRepository;
        }

        public Playtrack CreatePlayTrack(string name, Guid userId)
        {
            if (name is not null)
            {
                var playTrack = new Playtrack(name);

                _playTrackRepository.CreatePlayTrack(playTrack,userId);

                return playTrack;
            }
            return null;
        }

        public void AddMediaToPlayTrack(Guid playTrackId,Guid userId,Guid mediaId)
        {
             _playTrackRepository.AddMediaToPlayTrack(playTrackId,userId, mediaId);
        }

        public void RemoveMediaToPlayTrack(Guid playTrackId, Guid userId, Guid mediaId)
        {
            _playTrackRepository.RemoveMediaToPlayTrack(playTrackId, userId, mediaId);
        }
    }
}