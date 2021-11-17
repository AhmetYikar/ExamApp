using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamApp.Entity
{
    public enum Answer : byte
    {
        A,
        B,
        C,
        D
    }
    public class Question : IValidatableObject
    {
        public int Id { get; set; }       

        [Required(ErrorMessage = "Soru boş geçilemez.")]
        [MaxLength(512, ErrorMessage = "Soru 512 karakterden uzun olamaz.")]
        [Display(Name ="Soru")]
        public string QuestionTitle { get; set; }

        [MaxLength(4096, ErrorMessage = "Soru metni 4096 karakterden uzun olamaz.")]
        [Display(Name = "Soru Metni")]
        public string QuestionText { get; set; }


        [Required(ErrorMessage = "Şıklar boş geçilemez.")]
        [MaxLength(512, ErrorMessage = "Şıklar 512 karakterden uzun olamaz.")]
        public string A { get; set; }

        [Required(ErrorMessage = "Şıklar boş geçilemez.")]
        [MaxLength(512, ErrorMessage = "Şıklar 512 karakterden uzun olamaz.")]
        public string B { get; set; }

        [Required(ErrorMessage = "Şıklar boş geçilemez.")]
        [MaxLength(512, ErrorMessage = "Şıklar 512 karakterden uzun olamaz.")]
        public string C { get; set; }

        [Required(ErrorMessage = "Şıklar boş geçilemez.")]
        [MaxLength(512, ErrorMessage = "Şıklar 512 karakterden uzun olamaz.")]
        public string D { get; set; }

        [Display(Name = "Cevap")]
        public Answer Answer { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        //for custom model validation 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
