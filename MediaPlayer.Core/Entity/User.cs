using MediaPlayer.Core.Abstraction;
using MediaPlayer.Core.Enum;

namespace MediaPlayer.Core.Entity
{
    public class User : IMediaAdjust
    {
        public User(string name, UserType userType )
        {
            UserName = name;
            UserId = Guid.NewGuid() ;
            UserType = userType;
            Volume = 0;
            Brightness = 100;
            Playtracks = new List<Playtrack>();
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public UserType UserType { get; set; }
        public List<Playtrack> Playtracks { get; set; }
        public int Volume { get; set; }
        public int Brightness { get; set; }

        public void AdjustBrightness(int amount)
        {
            Brightness = amount;
            Console.WriteLine($"Change the Brightness to {amount}");
        }

        public void AdjustVolumn(int amount)
        {
            Volume = amount;
            Console.WriteLine($"Change the Volumn to {amount}");
        }

        public override string ToString()
        {
            string userInformation = $"{UserName} - {UserType}\n";

            if (Playtracks.Any())
            {
                foreach (var playtrack in Playtracks)
                {
                    userInformation += $"Name: {playtrack.PlayTrackName}\n";

                    if (playtrack.MediaFiles.Any())
                    {
                        userInformation += "MediaFiles: ";

                        foreach (var mediaFile in playtrack.MediaFiles)
                        {
                            userInformation += $"{mediaFile.FileName}, ";
                        }
                    }
                    else
                    {
                        userInformation += "No media files";
                    }

                    userInformation += "\n";
                }
            }
            else
            {
                userInformation += "No playtracks";
            }

            return userInformation;
        }
    }
}