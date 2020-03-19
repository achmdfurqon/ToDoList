using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Data.ViewModels;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        readonly HttpClient httpClient = new HttpClient();
        public ToDoListController()
        {
            httpClient.BaseAddress = new Uri("https://localhost:44306/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
        }
        // GET: ToDoLists
        public ActionResult Index()
        {
            //HttpContext.Session.SetString("IdUser", "16151");
            //if (HttpContext.Session.GetString("IdUser") != null)
            //{
            //    ViewBag.ToDoLists = ToDoLists();
                return View();
            //}
            //else
            //{
            //    return RedirectToAction("", "Users");
            //}
        }
        // GET : ToDoLists/Lists
        public async Task<DataTableView> GetData(int status, string keyword, int size, int page)
        {
            var iduser = "16151"; // HttpContext.Session.GetString("IdUser");
            var url = "Activities/data?uid=" + iduser + "&status=" + status + "&keyword=" + keyword + "&page=" + page + "&size=" + size;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<DataTableView>();
            }
            return null;
        }

        [HttpGet("ToDoList/Data/{status}")]
        public IActionResult Data(IDataTablesRequest request, int status)
        {
            var pageSize = request.Length;
            var pageNumber = request.Start / request.Length + 1;
            string keyword = string.Empty;
            if (request.Search.Value != null)
            {
                keyword = request.Search.Value;
            }
            var dataPage = GetData(status, keyword, pageSize, pageNumber).Result;
            var filteredData = dataPage.filterLength;
            var response = DataTablesResponse.Create(request, dataPage.length, filteredData, dataPage.data);
            return new DataTablesJsonResult(response, true);
        }
        //public IList<ActivityVM> ToDoLists()
        //{
        //    var iduser = HttpContext.Session.GetString("IdUser");
        //    IList<ActivityVM> toDoLists = null;
        //    var responseTask = httpClient.GetAsync("Activities/1/10/" + iduser);
        //    responseTask.Wait();
        //    var result = responseTask.Result;
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var readTask = result.Content.ReadAsAsync<IList<ActivityVM>>();
        //        readTask.Wait();
        //        toDoLists = readTask.Result;
        //    }
        //    return toDoLists;
        //}
        // GET: ToDoLists/Details/5
        public JsonResult Get(int id)
        {
            var cek = httpClient.GetAsync("Activities/" + id).Result;
            var read = cek.Content.ReadAsAsync<ActivityVM>().Result;
            return Json(new { data = read });
        }
        // POST: ToDoLists/Create
        public ActionResult Create(ActivityVM activityVM)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(activityVM);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var affectedRow = httpClient.PostAsync("Activities", byteContent).Result;
                return Json(new { data = affectedRow, affectedRow.StatusCode });
            }
            catch
            {
                return View();
            }
        }

        // POST: ToDoLists/Edit/5
        public ActionResult Edit(int id, ActivityVM activityVM)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(activityVM);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var ByteContent = new ByteArrayContent(buffer);
                ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var update = httpClient.PutAsync("Activities/" + id, ByteContent).Result;
                return Json(new { data = update, update.StatusCode });
            }
            catch
            {
                return View();
            }
        }
        // GET: ToDoLists/Delete/5
        public ActionResult Delete(int id)
        {
            var affectedRow = httpClient.DeleteAsync("Activities/" + id).Result;
            return Json(new { data = affectedRow });
        }
        // POST: ToDoLists/UpdateStatus/5
        public ActionResult Complete(int id, ActivityVM activityVM)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(activityVM);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var ByteContent = new ByteArrayContent(buffer);
                ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var update = httpClient.PutAsync("Activities/Complete/" + id, ByteContent).Result;
                return Json(new { data = update, update.StatusCode });
            }
            catch
            {
                return View();
            }
        }
    }
}