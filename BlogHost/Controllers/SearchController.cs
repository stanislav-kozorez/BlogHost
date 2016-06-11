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
    public class SearchController : Controller
    {
        public ActionResult Index(string keyword, int page = 1)
        {
            if (string.IsNullOrEmpty(keyword))
                return RedirectToAction("Index", "Home");

            int pageSize = 1;

            using (var context = new BlogHostDbContext())
            {
                var ormArticles = context.Articles.OrderByDescending(x => x.CreationDate).Where(x => x.Title.Contains(keyword) || x.Text.Contains(keyword)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                int searchResultCount = context.Articles.Where(x => x.Title.Contains(keyword) || x.Text.Contains(keyword)).Count();
                var model = new EntityListViewModel<ArticleViewModel>();
                model.Items = ormArticles.Select(x => new ArticleViewModel()
                {
                    ArticleId = x.ArticleId,
                    CreationDate = x.CreationDate,
                    Title = x.Title,
                    Text = x.Text
                });
                model.PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = searchResultCount
                };
                return View("SearchResult", model);
            }
        }
    }
}