using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCHomeWork.Areas.HomeWork.Models
{
    public class BaseUserRepository : EFRepository<BaseUser>, IBaseUserRepository
    {
        internal string ValidateLogin(string email, string strPassword)
        {
            return this.All().Where(p => p.Email == email && p.Password == strPassword).Select(p => p.Role).FirstOrDefault() ?? string.Empty;
        }
    }

    public  interface IBaseUserRepository : IRepository<BaseUser>
	{

	}
}