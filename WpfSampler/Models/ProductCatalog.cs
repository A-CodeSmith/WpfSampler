using System.Collections.Generic;

namespace WpfSampler.Models
{
    class ProductCatalog
    {
        public List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product() { Name = "Word Processor", Version = 5.1 },
                new Product() { Name = "Spread Sheets", Version = 2.2 },
                new Product() { Name = "Mighty Calculator", Version = 7.8 },
            };
        }

        public List<Feature> GetFeatures()
        {
            return new List<Feature>()
            {
                new Feature() { Name = "Animations", Included = false },
                new Feature() { Name = "Sounds", Included = false },
                new Feature() { Name = "Music", Included = false },
                new Feature() { Name = "Hotkeys", Included = false },
            };
        }
    }
}
