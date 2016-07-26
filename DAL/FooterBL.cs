using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FooterBL
    {
        public void Insert(Footer input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.Footers.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(Footer input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var footer = context.Footers.FirstOrDefault(m => m.Id == input.Id);
                if (footer != null)
                {
                    footer.Content = input.Content;
                    footer.Status = input.Status;

                    context.Footers.Add(footer);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var footer = context.Footers.FirstOrDefault(m => m.Id==id);
                if (footer != null)
                {
                    context.Entry(footer).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public Footer GetById(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Footers.FirstOrDefault(m => m.Id == id);
            }
        }
    }
}
