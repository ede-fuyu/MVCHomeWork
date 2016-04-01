using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCHomeWork.Areas.HomeWork.Models
{
	public class QueryCompanyModel
	{
        [Display(Name = "客戶名稱")]
        public string CompanyName { get; set; }

        [Display(Name = "統一編號")]
        public string CompanyNo { get; set; }
    }

    public class QueryBankModel : QueryCompanyModel
    {
        [Display(Name = "銀行名稱")]
        public string BankName { get; set; }

        [Display(Name = "銀行代碼")]
        public int? BankNo { get; set; }

        [Display(Name = "帳戶名稱")]
        public string AccountName { get; set; }
    }

    public class QueryContactModel : QueryCompanyModel
    {
        [Display(Name = "聯絡人姓名")]
        public string ContactName { get; set; }
    }
}