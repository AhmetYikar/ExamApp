using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamApp.Web.Areas.Admin.ViewModels
{
    public enum Tag
    {
        Success, Danger, Info
    }
    public class ResultStatusViewModel
    {
        public string Message { get; set; }
        public Tag Tag { get; set; }
    }
  
}
