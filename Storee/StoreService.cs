using System;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    internal class StoreService : IStoreService
    {
        public double Balance { get; set; }
        private readonly List<Product> _products;


        public StoreService(List<Product> products, double balance)
        {
            _products = new List<Product>();
            Balance = balance;
            SetDefault();
        }

        private void SetDefault()
        {
            _products.Add(new Product("Apple", 10, 20, DateTime.Now.AddYears(2), 30) { ReceiptDate = DateTime.Now });
            _products.Add(new Product("Mars", 23, 40, DateTime.Now.AddYears(2), 10) { ReceiptDate = DateTime.Now });
            _products.Add(new Product("Pringles", 30, 35, DateTime.Now.AddYears(2), 15) { ReceiptDate = DateTime.Now });
            _products.Add(new Product("Pepero", 15, 22, DateTime.Now.AddYears(2), 5) { ReceiptDate = DateTime.Now });
        }

        public void AddProduct(Product product)
        {
            if (CheckProduct(product))
            {
                if (Balance < product.Price * product.Count)
                {
                    throw new Exception("Not enough money");
                }

                Balance -= product.Price * product.Count;
                product.ReceiptDate = DateTime.Now;
                _products.Add(product);
            }
        }

        public void RemoveProduct(Product product)
        {
            if (product != null)
            {
                _products.Remove(product);
            }
        }

        private void RemoveProductByCount(int id, int count)
        {
            Product product = GetProductById(id);
            if(product.Count < count)
            {
                throw new Exception("Not enough products in stock");
            }
            else if(product.Count > count)
            {
                product.Count -= count;
            }
            else
            {
                RemoveProduct(product);
            }
        }

        public void SellProducts(Bag bag, double amount)
        {
            List<Product> products = bag.GetAllFromBag();

            if (amount < bag.PayAmount)
            {
                throw new Exception("Not enough money");
            }

            foreach (var product in products)
            {
                if (product.ExpirationDate < DateTime.Now)
                {
                    throw new Exception("Product is expired");
                }
            }

            Balance += bag.PayAmount;

            if (amount > bag.PayAmount)
                Console.WriteLine($"Your change: {amount - bag.PayAmount}");

            Transaction transaction = new Transaction(bag, amount);
            transaction.ShowTransaction();
        }

        public void UpdateProduct(int id, Product product)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                if (_products[i].Id == id)
                {
                    _products[i] = product;
                    return;
                }
            }
        }

        public Product GetProductById(int id)
        {
            Product product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            return product;
        }

        public List<Product> GetProducts()
        {
            return _products;
        }

        public void ShowBalance()
        {
            Console.WriteLine($"Balance: {Balance}");
        }

        public Product GetProductForBag(Product product, int count)
        {
            if (product.Count >= count)
            {
                Product newProduct = new Product(product.Name, product.Price, product.SellPrice, product.ExpirationDate, count);
                newProduct.Id = product.Id;
                RemoveProductByCount(product.Id, count);
                return newProduct;
            }
            else
            {
                throw new Exception("Not enough products in stock");
            }
        }

        private bool CheckProduct(Product product)
        {
            if (product == null)
            {
                throw new Exception("Product is null");
            }
            if (product.ExpirationDate < DateTime.Now)
            {
                throw new Exception("Product is expired");
            }
            return true;
        }
    }
}
