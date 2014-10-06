using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.Views;
using OmnicTabs.Core.ViewModels;
using System.IO;
using OmnicTabs.DL.SQLite;
using OmnicTabs.Core.BusinessLayer;
using OmnicTabsApplication;
using Environment = Android.OS.Environment;

namespace OmnicTabs.Droid.Views
{
    [Activity(Label = "View for GrandChildViewModel")]
    public class GrandChildView : MvxActivity
    {
        Button _buttonSave;
        Button _buttonRemove;
        MvxImageView _imageView;
        Bitmap _image;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ZoomedImageView);
            _buttonSave = FindViewById<Button>(Resource.Id.button1);
            _buttonSave.Click += DownloadAsync;
            _buttonRemove = FindViewById<Button>(Resource.Id.button2);
            _buttonRemove.Click += (sender, args) => Finish();
            _imageView = FindViewById<MvxImageView>(Resource.Id.big_image_view);
            var bitmapDrawable = _imageView.Drawable as BitmapDrawable;
            if (bitmapDrawable != null) _image = bitmapDrawable.Bitmap;
          //  OmnicTabsApp.Current.CustLocMgr.SaveItem(new LocationEntity() { Name = "aaa" });
        }

        private async void DownloadAsync(object sender, EventArgs e)
        {
            var documentsPath =
                (new Java.IO.File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDcim), "Camera"));
            if (!documentsPath.Exists())
            {
                documentsPath.Mkdir();
            }
            var localFilename = Parameters.GetImageUrl().GetHashCode() + ".jpg"; // TODO use Random
            var localPath = System.IO.Path.Combine(documentsPath.AbsolutePath, localFilename);

            using (var stream = new FileStream(localPath, FileMode.OpenOrCreate))
            {
                _image.Compress(Bitmap.CompressFormat.Jpeg, 50, stream);
                await stream.FlushAsync();
                stream.Close();
            }
            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(new Java.IO.File(localPath));
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            Bitmap bitmap;
            BitmapFactory.Options options;
            using (options = new BitmapFactory.Options())
            {
                options.InJustDecodeBounds = true;
                BitmapFactory.DecodeFile(localPath, options);
                options.InSampleSize = options.OutWidth > options.OutHeight ? options.OutHeight / _imageView.Height : options.OutWidth / _imageView.Width;
                options.InJustDecodeBounds = false;
                bitmap = BitmapFactory.DecodeFile(localPath, options);
            }
            _imageView.SetImageBitmap(bitmap);
            Finish();
            if (new Java.IO.File(localPath).Exists())
            Toast.MakeText(this, "Image saved to gallery", ToastLength.Long).Show();
        }

        /* async void DownloadAsync(object sender, System.EventArgs e)
        {
            _webClient = new WebClient();
            var url = new Uri(Parameters.GetImageUrl());
            byte[] bytes = null;
            try
            {
                bytes =  await _webClient.DownloadDataTaskAsync(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
            string documentsPath = (new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim), "Camera")).AbsolutePath;
            string localFilename = url.GetHashCode() + ".png";
            string localPath = System.IO.Path.Combine(documentsPath, localFilename);

            var fs = new FileStream(localPath, FileMode.OpenOrCreate);
            await fs.WriteAsync(bytes, 0, bytes.Length);
            fs.Close();
            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                Android.Net.Uri contentUri = Android.Net.Uri.FromFile(new Java.IO.File(localPath));
                mediaScanIntent.SetData(contentUri);
                SendBroadcast(mediaScanIntent);

            Bitmap bitmap;
            BitmapFactory.Options options;
            using (options = new BitmapFactory.Options())
            {
                options.InJustDecodeBounds = true;
                BitmapFactory.DecodeFile(localPath, options);
                options.InSampleSize = options.OutWidth > options.OutHeight ? options.OutHeight / _imageView.Height : options.OutWidth / _imageView.Width;
                options.InJustDecodeBounds = false;
                bitmap = BitmapFactory.DecodeFile(localPath, options);
            }
            _imageView.SetImageBitmap(bitmap);
        } */      
    }
    [Activity(Label = "View for Child1ViewModel")]
    public class Child1View : MvxActivity
    {
        private MvxListView _listView;
        protected override void OnCreate(Bundle bundle)
        {
           
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Child1View);
            _listView = FindViewById<MvxListView>(Resource.Id.list_view);
            (_listView as ListView).ItemClick += _listView_ItemClick;
        }

        void _listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e == null) throw new ArgumentNullException("e");
            new Parameters().ImageToDel = e.Position;
        }
    }
    [Activity(Label = "View for Child2ViewModel")]
    public class Child2View : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Child2View);
        }
    }
    [Activity(Label = "View for Child3ViewModel")]
    public class Child3View : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Child3View);
        }
    }
    [Activity(Label = "View for LocationEntityDetailsViewModel")]
    public class LocationEntityDetails : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LocationEntityDetails);
        }
    }

    [Activity(Label = "View for OmnicTabsViewModel")]
    public class OmnicTabsView : MvxTabActivity
    {
        protected OmnicTabsViewModel FirstViewModel
        {
            get
            {
                if (ViewModel != null) 
                    return ViewModel as OmnicTabsViewModel;
                return new OmnicTabsViewModel();
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.OmnicTabsView);


            TabHost.TabSpec spec = TabHost.NewTabSpec("child1");
            spec.SetIndicator("1");
            spec.SetContent(this.CreateIntentFor(FirstViewModel.Child1));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("child2");
            spec.SetIndicator("2");
            spec.SetContent(this.CreateIntentFor(FirstViewModel.Child2));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("child3");
            spec.SetIndicator("3", Resources.GetDrawable(Resource.Drawable.Tab_Tweets));
            spec.SetContent(this.CreateIntentFor(FirstViewModel.Child3));
            TabHost.AddTab(spec);
        }
    }
}