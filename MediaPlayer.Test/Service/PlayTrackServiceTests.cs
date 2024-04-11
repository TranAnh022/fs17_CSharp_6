using System;
using System.Collections.Generic;
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Core.RepositoryAbstraction;
using MediaPlayer.Service.src.ServiceImplemention;
using Xunit;

namespace MediaPlayer.Test.Service
{
    public class PlayTrackServiceTests
    {
        private readonly PlayTrackService _service;

        public PlayTrackServiceTests()
        {

            var repo = new PlayTrackFakeRepository();
            _service = new PlayTrackService(repo);
        }

        [Fact]
        public void AddMediaToPlayTrack_ShouldReturnUserPlayTrack()
        {
            var playtrack = new Playtrack("playTrackTest");
            var user = new User("ben", UserType.Memeber);
            var media = new MediaFile("test1", MediaType.Video);

            _service.AddMediaToPlayTrack(playtrack.PlayTrackId, user.UserId, media.MediaFileId);

            Assert.NotNull(user.Playtracks);
        }

        [Fact]
        public void RemoveMediaFromPlayTrack_ShouldReturnUserPlay()
        {
            var playtrack = new Playtrack("playTrackTest");
            var user = new User("ben", UserType.Memeber);
            var media = new MediaFile("test1", MediaType.Video);

            _service.AddMediaToPlayTrack(playtrack.PlayTrackId, user.UserId, media.MediaFileId);

            _service.RemoveMediaToPlayTrack(playtrack.PlayTrackId, user.UserId, media.MediaFileId);

            Assert.NotNull(user.Playtracks);
        }

        [Fact]
        public void CreatePlayTrack_ShouldReturnPlayTrack()
        {
            var user = new User("ben", UserType.Memeber);
            // Act
            var playtrack = _service.CreatePlayTrack("My Playlist", user.UserId);

            // Assert
            Assert.NotNull(playtrack);
            Assert.Equal("My Playlist", playtrack.PlayTrackName);
        }

        // Create a fake repository for testing purposes
        public class PlayTrackFakeRepository : IPlayTrackRepository
        {
            private readonly List<Playtrack> _playTracks;
            private readonly HashSet<User> _users;
            private readonly List<MediaFile> _medias;

            public PlayTrackFakeRepository()
            {
                var user = new User("ben", UserType.Memeber);
                _playTracks = new List<Playtrack>();
                _users.Add(user);
                _medias = new List<MediaFile>
            {
                new MediaFile("video1",MediaType.Video),
                new MediaFile("video2",MediaType.Video),
                new MediaFile("video3",MediaType.Video),
            };
            }

            public Playtrack AddMediaToPlayTrack(Guid playTrackId, Guid userId, Guid mediaId)
            {
                var user = _users.FirstOrDefault(u => u.UserId == userId);
                var media = _medias.FirstOrDefault(m => m.MediaFileId == mediaId);
                var playTrack = _playTracks.FirstOrDefault(p => p.PlayTrackId == playTrackId);

                if (user != null && playTrack != null && media != null)
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

            public Playtrack CreatePlayTrack(string name, Guid userId)
            {
                // Implement this method according to your test requirements
                throw new NotImplementedException();
            }

            public Playtrack RemoveMediaToPlayTrack(Guid playTrackId, Guid userId, Guid mediaId)
            {
                // Implement this method according to your test requirements
                throw new NotImplementedException();
            }
        }
    }
}