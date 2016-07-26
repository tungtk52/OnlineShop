using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MenuBL
    {
        public void Insert(Menu input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.Menus.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(Menu input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var menu = context.Menus.FirstOrDefault(m => m.Id == input.Id);
                if (menu != null)
                {
                    menu.Text = input.Text;
                    menu.Link = input.Link;
                    menu.DisplayOrder = input.DisplayOrder;
                    menu.Target = input.Target;
                    menu.Status = input.Status;
                    menu.TypeId = input.TypeId;

                    context.Menus.Add(menu);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var menu = context.Menus.FirstOrDefault(m => m.Id==id);
                if (menu != null)
                {
                    context.Entry(menu).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public Menu GetById(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Menus.FirstOrDefault(m => m.Id == id);
            }
        }

        public List<Menu> GetAll()
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Menus.ToList<Menu>();
            }
        }
    }
}
