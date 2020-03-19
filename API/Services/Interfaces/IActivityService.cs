using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityVM>> Get();
        Task<ActivityVM> Get(int Id);
        Task<DataTableView> Get(string userId, int status, string keyword, int page, int pageSize);
        Task<int> Create(ActivityVM activityVM);
        Task<int> Update(int Id, ActivityVM activityVM);
        Task<bool> Complete(int Id);
        Task<bool> Delete(int Id);
        Task<bool> Remove(int Id);
    }
}
