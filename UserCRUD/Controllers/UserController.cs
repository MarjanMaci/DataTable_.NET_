using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UserCRUD.Models;

namespace UserCRUD.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult GetUserList()
        {
            List<Models.User> UserList = new List<Models.User>();
            if (Session["UserList"] != null) UserList = (List<Models.User>)Session["UserList"];
            else
            {
                UserList = new List<Models.User>();
                Models.User User = new Models.User();
                User.Id = 1001;
                User.FullName = "User 1aaaa";
                User.UserName = "user1001";
                User.Password = "P@$$w0rd1001";
                User.Email = "user1001@crud.mk";
                User.RegisteredAt = DateTime.Now;
                User.Comment = "...";
                UserList.Add(User);

                User = new Models.User();
                User.Id = 1002;
                User.FullName = "User 2aaaa";
                User.UserName = "user1002";
                User.Password = "P@$$w0rd1002";
                User.Email = "user1002@crud.mk";
                User.RegisteredAt = DateTime.Now;
                User.Comment = "...";
                UserList.Add(User);

                User = new Models.User();
                User.Id = 1003;
                User.FullName = "User 3aaaa";
                User.UserName = "user1003";
                User.Password = "P@$$w0rd1003";
                User.Email = "user1003@crud.mk";
                User.RegisteredAt = DateTime.Now;
                User.Comment = "...";
                UserList.Add(User);

                User = new Models.User();
                User.Id = 1004;
                User.FullName = "User 4aaaa";
                User.UserName = "user1004";
                User.Password = "P@$$w0rd1004";
                User.Email = "user1004@crud.mk";
                User.RegisteredAt = DateTime.Now;
                User.Comment = "...";
                UserList.Add(User);

                Session["UserList"] = UserList;
            }

            App_Helpers.JsonResponse jResponse = new App_Helpers.JsonResponse();
            jResponse.status = true;
            jResponse.message = "";
            jResponse.data = UserList;

            return Json(jResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUser(long Id)
        {
            App_Helpers.JsonResponse jResponse = new App_Helpers.JsonResponse();

            List<Models.User> UserList = new List<Models.User>();
            if (Session["UserList"] != null) UserList = (List<Models.User>)Session["UserList"];

            Models.User User = UserList.FirstOrDefault(x => x.Id == Id);

            if (User == null)
            {
                User = new Models.User();
                jResponse.status = false;
                jResponse.message = "User not found";
            }
            else
            {
                jResponse.status = true;
                jResponse.message = "OK";
            }

            jResponse.data = User;

            return Json(jResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertUser(User userce)
        {
            App_Helpers.JsonResponse jResponse = new App_Helpers.JsonResponse();

            List<Models.User> UserList = new List<Models.User>();
            if (Session["UserList"] != null) UserList = (List<Models.User>)Session["UserList"];

            Models.User _User = UserList.FirstOrDefault(x => x.Id == userce.Id);

            if (_User == null)
            {
                _User = new Models.User();
                _User.Id = UserList.LastOrDefault().Id + 1;
                _User.FullName = userce.FullName;
                _User.UserName = userce.UserName;
                _User.Password = userce.Password;
                _User.Email = userce.Email;
                _User.RegisteredAt = DateTime.Now;
                _User.Comment = userce.Comment;

                UserList.Add(_User);
                Session["UserList"] = UserList;

                jResponse.status = true;
                jResponse.message = "OK";
            }
            else
            {
                _User = new Models.User();
                jResponse.status = false;
                jResponse.message = "Error on insert";
            }

            jResponse.data = _User;

            return Json(jResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateUser(User userce)
        {
            App_Helpers.JsonResponse jResponse = new App_Helpers.JsonResponse();

            List<Models.User> UserList = new List<Models.User>();
            if (Session["UserList"] != null) UserList = (List<Models.User>)Session["UserList"];

            Models.User _User = UserList.FirstOrDefault(x => x.Id == userce.Id);

            if (_User != null)
            {
              _User.FullName = userce.FullName;
              _User.UserName = userce.UserName;
              _User.Password = userce.Password;
              _User.Email = userce.Email;
              _User.Comment = userce.Comment;

                Session["UserList"] = UserList;

                jResponse.status = true;
                jResponse.message = "OK";
            }
            else
            {
                _User = new Models.User();
                jResponse.status = false;
                jResponse.message = "Error on update";
            }

            jResponse.data = _User;

            return Json(jResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUser(long Id)
        {
            App_Helpers.JsonResponse jResponse = new App_Helpers.JsonResponse();

            List<Models.User> UserList = new List<Models.User>();
            if (Session["UserList"] != null) UserList = (List<Models.User>)Session["UserList"];

            Models.User User = UserList.FirstOrDefault(x => x.Id == Id);

            if (User == null)
            {
                User = new Models.User();
                jResponse.status = false;
                jResponse.message = "Error on delete";
            }
            else
            {
                UserList.Remove(User);
                Session["UserList"] = UserList;

                jResponse.status = true;
                jResponse.message = "OK";
            }

            jResponse.data = User;

            return Json(jResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLastUsers()
        {
            App_Helpers.JsonResponse jResponse = new App_Helpers.JsonResponse();

            List<Models.User> UserList = new List<Models.User>();
            if (Session["UserList"] != null) UserList = (List<Models.User>)Session["UserList"];

            List<Models.User> UserListTen = UserList.OrderByDescending(x => x.RegisteredAt).Take(10).ToList();

            if (UserListTen == null)
            {
                jResponse.status = false;
                jResponse.message = "User not found";
            }
            else
            {
                jResponse.status = true;
                jResponse.message = "OK";
            }

            jResponse.data = UserListTen;

            return Json(jResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult FilterUsers(trans t)
        {
            App_Helpers.JsonResponse jResponse = new App_Helpers.JsonResponse();

            List<Models.User> UserList = new List<Models.User>();
            if (Session["UserList"] != null) UserList = (List<Models.User>)Session["UserList"];

            DateTime parsedDateOdKoga = DateTime.Parse(t.odKoga);
            DateTime parsedDateDoKoga = DateTime.Parse(t.doKoga);

            Console.WriteLine(parsedDateOdKoga);
            Console.WriteLine(parsedDateDoKoga);

            List<Models.User> filteredList = new List<User>();

            foreach (User u in UserList)
            {
                if(u.RegisteredAt>=parsedDateOdKoga && u.RegisteredAt <= parsedDateDoKoga)
                {
                    filteredList.Add(u);
                }
            }


            if (filteredList == null)
            {
                jResponse.status = false;
                jResponse.message = "User not found";
            }
            else
            {
                jResponse.status = true;
                jResponse.message = "OK";
            }

            jResponse.data = filteredList;

            return Json(jResponse, JsonRequestBehavior.AllowGet);
        }
    }
}
