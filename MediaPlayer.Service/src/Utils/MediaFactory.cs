using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;

namespace MediaPlayer.Service.src.Utils
{
    public class MediaFactory
    {
        public MediaFactory()
        {
        }
        public MediaFile CreateMedia(string username, MediaType type)
        {

            var media = new MediaFile(username, type);
            return media;
        }
    }
}