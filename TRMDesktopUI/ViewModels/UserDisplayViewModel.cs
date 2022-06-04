﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TRMDesktopUI.Library.API;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class UserDisplayViewModel : Screen
    {
        private readonly IUserEndpoint _userEndpoint;
        private readonly StatusInfoViewModel _status;
        private readonly IWindowManager _window;

        BindingList<UserModel> _users;
        public BindingList<UserModel> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        private UserModel _selectedUser;
                
        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set 
            { 
                _selectedUser = value;
                SelectedUserName = value.Email;
                UserRoles = new BindingList<string>(value.Roles.Select(x => x.Value).ToList());
                LoadRoles();
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        private string _selectedUserRole;

        public string SelectedUserRole
        {
            get { return _selectedUserRole; }
            set 
            { 
                _selectedUserRole = value;
                NotifyOfPropertyChange(() => SelectedUserRole);
            }
        }

        private string _selectedAvailableRole;

        public string SelectedAvailableRole
        {
            get { return _selectedAvailableRole; }
            set 
            { 
                _selectedAvailableRole = value;
                NotifyOfPropertyChange(() => SelectedAvailableRole);
            }
        }


        private string _selectedUserName;

        public string SelectedUserName
        {
            get { return _selectedUserName; }
            set 
            { 
                _selectedUserName = value;
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }

        private BindingList<string> _UserRoles = new BindingList<string>();

        public BindingList<string> UserRoles
        {
            get 
            { 
                return _UserRoles; 
            }
            set 
            { 
                _UserRoles = value;
                NotifyOfPropertyChange(() => UserRoles);
            }
        }

        private BindingList<string> _availableRoles = new BindingList<string>();

        public BindingList<string> AvailableRoles
        {
            get
            {
                return _availableRoles;
            }
            set
            {
                _availableRoles = value;
                NotifyOfPropertyChange(() => AvailableRoles);
            }
        }

        public UserDisplayViewModel(IUserEndpoint userEndpint, StatusInfoViewModel status, IWindowManager window)
        {
            _userEndpoint = userEndpint;
            _status = status;
            _window = window;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadUsers();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorize Access", "Invalid Authorization for sales Form");
                    await _window.ShowDialogAsync(_status, null, settings);
                }
                else
                {
                    _status.UpdateMessage("Fatal Error", ex.Message);
                    await _window.ShowDialogAsync(_status, null, settings);
                }

                TryCloseAsync();

            }

        }

        private async Task LoadUsers()
        {
            var userList = await _userEndpoint.GetAll();
            Users = new BindingList<UserModel>(userList);
        }

        private async Task LoadRoles()
        {
            var roles = await _userEndpoint.GetAllRoles();

            foreach (var role in roles)
            {
                if (UserRoles.IndexOf(role.Value) < 0)
                {
                    AvailableRoles.Add(role.Value);
                }
            }
        }

        public async Task AddSelectedRole()
        {
            await _userEndpoint.AddUserToRole(SelectedUser.Id, SelectedAvailableRole);
           
            UserRoles.Add(SelectedAvailableRole);
            AvailableRoles.Remove(SelectedAvailableRole); 

        }

        public async Task RemoveSelectedRole()
        {
            await _userEndpoint.RemoveUserFromRole(SelectedUser.Id, SelectedUserRole);
            
            AvailableRoles.Add(SelectedUserRole);
            UserRoles.Remove(SelectedUserRole);
        }
    }
}
