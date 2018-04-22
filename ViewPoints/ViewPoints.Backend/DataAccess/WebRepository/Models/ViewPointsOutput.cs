using System;
using System.Collections.Generic;
using System.Text;
using ViewPoints.Backend.Models;

namespace ViewPoints.Backend.DataAccess.WebRepository.Models
{
    class ViewPointsOutput
    {
        public static implicit operator ViewPoint(ViewPointsOutput viewpoint)
        {
            return new ViewPoint()
            {
                Id = viewpoint.Id.Value,
                TowerHeight = viewpoint.TowerHeight,
                Title = viewpoint.Title,
                Description = viewpoint.Description,
                OpeningHours = viewpoint.OpeningHours,
                Owner = viewpoint.Owner,
                Location = new Position()
                {
                    Latitude = viewpoint.Latitude,
                    Longitude = viewpoint.Longitude,
                    Altitude = viewpoint.Altitude
                }
            };
        }

        public ViewPointsOutput()
        {
        }

        public int? Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float? Altitude { get; set; }
        public float? TowerHeight { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OpeningHours { get; set; }
        public string Owner { get; set; }
        public int IdOwner { get; set; }
    }
}
