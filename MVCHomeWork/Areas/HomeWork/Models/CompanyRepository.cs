using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVCHomeWork.Areas.HomeWork.Models
{   
	public  class CompanyRepository : EFRepository<Company>, ICompanyRepository
	{
        public override IQueryable<Company> All()
        {
            return base.All().Where(p => p.IsDelete == false);
        }

        public IQueryable<Company> All(bool isAll)
        {
            if (isAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }

        public IQueryable<Company> Query(QueryCompanyModel model)
        {
            var data = this.All();
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

        public Company Find(int id)
        {
            if (id != 0)
            {
                return this.All().FirstOrDefault(p => p.Id == id);
            }
            else
            {
                return new Company();
            }
        }

        public void Update(Company entity)
        {
            var context = (CustomerEntities)this.UnitOfWork.Context;
            context.Entry(entity).State = EntityState.Modified;
        }

        public override void Delete(Company entity)
        {
            entity.IsDelete = true;
            this.Update(entity);
        }
    }

	public  interface ICompanyRepository : IRepository<Company>
	{

	}
}