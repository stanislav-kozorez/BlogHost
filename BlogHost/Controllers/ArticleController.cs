using BlogHost.Models;
using ORM;
using ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogHost.Controllers
{
    [Authorize(Roles = "User")]
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ForUser(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            using (var context = new BlogHostDbContext())
            {
                var articles = context.Articles.Where(x => x.Author.UserId == id.Value).OrderBy(x => x.CreationDate).ToList();
                return View(articles);
            }
        }

        [AllowAnonymous]
        public ActionResult All(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArticleViewModel model)
        { 
            if(ModelState.IsValid)
            {
                using (var context = new BlogHostDbContext())
                {
                    var user = context.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

                    if (user == null)
                        return View(model);

                    Article article = new Article();
                    article.Author = user;
                    article.CreationDate = DateTime.Now;
                    article.Title = model.Title;
                    article.Text = model.Text;
                    article.Tag1 = model.Tag1;
                    article.Tag2 = model.Tag2;
                    article.Tag3 = model.Tag3;
                    context.Articles.Add(article);
                    context.SaveChanges();
                }
                    return RedirectToAction("Index", "Account");
            }
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
                return RedirectToAction("Index", "Account");
            ArticleViewModel model = new ArticleViewModel();
            model.Title = "Titile sdfsdf";
            model.Text = "sdfsdfsdlsdfsdfsdfsjdhfgskfjh";
            return View(model);
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}