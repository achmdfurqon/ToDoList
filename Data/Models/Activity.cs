using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class Activity : Base
    {
        public string Name { get; set; }
        public DateTime? FinishedDate { get; set; }
        public User User { get; set; }
        public Activity() { }
    }
}
