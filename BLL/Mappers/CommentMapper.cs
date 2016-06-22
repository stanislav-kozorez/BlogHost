using BLL.Interface.Entities;
using DAL.Interface.Entities;

namespace BLL.Mappers
{
    public static class CommentMapper
    {
        public static DalComment ToDalComment(this BllComment comment)
        {
            return new DalComment()
            {
                Id = comment.CommentId,
                CreationDate = comment.CreationDate,
                Text = comment.Text,
                Author = comment.Author?.ToDalUser()
            };
        }

        public static BllComment ToBllComment(this DalComment comment)
        {
            return new BllComment()
            {
                CommentId = comment.Id,
                CreationDate = comment.CreationDate,
                Text = comment.Text,
                Author = comment.Author?.ToBllUser()
            };
        }
    }
}
