using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Models
{
    public enum QuestionType
    {
        Teorija,
        Znakovi,
        Raskrsnice
    }
    public class Question
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? Image { get; set; }
        public float Points { get; set; }
        public QuestionType QuestionType { get; set; }
        [ForeignKey(nameof(Test))]
        public int TestId { get; set; }
        public Test? Test { get; set; }
        public int Order { get; set; }
        public virtual List<Answer>? Answers { get; set; }
    }
}
