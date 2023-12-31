﻿using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class NewsAddVM
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
        public string? Image { get; set; }
        public int UserId { get; set; }
    }
}
