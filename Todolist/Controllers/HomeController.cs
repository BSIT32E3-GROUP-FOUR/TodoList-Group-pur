using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Service;
using TodoList.Domain;
using Todolist.Models;
using TodoList.Domain.Interface;
using System.Linq;


namespace Todolist.Controllers
{
    public class HomeController(ITodoService service) : Controller
    {
        
        public IActionResult Index(string id)
        {
            var temp = service.GetAll();

            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Categories = Enum.GetValues(typeof(Category)).OfType<Category>().ToDictionary(x => x, x => Enum.GetName(x));
            ViewBag.Statuses = Enum.GetValues(typeof(Status)).OfType<Status>().ToDictionary(x => x, x => Enum.GetName(x));
            ViewBag.DueFilters = Filters.DueFilterValues;
            var query = service.GetAll();
            //IQueryable<ToDo> query = context.ToDoS
            //    .Include(t => t.Category)
            //    .Include(t => t.Status);

            //if (filters.HasCategory)
            //{
            //    query = query.Where(t => t.CategoryId == filters.CategoryId);
            //}

            //if (filters.HasStatus)
            //{
            //    query = query.Where(t => t.StatusId == filters.StatusId);
            //}

            //if (filters.HasDue)
            //{
            //    var today = DateTime.Today;
            //    if (filters.IsPast)
            //    {
            //        query = query.Where(t => t.DueDate < today);
            //    }
            //    else if (filters.IsFuture)
            //    {
            //        query = query.Where(t => t.DueDate > today);

            //    }
            //    else if (filters.IsToday)
            //    {
            //        query = query.Where(t => t.DueDate == today);
            //    }
            //}
            //var tasks = query.OrderBy(t => t.DueDate).ToList();

            return View(temp);

          
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = Enum.GetValues(typeof(Category));
            

            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTodoModel task)
        {
            if (ModelState.IsValid)
            {
              service.Add(task);
                return RedirectToAction("Index");
            }
           
                ViewBag.Categories = Enum.GetValues(typeof(Category));

                return View(task);
            
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join("-", filter);
            return RedirectToAction("Index", new { ID = id });
        }
     
        
        [HttpPost]
        public async Task<IActionResult> MarkComplete([FromRoute] string id, ToDo selected)
        {
            var markedAsDone = await service.MarkAsDone(int.Parse(id));
            
            return RedirectToAction("Index", new { ID = id });
        }
        [HttpPost]
        public IActionResult DeleteComplete(int id)
        {
           
            service.Delete(id);

            return RedirectToAction("Index", new { ID = id });
        }
    }
}
