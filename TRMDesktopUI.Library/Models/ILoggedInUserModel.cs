using System;

namespace TRMDesktopUI.Library.Models
{
    public interface ILoggedInUserModel
    {
        DateTime DateCreated { get; set; }
        string EmailAddress { get; set; }
        string Firstname { get; set; }
        string Id { get; set; }
        string Lastname { get; set; }
        string Token { get; set; }

        void ResetUserModel();
    }
}