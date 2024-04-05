using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Core.RepositoryAbstraction;
using MediaPlayer.Infrastructure.src.Data;
using MediaPlayer.Service.src.DTO;

namespace MediaPlayer.Infrastructure.src.Repository
{
    public class UserRepository : IUserRepository
    {

        private HashSet<User> _users;

        public UserRepository(Database database)
        {
            _users = database._users;

        }

        public User CreateNewUser(User user)
        {
            if (!_users.Any(u => u.UserName == user.UserName))
            {
                _users.Add(user);
                return user;
            }
            return null;
        }

        public IEnumerable<User> GetUsers()
        {
            return _users.ToList();
        }

        public User? UpdateUser(Guid id, UserDto user)
        {
            var updatedUser = _users.FirstOrDefault(u => u.UserId == id);
            if (updatedUser is not null)
            {
                updatedUser.UserName = user.UserName;
                updatedUser.UserType = user.Type;

                Console.WriteLine("User updated successfully.");
                return updatedUser;
            };
            Console.WriteLine($"User with ID {id} not found.");
            return null;
        }

        public void DeleteUser(Guid id)
        {
            var deletedUser = _users.FirstOrDefault(u => u.UserId == id);
            if (deletedUser is not null)
            {
                _users.Remove(deletedUser);
                Console.WriteLine("User deleted successfully.");
                return;
            };
            Console.WriteLine($"User with ID {id} not found.");
            return; ;
        }
    }
}