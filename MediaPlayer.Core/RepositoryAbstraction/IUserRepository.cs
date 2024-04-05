using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Service.src.DTO;

namespace MediaPlayer.Core.RepositoryAbstraction
{
    public interface IUserRepository
    {
        public User CreateNewUser(User user);

        public IEnumerable<User> GetUsers();

        public void DeleteUser(Guid id);

        public User UpdateUser(Guid id, UserDto user);
    }
}