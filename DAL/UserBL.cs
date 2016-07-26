using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;

namespace DAL
{
    public class UserBL
    {
        public bool Insert(User user)
        {
            using (var context = OnlineShopDbContext.MainDB())
            {
                var res=context.StoredProcedure("User_Insert")
                    .Parameter("UserName", user.UserName)
                    .Parameter("Password", user.UserName)
                    .Parameter("FullName", user.UserName)
                    .Parameter("Address", user.UserName)
                    .Parameter("Email", user.UserName)
                    .Parameter("Phone", user.UserName)
                    .Parameter("CreatedDate", user.UserName)
                    .Parameter("CreatedBy", user.UserName)
                    .Parameter("ModifiedDate", user.UserName)
                    .Parameter("ModifiedBy", user.UserName)
                    .Parameter("Status", user.UserName)
                    .QuerySingle<int>();
                return res == 1;
            }
        }

        public bool Update(User user)
        {
            using (var context = OnlineShopDbContext.MainDB())
            {
                var res = context.StoredProcedure("User_Update")
                    .Parameter("UserName", user.UserName)
                    .Parameter("Password", user.UserName)
                    .Parameter("FullName", user.UserName)
                    .Parameter("Address", user.UserName)
                    .Parameter("Email", user.UserName)
                    .Parameter("Phone", user.UserName)
                    .Parameter("CreatedDate", user.UserName)
                    .Parameter("CreatedBy", user.UserName)
                    .Parameter("ModifiedDate", user.UserName)
                    .Parameter("ModifiedBy", user.UserName)
                    .Parameter("Status", user.UserName)
                    .QuerySingle<int>();
                return res == 1;
            }
        }

        public bool Delete(int id)
        {
            using (var context = OnlineShopDbContext.MainDB())
            {
                var res = context.StoredProcedure("User_Delete")
                    .Parameter("Id", id)
                    .QuerySingle<int>();
                return res == 1;
            }
        }

        public User GetById(int id)
        {
            using (var context = OnlineShopDbContext.MainDB())
            {
                var res = context.StoredProcedure("User_GetById")
                    .Parameter("Id", id)
                    .QuerySingle<User>();
                return res;
            }
        }

        public bool Login(string userName,string password)
        {
            using (var context = OnlineShopDbContext.MainDB())
            {
                var res = context.StoredProcedure("User_ByUserNameNPassword")
                    .Parameter("UserName", userName)
                    .Parameter("Password", password)
                    .QuerySingle<User>();
                return res!=null;
            }
        }

        public List<User> GetAll()
        {
            using (var context = OnlineShopDbContext.MainDB())
            {
                var res = context.StoredProcedure("User_GetAll")
                    .QueryMany<User>();
                return res;
            }
        }
    }
}
