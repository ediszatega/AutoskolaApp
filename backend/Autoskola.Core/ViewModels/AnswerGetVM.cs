﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class AnswerGetVM
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
