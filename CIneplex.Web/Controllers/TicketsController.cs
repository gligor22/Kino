using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cineplex.Domain.Domain;
using Cineplex.Domain.DTO;
using System.Security.Claims;
using Cineplex.Service.Interface;

namespace CIneplex.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IMovieService _movieService;

        public TicketsController(ITicketService ticketService, IMovieService movieService)
        {
            _movieService = movieService;
            _ticketService = ticketService;
        }

        // GET: Tickets
        public IActionResult Index()
        {
            var allTickets = _ticketService.getAllTickets();
            return View(allTickets);
        }

        // GET: Tickets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetDetailsForTickets(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["MovieID"] = new SelectList(_movieService.getAll(), "id", "Title");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Price,DateCreated,ValidTo,MovieID,id")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _ticketService.CreateNewTicket(ticket); 
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieID"] = new SelectList(_movieService.getAll(), "id", "Title", ticket.MovieID);
            return View(ticket);
        }

        //Get: AddToCart
        [HttpGet]
        public IActionResult AddToCart(Guid? id)
        {
            var model = _ticketService.GetShoppingCartInfo(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCartP(Guid? ticketID, TicketShoppingCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._ticketService.AddToShoppingCart(model, userId);

            if(result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    // GET: Tickets/Edit/5
    public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetDetailsForTickets(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["MovieID"] = new SelectList(_movieService.getAll(), "id", "Title", ticket.MovieID);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Price,DateCreated,ValidTo,MovieID,id")] Ticket ticket)
        {
            if (id != ticket.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ticketService.UpdateTicket(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.id))
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
            ViewData["MovieID"] = new SelectList(_movieService.getAll(), "id", "Title", ticket.MovieID);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket =_ticketService.GetDetailsForTickets(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _ticketService.DeleteTickeet(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            Ticket t = _ticketService.GetDetailsForTickets(id);
            return t != null;
        }
    }
}
