using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewPoints.Backend.Managers;
using ViewPoints.ViewModels.Abstract;
using ViewPoints.ViewModels.ItemViewModel;

namespace ViewPoints.ViewModels
{
    class ViewPointListViewModel : ViewModel
    {
        /// <summary>
        /// Udalost ktera se vyvola po prenacteni seznamu rozhleden
        /// </summary>
        public event EventHandler ViewPointsReloaded;

        private List<ViewPointItemViewModel> viewPoints;

        public async Task LoadData()
        {
            var manager = new ViewPointManager();
            var list = new List<ViewPointItemViewModel>();
            var viewPointData = await manager.GetViewPoints();

            foreach (var viewPoint in viewPointData)
            {
                list.Add(new ViewPointItemViewModel(viewPoint));
            }
            this.ViewPoints = list;
            ViewPointsReloaded?.Invoke(this, null);
        }

        public List<ViewPointItemViewModel> ViewPoints
        {
            get
            {
                return this.viewPoints;
            }
            set
            {
                if (this.viewPoints != value)
                {
                    this.viewPoints = value;
                    this.OnPropertyChanged(nameof(this.ViewPoints));
                }
            }
        }
    }
}
