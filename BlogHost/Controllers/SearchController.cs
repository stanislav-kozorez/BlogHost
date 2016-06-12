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
            if (string.IsNullOrWhiteSpace(keyword) || User.IsInRole("Admin"))
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
                ViewBag.Keyword = keyword;
                return View("SearchResult", model);
            }
        }

        public ActionResult ByTagName(string tagName, int page = 1)
        {
            if(string.IsNullOrWhiteSpace(tagName) || User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            int pageSize = 1;

            using (var context = new BlogHostDbContext())
            {
                var ormArticles = context.Articles.OrderByDescending(x => x.CreationDate)
                    .Where(x => string.Compare(x.Tag1, tagName, true) == 0 
                    || string.Compare(x.Tag2, tagName, true) == 0
                    || string.Compare(x.Tag3, tagName, true) == 0).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                int searchResultCount = context.Articles.Where(x => string.Compare(x.Tag1, tagName, true) == 0
                    || string.Compare(x.Tag2, tagName, true) == 0
                    || string.Compare(x.Tag3, tagName, true) == 0).Count();
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
                ViewBag.TagName = tagName;
                return View("SearchResult", model);
            }
        }
    }
}