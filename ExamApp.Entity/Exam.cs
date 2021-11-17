using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Entity
{
    public class Exam 
    {
        public int Id { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Oluşturan")]
        public ExamAppUser CreatedBy { get; set; }

        [Display(Name = "Son Düzenleme Tarihi")]
        public DateTime? ModifiedAt { get; set; }

        [Display(Name = "Son Düzenleyen")]
        public ExamAppUser ModifiedBy { get; set; }

        public List<Question>Questions { get; set; }
    }
}
