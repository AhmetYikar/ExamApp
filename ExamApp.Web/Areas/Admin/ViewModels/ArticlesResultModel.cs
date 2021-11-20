using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamApp.Web.Areas.Admin.ViewModels
{
    public class ArticlesResultModel
    {
        public bool IsSuccess { get; set; }
        public List<ArticleViewModel> Articles { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ArticleContentResultModel
    {
        public bool IsSuccess { get; set; }
        public string Content { get; set; }
        public string ErrorMessage { get; set; }
    }
}
