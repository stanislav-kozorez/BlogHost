using BlogHost.Models;
using ORM;
using ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogHost.Controllers
{
    [Authorize(Roles = "User,Moderator")]
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
                var email = context.Users.Where(x => x.UserId == id.Value).FirstOrDefault().Email;
                if (email != null)
                    ViewBag.UserEmail = email;
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

        public ActionResult Details(int id)
        {
            using(var context = new BlogHostDbContext())
            {
                var ormArticle = context.Articles.Find(id);
                if(ormArticle != null)
                {
                    var articleViewModel = new ArticleViewModel();
                    articleViewModel.ArticleId = ormArticle.ArticleId;
                    articleViewModel.AuthorId = ormArticle.Author.UserId;
                    articleViewModel.Tag1 = ormArticle.Tag1;
                    articleViewModel.Tag2 = ormArticle.Tag2;
                    articleViewModel.Tag3 = ormArticle.Tag3;
                    articleViewModel.Title = ormArticle.Title;
                    articleViewModel.Text = ormArticle.Text;
                    articleViewModel.CreationDate = ormArticle.CreationDate;
                    var loggedUser = context.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
                    ViewBag.LoggedUserId = loggedUser.UserId;

                    return View(articleViewModel);
                }
            }
            return RedirectToAction("Index", "Home");
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
            using (var context = new BlogHostDbContext())
            {
                var ormArticle = context.Articles.Find(id.Value);
                if(ormArticle != null && (ormArticle.Author.Email == User.Identity.Name || Roles.IsUserInRole("Moderator")))
                {
                    ArticleViewModel model = new ArticleViewModel();
                    model.ArticleId = ormArticle.ArticleId;
                    model.Tag1 = ormArticle.Tag1;
                    model.Tag2 = ormArticle.Tag2;
                    model.Tag3 = ormArticle.Tag3;
                    model.Title = ormArticle.Title;
                    model.Text = ormArticle.Text;
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public ActionResult Edit(ArticleViewModel articleViewModel)
        {
            if (ModelState.IsValid)
            {
                using (var context = new BlogHostDbContext())
                {
                    var ormArticle = context.Articles.Find(articleViewModel.ArticleId);
                    if(ormArticle != null)
                    {
                        ormArticle.Tag1 = articleViewModel.Tag1;
                        ormArticle.Tag2 = articleViewModel.Tag2;
                        ormArticle.Tag3 = articleViewModel.Tag3;
                        ormArticle.Title = articleViewModel.Title;
                        ormArticle.Text = articleViewModel.Text;
                        context.SaveChanges();
                        return RedirectToAction("Index", "Account");
                    }
                }
            }

            return View(articleViewModel);
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            using (var context = new BlogHostDbContext())
            {
                var ormArticle = context.Articles.Find(id);
                if (ormArticle != null && (ormArticle.Author.Email == User.Identity.Name || Roles.IsUserInRole("Moderator")))
                {
                    var articleViewModel = new ArticleViewModel() { Title = ormArticle.Title, ArticleId = ormArticle.ArticleId };
                    return View(articleViewModel);
                }

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Delete(int articleId)
        {
            using (var context = new BlogHostDbContext())
            {
                var article = context.Articles.Find(articleId);
                if (article != null)
                    context.Articles.Remove(article);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
    }
}