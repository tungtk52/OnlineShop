using System.Configuration;
using FluentData;
using System.Data.Entity;

namespace Model
{
    public class OnlineShopDbContext 
    {
        public static IDbContext MainDB()
        {
            return new FluentData.DbContext().ConnectionString(ConfigurationManager.ConnectionStrings["OnlineShop"].ConnectionString, new SqlServerProvider());
        }
    }
}
