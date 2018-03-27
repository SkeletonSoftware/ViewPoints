using System;
using System.Collections.Generic;
using System.Text;
using ViewPoints.Backend.Managers;
using ViewPoints.Backend.Models;
using ViewPoints.ViewModels.Abstract;
using Xamarin.Forms;

namespace ViewPoints.ViewModels
{
    class AddViewPointViewModel : ViewModel
    {
        private ViewPoint model;

        public AddViewPointViewModel()
        {
            this.model = new ViewPoint();
            this.SaveCommand = new Command(this.SaveCommand_Execute);
        }

        private async void SaveCommand_Execute(object obj)
        {
            var manager = new ViewPointManager();
            if (await manager.AddViewPoint(this.model))
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Chyba", "Rozhlednu se nepodařilo uložit", "OK");
        }

        public string Title
        {
            get
            {
                return this.model.Title;
            }
            set
            {
                if (this.model.Title != value)
                {
                    this.model.Title = value;
                    this.OnPropertyChanged(nameof(this.Title));
                }
            }
        }

        public string TowerHeight
        {
            get
            {
                return this.model.TowerHeight.ToString();
            }
            set
            {
                float floatValue;
                if (float.TryParse(value, out floatValue))
                {
                    if (this.model.TowerHeight != floatValue)
                    {
                        this.model.TowerHeight = floatValue;
                        this.OnPropertyChanged(nameof(this.TowerHeight));
                    }
                }
            }
        }

        public string Location
        {
            get
            {
                return string.Empty;
            }
            set
            {

            }
        }

        public string Altitude
        {
            get
            {
                return string.Empty;
            }
            set
            {

            }
        }

        public string OpeningHours
        {
            get
            {
                return this.model.OpeningHours;
            }
            set
            {
                if (this.model.OpeningHours != value)
                {
                    this.model.OpeningHours = value;
                    this.OnPropertyChanged(nameof(this.OpeningHours));
                }
            }
        }

        public string Description
        {
            get
            {
                return this.model.Description;
            }
            set
            {
                if (this.model.Description != value)
                {
                    this.model.Description = value;
                    this.OnPropertyChanged(nameof(this.Description));
                }
            }
        }

        public Command SaveCommand { get; set; }
    }
}
