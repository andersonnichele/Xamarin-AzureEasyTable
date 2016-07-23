using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using Android.Widget;
using XamarinAzureEasyTable.Droid.Model;

namespace XamarinAzureEasyTable.Droid.Adapter
{
    class ProductAdapter : BaseAdapter<Product>
    {
        public List<Product> lstProducts { get; set; }

        private Context myContext;
        private CustomDialog cDialog;

        private ProductServiceTable productServiceTable;

        public ProductAdapter(Context myContext, List<Product> lstProducts)
        {
            this.myContext = myContext;
            cDialog = new CustomDialog(this.myContext);
            this.lstProducts = lstProducts;

            productServiceTable = new ProductServiceTable();

        }

        public override int Count
        {
            get
            {
                return lstProducts.Count();
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Product this[int position]
        {
            get
            {
                return lstProducts[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(myContext).Inflate(Resource.Layout.ListProductsRow, null, false);
            }

            EditText txtProductName = row.FindViewById<EditText>(Resource.Id.txtProductName);
            txtProductName.Text = lstProducts[position].Name;

            ImageButton btnEditProduct = row.FindViewById<ImageButton>(Resource.Id.btnEditProduct);
            ImageButton btnDeleteProduct = row.FindViewById<ImageButton>(Resource.Id.btnDeleteProduct);

            btnEditProduct.Click += delegate
            {
                Product product = lstProducts[position];

                product.Name = txtProductName.Text;

                UpdateProduct(product);
            };

            btnDeleteProduct.Click += delegate
            {

                Product product = lstProducts[position];

                DeleteProduct(product);

            };

            return row;
        }

        private async void UpdateProduct(Product product)
        {

            try
            {
                cDialog.ShowProgressDialog();

                product = await productServiceTable.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.myContext, ex.Message, ToastLength.Short).Show();
            }
            finally
            {
                cDialog.HideProgressDialog();
                NotifyDataSetChanged();
            }
        }

        private async void DeleteProduct(Product product)
        {

            try
            {
                cDialog.ShowProgressDialog();

                await productServiceTable.DeleteProduct(product);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                lstProducts.Remove(product);

                cDialog.HideProgressDialog();
                NotifyDataSetChanged();
            }
        }
    }
}