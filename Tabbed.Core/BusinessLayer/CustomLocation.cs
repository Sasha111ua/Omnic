using OmnicTabs.BL.Contracts;
using OmnicTabs.DL.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmnicTabs.Core.BusinessLayer
{
    public class LocationEntity : IBusinessEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id{ get ;set ; }
        public string Name { get; set; }
        public double? Latitude { get; set; } //Vertical
        public double? Longitude { get; set; } //Horizontal
        public DateTime TimeUpdated { get; set; }
    }
}
