using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
using OmnicTabs.Core.ViewModels;

namespace OmnicTabs.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
           // RegisterAppStart<ViewModels.OmnicTabsViewModel>();
            RegisterAppStart(new MvxAppStart<OmnicTabsViewModel>());
        }
    }
}