using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    internal class Transaction
    {
        private List<Product> _products;
        private Bag _bag;
        private double _amount;
        private DateTime _date;

        public Transaction(Bag bag, double amount)
        {
            _bag = bag;
            _products = bag.GetAllFromBag();
            _amount = amount;
            _date = DateTime.Now;
        }

        public void ShowTransaction()
        {
            Console.WriteLine($"Products:\n{GetProductsInfo()}\n\nTo pay: {_bag.PayAmount}\nPayed:{_amount}");
        }

        private string GetProductsInfo()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var product in _products)
            {
                sb.AppendLine($"Product: {product.Name} x({product.Count}) - Price: {product.SellPrice}");
            }

            return sb.ToString();
        }
    }
}
