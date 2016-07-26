using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductBL
    {
        public void Insert(Product input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.Products.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(Product input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var product = context.Products.FirstOrDefault(m => m.Id == input.Id);
                if (product != null)
                {
                    product.Name = input.Name;
                    product.Code = input.Code;
                    product.MetaTitle = input.MetaTitle;
                    product.Description = input.Description;
                    product.Image = input.Image;
                    product.MoreImage = input.MoreImage;
                    product.Price = input.Price;
                    product.IncludeVAT = input.IncludeVAT;
                    product.PromotionPrice = input.PromotionPrice;
                    product.Quantity = input.Quantity;
                    product.CategoryId = input.CategoryId;
                    product.Detail = input.Detail;
                    product.ModifiedDate = input.ModifiedDate;
                    product.ModifiedBy = input.ModifiedBy;
                    product.MetaKeyword = input.MetaKeyword;
                    product.MetaDescription = input.MetaDescription;
                    product.Status = input.Status;
                    product.TopHot = input.TopHot;

                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var product = context.Products.FirstOrDefault(m => m.Id == id);
                if (product != null)
                {
                    context.Entry(product).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public Product GetById(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Products.FirstOrDefault(m => m.Id == id);
            }
        }

        public List<Product> GetAll()
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Products.ToList<Product>();
            }
        }
    }
}
