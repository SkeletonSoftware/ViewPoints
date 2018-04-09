using System;
using System.Collections.Generic;
using System.Text;
using ViewPoints.Backend.Managers;
using ViewPoints.Backend.Models;
using ViewPoints.DependencyServices;
using ViewPoints.ViewModels.Abstract;
using Xamarin.Forms;

namespace ViewPoints.ViewModels
{
    class AddViewPointViewModel : ViewModel
    {
        private ViewPoint model;

        private byte[] imageData; //in future should be moved to model or manager 

        public AddViewPointViewModel()
        {
            this.model = new ViewPoint();
            this.SaveCommand = new Command(this.SaveCommand_Execute);
            this.AddPictureCommand = new Command(this.AddPictureCommand_Execute);
        }

        private async void SaveCommand_Execute(object obj)
        {
            var manager = new ViewPointManager();
            if (await manager.AddViewPoint(this.model))
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Chyba", "Rozhlednu se nepodařilo uložit", "OK");
        }

        private async void AddPictureCommand_Execute(object obj)
        {
            var gallery = "Fotogalerie";
            var camera = "Vyfotit";
            var result = await App.Current.MainPage.DisplayActionSheet("Vložit fotografii", null, null, gallery, camera);
            var picturePicker = DependencyService.Get<IPicturePicker>();
            if (string.Equals(gallery, result))
            {
                ImageData = await picturePicker.GetPictureFromGallery();
            }
            else if (string.Equals(camera, result))
            {
                ImageData = await picturePicker.GetPictureFromCamera();
            }


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

        public byte[] ImageData
        {
            get
            {
                return this.imageData;
            }
            set
            {
                if (this.imageData != value)
                {
                    this.imageData = value;
                    this.OnPropertyChanged(nameof(ImageData));
                }
            }
        }

        public Command SaveCommand { get; set; }

        public Command AddPictureCommand { get; set; }
    }
}
