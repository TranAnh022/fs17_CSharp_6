
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using Xunit;

namespace MediaPlayer.Test.Core
{
    public class UserTest
    {
        [Fact]
        public void AdjustBrightness_ChangesBrightness()
        {
            // Arrange
            var user = new User("John", UserType.Memeber);
            var expectedBrightness = 50;

            // Act
            user.AdjustBrightness(expectedBrightness);

            // Assert
            Assert.Equal(expectedBrightness, user.Brightness);
        }

        [Fact]
        public void AdjustVolume_ChangesVolume()
        {
            // Arrange
            var user = new User("Alice", UserType.Admin);
            var expectedVolume = 70;

            // Act
            user.AdjustVolumn(expectedVolume);

            // Assert
            Assert.Equal(expectedVolume, user.Volume);
        }
    }
}