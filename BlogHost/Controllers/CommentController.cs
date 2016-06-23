using BLL.Interface.Entities;
using BLL.Interface.Services;
using BlogHost.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogHost.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IUserService userService;
        private readonly IArticleService articleService;

        public CommentController(ICommentService commentService, IUserService userService, IArticleService articleService)
        {
            this.commentService = commentService;
            this.userService = userService;
            this.articleService = articleService;
        }

        [ChildActionOnly]
        public ActionResult Index(int articleId, int page = 1)
        {
            int pageSize = 3;
            var model = new EntityListViewModel<CommentViewModel>();
            var comments = commentService.GetPagedComments(page, pageSize, articleId);

            var commentViewModel = comments.Select(x => new CommentViewModel()
            {
                AuthorId = x.Author.UserId,
                AuthorName = x.Author.Name,
                AuthorEmail = x.Author.Email,
                CreationDate = x.CreationDate,
                Id = x.CommentId,
                Text = x.Text
            });
            model.Items = commentViewModel;
            model.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = commentService.GetCommentCount(articleId)
            };

            ViewBag.ArticleId = articleId;

            return PartialView("CommentsPartial", model);
        }

        public ActionResult Add(CommentViewModel comment)
        {
            if(ModelState.IsValid)
            {

                var bllComment = new BllComment()
                {
                    CreationDate = DateTime.Now,
                    Text = comment.Text,
                    Author = userService.GetUserEntity(comment.AuthorEmail),
                    Article = articleService.GetArticle(comment.ArticleId)
                };

                commentService.CreateComment(bllComment);
            }
            if (Request.IsAjaxRequest())
                return Index(comment.ArticleId);
            return RedirectToAction("Details", "Article", new { id = comment.ArticleId });
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            var comment = commentService.GetComment(id);
            if (comment != null && (comment.Author.Email == User.Identity.Name || Roles.IsUserInRole("Moderator")))
            {
                var commentViewModel = new CommentViewModel() { Id = comment.CommentId, Text = comment.Text };
                return View(commentViewModel);
            }
            throw new HttpException(404, "Not found");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var comment = commentService.GetComment(id);
            if (comment != null)
                commentService.DeleteComment(comment);
            
            return RedirectToAction("Index", "Home");
        }
    }
}