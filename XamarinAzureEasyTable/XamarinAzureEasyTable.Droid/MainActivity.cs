using Android.App;
using Android.Widget;
using Android.OS;
using XamarinAzureEasyTable.Droid.Model;
using System.Collections.Generic;
using System;
using XamarinAzureEasyTable.Droid.Adapter;

namespace XamarinAzureEasyTable.Droid
{
    [Activity(Label = "Products", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {

        private ImageButton btnNewProduct;
        private EditText txtNewProductName;
        private ListView lstViewProducts;
        private CustomDialog cDialog;

        private ProductServiceTable productServiceTable;

        private ProductAdapter productAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            cDialog = new CustomDialog(this);

            productServiceTable = new ProductServiceTable();

            FindViewById();

            LoadProductList();

            btnNewProduct.Click += delegate { SaveProduct(); };

        }

        private void FindViewById()
        {
            btnNewProduct = FindViewById<ImageButton>(Resource.Id.btnNewProduct);
            txtNewProductName = FindViewById<EditText>(Resource.Id.txtNewProductName);
            lstViewProducts = FindViewById<ListView>(Resource.Id.lstViewProducts);
        }

        private async void LoadProductList()
        {

            List<Product> lstProducts = new List<Product>();

            try
            {
                lstProducts = await productServiceTable.GetProduct();
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
            finally
            {
                if (productAdapter == null)
                {
                    productAdapter = new ProductAdapter(this, lstProducts);
                    productAdapter.lstProducts = lstProducts;
                }
                else
                {
                    productAdapter.lstProducts = lstProducts;
                    productAdapter.NotifyDataSetChanged();
                }

                lstViewProducts.Adapter = productAdapter;
            }

        }

        private async void SaveProduct()
        {
            if (!String.IsNullOrEmpty(txtNewProductName.Text))
            {
                Product product = new Product();

                cDialog.ShowProgressDialog();

                try
                {
                    product.Name = txtNewProductName.Text;

                    product = await productServiceTable.InsertProduct(product);

                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
                finally
                {
                    cDialog.HideProgressDialog();
                    LoadProductList();
                }
            }
        }
    }
}

