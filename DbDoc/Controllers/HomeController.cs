using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DbDoc.Models;
using DbDocGen;
using System.Text;

namespace DbDoc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Context _context;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Db> dbs = _context.Dbs.Where(x => true).ToList();

            ViewData.Model = dbs;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Get(Guid id)
        {
            var db = _context.Dbs.FirstOrDefault(x => x.ID == id);
            if (db == null) return NoContent();
            return Ok(db);
        }
        public IActionResult Add(Db model)
        {

            model.ID = Guid.NewGuid();


            _context.Dbs.Add(model);

            _context.SaveChanges();

            return Ok(model);
        }

        public IActionResult Gen(Db model)
        {
            model = _context.Dbs.First(x => x.ID == model.ID);

            if (string.IsNullOrEmpty(model.ConnectionString))
            {
                throw new ApplicationException("链接字符串不能为空！");
            }

            var dbg = new DbDocGenerator(model.ConnectionString);

            model.Html = dbg.ExportToHtml();

            _context.SaveChanges();

            return Ok(model);
        }

        public IActionResult Show(Guid id)
        {
            var model = _context.Dbs.First(x => x.ID == id);

            return Content(model.Html, "text/html", Encoding.UTF8);
        }

        public IActionResult Update(Db model)
        {
            Db exist = _context.Dbs.First(x => x.ID == model.ID);

            exist.Name = model.Name;
            exist.Desc = model.Desc;

            if (!string.IsNullOrEmpty(model.ConnectionString))
            {
                exist.ConnectionString = model.ConnectionString;
            }

            _context.SaveChanges();

            return Ok(model);
        }

    }
}
