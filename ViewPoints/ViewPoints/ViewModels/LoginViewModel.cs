using System;
using System.Collections.Generic;
using System.Text;
using ViewPoints.Backend.Managers;
using ViewPoints.DependencyServices;
using ViewPoints.ViewModels.Abstract;
using ViewPoints.Views;
using Xamarin.Forms;

namespace ViewPoints.ViewModels
{
    class LoginViewModel : ViewModel
    {
        public LoginViewModel()
        {
            SendCommand = new Command(SendCommand_Execute);
        }

        public string Nickname { get; set; }

        public Command SendCommand { get; private set; }

        private async void SendCommand_Execute(object obj)
        {
            var userManager = new UserManager();
            if(await userManager.LoginUser(Nickname))
            {
                App.Current.MainPage = new NavigationPage(new ViewPointListPage());
            }
        }
    }
}
