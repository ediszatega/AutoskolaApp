using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public bool IsCorrect { get; set; }
        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
