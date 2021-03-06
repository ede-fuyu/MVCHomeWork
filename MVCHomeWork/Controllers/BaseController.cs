﻿using MVCHomeWork.Areas.HomeWork.Models;
using MVCHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork.Controllers
{
    [TimeLogToDebug]
    [Authorize]
    public class BaseController : Controller
    {
        protected CustomersListRepository ListRepo = RepositoryHelper.GetCustomersListRepository();
        protected CompanyRepository CompanyRepo = RepositoryHelper.GetCompanyRepository();
        protected BankInfoRepository BankRepo = RepositoryHelper.GetBankInfoRepository();
        protected ContactsRepository ContactRepo = RepositoryHelper.GetContactsRepository();
        protected ConfigCodeRepository CodeRepo = RepositoryHelper.GetConfigCodeRepository();
    }
}