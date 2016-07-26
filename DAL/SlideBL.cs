using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SlideBL
    {
        public void Insert(Slide input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.Slides.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(Slide input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var slide = context.Slides.FirstOrDefault(m => m.Id == input.Id);
                if (slide != null)
                {
                    slide.Image = input.Image;
                    slide.DisplayOrder = input.DisplayOrder;
                    slide.Link = input.Link;
                    slide.Description = input.Description;
                    slide.ModifiedDate = input.ModifiedDate;
                    slide.ModifiedBy = input.ModifiedBy;
                    slide.Status = input.Status;

                    context.Slides.Add(slide);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var slide = context.Slides.FirstOrDefault(m => m.Id == id);
                if (slide != null)
                {
                    context.Entry(slide).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public Slide GetById(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Slides.FirstOrDefault(m => m.Id == id);
            }
        }
        public List<Slide> GetAll()
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Slides.ToList<Slide>();
            }
        }
    }
}
