using ExamApp.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamApp.Web.Areas.Admin.Helper
{
    public class StatusMessageHelper : Controller
    {
        public ResultStatusViewModel GetStatusMessage(HttpContext httpContext)
        {
            ResultStatusViewModel messageModel = new ResultStatusViewModel();
            var message = httpContext.Session.GetString("message");
            var tag = httpContext.Session.GetString("tag");
            if (!string.IsNullOrEmpty(message))
            {
                messageModel.Message = message;
                messageModel.Tag = (Tag)Enum.Parse(typeof(Tag), tag);

                httpContext.Session.Remove("message");
                httpContext.Session.Remove("tag");
            }
            return messageModel;
        }
    }
}
