using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MenuTypeBL
    {
        public void Insert(MenuType input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.MenuTypes.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(MenuType input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var menu = context.MenuTypes.FirstOrDefault(m => m.Id == input.Id);
                if (menu != null)
                {
                    menu.Name = input.Name;

                    context.MenuTypes.Add(menu);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var MenuType = context.MenuTypes.FirstOrDefault(m => m.Id == id);
                if (MenuType != null)
                {
                    context.Entry(MenuType).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public MenuType GetById(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.MenuTypes.FirstOrDefault(m => m.Id == id);
            }
        }
        public List<MenuType> GetAll()
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.MenuTypes.ToList<MenuType>();
            }
        }
    }
}
