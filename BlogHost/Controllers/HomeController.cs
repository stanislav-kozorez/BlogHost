using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ORM;
using ORM.Entity;
using BlogHost.Models;

namespace BlogHost.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int page = 1)
        {
            using (var context = new BlogHostDbContext())
            {
                int pageSize = 5;
                var model = new EntityListViewModel<ArticleViewModel>();
                var ormArticles = context.Articles.Include("Author").OrderBy(x => x.CreationDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                model.Items = ormArticles.Select(
                    x => new ArticleViewModel() {
                        ArticleId = x.ArticleId,
                        AuthorId = x.Author.UserId,
                        Title = x.Title,
                        Text = x.Text,
                        CreationDate = x.CreationDate });
                model.PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = context.Articles.Count()
                };
                return View(model);
            }
        }
    }
}