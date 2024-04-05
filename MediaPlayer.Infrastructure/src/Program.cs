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
                var authService = new AuthorizationService();
                var userService = new UserService(userRepo, authService);

                var mediaRepo = new MediaRepository(database);
                var mediaService = new MediaFileService(mediaRepo, authService);

                var playTrackRepo = new PlayTrackRepository(database);
                var playTrackService = new PlayTrackService(playTrackRepo);

                var adminUser = new User("adminUser", UserType.Admin);

                //-- Testing login --
                Console.WriteLine("--- Login ----");

                authService.Login(adminUser);

                Console.WriteLine();

                //--Testing add user --
                Console.WriteLine("--- Create User ----");
                userService.CreateNewUser("Ben", UserType.Memeber); //check unique user
                Console.WriteLine();

                var testUser = userService.CreateNewUser("Test user", UserType.Memeber);
                Console.WriteLine();

                Console.WriteLine("--- Create File ----");
                var testFile = mediaService.CreateNewMediaFile("Test media", MediaType.Video);
                Console.WriteLine();

                var testFile2 = mediaService.CreateNewMediaFile("Test media2", MediaType.Audio);
                Console.WriteLine();

                //--Change the soundEffect --
                Console.WriteLine("--- Create Sound Effect ----");
                mediaService.UpdateMediaFileSoundEffect(testFile2.MediaFileId, SoundType.Reverb);
                Console.WriteLine($"Sound effect of media file updated: {testFile2}");
                Console.WriteLine();

                //--Testing adjust the volumn and brightness
                Console.WriteLine("--- Change Brightness and Volumn ----");
                testUser.AdjustBrightness(50);
                Console.WriteLine();
                testUser.AdjustVolumn(50);
                Console.WriteLine();

                //--Testing get users --
                Console.WriteLine("--- Get All Users ----");
                var users = userService.GetAllUsers();
                foreach (var user in users)
                {
                        Console.WriteLine(user);
                }
                Console.WriteLine();

                //--Testing get medias --
                Console.WriteLine("--- Get All Files ----");
                var medias = mediaService.GetAllMediaFile();
                foreach (var media in medias)
                {
                        Console.WriteLine(media);
                }
                Console.WriteLine();

                //--Testing add media to play track belonging to test user --
                Console.WriteLine("--- Create new PlayTrack (belong to testUser) ----");
                var testPlayTrack = playTrackService.CreatePlayTrack("Test Play Track", testUser.UserId);
                Console.WriteLine(testPlayTrack);
                Console.WriteLine();

                Console.WriteLine("--- Add media to PlayTrack (belong to testUser) ----");
                playTrackService.AddMediaToPlayTrack(testPlayTrack.PlayTrackId, testUser.UserId, testFile.MediaFileId);
                Console.WriteLine(testPlayTrack);
                Console.WriteLine();
                Console.WriteLine(testUser);

                //--Testing remove media from play track belonging to test user --
                Console.WriteLine("--- Remove media to PlayTrack (belong to testUser) ----");
                playTrackService.RemoveMediaToPlayTrack(testPlayTrack.PlayTrackId, testUser.UserId, testFile.MediaFileId);
                Console.WriteLine(testPlayTrack);
                Console.WriteLine();
                Console.WriteLine(testUser);

                //--Testing the update --
                Console.WriteLine("--- Update File ----");
                var updatedMediaFileDto = new MediaFileDto("updated test media", MediaType.Audio);
                var updatedMedia = mediaService.UpdateMediaFile(testFile.MediaFileId, updatedMediaFileDto);
                Console.WriteLine(updatedMedia);
                Console.WriteLine();
                Console.WriteLine("--- Update User ----");
                var updatedUserDto = new UserDto("updated test user", UserType.Memeber);
                var updatedUser = userService.UpdateUser(testUser.UserId, updatedUserDto);
                Console.WriteLine(updatedUser);
                Console.WriteLine();

                //--Testing delete
                Console.WriteLine("--- Delete User ----");
                userService.DeleteUser(testUser.UserId);
                Console.WriteLine();
                Console.WriteLine("--- Delete File ----");
                mediaService.DeleteMediaFile(testFile.MediaFileId);
                Console.WriteLine();

                // // Simulate logout process
                Console.WriteLine("--- Logout ----");
                authService.Logout();
        }
}
