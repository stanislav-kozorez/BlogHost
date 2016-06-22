using System.Linq;
using System.Web.Mvc;
using BlogHost.Models;
using BLL.Interface.Services;
using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BlogHost.Controllers
{
    public class SearchController : Controller
    {
        private readonly IArticleService articleService;
        private const int pageSize = 3;

        public SearchController(IArticleService articleService)
        {
            this.articleService = articleService;
        }
        public ActionResult Index(string keyword, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(keyword) || User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var articles = articleService.FindByKeyword(keyword, page, pageSize);
            int searchResultCount = articleService.FindByKeywordCount(keyword);

            var model = FillSearchModel(articles, searchResultCount, page);

            ViewBag.Keyword = keyword;
            return View("SearchResult", model);
        }

        public ActionResult ByTagName(string tagName, int page = 1)
        {
            if(string.IsNullOrWhiteSpace(tagName) || User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var articles = articleService.FindByTag(tagName, page, pageSize);
            int searchResultCount = articleService.FindByTagCount(tagName);

            var model = FillSearchModel(articles, searchResultCount, page);

            ViewBag.TagName = tagName;
            return View("SearchResult", model);
        }

        private EntityListViewModel<ArticleViewModel> FillSearchModel(IEnumerable<BllArticle> articles, int searchResultCount, int page)
        {
            var model = new EntityListViewModel<ArticleViewModel>();
            model.Items = articles.Select(x => new ArticleViewModel()
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

            return model;
        }
    }
}