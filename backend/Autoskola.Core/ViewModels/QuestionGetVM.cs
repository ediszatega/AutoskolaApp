using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class QuestionGetVM
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? Image { get; set; }
        public float Points { get; set; }
        public int Order { get; set; }
        public List<AnswerGetVM>? Answers { get; set; }
    }
}
