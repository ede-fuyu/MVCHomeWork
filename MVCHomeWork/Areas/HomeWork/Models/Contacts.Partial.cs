namespace MVCHomeWork.Areas.HomeWork.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ContactsMetaData))]
    public partial class Contacts : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CustomerEntities db = new CustomerEntities();

            var data = db.Contacts.Where(p => p.CompanyId == CompanyId && p.Id != Id).ToList();

            if (db.Contacts.Where(p => p.CompanyId == CompanyId && p.Id != Id).Any(p => p.Email.ToLower() == Email.ToLower()))
            {
                yield return new ValidationResult("同一個客戶下的聯絡人，其 Email 不能重複", new[] { "Email" });
            }
        }
    }

    public partial class ContactsMetaData
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "客戶名稱", Description = "FK.Company.CompanyName")]
        public int CompanyId { get; set; }
        
        [StringLength(50, ErrorMessage= "職稱欄位長度不得大於 50 個字元")]
        [Required(ErrorMessage = "職稱必需填寫")]
        [Display(Name = "職稱")]
        public string JobTitle { get; set; }
        
        [StringLength(50, ErrorMessage= "姓名欄位長度不得大於 50 個字元")]
        [Required(ErrorMessage = "姓名必需填寫")]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        
        [StringLength(250, ErrorMessage= "Email欄位長度不得大於 250 個字元")]
        [Required(ErrorMessage = "Email必需填寫")]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage= "手機欄位長度不得大於 50 個字元")]
        [PhoneNumberValidatable(ErrorMessage = "手機格式輸入錯誤")]
        [Display(Name = "手機")]
        public string Phone { get; set; }
        
        [StringLength(50, ErrorMessage= "電話欄位長度不得大於 50 個字元")]
        [Display(Name = "電話")]
        public string Tel { get; set; }

        [Required]
        public bool IsDelete { get; set; }
    
        public virtual Company Company { get; set; }
    }

    public class BatchContacts
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "職稱欄位長度不得大於 50 個字元")]
        [Required(ErrorMessage = "職稱必需填寫")]
        [Display(Name = "職稱")]
        public string JobTitle { get; set; }

        [StringLength(50, ErrorMessage = "手機欄位長度不得大於 50 個字元")]
        [PhoneNumberValidatable(ErrorMessage = "手機格式輸入錯誤")]
        [Display(Name = "手機")]
        public string Phone { get; set; }

        [StringLength(50, ErrorMessage = "電話欄位長度不得大於 50 個字元")]
        [Display(Name = "電話")]
        public string Tel { get; set; }
    }
}
