using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    internal class Bag
    {
        private List<Product> _productsInBag { get; set; }
        public double PayAmount
        {
            get
            {
                double amount = 0;
                foreach (var product in _productsInBag)
                {
                    amount += product.SellPrice * product.Count;
                }

                return amount;
            }
        }

        public Bag()
        {
            _productsInBag = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            Product bagProduct = _productsInBag.FirstOrDefault(n => n.Id == product.Id);

            if (bagProduct != null)
            {
                bagProduct.Count += product.Count;
                return;
            }

            if (product != null)
            {
                _productsInBag.Add(product); // 123
            }
        }

        public void RemoveProductById(int id,int count)
        {
            Product bagProduct = _productsInBag.FirstOrDefault(n => n.Id == id);

            if (bagProduct == null)
            {
                throw new Exception("Product not found in bag");
            }
            else if (bagProduct.Count < count)
            {
                throw new Exception("Not enough products in bag");
            }
            else if(bagProduct.Count > count)
            {
                bagProduct.Count -= count;
            }
            else if (bagProduct.Count == count)
            {
                _productsInBag.Remove(bagProduct);
            }
        }
        public void ShowInfo()
        {
            foreach (var product in _productsInBag)
            {
                Console.WriteLine($"Product: {product.Name} x({product.Count}) - Price: {product.SellPrice}");
            }
        }
        public List<Product> GetAllFromBag()
        {
            return _productsInBag;
        }
    }
}