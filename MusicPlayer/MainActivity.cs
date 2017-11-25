using Android.App;
using Android.Widget;
using Android.OS;
using Android.Media;
using System.Collections.Generic;

namespace MusicPlayer
{
    [Activity(Label = "MusicPlayer", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        MediaPlayer player = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            List<string> tracks = new List<string>();

            var fields = typeof(Resource.Raw).GetFields();
       
            foreach(var file in fields)
            {
                tracks.Add(file.Name);
            }

            var listView = FindViewById<ListView>(Resource.Id.listView1);

            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, tracks);

            listView.Adapter = adapter;

            listView.ItemClick += (sender, e) =>
            {
                var item = tracks[e.Position];
                PlaySong(item);
            };
        }

        public void PlaySong(string name)
        {
            var resId = Resources.GetIdentifier(name, "raw", PackageName);

            if (player != null && player.IsPlaying)
                player.Stop();

            player = MediaPlayer.Create(this, resId);
            player.Completion += delegate
            {
                player = null; //Free the memory
            };
            player.Start();
        }

    }
}

