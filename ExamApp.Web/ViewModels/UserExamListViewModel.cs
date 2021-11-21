using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamApp.Web.ViewModels
{
    public class UserExamListViewModel
    {
        public int Id { get; set; }

        public bool IsTaken { get; set; }

        [Display(Name ="Sınav Metni Başlığı")]
        public string TextTitle { get; set; }

        [Display(Name = "Doğru Cevap Sayısı")]
        public int CorrectAnswersCount { get; set; }

        [Display(Name = "Yanlış Cevap Sayısı")]
        public int WrongAnswersCount { get; set; }

        [Display(Name = "Sınav Tarihi")]
        public DateTime? ResultDate { get; set; }
    }
}
