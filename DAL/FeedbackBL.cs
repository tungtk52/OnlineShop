using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FeedbackBL
    {
        public void Insert(Feedback input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.Feedbacks.Add(input);
                context.SaveChanges();
            }
        }

        public void Update(Feedback input)
        {
            using (var context = new OnlineShopDbContext())
            {
                var feedback = context.Feedbacks.FirstOrDefault(m => m.Id == input.Id);
                if (feedback != null)
                {
                    feedback.Name = input.Name;
                    feedback.Phone = input.Phone;
                    feedback.Email = input.Email;
                    feedback.Address = input.Address;
                    feedback.Content = input.Content;
                    feedback.Status = input.Status;
                    feedback.CreatedDate = input.CreatedDate;

                    context.Feedbacks.Add(feedback);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                var feedback = context.Feedbacks.FirstOrDefault(m => m.Id == id);
                if (feedback != null)
                {
                    context.Entry(feedback).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public Feedback GetById(int id)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Feedbacks.FirstOrDefault(m => m.Id == id);
            }
        }

        public List<Feedback> GetAll()
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.Feedbacks.ToList<Feedback>();
            }
        }
    }
}
