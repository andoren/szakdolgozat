using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
    class FelhasznalokViewModel:Conductor<Screen>
    {

		public FelhasznalokViewModel()
		{
			AvailabelUsers = serverHelper.GetAllUser();
		}
		private EventAggregatorSingleton eventAggregator = EventAggregatorSingleton.GetInstance();
		private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
		private BindableCollection<UserProxy> _availableUsers;
		private UserProxy _selectedUser;
		private bool _usersIsVisible = true;

		public bool UsersIsVisible
		{
			get { return _usersIsVisible; }
			set { _usersIsVisible = value; }
		}

		public UserProxy SelectedUser
		{
			get { return _selectedUser; }
			set {  if (value != null)_selectedUser = value;
				NotifyOfPropertyChange(()=> SelectedUser);
				NotifyOfPropertyChange(() => CanDisableUser);
				NotifyOfPropertyChange(() => CanModifyUser);
			}
		}

		public BindableCollection<UserProxy> AvailabelUsers
		{
			get { return _availableUsers; }
			set { _availableUsers = value;
				NotifyOfPropertyChange(() => AvailabelUsers);
			}
		}
		public bool CanDisableUser
		{
			get {
				return SelectedUser != null;
			}
		}
		public bool CanModifyUser {
			get {
				return SelectedUser != null;
			}
		}
		public void CreateUser() { 
		
		}
		public void ModifyUser() { 
		
		}
		public void DisableUser() { 
		
		}
	}
}
