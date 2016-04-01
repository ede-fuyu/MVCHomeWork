namespace MVCHomeWork.Areas.HomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(BankInfoMetaData))]
    public partial class BankInfo
    {
    }
    
    public partial class BankInfoMetaData
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "客戶名稱")]
        public int CompanyId { get; set; }
        
        [StringLength(50, ErrorMessage= "銀行名稱欄位長度不得大於 50 個字元")]
        [Required(ErrorMessage = "銀行名稱必需填寫")]
        [Display(Name = "銀行名稱")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "銀行代碼必需填寫")]
        [Display(Name = "銀行代碼")]
        public int BankNo { get; set; }

        [Display(Name = "分行代碼")]
        public Nullable<int> SubBankNo { get; set; }
        
        [StringLength(50, ErrorMessage= "帳戶名稱欄位長度不得大於 50 個字元")]
        [Required(ErrorMessage = "帳戶名稱必需填寫")]
        [Display(Name = "帳戶名稱")]
        public string AccountName { get; set; }
        
        [StringLength(20, ErrorMessage= "帳戶號碼欄位長度不得大於 20 個字元")]
        [Required(ErrorMessage = "帳戶號碼必需填寫")]
        [Display(Name = "帳戶號碼")]
        public string AccountNo { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    
        public virtual Company Company { get; set; }
    }
}
