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
    class Constants
    {
        #region MYSQL CONNECTION STRING
        public static string connectionstring = "server=localhost;port=3306;user=root;password=;databse=voicedb;";

        #endregion

        #region MYSQL SELECT ALL QUERY
        public static string selectAllQuery = "select * from audio order by ID";

        #endregion

        #region MY SQL INSERT COMMAND
        public static string insertNote = "insert into audio (path, titre) values (@path, @titre)";
        #endregion
    }
}