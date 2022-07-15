using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Cineplex.Domain.Domain;
using Cineplex.Service.Interface;

namespace CIneplex.Web.Controllers
{
    public class ActhorsController : Controller
    {
        private readonly IActhorsService _achtorsService;

        public ActhorsController(IActhorsService achtorsService)
        {
            _achtorsService = achtorsService;
        }

        // GET: Acthors
        public IActionResult Index()
        {
            return View(_achtorsService.getAll());
        }

        // GET: Acthors/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acthor = _achtorsService.GetDetails(id);
            if (acthor == null)
            {
                return NotFound();
            }

            return View(acthor);
        }

        // GET: Acthors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Acthors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("first_name,last_name,id")] Acthor acthor)
        {
            if (ModelState.IsValid)
            {
                _achtorsService.CreateNew(acthor);
                return RedirectToAction(nameof(Index));
            }
            return View(acthor);
        }

        // GET: Acthors/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acthor = _achtorsService.GetDetails(id);
            if (acthor == null)
            {
                return NotFound();
            }
            return View(acthor);
        }

        // POST: Acthors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("first_name,last_name,id")] Acthor acthor)
        {
            if (id != acthor.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   _achtorsService.Update(acthor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActhorExists(acthor.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(acthor);
        }

        // GET: Acthors/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acthor = _achtorsService.GetDetails(id);
            if (acthor == null)
            {
                return NotFound();
            }

            return View(acthor);
        }

        // POST: Acthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _achtorsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ActhorExists(Guid id)
        {
            return _achtorsService.GetDetails(id) != null;
        }
    }
}
