using MVCHomeWork.Areas.HomeWork.Models;
using MVCHomeWork.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork.Areas.HomeWork.Controllers
{
    public class BankInfoController : BaseController
    {
        // GET: HomeWork/BankInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QueryList(QueryBankModel model)
        {
            return PartialView(BankRepo.Query(model));
        }

        public ActionResult QueryBankList (int id)
        {
            return PartialView(BankRepo.Query(id));
        }

        public ActionResult Edit(int id, int dataid)
        {
            ViewBag.isEdit = true;
            return loadData(id, dataid);
        }

        public ActionResult Detail(int id, int dataid)
        {
            ViewBag.isEdit = false;
            if (dataid == 0)
            {
                return PartialView("Edit", null);
            }
            return loadData(id, dataid);
        }

        public ActionResult loadData(int id, int dataid)
        {
            if (id == 0)
            {
                ViewBag.Client = new SelectList(CompanyRepo.All().Select(p => new { value = p.Id, text = p.CompanyName }), "value", "text");
            }
            else
            {
                var company = CompanyRepo.Find(id);
                ViewBag.Client = company == null ? "" : company.CompanyName;
            }
            return PartialView("Edit", BankRepo.Find(id, dataid));
        }

        public ActionResult Save(BankInfo model)
        {
            var msg = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id == 0)
                    {
                        BankRepo.Add(model);
                        msg = "客戶銀行資訊新增成功";
                    }
                    else
                    {
                        BankRepo.Update(model);
                        msg = "客戶銀行資訊儲存成功";
                    }
                    BankRepo.UnitOfWork.Commit();
                    return Json(new { id = model.Id, isValid = true, message = HttpUtility.HtmlEncode(msg) });
                }
                catch (Exception ex)
                {
                    return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶銀行資訊儲存失敗。錯誤訊息: " + ex.Message) });
                }
            }
            msg = string.Join(" ", ModelState.Values.SelectMany(p => p.Errors).Select(p => p.ErrorMessage));
            return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶銀行資訊儲存時，驗證欄位失敗。" + msg) });
        }

        public ActionResult Delete(int id)
        {
            var data = BankRepo.Find(id);
            if (data != null)
            {
                try
                {
                    BankRepo.Delete(data);
                    BankRepo.UnitOfWork.Commit();
                    return Json(new { isValid = true, id = data.CompanyId, message = HttpUtility.HtmlEncode("客戶銀行資訊刪除成功。") });
                }
                catch (Exception ex)
                {
                    return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶銀行資訊刪除失敗。錯誤訊息: " + ex.Message) });
                }
            }
            return Json(new { isValid = false, message = HttpUtility.HtmlEncode("客戶銀行資訊不存在，請重新整理網頁。") });
        }
    }
}