﻿using System;
using System.Linq;
using myCoreMvc.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PyaFramework.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace myCoreMvc.Controllers
{
    [Area("Users")]
    public class UserSetPasswordController : BaseController
    {
        private IUserService _userService;

        public UserSetPasswordController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(Guid id)
        {
            var user = DataProvider.Get<User>(id);
            var inputModel = new EnterModel();
            if (user != null) inputModel.CopySimilarPropertiesFrom(user);
            return View("UserSetPassword", inputModel);
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var transactionResult = _userService.SetPassword(inputModel.Id, inputModel.Password);
                var resultMessage = "";
                switch (transactionResult)
                {
                    case TransactionResult.Updated: resultMessage = "Password updated"; break;
                    case TransactionResult.NotFound: resultMessage = "User not found"; break;
                    default: resultMessage = transactionResult.ToString(); break;
                }
                return RedirectToAction(nameof(UserListController.Index), ShortNameOf<UserListController>(), new { message = resultMessage });  // Prevents re-submission by refresh
            }
            else
            {
                inputModel.Message = "Invalid values for: "
                    + ModelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => p.Key).ToString(", ");
                return View("UserSetPassword", inputModel);
            }
        }

        public class EnterModel : IClonable
        {
            public Guid Id { get; set; }

            public string Name { get; set; }
            public string Password { get; set; }
            public string Message = ""; //Task: Do we need these Messages in enter controllers
        }
    }
}
