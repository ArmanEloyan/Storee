using System;
using System.Collections.Generic;

namespace Store
{
    internal class UI
    {
        IStoreService _storeService;

        public UI()
        {
            _storeService = new StoreService(new List<Product>(), 15000);
        }

        public void ShowMenu()
        {
            while (true)
            {
                int option = Helper.TryConvert<int>($"1. Add product | 2. Remove product | 3. Update product | 4. Sell product | 5. Show products | 6. Show Balance | 0. Exit", true);

                switch (option)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        RemoveProduct();
                        break;
                    case 3:
                        UpdateProduct();
                        break;
                    case 4:
                        SellProduct();
                        break;
                    case 5:
                        ShowProducts();
                        break;
                    case 6:
                        _storeService.ShowBalance();
                        break;
                    case 0:
                        return;
                }
            }
        }

        public void AddProduct()
        {
            Product product = null;
            string name;

            while (true)
            {
                Console.Write("Product Name: ");
                name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                Helper.ErrorMessage("Name cant be empty");
            }

            double price = Helper.TryConvert<double>("Product Price: ", false);

            double sellPrice = Helper.TryConvert<double>("Product Sell Price: ", false);

            DateTime expireDate = Helper.TryConvert<DateTime>("Product Expire Date (30/12/0000) ", true);

            int count = Helper.TryConvert<int>("Product Count: ", false);

            try
            {
                product = new Product(name, price, sellPrice, expireDate, count);
                _storeService.AddProduct(product);
            }
            catch (Exception ex)
            {
                Helper.ErrorMessage(ex.Message);
            }
        }
        private void RemoveProduct()
        {
            int id = Helper.TryConvert<int>("Enter Id: ", false);

            Product product = _storeService.GetProductById(id);

            _storeService.RemoveProduct(product);
        }

        private void UpdateProduct()
        {
            int id = Helper.TryConvert<int>("Enter Id: ", false);

            Product product = _storeService.GetProductById(id);
            product.DisplayInfo();

            while (true)
            {
                int option = Helper.TryConvert<int>("What you want to change\n1.Name 2.Price 3. Sell Price 4. Count 5.  Expiration Date 6. Receipt Date 0. Back", true);

                try
                {
                    switch (option)
                    {
                        case 1:
                            string name = Helper.TryConvert<string>("Enter new Name: ", false);
                            product.Name = name;
                            break;
                        case 2:
                            double price = Helper.TryConvert<double>("Enter new Price: ", false);
                            product.Price = price;
                            break;
                        case 3:
                            double sellPrice = Helper.TryConvert<double>("Enter new Sell Price: ", false);
                            product.SellPrice = sellPrice;
                            break;
                        case 4:
                            int count = Helper.TryConvert<int>("Enter new Count: ", false);
                            product.Count = count;
                            break;
                        case 5:
                            DateTime expireDate = Helper.TryConvert<DateTime>("Enter new expiration Date (31/12/0000) ", true);
                            product.ExpirationDate = expireDate;
                            break;
                        case 6:
                            DateTime receiptDate = Helper.TryConvert<DateTime>("Enter new receipt Date (31/12/0000)", true);
                            product.ReceiptDate = receiptDate;
                            break;
                        case 0:
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Helper.ErrorMessage(ex.Message);
                }
            }
        }

        private void SellProduct()
        {
            int id = 0;
            int count = 0;
            double amount = 0;

            Product product = null;
            List<Product> products = new List<Product>();
            Bag bag = new Bag();

            while (true)
            {
                Console.WriteLine("1. Add 2. Remove 3. Pay");
                int option = Helper.TryConvert<int>("Enter option: ", false);

                if (option == 1)
                {
                    id = Helper.TryConvert<int>("Enter Id: ", false);

                    try
                    {
                        product = _storeService.GetProductById(id);
                    }
                    catch (Exception ex)
                    {
                        Helper.ErrorMessage(ex.Message);
                        continue;
                    }

                    count = Helper.TryConvert<int>("Enter count: ", false);
                    Product bagProduct = null;

                    try
                    {
                        bagProduct = _storeService.GetProductForBag(product, count);
                    }
                    catch (Exception ex)
                    {
                        Helper.ErrorMessage(ex.Message);
                        continue;
                    }

                    bag.AddProduct(bagProduct);
                    bag.ShowInfo();

                    Console.WriteLine($"To pay: {bag.PayAmount}");
                }
                else if (option == 2)
                {
                    id = Helper.TryConvert<int>("Enter Id: ", false);

                    try
                    {
                        product = _storeService.GetProductById(id);
                    }
                    catch (Exception ex)
                    {
                        Helper.ErrorMessage(ex.Message);
                        continue;
                    }

                    count = Helper.TryConvert<int>("Enter count: ", false);

                    try
                    {
                        bag.RemoveProductById(id, count);
                    }
                    catch(Exception ex)
                    {
                        Helper.ErrorMessage(ex.Message);
                        continue;
                    }

                    bag.ShowInfo();

                    Console.WriteLine($"To pay: {bag.PayAmount}");
                }
                else if (option == 3)
                {
                    if (bag.PayAmount > 0)
                    {
                        amount = Helper.TryConvert<double>("Enter amount: ", false);
                        break;
                    }
                    else
                    {
                        Helper.ErrorMessage("No items on bag");
                    }
                }
            }

            try
            {
                _storeService.SellProducts(bag, amount);
            }
            catch (Exception ex)
            {
                Helper.ErrorMessage(ex.Message);
            }

        }

        private void ShowProducts()
        {
            List<Product> products = _storeService.GetProducts();

            foreach (var product in products)
            {
                product.DisplayInfo();
                Console.WriteLine(new string('-', 25));
            }

        }

    }
}
