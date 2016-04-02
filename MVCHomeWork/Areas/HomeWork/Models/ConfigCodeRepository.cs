using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCHomeWork.Areas.HomeWork.Models
{   
	public  class ConfigCodeRepository : EFRepository<ConfigCode>, IConfigCodeRepository
	{
        public List<SelectListItem> GetCode(string codetype)
        {
            return this.All().Where(p => p.CodeType == codetype).Select(p => new SelectListItem { Value = p.CodeKey.ToString(), Text = p.CodeName }).ToList();
        }

        public string GetCode(int codekey, string codetype)
        {
            var tt = this.All().Where(p => p.CodeKey == codekey && p.CodeType == codetype).Select(p => p.CodeName).FirstOrDefault();
            return this.All().Where(p => p.CodeKey == codekey && p.CodeType == codetype).Select(p => p.CodeName).FirstOrDefault() ?? "";
        }
    }

	public  interface IConfigCodeRepository : IRepository<ConfigCode>
	{

	}
}