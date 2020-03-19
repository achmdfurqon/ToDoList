using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class ActivityVM : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; } 
        public DateTime? CreateDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public string UserId { get; set; }        
        public bool IsDeleted { get; set; }
    }
}
