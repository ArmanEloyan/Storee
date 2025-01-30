using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    internal class Product
    {
        private static int s_productcount = 0;

        private string _name;
        private double _price;
        private double _sellPrice;
        private int _count;
        private DateTime _expirationDate;

        public int Id { get; set; }
        public DateTime ReceiptDate { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _name = value;
            }
        }

        public int Count
        {
            get { return _count; }
            set
            {
                if (_count < 0)
                    throw new Exception("Count can't be less then 0");

                _count = value;
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (value > 0)
                {
                    _price = value;
                }
                else
                {
                    throw new Exception("Price cant be less than 0");
                }
            }
        }

        public double SellPrice
        {
            get { return _sellPrice; }
            set
            {
                if (value > 0)
                {
                    _sellPrice = value;
                }
                else
                {
                    throw new Exception("Price cant be less than 0");
                }
            }
        }

        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set
            {
                if (value < DateTime.Now)
                    throw new Exception("Expiration date can't be less than current date");

                _expirationDate = value;
            }
        }


        public Product(string name, double price, double sellPrice, DateTime expirationDate, int count)
        {
            Name = name;
            Price = price;
            SellPrice = sellPrice;
            ExpirationDate = expirationDate;
            Count = count;
            Id = ++s_productcount;
        }

        public void DisplayInfo()
        {
            Console.WriteLine(@$"Id: {Id}
Name {Name}
Price {Price}
Sell Price {SellPrice}
Expiration Date {ExpirationDate}
Count {Count}
Receipt Date {ReceiptDate}");
        }

    }
}

