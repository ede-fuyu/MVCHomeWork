namespace MVCHomeWork.Areas.HomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ConfigCodeMetaData))]
    public partial class ConfigCode
    {
    }
    
    public partial class ConfigCodeMetaData
    {
        [Required]
        public int CodeKey { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string CodeType { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string CodeName { get; set; }

        [Required]
        public System.DateTime AddDate { get; set; }
    }
}
