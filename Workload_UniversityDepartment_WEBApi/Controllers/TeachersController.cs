 using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Workload_UniversityDepartment_WEBApi.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ILogger<TeachersController> _logger;
        public IRepository<Teacher> Repository { get; private set; }

        public TeachersController(ILogger<TeachersController> logger, IRepository<Teacher> db)
        {
            _logger = logger;
            this.Repository = db;
        }

        [Route("Teachers")]
        public IActionResult Index()
        {
            return View(Repository.All);
        }

        [HttpPost]
        [Route("Teachers")]
        public IActionResult Index(string searchName)
        {
            return View(Repository.FindByName(searchName));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Genders = Repository.GetGenders();
            ViewBag.MaritalStatuses = Repository.GetMaritalStatuses();
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Teacher teacher)
        {
            ViewBag.Genders = Repository.GetGenders();
            ViewBag.MaritalStatuses = Repository.GetMaritalStatuses();
            if (ModelState.IsValid)
            {
                try
                {
                    Repository.Add(teacher);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(teacher);
        }

        [HttpGet]
        public IActionResult Read(int id)
        {
            Teacher teacher = Repository.FindById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Teacher teacher = Repository.FindById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewBag.Genders = Repository.GetGenders();
            ViewBag.MaritalStatuses = Repository.GetMaritalStatuses();
            return View(teacher);
        }

        [HttpPost]
        public IActionResult Update([FromForm] Teacher teacher)
        {
            ViewBag.Genders = Repository.GetGenders();
            ViewBag.MaritalStatuses = Repository.GetMaritalStatuses();
            if (ModelState.IsValid)
            {
                try
                {
                    Repository.Update(teacher);
                }
                catch (Exception ex)
                {
                    if(ex.InnerException != null)
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    
                    return View(teacher);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Teacher teacher = Repository.FindById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpPost]
        public IActionResult Delete([FromForm] Teacher teacher)
        {
            try
            {
                Repository.Delete(Repository.FindById(teacher.Id));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(Repository.FindById(teacher.Id));
            }
        }
    }
}
