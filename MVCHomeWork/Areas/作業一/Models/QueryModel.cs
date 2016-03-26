using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHomeWork.Areas.作業一.Models
{
	public class QueryModel
	{
        public string 客戶名稱 { get; set; }
        public string 統一編號 { get; set; }
    }

    public class QueryBankModel : QueryModel
    {
        public string 銀行名稱 { get; set; }
        public int? 銀行代碼 { get; set; }
        public string 帳戶名稱 { get; set; }
    }

    public class QueryContactPersonModel : QueryModel
    {
        public string 聯絡人姓名 { get; set; }
    }
}