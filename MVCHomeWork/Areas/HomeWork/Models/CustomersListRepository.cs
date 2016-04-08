using System;
using System.Linq;
using System.Linq.Dynamic;
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

        public override byte[] ExportXLS(IQueryable<CustomersList> entities, params string[] notExportCol)
        {
            var notCol = notExportCol.ToList();
            notCol.Add("Id");
            notCol.Add("CompanyNo");
            return base.ExportXLS(entities, notCol.ToArray());
        }

        public override byte[] ExportXLSX(IQueryable<CustomersList> entities, params string[] notExportCol)
        {
            var notCol = notExportCol.ToList();
            notCol.Add("Id");
            notCol.Add("CompanyNo");
            return base.ExportXLSX(entities, notCol.ToArray());
        }
    }

	public  interface ICustomersListRepository : IRepository<CustomersList>
	{

	}
}