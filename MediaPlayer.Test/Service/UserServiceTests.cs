using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Core.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.ServiceImplemention;
using Moq;
using Xunit;

namespace MediaPlayer.Test.Service
{
    public class UserServiceTests
    {
        private UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<AuthorizationService> _authorizationServiceMock;

        public UserServiceTests()
        {

            _userRepositoryMock = new Mock<IUserRepository>();
            _authorizationServiceMock = new Mock<AuthorizationService>();
        }

        [Fact]
        public void GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
        {
            new User("user1", UserType.Memeber),
            new User("user2", UserType.Admin)
        };
            _userRepositoryMock.Setup(repo => repo.GetUsers()).Returns(expectedUsers);

            _userService = new UserService(_userRepositoryMock.Object, _authorizationServiceMock.Object);

            // Act
            var actualUsers = _userService.GetAllUsers();

            // Assert
            Assert.Equal(expectedUsers, actualUsers);
        }

        [Fact]
        public void CreateNewUser_AdminCreatesUser_Successfully()
        {
            // Arrange
            var adminUser = new User("admin", UserType.Admin);
            var newUserDto = new UserDto("newuser", UserType.Memeber);

            _authorizationServiceMock.Setup(auth => auth.IsAdmin()).Returns(true);
            _userRepositoryMock.Setup(repo => repo.CreateNewUser(It.IsAny<User>())).Returns<User>(u => u);
            _userService = new UserService(_userRepositoryMock.Object, _authorizationServiceMock.Object);

            // Act
            var createdUser = _userService.CreateNewUser(newUserDto.UserName, newUserDto.Type);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(newUserDto.UserName, createdUser.UserName);
            Assert.Equal(newUserDto.Type, createdUser.UserType);
        }

        [Fact]
        public void UpdateUser_AdminUpdatesUser_Successfully()
        {
            // Arrange
            var adminUser = new User("admin", UserType.Admin);
            var userIdToUpdate = Guid.NewGuid();
            var updatedUserDto = new UserDto("updateduser", UserType.Admin);

            _authorizationServiceMock.Setup(auth => auth.IsAdmin()).Returns(true);
            _userRepositoryMock.Setup(repo => repo.UpdateUser(userIdToUpdate, updatedUserDto)).Returns(new User(updatedUserDto.UserName, updatedUserDto.Type));

            var userService = new UserService(_userRepositoryMock.Object, _authorizationServiceMock.Object);

            // Act
            var updatedUser = userService.UpdateUser(userIdToUpdate, updatedUserDto);

            // Assert
            Assert.NotNull(updatedUser);
            Assert.Equal(updatedUserDto.UserName, updatedUser.UserName);
            Assert.Equal(updatedUserDto.Type, updatedUser.UserType);
        }
    }
}