using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Core.RepositoryAbstraction;
using MediaPlayer.Service.src.ServiceImplemention;
using Moq;
using Xunit;

namespace MediaPlayer.Test.Service
{
    public class MediaFileServerTest
    {
        private readonly MediaFileService _mediaFileService;
        private readonly Mock<IMediaFileRepository> _mediaFileRepositoryMock;
        private readonly Mock<AuthorizationService> _authorizationServiceMock;

        public MediaFileServerTest()
        {
            _mediaFileRepositoryMock = new Mock<IMediaFileRepository>();
            _authorizationServiceMock = new Mock<AuthorizationService>();

            _mediaFileService = new MediaFileService(_mediaFileRepositoryMock.Object, _authorizationServiceMock.Object);
        }

        [Fact]
        public void GetAllMediaFile_ReturnsAllMediaFiles()
        {
            // Arrange
            var expectedMediaFiles = new List<MediaFile>
            {
                new MediaFile("media1", MediaType.Video),
                new MediaFile("media2", MediaType.Audio)
            };
            _mediaFileRepositoryMock.Setup(repo => repo.GetMedias()).Returns(expectedMediaFiles);

            // Act
            var actualMediaFiles = _mediaFileService.GetAllMediaFile();

            // Assert
            Assert.Equal(expectedMediaFiles, actualMediaFiles);
        }

        [Fact]
        public void ChangeMediaStatus_UpdatesMediaStatus()
        {
            // Arrange
            var media = new MediaFile("test", MediaType.Video);
            var newStatus = MediaStatus.Playing;

            // Act
            _mediaFileService.ChangeMediaStatus(media.MediaFileId, newStatus);

            // Assert
            Assert.Equal(media.Status, newStatus);
        }

        [Fact]
        public void CreateNewMediaFile_AdminCreatesMediaFile_Successfully()
        {
            // Arrange
            var adminUser = new User("admin", UserType.Admin);
            var newMediaName = "newmedia";
            var newMediaType = MediaType.Video;

            _authorizationServiceMock.Setup(auth => auth.IsAdmin()).Returns(true);

            // Act
            var createdMediaFile = _mediaFileService.CreateNewMediaFile(newMediaName, newMediaType);

            // Assert
            Assert.NotNull(createdMediaFile);
            Assert.Equal(newMediaName, createdMediaFile.FileName);
        }
    }
}