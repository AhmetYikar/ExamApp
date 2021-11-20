using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Entity
{
    public class Exam:IValidatableObject
    {
        public int Id { get; set; }

        [MaxLength(512, ErrorMessage = "Sınav metni başlığı 512 karakterden uzun olamaz.")]
        [Display(Name = "Sınav Metni Başlığı")]
        [Required(ErrorMessage = "Sınav metni boş geçilemez.")]
        public string TextTitle { get; set; }

      
        [Display(Name = "Sınav Metni")]
        [Required(ErrorMessage = "Sınav metni boş geçilemez.")]
        public string Text { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Oluşturan")]
        public string CreatedById { get; set; }

        [Display(Name = "Son Düzenleme Tarihi")]
        public DateTime? ModifiedAt { get; set; }

        [Display(Name = "Son Düzenleyen")]
        public string ModifiedById { get; set; }

        public List<Question> Questions { get; set; }= new List<Question>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Questions.Count()!=4)
            {
                yield return new ValidationResult(
                $"Soru sayısı 4 olmalıdır.",
                new[] { nameof(Questions) });
            }
          
        }
    }
}
