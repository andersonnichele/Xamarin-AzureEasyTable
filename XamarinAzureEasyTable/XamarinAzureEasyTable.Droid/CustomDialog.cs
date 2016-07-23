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

namespace XamarinAzureEasyTable.Droid
{
    public class CustomDialog
    {

        public ProgressDialog pDialog;
        public String Message { get;  set; }
        private Context myContext;

        public CustomDialog(Context myContext)
        {
            this.myContext = myContext;

            pDialog = new ProgressDialog(this.myContext);
            pDialog.SetMessage("Processing...");
            pDialog.SetCancelable(false);
        }        

        public void ShowProgressDialog()
        {
            //if (!pDialog.IsShowing)
            pDialog.Show();
        }

        public void HideProgressDialog()
        {
            //if (pDialog.IsShowing)
            pDialog.Hide();
        }
    }
}