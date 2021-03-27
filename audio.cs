using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Voice
{
    class audio
    {
        public int id { get; set; }
        public string title { get; set; }
        public string chemin { get; set; }

      public  audio(int _id ,string _title ,string _chemin)
        {
            id = _id;
            title = _title;
            chemin = _chemin;

        }
       public audio()
        {

        }

        public override string ToString()
        {
            return " " + id + " title" + "  " +title +"  "+ "chemein :" +"  "+ chemin;
        }

    }
}