// See https://aka.ms/new-console-template for more information
using MediaPlayer.Core.Entity;
using MediaPlayer.Core.Enum;
using MediaPlayer.Infrastructure.src.Data;
using MediaPlayer.Infrastructure.src.Repository;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.ServiceImplemention;

internal class Program
{
        private static void Main(string[] args)
        {
                var database = Database.GetDatabase();
                var userRepo = new UserRepository(database);
                var userService = new UserService(userRepo);

                var mediaRepo = new MediaRepository(database);
                var mediaService = new MediaFileService(mediaRepo);

                var playTrackRepo = new PlayTrackRepository(database);
                var playTrackService = new PlayTrackService(playTrackRepo);

                var adminUser = new User("adminUser", UserType.Admin);


                //--Testing add user --
                userService.CreateNewUser("Ben", UserType.Memeber, adminUser); //check unique user
                Console.WriteLine();

                var testUser = userService.CreateNewUser("Test user", UserType.Memeber, adminUser);
                Console.WriteLine();

                var testFile = mediaService.CreateNewMediaFile("Test media", MediaType.Video,adminUser);
                Console.WriteLine();

                var testFile2 = mediaService.CreateNewMediaFile("Test media2", MediaType.Audio, adminUser);
                Console.WriteLine();

                //--Change the soundEffect --
                mediaService.UpdateMediaFileSoundEffect(testFile2.MediaFileId, SoundType.Reverb);
                Console.WriteLine($"Sound effect of media file updated: {testFile2}");
                Console.WriteLine();

                //--Testing adjust the volumn and brightness
                testUser.AdjustBrightness(50);
                Console.WriteLine();
                testUser.AdjustVolumn(50);
                Console.WriteLine();

                //--Testing get users --
                var users = userService.GetAllUsers();
                foreach (var user in users)
                {
                        Console.WriteLine(user);
                }
                Console.WriteLine();

                //--Testing get medias --
                var medias = mediaService.GetAllMediaFile();
                foreach (var media in medias)
                {
                        Console.WriteLine(media);
                }
                Console.WriteLine();

                //--Testing add media to play track belonging to test user --
                var testPlayTrack = playTrackService.CreatePlayTrack("Test Play Track", testUser.UserId);
                Console.WriteLine(testPlayTrack);
                Console.WriteLine();

                playTrackService.AddMediaToPlayTrack(testPlayTrack.PlayTrackId, testUser.UserId, testFile.MediaFileId);
                Console.WriteLine(testPlayTrack);
                Console.WriteLine();
                Console.WriteLine(testUser);

                //--Testing remove media from play track belonging to test user --
                playTrackService.RemoveMediaToPlayTrack(testPlayTrack.PlayTrackId, testUser.UserId, testFile.MediaFileId);
                Console.WriteLine(testPlayTrack);
                Console.WriteLine();
                Console.WriteLine(testUser);

                //--Testing the update --
                var updatedMediaFileDto = new MediaFileDto("updated test media", MediaType.Audio);
                var updatedMedia = mediaService.UpdateMediaFile(Guid.Parse("e159e0e4-e4e0-44a7-8fa3-233ba75bffda"), updatedMediaFileDto, adminUser);
                Console.WriteLine(updatedMedia);
                Console.WriteLine();

                var updatedUserDto = new UserDto("updated test user", UserType.Memeber);
                var updatedUser = userService.UpdateUser(testUser.UserId, updatedUserDto,adminUser);
                Console.WriteLine(updatedUser);
                Console.WriteLine();

                //--Testing delete
                userService.DeleteUser(testUser.UserId, adminUser);
                Console.WriteLine();

                mediaService.DeleteMediaFile(testFile.MediaFileId, adminUser);
                Console.WriteLine();

               ;
        }
}
