
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;

namespace MediaPlayer.Service.src.ServiceImplemention
{
    public class AuthorizationService
    {
        private User _currentUser;

        public void Login(User user)
        {
            _currentUser = user;
            Console.WriteLine($"User {_currentUser.UserName} logged in.");
        }

        public void Logout()
        {
            Console.WriteLine($"User {_currentUser.UserName} logged out.");
            _currentUser = null;
        }

        public bool IsAdmin()
        {
            return _currentUser?.UserType == UserType.Admin;
        }

        public User CurrentUser => _currentUser;
    }
}