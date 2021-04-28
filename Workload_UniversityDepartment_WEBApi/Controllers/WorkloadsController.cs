 using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Workload_UniversityDepartment_WEBApi.Controllers
{
    public class WorkloadsController : Controller
    {
        private readonly ILogger<WorkloadsController> _logger;
        public IRepository<DepartmentWorkload> Repository { get; private set; }

        public WorkloadsController(ILogger<WorkloadsController> logger, IRepository<DepartmentWorkload> db)
        {
            _logger = logger;
            this.Repository = db;
        }

        [Route("Workloads")]
        public IActionResult Index()
        {
            return View(Repository.All);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Teachers = Repository.GetTeachers();
            ViewBag.Subjects = Repository.GetSubjects();
            ViewBag.Workloads = Repository.GetWorkloadTypes();
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] DepartmentWorkload entity)
        {
            ViewBag.Teachers = Repository.GetTeachers();
            ViewBag.Subjects = Repository.GetSubjects();
            ViewBag.Workloads = Repository.GetWorkloadTypes();
            if (ModelState.IsValid)
            {
                try
                {
                    Repository.Add(entity);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(entity);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Teachers = Repository.GetTeachers();
            ViewBag.Subjects = Repository.GetSubjects();
            ViewBag.Workloads = Repository.GetWorkloadTypes();

            DepartmentWorkload entity = Repository.FindById(id);
            if (entity == null)
            {
                return NotFound();
            }
            
            return View(entity);
        }

        [HttpPost]
        public IActionResult Update([FromForm] DepartmentWorkload entity)
        {
            ViewBag.Teachers = Repository.GetTeachers();
            ViewBag.Subjects = Repository.GetSubjects();
            ViewBag.Workloads = Repository.GetWorkloadTypes();
            if (ModelState.IsValid)
            {
                try
                {
                    Repository.Update(entity);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                    return View(entity);
                }
                return RedirectToAction(nameof(Index));
            }

            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            DepartmentWorkload entity = Repository.FindById(id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult Delete([FromForm] DepartmentWorkload entity)
        {
            try
            {
                Repository.Delete(Repository.FindById(entity.Id));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                return View(Repository.FindById(entity.Id));
            }
        }

        public IActionResult Teacher(int id)
        {
            return View(Repository.FindWorkloadByTeacherId(id));
        }

        public IActionResult Subject(int id)
        {
            return View(Repository.FindWorkloadBySubjectId(id));
        }
    }
}
