using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payement.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payement.WEB.Controllers
{
    public class PartyController : Controller
    {
        private IPayementService service;

        public PartyController(IPayementService srv)
        {
            service = srv;
        }
        
        public IActionResult Index()
        {
            var parties = service.GetAllParties();
            return View(parties);
        }

        
        public IActionResult Create()
        {
            return View(new PartyVM());
        }

        // POST: PartyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")]PartyVM vm)
        {
            if (ModelState.IsValid)
            {
                var party = new Party(vm.Name);
                service.InsertParty(party);

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: PartyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PartyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PartyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PartyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
