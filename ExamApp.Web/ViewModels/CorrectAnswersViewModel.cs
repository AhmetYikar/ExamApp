using ExamApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamApp.Web.ViewModels
{
    public class CorrectAnswersViewModel
    {
        public int QuestionId { get; set; }
        public Answer Answer { get; set; }
    }
}
