using MVCHomeWork.Areas.HomeWork.Models;
using MVCHomeWork.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork.Areas.HomeWork.Controllers
{
    public class CompanyController : BaseController
    {
        // GET: HomeWork/Company
        public ActionResult Index()
        {
            return View(0);
        }

        // GET: HomeWork/Company/List

        public ActionResult List()
        {
            return View("Index", 1);
        }

        public ActionResult QueryList(QueryCompanyModel model)
        {
            return PartialView(CompanyRepo.Query(model));
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
                return PartialView("Edit", null);
            }
            return loadData(id);
        }

        public ActionResult loadData(int id)
        {
            return PartialView("Edit", CompanyRepo.Find(id));
        }

        public ActionResult Save(Company model)
        {
            var msg = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id == 0)
                    {
                        CompanyRepo.Add(model);
                        msg = "客戶資料新增成功";
                    }
                    else
                    {
                        CompanyRepo.Update(model);
                        msg = "客戶資料儲存成功";
                    }
                    CompanyRepo.UnitOfWork.Commit();
                    return Json(new { id = model.Id, isValid = true, message = HttpUtility.HtmlEncode(msg) });
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
            var data = CompanyRepo.Find(id);
            if (data != null)
            {
                try
                {
                    foreach(var bankinfo in data.BankInfo)
                    {
                        BankRepo.Delete(bankinfo);
                    }
                    foreach(var contact in data.Contacts)
                    {
                        ContactRepo.Delete(contact);
                    }

                    CompanyRepo.Delete(data);
                    CompanyRepo.UnitOfWork.Commit();
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