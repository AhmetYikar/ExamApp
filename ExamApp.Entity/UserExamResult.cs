using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Entity
{
    public class UserExamResult
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        [Display(Name = "Doğru Cevap Sayısı")]
        public int CorrectAnswersCount { get; set; }

        [Display(Name = "Yanlış Cevap Sayısı")]
        public int WrongAnswersCount { get; set; }

        [Display(Name = "Sınav Tarihi")]
        public DateTime ResultDate { get; set; }
    }
}
