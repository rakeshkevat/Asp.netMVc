using MyApp.Db;
using MyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Management.Controllers
{
    public class PageController : Controller
    {
        private EmpManagementEntities db = new EmpManagementEntities();
        // GET: Page
        public ActionResult Index()
        {

            List<Page> page = db.Pages.Where(x=>x.IsActive == true).ToList();
            List<PageViewModel> pageViewModels = new List<PageViewModel>();
            foreach (var item in page)
            {
                pageViewModels.Add(new PageViewModel()
                {
                    PageID = item.PageID,
                    PageName = item.PageName,
                    PageUrl = item.PageUrl,
                    IsActive = item.IsActive
                });
            }    
            return View(pageViewModels);
        }    

 

        public ActionResult Create()
        {

            return View();
        }
         
        [HttpPost]
        public ActionResult Create( PageViewModel  pageViewModel)
        {
            Page page = new Page();
            page.PageID = pageViewModel.PageID;
            page.PageName = pageViewModel.PageName;
            page.PageUrl = pageViewModel.PageUrl;
            page.CreateDate = DateTime.Now;
            db.Pages.Add(page);
            db.SaveChanges();            
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Page page = db.Pages.Find(id);
            PageViewModel pageViewModel = new PageViewModel();
            pageViewModel.PageID = page.PageID;
            pageViewModel.PageName = page.PageName;
            pageViewModel.PageUrl = page.PageUrl;
            pageViewModel.IsActive = page.IsActive;
            return View(pageViewModel);
        }

        [HttpPost]
        public ActionResult Edit(PageViewModel pageViewModel)
        {
            Page page = db.Pages.Find(pageViewModel.PageID);
            page.PageName = pageViewModel.PageName;
            page.PageUrl = pageViewModel.PageUrl;
            page.IsActive = pageViewModel.IsActive;
            page.UpdateDate = pageViewModel.UpdateDate;
            db.Entry(page).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            Page page = db.Pages.Find(id);
            PageViewModel pageViewModel = new PageViewModel();
            pageViewModel.PageID = page.PageID;
            pageViewModel.PageName = page.PageName;
            pageViewModel.PageUrl = page.PageUrl;
            pageViewModel.IsActive = page.IsActive;
            return View(pageViewModel);
        }

        public ActionResult Delete(int id)
        {
            Page page = db.Pages.Find(id);
            db.Pages.Remove(page);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}