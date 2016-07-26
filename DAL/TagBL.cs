using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TagBL
    {
        public void Insert(Tag input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.Tags.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(Tag input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var tag = context.Tags.FirstOrDefault(m => m.Id == input.Id);
                if (tag != null)
                {
                    tag.Name = input.Name;
                    context.Tags.Add(tag);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(string id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var tag = context.Tags.FirstOrDefault(m => m.Id == id);
                if (tag != null)
                {
                    context.Entry(tag).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public Tag GetById(string id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Tags.FirstOrDefault(m => m.Id == id);
            }
        }
        public List<Tag> GetAll()
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Tags.ToList<Tag>();
            }
        }
    }
}
