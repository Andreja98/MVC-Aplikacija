using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{

    public class HomeController : Controller
    {
        private DataContextEntities _context = new DataContextEntities();


        // GET: Home
        public ActionResult Index()
        {
            return View(_context.Proizvodis.ToList());
        }

        // GET: Home/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Proizvodi proizvod)
        {
            if (!ModelState.IsValid)
                return View();

            _context.Proizvodis.Add(proizvod);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var proizvodZaIzmenu = _context.Proizvodis.Where(proizvod => proizvod.Id == id).FirstOrDefault();

            return View(proizvodZaIzmenu);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Proizvodi proizvodZaIzmenu)
        {
            var proizvodIzBaze = _context.Proizvodis.Where(proizvod => proizvod.Id == proizvodZaIzmenu.Id).FirstOrDefault();

            if (!ModelState.IsValid)
                return View(proizvodIzBaze);

            _context.Entry(proizvodIzBaze).CurrentValues.SetValues(proizvodZaIzmenu);
            _context.SaveChanges();

            return RedirectToAction("Index");



        }

    }
}
