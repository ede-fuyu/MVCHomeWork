using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCHomeWork.Areas.HomeWork.Models
{   
	public  class CustomersListRepository : EFRepository<CustomersList>, ICustomersListRepository
	{
        public IQueryable<CustomersList> Query(QueryCompanyModel model)
        {
            var data = base.All();
            if (!string.IsNullOrEmpty(model.CompanyNo))
            {
                data = data.Where(p => p.CompanyNo == model.CompanyNo);
            }
            if (!string.IsNullOrEmpty(model.CompanyName))
            {
                data = data.Where(p => p.CompanyName.Contains(model.CompanyName));
            }
            return data.AsQueryable();
        }
    }

	public  interface ICustomersListRepository : IRepository<CustomersList>
	{

	}
}