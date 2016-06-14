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
    public class CommentController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index(int articleId, int page = 1)
        {
            using (var context = new BlogHostDbContext())
            {
                int pageSize = 3;
                var model = new EntityListViewModel<CommentViewModel>();
                var ormComments = context.Comments.Include("Author").OrderByDescending(x => x.CreationDate).Where(x => x.Article.ArticleId == articleId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                var commentViewModel = ormComments.Select(x => new CommentViewModel()
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
                    TotalItems = context.Comments.Where(x => x.Article.ArticleId == articleId).Count()
                };

                ViewBag.ArticleId = articleId;
                
                return PartialView("CommentsPartial", model);
            }
        }

        public ActionResult Add(CommentViewModel comment)
        {
            if(ModelState.IsValid)
            {
                using (var context = new BlogHostDbContext())
                {
                    var ormComment = new Comment()
                    {
                        CreationDate = DateTime.Now,
                        Text = comment.Text,
                        Author = context.Users.Where(x => x.Email == comment.AuthorEmail).FirstOrDefault(),
                        Article = context.Articles.Find(comment.ArticleId)
                    };
                    context.Comments.Add(ormComment);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            using (var context = new BlogHostDbContext())
            {
                var ormComment = context.Comments.Find(id);
                if (ormComment != null && (ormComment.Author.Email == User.Identity.Name || Roles.IsUserInRole("Moderator")))
                {
                    var commentViewModel = new CommentViewModel() { Id = ormComment.CommentId, Text = ormComment.Text };
                    return View(commentViewModel);
                }

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var context = new BlogHostDbContext())
            {
                var ormComment = context.Comments.Find(id);
                if (ormComment != null)
                    context.Comments.Remove(ormComment);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
    }
}