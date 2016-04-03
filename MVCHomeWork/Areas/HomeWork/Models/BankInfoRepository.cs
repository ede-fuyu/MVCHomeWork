using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVCHomeWork.Areas.HomeWork.Models
{   
	public  class BankInfoRepository : EFRepository<BankInfo>, IBankInfoRepository
	{
        public override IQueryable<BankInfo> All()
        {
            return base.All().Where(p => p.IsDelete == false);
        }

        public IQueryable<BankInfo> All(bool isAll)
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

        public IQueryable<BankInfo> Query(QueryBankModel model)
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
            if (!string.IsNullOrEmpty(model.BankName))
            {
                data = data.Where(p => p.BankName.Contains(model.BankName));
            }
            if (model.BankNo != null)
            {
                data = data.Where(p => p.BankNo == model.BankNo);
            }
            if (!string.IsNullOrEmpty(model.AccountName))
            {
                data = data.Where(p => p.AccountName.Contains(model.AccountName));
            }
            return data.AsQueryable();
        }

        public IQueryable<BankInfo> Query(int id)
        {
            return this.All().Where(p => p.CompanyId == id).AsQueryable();
        }

        public BankInfo Find(int id)
        {
            if (id != 0)
            {
                return this.All().FirstOrDefault(p => p.Id == id);
            }
            else
            {
                return new BankInfo();
            }
        }
        public BankInfo Find(int id, int dataid)
        {
            if (dataid != 0)
            {
                return this.All().FirstOrDefault(p => p.Id == dataid && p.CompanyId == id);
            }
            else
            {
                return new BankInfo() { CompanyId = id };
            }
        }

        public void Update(BankInfo entity)
        {
            var context = (CustomerEntities)this.UnitOfWork.Context;
            context.Entry(entity).State = EntityState.Modified;
        }

        public override void Delete(BankInfo entity)
        {
            entity.IsDelete = true;
            this.Update(entity);
        }

        public override byte[] ExportXLS(IQueryable<BankInfo> entities, params string[] notExportCol)
        {
            var notCol = notExportCol.ToList();
            notCol.Add("Id");
            notCol.Add("IsDelete");
            return base.ExportXLS(entities, notCol.ToArray());
        }

        public override byte[] ExportXLSX(IQueryable<BankInfo> entities, params string[] notExportCol)
        {
            var notCol = notExportCol.ToList();
            notCol.Add("Id");
            notCol.Add("IsDelete");
            return base.ExportXLSX(entities, notCol.ToArray());
        }
    }

	public  interface IBankInfoRepository : IRepository<BankInfo>
	{

	}
}