using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ContentTagBL
    {
        public void Insert(ContentTag input)
        {
            using (var context = new OnlineShopDbContext())
            {
                context.ContentTags.Add(input);
                context.SaveChanges();
            }
        }

        public void Delete(int contentId,string tagId)
        {
            using (var context = new OnlineShopDbContext())
            {
                var contentTag = context.ContentTags.FirstOrDefault(m => m.ContentId ==contentId && m.TagId==tagId);
                if (contentTag != null)
                {
                    context.Entry(contentTag).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public ContentTag GetById(int contentId, string tagId)
        {
            using (var context = new OnlineShopDbContext())
            {
                return context.ContentTags.FirstOrDefault(m => m.ContentId == contentId && m.TagId == tagId);
            }
        }
    }
}
