using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Service.src.DTO;

namespace MediaPlayer.Service.src.Utils
{
    public class UserFactory
    {
        public UserFactory()
        {
        }
        public User CreateUser(string username, UserType userType)
        {
            var user = new User(username, userType);

            return user;
        }
    }
}