using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaPlayer.Core.Abstraction
{
    public interface IMediaAdjust
    {
        public void AdjustVolumn(int amount);
        public void AdjustBrightness(int amount);
    }
}