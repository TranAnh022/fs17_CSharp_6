using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;

namespace MediaPlayer.Service.src.Utils
{
    public class PlayTrackFactory
    {
        public PlayTrackFactory()
        {
        }
        public Playtrack CreateTrack(string username)
        {
            var playTrack = new Playtrack(username);

            return playTrack;
        }
    }
}