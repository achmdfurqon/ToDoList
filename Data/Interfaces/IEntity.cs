using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        bool Status { get; set; }
        bool IsDeleted { get; set; }        
    }
}
