using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;
using PizzaCenter;

namespace PizzaCenter
{
	[Activity(Label = "Pizza Center", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			var instructorList = FindViewById<ListView>(Resource.Id.instructorListView);
			instructorList.FastScrollEnabled = true;
			instructorList.ItemClick += OnItemClick;
			ToppingData ID = new ToppingData (this);
			instructorList.Adapter = new PizzaAdapter(ToppingData.Instructors);
		}

		void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var intent = new Intent(this, typeof(PizzaDetailActivity));

			intent.PutExtra("position", e.Position);

			StartActivity(intent);
		}
	}
}