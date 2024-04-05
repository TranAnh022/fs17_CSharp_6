using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.RepositoryAbstraction;
using MediaPlayer.Service.src.Utils;

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
                var playTrackFactory = new PlayTrackFactory();

                var playTrack = playTrackFactory.CreateTrack(name);

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