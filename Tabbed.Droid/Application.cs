using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OmnicTabs.Core.BusinessLayer;
using OmnicTabs.DL.SQLite;
using OmnicTabs.Core.ViewModels;

namespace OmnicTabsApplication
{
    [Application]
    public class OmnicTabsApp: Application
    {
        public static OmnicTabsApp Current { get; private set; }
        
        public LocationEntityManager LocMgr { get; set; }
        Connection conn;

        public OmnicTabsApp(IntPtr handle, global::Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer) {
                Current = this;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            var sqliteFilename = "OmnicTabsDB.db3";
            string libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new Connection(path);

            LocMgr = new LocationEntityManager(conn);
            LocMgr.SaveItem(new LocationEntity() { Name = "Home", Latitude = -5.122416, Longitude = 95.904083, TimeUpdated = DateTime.Now });
            LocMgr.SaveItem(new LocationEntity() { Name = "Home2", Latitude = 51.122416, Longitude = 95.904083, TimeUpdated = DateTime.Now });
            Parameters.LocationEntityManager = LocMgr;
        }
    }
}