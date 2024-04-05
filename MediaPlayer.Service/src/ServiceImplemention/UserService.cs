using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Core.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;


namespace MediaPlayer.Service.src.ServiceImplemention
{
    public class UserService
    {
        private IUserRepository _userRepository;
        private AuthorizationService _authorizationService;

        public UserService(IUserRepository userRepository, AuthorizationService authorizationService)
        {
            _userRepository = userRepository;
            _authorizationService = authorizationService;

        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetUsers();
        }


        public User CreateNewUser(string username, UserType userType)
        {
            if (_authorizationService.IsAdmin())
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var user = new User(username, userType);
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

        public void DeleteUser(Guid id)
        {
            if (_authorizationService.IsAdmin())
            {
                _userRepository.DeleteUser(id);
                return;
            }
            Console.WriteLine("Only admin can delete user");
        }

        public User UpdateUser(Guid id, UserDto user )
        {
            if (_authorizationService.IsAdmin())
            {
                return _userRepository.UpdateUser(id, user);
            }
            Console.WriteLine("Only admin can update user");
            return null;
        }
    }
}