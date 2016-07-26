using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AboutBL
    {
        public void Insert(About input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.Abouts.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(About input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var about = context.Abouts.FirstOrDefault(m => m.Id == input.Id);
                if (about != null)
                {
                    about.Name = input.Name;
                    about.MetaTitle = input.MetaTitle;
                    about.Description = input.Description;
                    about.Image = input.Image;
                    about.Detail = input.Detail;
                    about.ModifiedDate = input.ModifiedDate;
                    about.ModifiedBy = input.ModifiedBy;
                    about.MetaKeyword = input.MetaKeyword;
                    about.MetaDescription = input.MetaDescription;
                    about.Status = input.Status;

                    context.Abouts.Add(about);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var about = context.Abouts.FirstOrDefault(m => m.Id == id);
                if (about != null)
                {
                    context.Entry(about).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public About GetById(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Abouts.FirstOrDefault(m => m.Id == id);
            }
        }

        public List<About> GetAll()
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Abouts.ToList<About>();
            }
        }
    }
}
