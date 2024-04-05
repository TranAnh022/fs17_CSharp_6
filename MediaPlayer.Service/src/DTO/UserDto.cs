using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Enum;

namespace MediaPlayer.Service.src.DTO
{
    public class UserDto
    {
        public UserDto(string name, UserType type)
        {
            UserName = name;
            Type = type;
        }

        public string UserName { get; set; }
        public UserType Type { get; set; }
    }
}