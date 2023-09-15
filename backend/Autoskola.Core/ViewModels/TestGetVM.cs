using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class TestGetVM
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int CategoryId {get; set; }
        public Category Category { get; set; }
        public List<QuestionGetVM>? Questions { get; set; }

    }
}
