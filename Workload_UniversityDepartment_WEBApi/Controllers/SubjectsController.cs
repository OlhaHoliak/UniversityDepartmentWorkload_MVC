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
    public class SubjectsController : Controller
    {
        private readonly ILogger<SubjectsController> _logger;
        private readonly IRepository<Subject> Repository;

        public SubjectsController(ILogger<SubjectsController> logger, IRepository<Subject> repository)
        {
            _logger = logger;
            Repository = repository;
        }

        [Route("Subjects")]
        public IActionResult Index()
        {
            return View(Repository.All);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Subject subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Repository.Add(subject);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(subject);
        }

        #region Read-view is not implemented (we don't need it)
        //[HttpGet]
        //public IActionResult Read(int id)
        //{
        //    Subject subject = Repository.FindById(id);
        //    if (subject == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(subject);
        //} 
        #endregion

        [HttpGet]
        public IActionResult Update(int id)
        {
            Subject subject;
            try
            {
                subject = Repository.FindById(id);
                if (subject == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound();
            }
            return View(subject);
        }

        [HttpPost]
        public IActionResult Update([FromForm] Subject sbject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Repository.Update(sbject);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                    return View(sbject);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sbject);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Subject subject;
            try
            {
                subject = Repository.FindById(id);
                if (subject == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound();
            }
            return View(subject);
        }

        [HttpPost]
        public IActionResult Delete([FromForm] Subject subject)
        {
            try
            {
                Repository.Delete(subject);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(subject);
            }
        }
    }
}
