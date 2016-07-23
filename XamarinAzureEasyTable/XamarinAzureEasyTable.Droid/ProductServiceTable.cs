using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using XamarinAzureEasyTable.Droid.Model;
using System.Threading.Tasks;

namespace XamarinAzureEasyTable.Droid
{
    class ProductServiceTable
    {
        private MobileServiceClient client;
        private IMobileServiceTable<Product> productTable;


        public ProductServiceTable()
        {
            client = new MobileServiceClient("YOUR_APP_URL");
            productTable = client.GetTable<Product>();
        }

        public async Task<List<Product>> GetProduct()
        {

            List<Product> lstProducts = new List<Product>();

            lstProducts = await productTable.ToListAsync();

            return lstProducts;

        }

        public async Task<Product> InsertProduct(Product product)
        {

            await productTable.InsertAsync(product);

            return product;

        }

        public async Task<Product> UpdateProduct(Product product)
        {

            await productTable.UpdateAsync(product);

            return product;

        }

        public async Task<Product> DeleteProduct(Product product)
        {

            await productTable.DeleteAsync(product);

            return product;

        }
    }
}
