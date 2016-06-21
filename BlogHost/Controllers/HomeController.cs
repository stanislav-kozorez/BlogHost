using System.Linq;
using System.Web.Mvc;
using BlogHost.Models;
using BLL.Interface.Services;

namespace BlogHost.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService articleService;

        public HomeController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public ActionResult Index(int page = 1)
        {
            int pageSize = 3;
            var model = new EntityListViewModel<ArticleViewModel>();
            var articles = articleService.GetPagedArticles(page, pageSize);
            var articleCount = articleService.GetArticleCount();

            model.Items = articles.Select(
                    x => new ArticleViewModel()
                    {
                        ArticleId = x.ArticleId,
                        AuthorId = x.Author.UserId,
                        Title = x.Title,
                        Text = x.Text,
                        CreationDate = x.CreationDate
                    });
            model.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = articleCount
            };
            return View(model);
        }
    }
}