using OmnicTabs.Core.BusinessLayer;
using OmnicTabs.DL.SQLiteBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmnicTabs.Core.DataLayer
{
    public class CustomLocationDataBase
    {
        static object locker = new object ();

        SQLiteConnection database;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
        public CustomLocationDataBase(SQLiteConnection conn, string path)
		{
            database = conn;
			// create the tables
            database.CreateTable<LocationEntity>();
		}
		
		public IEnumerable<T> GetItems<T> () where T : BL.Contracts.IBusinessEntity, new ()
		{
            lock (locker) {
                return (from i in database.Table<T>() select i).ToList();
            }
		}

		public T GetItem<T> (int id) where T : BL.Contracts.IBusinessEntity, new ()
		{
            lock (locker) {
                return database.Table<T>().FirstOrDefault(x => x.Id == id);
                // Following throws NotSupportedException - thanks aliegeni
                //return (from i in Table<T> ()
                //        where i.ID == id
                //        select i).FirstOrDefault ();
            }
		}

		public int SaveItem<T> (T item) where T : BL.Contracts.IBusinessEntity
		{
            lock (locker) {
                if (item.Id != 0) {
                    database.Update(item);
                    return item.Id;
                } else {
                    return database.Insert(item);
                }
            }
		}
		
		public int DeleteItem<T>(int id) where T : BL.Contracts.IBusinessEntity, new ()
		{
            lock (locker) {
                return database.Delete<T>(new T() { Id = id });
            }
		}
	}
}