using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OmnicTabs.Core.BusinessLayer;
using OmnicTabs.Core.DataLayer;
using OmnicTabs.DL.SQLiteBase;


namespace OmnicTabs.Core.DAL
{
    public class CusomLocationRepository
    {
        CustomLocationDataBase db = null;
		protected static string dbLocation;		
		//protected static TaskRepository me;

        public CusomLocationRepository(SQLiteConnection conn, string dbLocation)
		{
            db = new CustomLocationDataBase(conn, dbLocation);
		}

        public LocationEntity GetItem(int id)
		{
            return db.GetItem<LocationEntity>(id);
		}

        public IEnumerable<LocationEntity> GetItems()
		{
            return db.GetItems<LocationEntity>();
		}

        public int SaveItem(LocationEntity item)
		{
            return db.SaveItem<LocationEntity>(item);
		}

		public int DeleteItem(int id)
		{
            return db.DeleteItem<LocationEntity>(id);
		}
	}
}
