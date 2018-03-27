﻿using System;
using System.Collections.Generic;
using System.Text;
using ViewPoints.Backend.Models;

namespace ViewPoints.ViewModels.ItemViewModel
{
    class ViewPointItemViewModel : ViewModels.Abstract.ViewModel
    {
        private ViewPoint model;

        public ViewPointItemViewModel(ViewPoint viewPoint)
        {
            this.model = viewPoint;
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
                    this.OnPropertyChanged();
                }
            }
        }

        public string Subtitle
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
                    this.OnPropertyChanged(nameof(this.Subtitle));
                }
            }
        }
    }
}
