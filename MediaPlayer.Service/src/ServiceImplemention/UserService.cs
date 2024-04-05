using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Core.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Utils;

namespace MediaPlayer.Service.src.ServiceImplemention
{
    public class UserService
    {
        private IUserRepository _userRepository;


        public UserService(IUserRepository userRepository )
        {
            _userRepository = userRepository;

        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetUsers();
        }

        public bool IsAdmin(User user)
        {
            if (user.UserType == UserType.Admin) return true;
            return false;
        }

        public User CreateNewUser(string username, UserType userType, User userAdmin)
        {
            if (IsAdmin(userAdmin))
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var userFactory = new UserFactory();
                    var user = userFactory.CreateUser(username, userType);

                    if (_userRepository.GetUsers().Any(u => u.UserName == user.UserName))
                    {
                        Console.WriteLine($"User with username '{user.UserName}' already exists. Cannot create user.");
                        return null;
                    }

                    Console.WriteLine($"New user name: {user.UserName} created successfully.");
                    return _userRepository.CreateNewUser(user);
                }
                Console.WriteLine("Username cannot be null or empty. Cannot create user.");
                return null ;
            }
            Console.WriteLine("Only admin can create new user");
            return null;
        }

        public void DeleteUser(Guid id, User userAdmin)
        {
            if (IsAdmin(userAdmin))
            {
                _userRepository.DeleteUser(id);
                return;
            }
            Console.WriteLine("Only admin can delete user");
        }

        public User UpdateUser(Guid id, UserDto user, User userAdmin)
        {
            if (IsAdmin(userAdmin))
            {
                return _userRepository.UpdateUser(id, user);
            }
            Console.WriteLine("Only admin can update user");
            return null;
        }
    }
}