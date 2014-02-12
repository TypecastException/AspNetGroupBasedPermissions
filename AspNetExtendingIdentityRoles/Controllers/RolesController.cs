using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AspNetExtendingIdentityRoles.Models;
using System.Net;

namespace AspNetExtendingIdentityRoles.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var rolesList = new List<RolesViewModel>();
            foreach(var role in _db.Roles)
            {
                var roleModel = new RolesViewModel(role);
                rolesList.Add(roleModel);
            }
            return View(rolesList);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Create(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "RoleName,Description")]RolesViewModel model)
        {
            string message = "That role name has already been used";
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole(model.RoleName, model.Description);
                var idManager = new IdentityManager();

                if(idManager.RoleExists(model.RoleName))
                {
                    return View(message);
                }
                else
                {
                    idManager.CreateRole(model.RoleName, model.Description);
                    return RedirectToAction("Index", "Account");
                }
            }
            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            // It's actually the Role.Name tucked into the id param:
            var role = _db.Roles.First(r => r.Name == id);
            var roleModel = new EditRolesViewModel(role);
            return View(roleModel);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "RoleName,OriginalRoleName,Description")] EditRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _db.Roles.First(r => r.Name == model.OriginalRoleName);
                role.Name = model.RoleName;
                role.Description = model.Description;
                _db.Entry(role).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = _db.Roles.First(r => r.Name == id);
            var model = new RolesViewModel(role);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            var role = _db.Roles.First(r => r.Name == id);
            var idManager = new IdentityManager();
            idManager.DeleteRole(role.Id);
            return RedirectToAction("Index");
        }
    }
}
