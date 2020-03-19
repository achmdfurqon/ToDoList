using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Data.Interfaces;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activity;
        public ActivitiesController(IActivityService service)
        {
            _activity = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var get = _activity.Get().Result;
            get = get.Where(a => a.IsDeleted.Equals(false));
            return Ok(get);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var get = _activity.Get(id).Result;
            return Ok(get);
        }

        [HttpGet("data")]
        public async Task<ActionResult<DataTableView>> GetData(string uid, int status, string keyword, int page, int size)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            var data = await _activity.Get(uid, status, keyword, page, size);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post(ActivityVM activity)
        {
            var post = _activity.Create(activity).Result;
            return Ok(post);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ActivityVM activity)
        {
            var put = _activity.Update(id, activity).Result;
            return Ok(put);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var del = _activity.Delete(id).Result;
            return Ok(del);
        }

        [HttpPut("Complete/{id}")]
        public IActionResult Complete(int id)
        {
            var com = _activity.Complete(id).Result;
            return Ok(com);
        }

        [HttpPut("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var rem = _activity.Remove(id).Result;
            return Ok(rem);
        }
    }
}