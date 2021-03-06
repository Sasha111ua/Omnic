using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using OmnicTabs.Core.BusinessLayer;
using OmnicTabs.Core.Services;
using System.Threading.Tasks;
using System;
using Cirrious.MvvmCross.Plugins.Location;

namespace OmnicTabs.Core.ViewModels
{
    public class OmnicTabsViewModel 
		: MvxViewModel
    {
        public OmnicTabsViewModel(IMvxLocationWatcher watcher)
        {
            Child1 = new Child1ViewModel(new ImageLoader());
            Child2 = new Child2ViewModel();
            Child3 = new Child3ViewModel(watcher);
        }
        private Child1ViewModel _child1;
        public Child1ViewModel Child1
        {
            get { return _child1; }
            set { _child1 = value; RaisePropertyChanged(() => Child1); }
        }

        private Child2ViewModel _child2;
        public Child2ViewModel Child2
        {
            get { return _child2; }
            set { _child2 = value; RaisePropertyChanged(() => Child2); }
        }

        private Child3ViewModel _child3;
        public Child3ViewModel Child3
        {
            get { return _child3; }
            set { _child3 = value; RaisePropertyChanged(() => Child3); }
        }
    }

    public class Child1ViewModel
    : MvxViewModel
    {

        public ICommand ZoomImageCommand
        {
            // I thinks its a bug, but ShowViewModel<T> doesnt work, so for now i pass params throu static.
            get
            {
                return new MvxCommand(() => ShowViewModel(typeof(GrandChildViewModel)));
            }

        }
        public ICommand RefreshCommand
        {
            get { return new MvxCommand(() => LoadImages(new ImageLoader())); }
        }

        public string Refresh { get { return "Refresh"; } }

        static ObservableCollection<Image> _images;
        public ObservableCollection<Image> Images
        {
            get { return _images; }
            set { _images = value; RaisePropertyChanged(() => Images); }
        }
        Image _chosenItem;
        public Image ChosenItem 
        {
            get { return _chosenItem; } 
            set { _chosenItem = value ;
            Parameters.SetImageUrl(value.Url);
            RaisePropertyChanged(() => ChosenItem);
            } 
        }

        public Child1ViewModel(IImageService service)
        {
            LoadImages(service);
        } 
        private async void LoadImages(IImageService service)
        {
             Images = await Task<ObservableCollection<Image>>.Factory.StartNew(() =>
                {
                    var newList = new ObservableCollection<Image>();
                    for (var i = 0; i < 100; i++)
                    {
                        var newKitten = service.ImageFactory();
                        newList.Add(newKitten);
                    }

                    return newList;
                });
        }


        public static void DeleteImage()
        {
            var imageToDel = new Parameters().ImageToDel;
            if (_images.Any() && imageToDel.HasValue)
                _images.RemoveAt(imageToDel.Value);
        }
    }
    public class Child2ViewModel
    : MvxViewModel
    {
        public Child2ViewModel()
        {
            LocationEntity = new ObservableCollection<LocationEntity>(Parameters.LocationEntityManager.GetItems().ToList());
        }
        private ObservableCollection<LocationEntity> _locationEntity;
        public ObservableCollection<LocationEntity> LocationEntity
        {
            get { return _locationEntity; }
            set { _locationEntity = value; RaisePropertyChanged(() => LocationEntity); }
        }
        private MvxCommand _itemClickCommand;
        public ICommand ItemClickCommand
        {
            get
            {
                return new MvxCommand(
                    () => 
                        ShowViewModel(typeof(LocationEntityDetailsViewModel)));
                /*
                _itemClickCommand = _itemClickCommand ?? new MvxCommand(NavigateToDetails);
                return _itemClickCommand;*/
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new MvxCommand(
                    () =>
                        ShowViewModel(typeof(LocationEntityDetailsViewModel)));
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new MvxCommand(
                    RefreshListView
                       );
            }
        }

        void RefreshListView()
        {
            LocationEntity = new ObservableCollection<LocationEntity>(Parameters.LocationEntityManager.GetItems().ToList());
        }

        LocationEntity _chosenItem;
        private IMvxLocationWatcher watcher;
        public LocationEntity ChosenItem
        {
            get { return _chosenItem; }
            set
            {
                _chosenItem = value;
                Parameters.LocationEntity = value;
                RaisePropertyChanged(() => ChosenItem);
            }
        }
        
    }
    public class Child3ViewModel 
    : MvxViewModel
    {

        private IMvxLocationWatcher _watcher;


        private double _lng;
        public double Lng {
            get { return  _lng; }
            set { _lng = value; RaisePropertyChanged(()=> Lng); }
        }
        private double _lt;
        public double Lt
        {
            get { return _lt; }
            set { _lt = value; RaisePropertyChanged(()=> Lt); }
        }

        public Child3ViewModel(IMvxLocationWatcher watcher)
        {

            _watcher = watcher;
            _watcher.Start(new MvxLocationOptions(), OnFix, OnError);
        }

        private void OnError(MvxLocationError obj)
        {
            throw new NotImplementedException();
        }

        private void OnFix(MvxGeoLocation obj)
        {
            Lt = obj.Coordinates.Latitude;
            Lng = obj.Coordinates.Longitude;
        }
    }
    public class GrandChildViewModel
        : MvxViewModel
    {
        string _imageUrl;
        public GrandChildViewModel()
        {
            _imageUrl = Parameters.GetImageUrl();
            
        }
       public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; RaisePropertyChanged(() => ImageUrl); }
        }
       private MvxCommand _deleteCommand;
       public ICommand DeleteCommand
       {
           get
           {
               _deleteCommand = _deleteCommand ?? new MvxCommand(Child1ViewModel.DeleteImage);
               return _deleteCommand;
           }
       }
    }

    public class LocationEntityDetailsViewModel
        : MvxViewModel
    {
        public LocationEntityDetailsViewModel()
        {
            LocationEntity = Parameters.LocationEntity?? new LocationEntity();
        }

        private LocationEntity _locationEntity;
        public LocationEntity LocationEntity {
            get { return _locationEntity; }
            set { _locationEntity = value; RaisePropertyChanged(()=> LocationEntity); }
        }

        public string Longitude
        {
            get { return LocationEntity.Longitude.HasValue?LocationEntity.Longitude.Value.ToString():String.Empty; }
            set { LocationEntity.Longitude = Convert.ToDouble(value); RaisePropertyChanged(() => Longitude); }
        }

        public string Latitude
        {
            get { return LocationEntity.Latitude.ToString(); }
            set
            {
                if (value == "")
                    LocationEntity.Latitude = null;
                else if (value == "-")
                    LocationEntity.Latitude = -0;
                else
                LocationEntity.Latitude = Convert.ToDouble(value);
            }
        }
        public DateTime TimeUpdated
        {
            get { return LocationEntity.TimeUpdated; }
            set { LocationEntity.TimeUpdated = value; RaisePropertyChanged(() => TimeUpdated); }
        }

        private MvxCommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                _cancelCommand = _cancelCommand ?? new MvxCommand(()=>Close(this));
                Parameters.LocationEntity = new LocationEntity();
                return _cancelCommand;
            }
        }
        private MvxCommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new MvxCommand(SaveLocationEntity);
                Parameters.LocationEntity = new LocationEntity();
                return _saveCommand;
            }
        }

        void SaveLocationEntity()
        {
            LocationEntity.TimeUpdated = DateTime.Now;
            Parameters.LocationEntityManager.SaveItem(LocationEntity);
            Close(this);
        }

        public string Color {
            get { return "background_light"; }
        }

    }
    
    public class Parameters
    {
       static string _imageUrl;
       public static void SetImageUrl(string url)
        {
            _imageUrl = url;
        }
       public static string GetImageUrl()
        {
            return _imageUrl;
        }

        private static int? _imageToDel;
        public int? ImageToDel {
            get { return  _imageToDel; }
            set { _imageToDel = value; }
        }

        private static LocationEntityManager _locationEntityManager;
        public static LocationEntityManager LocationEntityManager
        {
            get { return _locationEntityManager; }
            set { _locationEntityManager = value; }
        }

        private static LocationEntity _locationEntity;
        public static LocationEntity LocationEntity
        {
            get { return  _locationEntity; }
            set { _locationEntity = value; }
        }
    }
}
