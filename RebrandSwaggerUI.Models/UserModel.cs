using System;

namespace RebrandSwaggerUI.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string UserId { get; set; }
    }

    public class UserModelV2 : UserModel
    {
        public DateTime StartDate { get; set; }
    }
}
