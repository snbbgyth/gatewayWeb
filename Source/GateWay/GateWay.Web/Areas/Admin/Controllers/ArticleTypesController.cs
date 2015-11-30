using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GateWay.DbHelper.BLL;
using GateWay.DbHelper.Model;

namespace GateWay.Web.Areas.Admin.Controllers
{
    public class ArticleTypesController : Controller
    {
        private DataContextBll db = new DataContextBll();

        // GET: Admin/ArticleTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.ArticleTypes.ToListAsync());
        }

        // GET: Admin/ArticleTypes/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleType articleType = await db.ArticleTypes.FindAsync(id);
            if (articleType == null)
            {
                return HttpNotFound();
            }
            return View(articleType);
        }

        // GET: Admin/ArticleTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ArticleTypes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name, IsDelete,Type")] ArticleType articleType)
        {
            if (ModelState.IsValid)
            {
                articleType.Id = Guid.NewGuid();
                articleType.CreateDateTime=DateTime.Now;
                db.ArticleTypes.Add(articleType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(articleType);
        }

        // GET: Admin/ArticleTypes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleType articleType = await db.ArticleTypes.FindAsync(id);
            if (articleType == null)
            {
                return HttpNotFound();
            }
            return View(articleType);
        }

        // POST: Admin/ArticleTypes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CreateDateTime,IsDelete,Type")] ArticleType articleType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articleType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(articleType);
        }

        // GET: Admin/ArticleTypes/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleType articleType = await db.ArticleTypes.FindAsync(id);
            if (articleType == null)
            {
                return HttpNotFound();
            }
            return View(articleType);
        }

        // POST: Admin/ArticleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            ArticleType articleType = await db.ArticleTypes.FindAsync(id);
            db.ArticleTypes.Remove(articleType);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
