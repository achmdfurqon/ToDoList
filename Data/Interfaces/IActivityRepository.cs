using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IActivityRepository
    {
        Task<DataTableView> Get(string uid, int status, string keyword, int page, int size);
        Task<IEnumerable<ActivityVM>> Get();
        Task<ActivityVM> Get(int id);
        Task<int> Post(ActivityVM activity);
        Task<int> Put(int id, ActivityVM activity);
        Task<bool> Delete(int id);
        Task<bool> Complete(int id);
        Task<bool> Remove(int id);
    }
}
