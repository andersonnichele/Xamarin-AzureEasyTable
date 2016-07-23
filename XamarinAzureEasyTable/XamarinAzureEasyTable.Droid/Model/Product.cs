using Newtonsoft.Json;

namespace XamarinAzureEasyTable.Droid.Model
{
    class Product
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}