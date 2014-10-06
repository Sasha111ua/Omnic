using OmnicTabs.DL.SQLiteBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmnicTabs.Core.BusinessLayer
{
    public class LocationEntityManager
    {
        DAL.CusomLocationRepository repository;

        public LocationEntityManager(SQLiteConnection conn) 
        {
            repository = new DAL.CusomLocationRepository(conn, "");
        }

        public LocationEntity GetItem(int id)
		{
            return repository.GetItem(id);
		}

        public IList<LocationEntity> GetItems()
		{
            return new List<LocationEntity>(repository.GetItems());
		}

        public int SaveItem(LocationEntity item)
		{
            return repository.SaveItem(item);
		}
		
		public int DeleteItem(int id)
		{
            return repository.DeleteItem(id);
		}
		
	}
}