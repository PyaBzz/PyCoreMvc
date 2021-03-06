﻿using Microsoft.AspNetCore.Mvc;
using Baz.Core;
using Microsoft.AspNetCore.Authorization;
using Baz.CoreMvc;

namespace myCoreMvc.UI.Controllers
{
    //Task: Can we use "AuthenticationSchemes = AuthConstants.AuthSchemeName" as a parameter to the Authorize attribute to choose among available schemes?
    [Authorize(Policy = AuthConstants.Level2PolicyName)]
    [Area("Level2Only")]
    public class Level2AtLeastController : BaseController
    {
        public ActionResult Index()
        {
            return View("MessageOnly", $"Access level 2 granted");
        }
    }
}
