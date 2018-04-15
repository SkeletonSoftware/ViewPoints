using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewPoints.Backend.DataAccess.Database;
using ViewPoints.DependencyServices;
using ViewPoints.Views;
using Xamarin.Forms;

namespace ViewPoints
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
			MainPage = new NavigationPage(new ViewPointListPage());
            DatabaseConfig.Database = new SQLiteAsyncConnection(DependencyService.Get<IInternalFileManager>().GetAbsolutePath(DatabaseConfig.DatabaseName));
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
