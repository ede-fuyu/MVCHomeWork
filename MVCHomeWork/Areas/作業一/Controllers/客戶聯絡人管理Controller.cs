﻿using MVCHomeWork.Areas.作業一.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork.Areas.作業一.Controllers
{
    public class 客戶聯絡人管理Controller : Controller
    {
        private 作業一資料Entities db = new 作業一資料Entities();

        // GET: 作業一/客戶聯絡人管理
        public ActionResult 主頁()
        {
            return View();
        }

        public ActionResult QueryList(QueryContactPersonModel model)
        {
            var data = from a in db.客戶聯絡人
                       join b in db.客戶資料 on a.客戶Id equals b.Id
                       where a.是否已刪除 == false && b.是否已刪除 == false
                       select new { a, b };

            if (!string.IsNullOrEmpty(model.統一編號))
            {
                data = data.Where(p => p.b.統一編號 == model.統一編號);
            }
            if (!string.IsNullOrEmpty(model.客戶名稱))
            {
                data = data.Where(p => p.b.客戶名稱.Contains(model.客戶名稱));
            }
            if (!string.IsNullOrEmpty(model.聯絡人姓名))
            {
                data = data.Where(p => p.a.姓名.Contains(model.聯絡人姓名));
            }

            return PartialView("客戶聯絡人資料清單", data.Select(p => p.a).ToList());
        }

        public ActionResult QueryContactPersonList(int id)
        {
            var data = db.客戶聯絡人.Where(p => p.客戶Id == id && p.是否已刪除 == false);
            ViewBag.id = id;
            return PartialView(data);
        }

        public ActionResult Edit(int id, int dataid)
        {
            ViewBag.isEdit = true;
            return loadData(id, dataid);
        }

        public ActionResult Detail(int id, int dataid)
        {
            ViewBag.isEdit = false;
            if (id == 0)
            {
                return PartialView("編輯頁", null);
            }
            return loadData(id, dataid);
        }

        public ActionResult loadData(int id, int dataid)
        {
            var model = new 客戶聯絡人() { 客戶Id = id };
            if (dataid != 0)
            {
                model = db.客戶聯絡人.Where(p => p.客戶Id == id && p.Id == dataid).FirstOrDefault();
            }
            else
            {
                if (id == 0)
                {
                    ViewBag.Client = new SelectList(db.客戶資料.Where(p => p.是否已刪除 == false).Select(p => new { value = p.Id, text = p.客戶名稱 }).ToList(), "value", "text");
                }
                else
                {
                    ViewBag.Client = db.客戶資料.Where(p=>p.Id == id).Select(p=>p.客戶名稱).FirstOrDefault();
                }
            }
            return PartialView("編輯頁", model);
        }

        public ActionResult Save(客戶聯絡人 model)
        {
            var msg = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id == 0)
                    {
                        db.客戶聯絡人.Add(model);
                        msg = "客戶聯絡人新增成功";
                    }
                    else
                    {
                        db.Entry(model).State = EntityState.Modified;
                        msg = "客戶聯絡人儲存成功";
                    }
                    db.SaveChanges();
                    return Json(new { isValid = true, message = HttpUtility.HtmlEncode(msg) });
                }
                catch (Exception ex)
                {
                    return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶聯絡人儲存失敗。錯誤訊息: " + ex.Message) });
                }
            }
            msg = string.Join(" ", ModelState.Values.SelectMany(p => p.Errors).Select(p => p.ErrorMessage));
            return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶聯絡人儲存時，驗證欄位失敗。" + msg) });
        }

        public ActionResult Delete(int id)
        {
            var data = db.客戶聯絡人.Where(p => p.Id == id).FirstOrDefault();
            if (data != null)
            {
                try
                {
                    data.是否已刪除 = true;
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { isValid = true, id = data.客戶Id, message = HttpUtility.HtmlEncode("客戶聯絡人刪除成功。") });
                }
                catch (Exception ex)
                {
                    return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶聯絡人刪除失敗。錯誤訊息: " + ex.Message) });
                }
            }
            return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶聯絡人不存在，請重新整理網頁。") });
        }
    }
}