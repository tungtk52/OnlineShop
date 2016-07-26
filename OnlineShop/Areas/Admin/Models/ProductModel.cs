using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class ProductIndexModel
    {
        public int CategoryId { get; set; }
        public int PriceStart { get; set; }
        public int PriceEnd { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<Product> ListProduct { get; set; }
        public ProductIndexModel()
        {
            PageIndex = 1;
            PageSize = 25;
            ListProduct = new List<Product>();
        }
    }

    public class ProductModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string MetaTitle { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string MoreImage { get; set; }

        public int? Price { get; set; }

        public bool? IncludeVAT { get; set; }

        public decimal? PromotionPrice { get; set; }

        public int? Quantity { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string Detail { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        public bool? Status { get; set; }

        public DateTime? TopHot { get; set; }
        public ProductModel(Product input)
        {
            this.Id = input.Id;
            this.Name = input.Name;
            this.Code = input.Code;
            this.MetaTitle = input.MetaTitle;
            this.Description = input.Description;
            this.Image = input.Image;
            this.MoreImage = input.MoreImage;
            this.Price = input.Price;
            this.IncludeVAT = input.IncludeVAT;
            this.PromotionPrice = input.PromotionPrice;
            this.Quantity = input.Quantity;
            this.CategoryId = input.CategoryId;
            this.Detail = input.Detail;
            this.CreatedDate = input.CreatedDate;
            this.CreatedBy = input.CreatedBy;
            this.ModifiedDate = input.ModifiedDate;
            this.ModifiedBy = input.ModifiedBy;
            this.MetaKeyword = input.MetaKeyword;
            this.MetaDescription = input.MetaDescription;
            this.Status = input.Status;
            this.TopHot = input.TopHot;
        }
    }
}