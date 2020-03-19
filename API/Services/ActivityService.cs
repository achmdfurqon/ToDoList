using API.Services.Interfaces;
using Data.Interfaces;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository activity;
        public ActivityService(IActivityRepository repository)
        {
            activity = repository;
        }

        public Task<bool> Complete(int Id)
        {
            return activity.Complete(Id);
        }

        public Task<int> Create(ActivityVM activityVM)
        {
            return activity.Post(activityVM);
        }

        public Task<bool> Delete(int Id)
        {
            return activity.Delete(Id);
        }

        public Task<IEnumerable<ActivityVM>> Get()
        {
            return activity.Get();
        }

        public Task<ActivityVM> Get(int Id)
        {
            return activity.Get(Id);
        }

        public Task<DataTableView> Get(string userId, int status, string keyword, int page, int pageSize)
        {
            return activity.Get(userId, status, keyword, page, pageSize);
        }

        public Task<bool> Remove(int Id)
        {
            return activity.Remove(Id);
        }

        public Task<int> Update(int Id, ActivityVM activityVM)
        {
            return activity.Put(Id, activityVM);
        }
    }
}
