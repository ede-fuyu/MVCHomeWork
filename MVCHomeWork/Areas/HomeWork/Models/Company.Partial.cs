namespace MVCHomeWork.Areas.HomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(CompanyMetaData))]
    public partial class Company
    {
    }
    
    public partial class CompanyMetaData
    {
        [Required]
        public int CompanyId { get; set; }
        
        [StringLength(50, ErrorMessage= "客戶名稱欄位長度不得大於 50 個字元")]
        [Required(ErrorMessage = "客戶名稱必需填寫")]
        [Display(Name = "客戶名稱")]
        public string CompanyName { get; set; }
        
        [StringLength(8, ErrorMessage= "統一編號欄位長度不得大於 8 個字元")]
        [Required(ErrorMessage = "統一編號必需填寫")]
        [Display(Name = "統一編號")]
        public string CompanyNo { get; set; }

        [Required(ErrorMessage = "客戶分類必需填寫")]
        [Display(Name = "客戶分類")]
        public Nullable<int> CompanyType { get; set; }
        
        [StringLength(50, ErrorMessage= "電話欄位長度不得大於 50 個字元")]
        [Required(ErrorMessage = "電話必需填寫")]
        [Display(Name = "電話")]
        public string Tel { get; set; }
        
        [StringLength(50, ErrorMessage= "傳真欄位長度不得大於 50 個字元")]
        [Display(Name = "傳真")]
        public string Fax { get; set; }
        
        [StringLength(100, ErrorMessage= "地址欄位長度不得大於 100 個字元")]
        [Display(Name = "地址")]
        public string Address { get; set; }
        
        [StringLength(250, ErrorMessage= "Email 欄位長度不得大於 250 個字元")]
        [EmailAddress(ErrorMessage = "Email格式輸入錯誤")]
        public string Email { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    
        public virtual ICollection<BankInfo> BankInfo { get; set; }
        public virtual ICollection<Contacts> Contacts { get; set; }
    }
}
