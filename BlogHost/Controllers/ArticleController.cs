using BLL.Interface.Entities;
using BLL.Interface.Services;
using BlogHost.Models;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogHost.Controllers
{
    [Authorize(Roles = "User,Moderator")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IUserService userService;

        public ArticleController(IArticleService articleService, IUserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ForUser(int id, int page = 1)
        {
            int pageSize = 3;
            var model = new EntityListViewModel<ArticleViewModel>();
            var articles = articleService.GetPagedArticles(page, pageSize, id);
            var articleViewModel = articles.Select(x => new ArticleViewModel()
            {
                ArticleId = x.ArticleId,
                CreationDate = x.CreationDate,
                Title = x.Title,
                Text = x.Text
            });
            model.Items = articleViewModel;
            model.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = articleService.GetArticleCount(id)
            };
            TempData.Add("PagingInfo", model.PagingInfo);
            var user = userService.GetUserEntity(id);
            if (user != null)
                ViewBag.UserEmail = user.Email;
            return PartialView("ArticleListPartial", model);
    }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Details(int id, int page = 1)
        {
            var article = articleService.GetArticle(id);
            if (article != null)
            {
                var loggedUser = userService.GetUserEntity(User.Identity.Name);
                ViewBag.LoggedUserId = loggedUser?.UserId;
                ViewBag.CurrentPage = page;

                return View(article);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Create(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userService.GetUserEntity(User.Identity.Name);
                if (user == null)
                    throw new HttpException(500, "Server error");

                BllArticle article = new BllArticle();
                article.Author = user;
                article.CreationDate = DateTime.Now;
                article.Title = model.Title;
                article.Text = EncodeArticleText(model.Text);
                article.Tag1 = model.Tag1;
                article.Tag2 = model.Tag2;
                article.Tag3 = model.Tag3;

                articleService.CreateArticle(article);

                return RedirectToAction("Index", "Account");
            }
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                throw new HttpException(404, "Not found");

            var article = articleService.GetArticle(id.Value);
            if (article != null && (article.Author.Email == User.Identity.Name || Roles.IsUserInRole("Moderator")))
            {
                var articleViewModel = new ArticleViewModel()
                {
                    ArticleId = article.ArticleId,
                    Tag1 = article.Tag1,
                    Tag2 = article.Tag2,
                    Tag3 = article.Tag3,
                    Title = article.Title,
                    Text = article.Text
                };
                return View(articleViewModel);
            }
           
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public ActionResult Edit(ArticleViewModel articleViewModel)
        {
            if (ModelState.IsValid)
            {
                var article = new BllArticle()
                {
                    ArticleId = articleViewModel.ArticleId,
                    Tag1 = articleViewModel.Tag1,
                    Tag2 = articleViewModel.Tag2,
                    Tag3 = articleViewModel.Tag3,
                    Title = articleViewModel.Title,
                    Text = EncodeArticleText(articleViewModel.Text)
                };
                
                articleService.UpdateArticle(article);
                return RedirectToAction("Index", "Account");
            }

            return View(articleViewModel);
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            var article = articleService.GetArticle(id);
            if (article != null && (article.Author.Email == User.Identity.Name || Roles.IsUserInRole("Moderator")))
            {
                var articleViewModel = new ArticleViewModel() { Title = article.Title, ArticleId = article.ArticleId };
                return View(articleViewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Delete(int articleId)
        {
            articleService.DeleteArticle(new BllArticle() { ArticleId = articleId });
            return RedirectToAction("Index", "Account");
        }

        private string EncodeArticleText(string text)
        {
            StringBuilder sb = new StringBuilder(HttpUtility.HtmlEncode(text));
            //<b>,<i>,<br>
            sb.Replace("&lt;b&gt;", "<b>");
            sb.Replace("&lt;/b&gt;", "</b>");
            sb.Replace("&lt;i&gt;", "<i>");
            sb.Replace("&lt;/i&gt;", "</i>");
            sb.Replace("&lt;br&gt;", "<br>");
            //sb.Replace("&lt;pre&gt;", "<pre>");
            //sb.Replace("&lt;/pre&gt;", "</pre>");
            return sb.ToString();
        }
    }
}