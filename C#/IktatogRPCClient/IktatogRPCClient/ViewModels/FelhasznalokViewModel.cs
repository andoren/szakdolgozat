using Caliburn.Micro;
using Iktato;
using IktatogRPCClient.Managers;
using IktatogRPCClient.Models;
using IktatogRPCClient.Models.Managers;
using IktatogRPCClient.Models.Managers.Helpers.Client;
using IktatogRPCClient.Models.Scenes;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IktatogRPCClient.ViewModels
{
	class FelhasznalokViewModel : Conductor<Screen>, IHandle<UserProxy>, IHandle<BindableCollection<Telephely>>
	{

		public FelhasznalokViewModel()
		{
			LoadData();
		}
		private async void LoadData() {
			AvailabelUsers = await serverHelper.GetAllUserAsync();
			eventAggregator.Subscribe(this);
		}
		private EventAggregatorSingleton eventAggregator = EventAggregatorSingleton.GetInstance();
		private ServerHelperSingleton serverHelper = ServerHelperSingleton.GetInstance();
		private BindableCollection<UserProxy> _availableUsers;
		private UserProxy _selectedUser;
		private bool _usersIsVisible = true;
		private BindableCollection<Telephely> telephelyek = new BindableCollection<Telephely>();
		public bool UsersIsVisible
		{
			get { return _usersIsVisible; }
			set { _usersIsVisible = value;
				NotifyOfPropertyChange(()=>UsersIsVisible);
				NotifyOfPropertyChange(() => CreationIsVisible);
			}
		}
		public bool CreationIsVisible { get {
				return !UsersIsVisible;
			} }
		public UserProxy SelectedUser
		{
			get { return _selectedUser; }
			set {  _selectedUser = value;
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
			UsersIsVisible = false;
			Log.Debug("{Class} Felhasználó létrehozása gomb megnyomva.", GetType() );
			Screen createScreen = SceneManager.CreateScene(Scenes.AddFelhasznalo);
			eventAggregator.Subscribe(createScreen);
			ActivateItem(createScreen);
			eventAggregator.PublishOnUIThread(telephelyek);
		}
		public void ModifyUser() {
			Log.Debug("{Class} Felhasználó módosítása gomb megnyomva.", GetType());
			UsersIsVisible = false;
			Screen modifyScreen = SceneManager.CreateScene(Scenes.ModifyFelhasznalo);
			eventAggregator.Subscribe(modifyScreen);
			ActivateItem(modifyScreen);
			eventAggregator.PublishOnUIThread(SelectedUser);
		}
		public async void DisableUser() {
			Log.Warning("{Class} Felhasználó törlése gomb megnyomva. User: {CurrentUser}", GetType(),UserHelperSingleton.CurrentUser);
			if ( await serverHelper.DisableUserAsync(SelectedUser.GetUser)) {
				Log.Warning("{Class} Felhasználó sikeresen törölve. User: {SelectedUser}", GetType(),SelectedUser);
				AvailabelUsers.Remove(SelectedUser);
				NotifyOfPropertyChange(() => AvailabelUsers);
				NotifyOfPropertyChange(()=>SelectedUser);
			}
		}

		public void Handle(UserProxy message)
		{
			
			if (message == SelectedUser) return;
			UsersIsVisible = true;
			UserProxy user = AvailabelUsers.Where(x=>x.Id == message.Id).FirstOrDefault();
			if (user != null) {
				AvailabelUsers.Remove(user);
				AvailabelUsers.Add(message);
				NotifyOfPropertyChange(()=>AvailabelUsers);
			}
			else if (!string.IsNullOrWhiteSpace(message.Fullname)) { AvailabelUsers.Add(message); }
		}

		public void Handle(BindableCollection<Telephely> message)
		{
			telephelyek = message;
		}
	}
}
