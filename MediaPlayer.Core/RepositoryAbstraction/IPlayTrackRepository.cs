using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;

namespace MediaPlayer.Core.RepositoryAbstraction
{
    public interface IPlayTrackRepository
    {
        public Playtrack CreatePlayTrack(string name,Guid userId);

        public Playtrack AddMediaToPlayTrack(Guid playTrackId, Guid userId, Guid mediaId);

        public Playtrack RemoveMediaToPlayTrack(Guid playTrackId, Guid userId, Guid mediaId);


    }
}