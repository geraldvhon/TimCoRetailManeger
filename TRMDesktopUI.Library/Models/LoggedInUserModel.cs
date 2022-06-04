using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDesktopUI.Library.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateCreated { get; set; }

        public void ResetUserModel()
        {
            Token = "";
            Id = "";
            Lastname = "";
            Firstname = "";
            EmailAddress = "";
            DateCreated = DateTime.MinValue;
        }
    }
}
