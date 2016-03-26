using MVCHomeWork.Areas.作業一.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork.Areas.作業一.Controllers
{
    public class 客戶資料管理Controller : Controller
    {
        private 作業一資料Entities db = new 作業一資料Entities();

        // GET: 作業一/客戶資料管理
        public ActionResult 主頁()
        {
            return View(0);
        }

        public ActionResult 清單查詢()
        {
            return View("主頁", 1);
        }

        public ActionResult QueryList(QueryModel model)
        {
            var data = db.清單.AsQueryable();
            if (!string.IsNullOrEmpty(model.統一編號))
            {
                data = data.Where(p => p.統一編號 == model.統一編號);
            }
            if (!string.IsNullOrEmpty(model.客戶名稱))
            {
                data = data.Where(p => p.客戶名稱.Contains(model.客戶名稱));
            }
            return PartialView("客戶清單", data.ToList());
        }

        public ActionResult QueryDataList(QueryModel model)
        {
            var data = db.客戶資料.Where(p => p.是否已刪除 == false).AsQueryable();
            if (!string.IsNullOrEmpty(model.統一編號))
            {
                data = data.Where(p => p.統一編號 == model.統一編號);
            }
            if (!string.IsNullOrEmpty(model.客戶名稱))
            {
                data = data.Where(p => p.客戶名稱.Contains(model.客戶名稱));
            }
            return PartialView("客戶資料清單", data.ToList());
        }

        public ActionResult Edit(int id)
        {
            ViewBag.isEdit = true;
            return loadData(id);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.isEdit = false;
            if (id == 0)
            {
                return PartialView("編輯頁", null);
            }
            return loadData(id);
        }

        public ActionResult loadData(int id)
        {
            var model = new 客戶資料();
            if (id != 0)
            {
                model = db.客戶資料.Where(p => p.Id == id).FirstOrDefault();
            }
            return PartialView("編輯頁", model);
        }

        public ActionResult Save(客戶資料 model)
        {
            var msg = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    if(model.Id == 0)
                    {
                        db.客戶資料.Add(model);
                        msg = "客戶資料新增成功";
                    }
                    else
                    {
                        db.Entry(model).State = EntityState.Modified;
                        msg = "客戶資料儲存成功";
                    }
                    db.SaveChanges();
                    return Json(new { isValid = true, message = HttpUtility.HtmlEncode(msg) });
                }
                catch (Exception ex)
                {
                    return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶資料儲存失敗。錯誤訊息: " + ex.Message) });
                }
            }
            msg = string.Join(" ", ModelState.Values.SelectMany(p => p.Errors).Select(p => p.ErrorMessage));
            return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶資料儲存時，驗證欄位失敗。" + msg) });
        }

        public ActionResult Delete(int id)
        {
            var data = db.客戶資料.Where(p=>p.Id == id).FirstOrDefault();
            if (data != null)
            {
                try
                {
                    foreach(var item in data.客戶銀行資訊)
                    {
                        item.是否已刪除 = true;
                        db.Entry(item).State = EntityState.Modified;
                    }
                    foreach (var item in data.客戶聯絡人)
                    {
                        item.是否已刪除 = true;
                        db.Entry(item).State = EntityState.Modified;
                    }

                    data.是否已刪除 = true;
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { isValid = true, message = HttpUtility.HtmlEncode("客戶資料刪除成功。") });
                }
                catch (Exception ex)
                {
                    return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶資料刪除失敗。錯誤訊息: " + ex.Message) });
                }
            }
            return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶資料不存在，請重新整理網頁。") });
        }
    }
}