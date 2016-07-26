using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NewBL
    {
        public void Insert(New input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.News.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(New input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var objNew=context.News.FirstOrDefault(m => m.Id == input.Id);
                if (objNew != null)
                {
                    objNew.Name = input.Name;
                    objNew.MetaTitle = input.MetaTitle;
                    objNew.Description = input.Description;
                    objNew.Image = input.Image;
                    objNew.CategoryId = input.CategoryId;
                    objNew.Detail = input.Detail;
                    objNew.ModifiedDate = input.ModifiedDate;
                    objNew.ModifiedBy = input.ModifiedBy;
                    objNew.MetaKeyword = input.MetaKeyword;
                    objNew.MetaDescription = input.MetaDescription;
                    objNew.Status = input.Status;
                    objNew.TopHot = input.TopHot;
                    objNew.Tags = input.Tags;
                    objNew.ViewCount = input.ViewCount;

                    context.News.Add(objNew);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var product = context.News.FirstOrDefault(m => m.Id == id);
                if (product != null)
                {
                    context.Entry(product).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public New GetById(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.News.FirstOrDefault(m => m.Id == id);
            }
        }

        public List<New> GetAll()
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.News.ToList<New>();
            }
        }
    }
}
