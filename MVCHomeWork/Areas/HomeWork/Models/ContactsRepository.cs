using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVCHomeWork.Areas.HomeWork.Models
{   
	public  class ContactsRepository : EFRepository<Contacts>, IContactsRepository
	{
        public override IQueryable<Contacts> All()
        {
            return base.All().Where(p => p.IsDelete == false);
        }

        public IQueryable<Contacts> All(bool isAll)
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

        public IQueryable<Contacts> Query(QueryContactModel model)
        {
            var data = this.All();

            if (!string.IsNullOrEmpty(model.CompanyNo))
            {
                data = data.Where(p => p.Company.CompanyNo == model.CompanyNo);
            }
            if (!string.IsNullOrEmpty(model.CompanyName))
            {
                data = data.Where(p => p.Company.CompanyName.Contains(model.CompanyName));
            }
            if (!string.IsNullOrEmpty(model.ContactName))
            {
                data = data.Where(p => p.Name.Contains(model.ContactName));
            }
            return data.AsQueryable();
        }

        public IQueryable<Contacts> Query(int id)
        {
            return this.All().Where(p => p.CompanyId == id).AsQueryable();
        }

        public Contacts Find(int id)
        {
            if (id != 0)
            {
                return this.All().FirstOrDefault(p => p.Id == id);
            }
            else
            {
                return new Contacts();
            }
        }

        public Contacts Find(int companyid, int dataid)
        {
            if (dataid != 0)
            {
                return this.All().FirstOrDefault(p => p.Id == dataid && p.CompanyId == companyid);
            }
            else
            {
                return new Contacts() { CompanyId = companyid };
            }
        }

        public void Save(Contacts entity)
        {
            if (entity.Id == 0)
            {
                this.Add(entity);
            }
            else
            {
                var context = (CustomerEntities)this.UnitOfWork.Context;
                context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void BatchUpdate(List<BatchContacts> model)
        {
            foreach(var data in model)
            {
                var entity = this.Find(data.Id);
                if(entity != null)
                {
                    entity.JobTitle = data.JobTitle;
                    entity.Phone = data.Phone;
                    entity.Tel = data.Tel;
                    this.Save(entity);
                }
            }
        }

        public override void Delete(Contacts entity)
        {
            entity.IsDelete = true;
            this.Save(entity);
        }

        public override byte[] ExportXLS(IQueryable<Contacts> entities, params string[] notExportCol)
        {
            var notCol = notExportCol.ToList();
            notCol.Add("Id");
            notCol.Add("IsDelete");
            return base.ExportXLS(entities, notCol.ToArray());
        }

        public override byte[] ExportXLSX(IQueryable<Contacts> entities, params string[] notExportCol)
        {
            var notCol = notExportCol.ToList();
            notCol.Add("Id");
            notCol.Add("IsDelete");
            return base.ExportXLSX(entities, notCol.ToArray());
        }
    }

	public  interface IContactsRepository : IRepository<Contacts>
	{

	}
}