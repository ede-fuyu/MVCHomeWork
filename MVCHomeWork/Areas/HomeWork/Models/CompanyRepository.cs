using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;

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
                return this.All().FirstOrDefault(p => p.CompanyId == id);
            }
            else
            {
                return new Company();
            }
        }

        public void Save(Company entity)
        {
            if (entity.CompanyId == 0)
            {
                this.Add(entity);
            }
            else
            {
                var context = (CustomerEntities)this.UnitOfWork.Context;
                context.Entry(entity).State = EntityState.Modified;
            }
        }

        public override void Delete(Company entity)
        {
            entity.IsDelete = true;
            this.Save(entity);
        }

        public override byte[] ExportXLS(IQueryable<Company> entities, params string[] notExportCol)
        {
            var notCol = notExportCol.ToList();
            notCol.Add("CompanyId");
            notCol.Add("IsDelete");
            return base.ExportXLS(entities, notCol.ToArray());
        }

        public override byte[] ExportXLSX(IQueryable<Company> entities, params string[] notExportCol)
        {
            var notCol = notExportCol.ToList();
            notCol.Add("CompanyId");
            notCol.Add("IsDelete");
            return base.ExportXLSX(entities, notCol.ToArray());
        }

        internal dynamic SetClient(int companyid)
        {
            if (companyid == 0)
            {
                return new SelectList(this.All().Select(p => new { value = p.CompanyId, text = p.CompanyName }), "value", "text");
            }
            else
            {
                var company = this.Find(companyid);
                return company == null ? "" : company.CompanyName;
            }
        }
    }

	public  interface ICompanyRepository : IRepository<Company>
	{

	}
}