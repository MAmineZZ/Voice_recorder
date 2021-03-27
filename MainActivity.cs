using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Media;
using System.IO;
using Java.Lang;
using Java.IO;
using System.Collections;
using System.Data;
using SQLite;






namespace Voice
{
    [Activity(Label = "Voice Recorder", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        int ID_f = 1;

        MediaRecorder _recorder;
        MediaPlayer _player;
        Button _start;
        Button _stop;

        TextView _saisirtitre;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            //bouton start // id="start"
            _start = FindViewById<Button>(Resource.Id.start);
            //bouton stop (submit aussi) // id="bt_stop_submit"
            _stop = FindViewById<Button>(Resource.Id.bt_stop_submit);



            //stoquer le titre  dans la variable "titre" 
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbTest.db3");




            _start.Click += delegate {
                //SQLlite



                // setup the db connection
                var db = new SQLiteConnection(dbPath);
                // setup a a table
                db.CreateTable<audio>();
                // create a new audio object



                //store the object into table

                var table = db.Table<audio>();
                int i = 0;
                foreach (var item in table)
                {
                    audio myaudio1 = new audio(item.id, item.title, item.chemin);
                    if (myaudio1.id > i)
                    {
                        i = myaudio1.id;
                    }




                }



                //store the object into table
                int j = i + 1;
                _saisirtitre = FindViewById<EditText>(Resource.Id.saisirtitre);
                string path1 = "/myvoice/" + j.ToString() + "_" + _saisirtitre.Text + ".mp3";


                //Creation du path (ID+titre+.mp3)
                string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + path1;


                _stop.Enabled = !_stop.Enabled;
                _start.Enabled = !_start.Enabled;
                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.Default);
                _recorder.SetAudioEncoder(AudioEncoder.Default);



                string titre = _saisirtitre.Text;
                _recorder.SetOutputFile(path);



                _recorder.Prepare();
                _recorder.Start();
            };
            _stop.Click += delegate {
                _stop.Enabled = !_stop.Enabled;
                _recorder.Stop();
                _recorder.Reset();
                // string path1 = "/myvoice/" + _saisirtitre.Text + ".mp3";
                //setup the db connection
                var db = new SQLiteConnection(dbPath);
                var table = db.Table<audio>();
                int i = 0;
                foreach (var item in table)
                {
                    audio myaudio1 = new audio(item.id, item.title, item.chemin);
                    if (myaudio1.id > i)
                    {
                        i = myaudio1.id;
                    }




                }



                //store the object into table
                int j = i + 1;



                string path1 = "/myvoice/" + j.ToString() + "_" + _saisirtitre.Text + ".mp3";
                audio myaudio = new audio(i + 1, _saisirtitre.Text, path1);



                db.Insert(myaudio);






                //Creation du path (ID+titre+.mp3)
                string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + path1;
                string titre = _saisirtitre.Text;



                _player.SetDataSource(path);
                _player.Prepare();
                _player.Start();
            };
            Button show;
            show = FindViewById<Button>(Resource.Id.bt_show);
            show.Click += delegate
            {
                TextView dis = FindViewById<TextView>(Resource.Id.textTable);
                //setup the db connection
                var db = new SQLiteConnection(dbPath);
                //connect to the table that contains the data we want
                var table = db.Table<audio>();
                LinearLayout zone = FindViewById<LinearLayout>(Resource.Id.bottom_linear);
                // zone.RemoveAllViews();
                dis.Text = "";
                foreach (var item in table)
                {
                    audio myaudio = new audio(item.id, item.title, item.chemin);
                    dis.Text += "REC N° " + myaudio + "\n";



                }




            };
            Button clear; //
            clear = FindViewById<Button>(Resource.Id.bt_clear);
            clear.Click += delegate
            {
                TextView dis = FindViewById<TextView>(Resource.Id.textTable);
                //setup the db connection
                var db = new SQLiteConnection(dbPath);
                //connect to the table that contains the data we want
                var table = db.Table<audio>();
                dis.Text = "";

                db.Execute("delete from audio");



            };






        }
        protected override void OnResume()
        {
            base.OnResume();
            _recorder = new MediaRecorder();
            _player = new MediaPlayer();
            _player.Completion += (sender, e) => {
                _player.Reset();
                _start.Enabled = !_start.Enabled;
            };
        }
        protected override void OnPause()
        {
            base.OnPause();
            _player.Release();
            _recorder.Release();
            _player.Dispose();
            _recorder.Dispose();
            _player = null;
            _recorder = null;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);



            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}