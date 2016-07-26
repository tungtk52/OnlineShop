using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class ProductCategoryModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string MetaTitle { get; set; }

        public long? ParentId { get; set; }

        public string ParentName { get; set; }

        public int? DisplayOrder { get; set; }

        public string SeoTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        public bool? Status { get; set; }

        public bool? ShowOnHome { get; set; }
    }
}