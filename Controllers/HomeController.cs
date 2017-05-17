using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace WebApplication
{
    public class HomeController : Controller
    {
        private EntityModelContext _context=null;
        public HomeController(EntityModelContext context)
        {
            _context = context;
        }

        [Route("home/index")]
        public IActionResult Index()
        {
            List<TestTable> data = null;
            data = _context.TestTables.ToList();
            return View(data);
        }
        
        [Route("home/edit/{id?}")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            TestTable data = new TestTable();
            if(id.HasValue)
            {
                data = _context.TestTables.Where(p=>p.Id==id.Value).FirstOrDefault();
                if(data==null)
                {
                    data = new TestTable();
                }
                return View("Edit",data);
            }
            return View("Edit",data);
        }

        [Route("home/post")]
        [HttpPost]
        public IActionResult Post(TestTable test)
        {
            if(test != null)
            {
                bool isNew = false;
                var data = _context.TestTables.Where(p=>p.Id==test.Id).FirstOrDefault();
                if(data == null)
                {
                    data = new TestTable();
                    isNew = true;
                }
                data.Name = test.Name;
                data.Description = test.Description;
                if(isNew)
                {
                    _context.Add(data);
                }
                _context.SaveChanges();
            }
           return RedirectToAction("Index");
        }
        [Route("home/delete/{id}")]
        [HttpGet]  
        public IActionResult Delete(int id)  
        {  
            TestTable data = _context.Set<TestTable>().FirstOrDefault(c => c.Id == id);  
            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;  
            _context.SaveChanges();  
            return RedirectToAction("Index");  
        }  

    }
}