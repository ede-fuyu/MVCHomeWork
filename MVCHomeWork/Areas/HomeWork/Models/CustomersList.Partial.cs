namespace MVCHomeWork.Areas.HomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(CustomersListMetaData))]
    public partial class CustomersList
    {
    }
    
    public partial class CustomersListMetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(8, ErrorMessage="欄位長度不得大於 8 個字元")]
        [Required]
        public string CompanyNo { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        [Display(Name = "客戶名稱")]
        public string CompanyName { get; set; }

        [Display(Name = "銀行帳戶數量")]
        public Nullable<int> BankCount { get; set; }

        [Display(Name = "聯絡人數量")]
        public Nullable<int> ContactCount { get; set; }
    }
}
