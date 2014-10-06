using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using OmnicTabs.Core.BusinessLayer;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;

namespace OmnicTabs.Droid.Views
{
    public class CustomAdapter : MvxAdapter
        {
            public CustomAdapter(Context context, IMvxAndroidBindingContext bindingContext)
                : base(context, bindingContext)
            {
            }

            public override int GetItemViewType(int position)
            {
                var item = GetRawItem(position);
                if (item is LocationEntity)
                    return 0;
                return 1;
            }

            public override int ViewTypeCount
            {
                get { return 2; }
            }

            protected override View GetBindableView(View convertView, object source, int templateId)
            {
                var location = source as LocationEntity;
                if(location != null)
                {
                    templateId = location.Latitude >= 0 ? Resource.Layout.Item_Customlocationnorth : Resource.Layout.Item_Customlocation;
                }
                return base.GetBindableView(convertView, source, templateId);
            }
        }
}