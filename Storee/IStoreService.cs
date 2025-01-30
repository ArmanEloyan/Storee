using System.Collections.Generic;

namespace Store
{
    internal interface IStoreService
    {
        void AddProduct(Product product);
        void SellProducts(Bag bag, double amount);
        Product GetProductById(int id);
        void RemoveProduct(Product product);
        void UpdateProduct(int id,Product product);
        List<Product> GetProducts();
        void ShowBalance();
        Product GetProductForBag(Product product,int count);
    }
}
