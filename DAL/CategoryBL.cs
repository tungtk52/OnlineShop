using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryBL
    {
        public void Insert(Category input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.Categories.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(Category input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var category = context.Categories.FirstOrDefault(m => m.Id == input.Id);
                if (category != null)
                {
                    category.Name = input.Name;
                    category.MetaTitle = input.MetaTitle;
                    category.ParentId = input.ParentId;
                    category.DisplayOrder = input.DisplayOrder;
                    category.SeoTitle = input.SeoTitle;
                    category.MetaKeyword = input.MetaKeyword;
                    category.MetaDescription = input.MetaDescription;
                    category.Status = input.Status;
                    category.ShowOnHome = input.ShowOnHome;
                    category.ModifiedDate = input.ModifiedDate;
                    category.ModifiedBy = input.ModifiedBy;
                    context.Entry(category).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var category = context.Categories.FirstOrDefault(m => m.Id == id);
                if (category != null)
                {
                    context.Entry(category).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public Category GetById(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Categories.FirstOrDefault(m => m.Id == id);
            }
        }

        public List<Category> GetAll()
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Categories.ToList<Category>();
            }
        }
    }
}
