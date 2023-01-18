using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class AnswerAddVM
    {
        public string? Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
