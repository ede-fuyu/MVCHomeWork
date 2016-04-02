using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace MVCHomeWork.Areas.HomeWork.Models
{
    public interface IExcelRepository<T>
    {
        byte[] ExportXLS(IQueryable<T> entities, params string[] notExportCol);

        byte[] ExportXLSX(IQueryable<T> entities, params string[] notExportCol);
    }

    public class ExcelRepository<T> : IExcelRepository<T>
    {
        public virtual byte[] ExportXLS(IQueryable<T> entities, params string[] notExportCol)
        {
            var coltitle = GetColumnTitle(entities.ElementType, notExportCol);
            if (coltitle.Count() > 0)
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet(entities.ElementType.Name);
                int i = 0, j = 0;
                IRow row = sheet.CreateRow(i);
                foreach (var col in coltitle)
                {
                    row.CreateCell(j).SetCellValue(col.Value);
                    j++;
                }

                foreach (var item in entities)
                {
                    i++;
                    row = sheet.CreateRow(i);
                    j = 0;
                    foreach (var col in coltitle)
                    {
                        PropertyInfo[] ps = item.GetType().GetProperties();
                        if (ps.Any(p => p.Name == col.Key))
                        {
                            var value = ps.Where(p => p.Name == col.Key).First().GetValue(item);
                            if (value == null)
                            {
                                row.CreateCell(j).SetCellValue("");
                            }
                            else
                            {
                                row.CreateCell(j).SetCellValue(value.ToString());
                            }
                            j++;
                        }
                    }
                }
                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                return ms.ToArray();
            }
            return null;
        }

        public virtual byte[] ExportXLSX(IQueryable<T> entities, params string[] notExportCol)
        {
            var coltitle = GetColumnTitle(entities.ElementType, notExportCol);
            if (coltitle.Count() > 0)
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet(entities.ElementType.Name);
                int i = 0, j = 0;
                IRow row = sheet.CreateRow(i);
                foreach (var col in coltitle)
                {
                    row.CreateCell(j).SetCellValue(col.Value);
                    j++;
                }

                foreach (var item in entities)
                {
                    i++;
                    row = sheet.CreateRow(i);
                    j = 0;
                    foreach (var col in coltitle)
                    {
                        PropertyInfo[] ps = item.GetType().GetProperties();
                        if (ps.Any(p => p.Name == col.Key))
                        {
                            var value = ps.Where(p => p.Name == col.Key).First().GetValue(item);
                            if (value == null)
                            {
                                row.CreateCell(j).SetCellValue("");
                            }
                            else
                            {
                                row.CreateCell(j).SetCellValue(value.ToString());
                            }
                            j++;
                        }
                    }
                }
                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                return ms.ToArray();
            }
            return null;
        }

        #region 私有方法
        private List<KeyValuePair<string, string>> GetColumnTitle(Type entityType, params string[] notExportCol)
        {
            var tableTitle = new List<KeyValuePair<string, string>>();

            var Properties = entityType.GetProperties().Where(p => p.SetMethod.Attributes == (MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName)).ToList();
            foreach (var prop in Properties)
            {
                var attr = (DisplayAttribute)entityType.GetProperty(prop.Name).GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();
                if (attr == null)
                {
                    MetadataTypeAttribute metadataType = (MetadataTypeAttribute)entityType.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                    if (metadataType != null)
                    {
                        var property = metadataType.MetadataClassType.GetProperty(prop.Name);
                        if (property != null)
                        {
                            attr = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();
                        }
                    }
                }

                if (!notExportCol.Any(p=> p == prop.Name))
                {
                    var param = new KeyValuePair<string, string>();
                    if (attr == null)
                    {
                        param = new KeyValuePair<string, string>(prop.Name, prop.Name);
                    }
                    else if (!notExportCol.Any(p => p == attr.Name))
                    {
                        param = new KeyValuePair<string, string>(prop.Name, attr.Name);
                    }
                    tableTitle.Add(param);
                }
            }
            return tableTitle;
        }
        #endregion
    }
}