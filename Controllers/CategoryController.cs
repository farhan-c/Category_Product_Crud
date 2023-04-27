using Crud_Operation.Models;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Crud_Operation.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private CrudDbContext db = new CrudDbContext();

        [Authorize]
        public async Task<ActionResult> CategoryList(string createdby)
        {
            Session["user"] = createdby;
            return View(await db.Categories.ToListAsync());
        }
        [Authorize(Roles = "admin")]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateCategory(Category c)
        {
            db.Categories.Add(c);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }
        public async Task<ActionResult> CategoryDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            await db.SaveChangesAsync();
            return View(category);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> EditCategory(int id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound(); 
            }
            await db.SaveChangesAsync();
            return View(category);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> EditCategory(Category c)
        {
            db.Entry(c).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            await db.SaveChangesAsync();
            return View(category);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");

        }
        public async Task<ActionResult> ProductList(int id)
        {
            Session["id"] = id;
            var data = await db.Products.Where(c => c.CategoryId == id).ToListAsync();
            return View(data);
        }
        [Authorize(Roles = "admin")]
        public ActionResult CreateProduct(int id)
        {
            var model = new Product();
            model.CategoryId = id;
            model.Createdby = Session["user"] as string;
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product p)
        {
            db.Products.Add(p);
            await db.SaveChangesAsync();
            return RedirectToAction("ProductList",new {id = p.CategoryId });
        }
        public async Task<ActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> EditProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> EditProduct(Product p)
        {
            db.Entry(p).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("ProductList", new { id = p.CategoryId });
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteProduct(int? id)

        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("ProductList", new { id = product.CategoryId });
        }
        public async Task<ActionResult> Activate(int id)
        {
            //var result = await db.Database.ExecuteSqlCommandAsync("EXEC spActivateCategory  @CategoryId", new SqlParameter("@CategoryId", id));
            var category = db.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            category.IsActive = true;
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }
        //public async Task<ActionResult> Deactivate(int id)
        public  async Task<ActionResult> Deactivate(int id)
        {
            //var result = await db.Database.ExecuteSqlCommandAsync("EXEC spDeActivateCategory  @CategoryId", new SqlParameter("@CategoryId", id));
            var category = db.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            category.IsActive = false;
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }
    }
}