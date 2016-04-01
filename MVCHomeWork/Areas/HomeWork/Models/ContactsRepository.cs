using System;
using System.Linq;
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

        public Contacts Find(int id, int dataid)
        {
            if (dataid != 0)
            {
                return this.All().FirstOrDefault(p => p.Id == dataid && p.CompanyId == id);
            }
            else
            {
                return new Contacts() { CompanyId = id };
            }
        }

        public void Update(Contacts entity)
        {
            var context = (CustomerEntities)this.UnitOfWork.Context;
            context.Entry(entity).State = EntityState.Modified;
        }

        public override void Delete(Contacts entity)
        {
            entity.IsDelete = true;
            this.Update(entity);
        }
    }

	public  interface IContactsRepository : IRepository<Contacts>
	{

	}
}