using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductCategoryBL
    {
        public void Insert(ProductCategory input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.ProductCategories.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(ProductCategory input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var productCategory = context.ProductCategories.FirstOrDefault(m => m.Id == input.Id);
                if (productCategory != null)
                {
                    productCategory.Name = input.Name;
                    productCategory.MetaTitle = input.MetaTitle;
                    productCategory.ParentId = input.ParentId;
                    productCategory.DisplayOrder = input.DisplayOrder;
                    productCategory.SeoTitle = input.SeoTitle;
                    productCategory.MetaKeyword = input.MetaKeyword;
                    productCategory.MetaDescription = input.MetaDescription;
                    productCategory.Status = input.Status;
                    productCategory.ShowOnHome = input.ShowOnHome;
                    productCategory.ModifiedDate = input.ModifiedDate;
                    productCategory.ModifiedBy = input.ModifiedBy;
                    context.Entry(productCategory).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var productCagegory = context.ProductCategories.Where(m => m.Id == id).FirstOrDefault<ProductCategory>();
                if (productCagegory != null)
                {
                    context.Entry(productCagegory).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public ProductCategory GetById(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.ProductCategories.Where(m => m.Id == id).FirstOrDefault<ProductCategory>();
            }
        }

        public List<ProductCategory> GetAll()
        {
            using (var context = new OnlineShopDbContext())
            {
                var param = new SqlParameter
                            {
                                ParameterName= "Branch",
                                Value = "--"
                            };
                return context.Database.SqlQuery<ProductCategory>("exec ProductCategory_GetAll @Branch", param).ToList();
            }
        }
    }
}
