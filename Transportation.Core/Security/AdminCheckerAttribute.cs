using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Core.Services;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Core.Security
{
    public class AdminCheckerAttribute : Attribute, IPageFilter
    {
        private int _permissionId = 0;
        private IAdminService _adminService;
        private IConfiguration _configuration;

        public AdminCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (context.HttpContext.Request.Cookies.Any(c => c.Key == "Admin.Auth"))
            {
                _adminService =
                    (IAdminService)context.HttpContext.RequestServices.GetService(typeof(IAdminService));
                _configuration =
                    (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));

                string secretKey = _configuration["Encryption:EncryptSecretKey"];
                string value = EncryptHelper.Decrypt(context.HttpContext.Request.Cookies.SingleOrDefault(c => c.Key == "Admin.Auth").Value, secretKey);
                User user = _adminService.GetUser(_adminService.GetUserIdByEmail(value));
                if (user != null)
                {
                    if (!_adminService.CheckUserPermissions(_permissionId, user.UserName))
                    {
                        context.HttpContext.Response.Redirect("/");
                    }
                }
            }
            else
            {
                context.HttpContext.Response.Redirect("/");
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            
        }
    }
}

