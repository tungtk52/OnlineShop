using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ContactBL
    {
         public void Insert(Contact input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.Contacts.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(Contact input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var contact = context.Contacts.FirstOrDefault(m => m.Id == input.Id);
                if (contact != null)
                {
                    contact.Content = input.Content;
                    contact.Status = input.Status;

                    context.Contacts.Add(contact);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var about = context.Contacts.FirstOrDefault(m => m.Id == id);
                if (about != null)
                {
                    context.Entry(about).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public Contact GetById(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Contacts.FirstOrDefault(m => m.Id == id);
            }
        }

        public List<Contact> GetAll()
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Contacts.ToList<Contact>();
            }
        }
    }
}
