package omnictabs.droid.views;


public class CustomAdapter
	extends cirrious.mvvmcross.binding.droid.views.MvxAdapter
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_getItemViewType:(I)I:GetGetItemViewType_IHandler\n" +
			"n_getViewTypeCount:()I:GetGetViewTypeCountHandler\n" +
			"";
		mono.android.Runtime.register ("OmnicTabs.Droid.Views.CustomAdapter, OmnicTabs.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CustomAdapter.class, __md_methods);
	}


	public CustomAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CustomAdapter.class)
			mono.android.TypeManager.Activate ("OmnicTabs.Droid.Views.CustomAdapter, OmnicTabs.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public CustomAdapter (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == CustomAdapter.class)
			mono.android.TypeManager.Activate ("OmnicTabs.Droid.Views.CustomAdapter, OmnicTabs.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public int getItemViewType (int p0)
	{
		return n_getItemViewType (p0);
	}

	private native int n_getItemViewType (int p0);


	public int getViewTypeCount ()
	{
		return n_getViewTypeCount ();
	}

	private native int n_getViewTypeCount ();

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
