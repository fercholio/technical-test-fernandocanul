using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using techical.test.Data.Contexts;
using techical.test.Data.Infrastructure;
using techical.test.Services;
using technical.test.Model;
using System.Data.Entity;
using Fcanul.TechnicalTest.Web;
using Fcanul.TechnicalTest.Models;
using techical.test.Utils;

namespace Fcanul.TechnicalTest.Controllers
{
    [TechnicalAutorize]
    public class UserController : BaseController
    {
        private TechnicalTestContext db = new TechnicalTestContext();
        private IUserService _userService;

        public UserController()
        {
            var dbf = new DatabaseFactory();
            _userService = new UserService(dbf);
        }
        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Session).Include(u => u.UserData);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Sessions, "Id", "Token");
            ViewBag.Id = new SelectList(db.UserData, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(/*[Bind(Include = "Id,FirstName,LastName,Username,Password,Role,Privilege,Enabled")] */UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                #region Validate duplicated
                bool duplicated = db.Users.Any(u => u.Username == user.Username && u.Enabled) || db.Users.Any(u => u.UserData.Email == user.Email && u.Enabled);
                if (duplicated)
                {
                    Error("A user with the same Username or Email already exists");
                    return View(user);
                }
                #endregion
                User _user = new technical.test.Model.User();
                _user.Created = DateTime.Now;
                _user.Enabled = true;
                _user.FirstName = user.FirstName;
                _user.LastName = user.LastName;
                _user.Username = user.Username;
                _user.Password = PasswordGenerator.EncryptPassword(user.Password);
                _user.Role = (user.Role != Role.ApplicationUser && user.Role != Role.Admin) ? Role.ApplicationUser : user.Role;
                _user.UserData = new UserData { BirthDate = DateTime.Now, Gender = user.Gender, Email = user.Email };
                int success = await _userService.Create(_user);
                if (success > 0)
                {
                    Success("User created successfully");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error on create user", "A error has ocurred when a user was created");
                    return View(user);
                }
            }

            ViewBag.Id = new SelectList(db.Sessions, "Id", "Token", user.Id);
            ViewBag.Id = new SelectList(db.UserData, "Id", "Id", user.Id);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            UserViewModel userModel = new UserViewModel();

            userModel.BirthDate = user.UserData.BirthDate;
            userModel.Created = user.Created;
            userModel.Email = user.UserData.Email;
            userModel.Enabled = user.Enabled;
            userModel.FirstName = user.FirstName;
            userModel.Gender = user.UserData.Gender;
            userModel.LastName = user.LastName;
            userModel.Password = "";
            userModel.PasswordConfirmation = "";
            userModel.Role = user.Role;
            userModel.Username = user.Username;

            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Sessions, "Id", "Token", user.Id);
            ViewBag.Id = new SelectList(db.UserData, "Id", "Id", user.Id);
            return View(userModel);
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(/*[Bind(Include = "Id,FirstName,LastName,Username,Password,Role,Privilege,Enabled")] */UserViewModel user)
        {
            var old = await _userService.Get(user.Id);

            if (old == null)
            {
                Error("User is invalid or doesn´t exists");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    #region Validate duplicated
                    bool duplicated = db.Users.Any(u => u.Id != user.Id && u.Username == user.Username && u.Enabled) || db.Users.Any(u => u.Id != user.Id && u.UserData.Email == user.Email && u.Enabled);
                    if (duplicated)
                    {
                        Error("A user with the same Username or Email already exists");
                        return View(user);
                    }
                    #endregion

                    old.Created = user.Created;
                    old.Enabled = user.Enabled;
                    old.FirstName = user.FirstName;
                    old.LastName = user.LastName;
                    old.Username = user.Username;
                    old.Password = PasswordGenerator.EncryptPassword(user.Password);
                    old.Role = (user.Role != Role.ApplicationUser && user.Role != Role.Admin) ? Role.ApplicationUser : user.Role;
                    old.UserData.Email = user.Email;

                    int success = await _userService.Update(old);
                    if (success > 0)
                    {
                        Success("User updated successfully");
                        return RedirectToAction("Index");
                    }
                    else { throw new Exception("Cannot update user at this time"); }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message, ex.StackTrace);
                    Error("An error ocurred while updating the entries. Try again later.");
                    return View(user);
                }
            }
            ViewBag.Id = new SelectList(db.Sessions, "Id", "Token", user.Id);
            ViewBag.Id = new SelectList(db.UserData, "Id", "Id", user.Id);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var old = await _userService.Get(id);

            if (old == null)
            {
                Error("User is invalid or doesn´t exists");
                return View(old);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    old.Enabled = false;
                    old.PasswordConfirmation = old.Password;
                    //old.Session = new Model.Session() { Token = "" };

                    int success = await _userService.Update(old);
                    if (success > 0)
                    {
                        Success("User updated successfully");
                        return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else { throw new Exception("Cannot update user at this time"); }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message, ex.StackTrace);
                    throw new Exception("User cannot be deleted");
                }
            }
            ViewBag.Id = new SelectList(db.Sessions, "Id", "Token", old.Id);
            ViewBag.Id = new SelectList(db.UserData, "Id", "Id", old.Id);
            return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);                       
        }
    }
}