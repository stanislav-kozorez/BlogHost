using DAL.Interface.Entities;
using ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    class ReplaceVisitor<TFrom, TTo> : ExpressionVisitor
    {
        private readonly ParameterExpression parameter;

        public ReplaceVisitor(ParameterExpression param)
        {
            this.parameter = param;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Type == typeof(TFrom))
                return parameter;

            return base.VisitParameter(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(TFrom))
                return Expression.MakeMemberAccess(Visit(node.Expression), typeof(TTo).GetMember(node.Member.Name).FirstOrDefault());

            return base.VisitMember(node);
        }
    }

    public static class ExpressionExtension
    {
        public static Expression<Func<TTo, TResult>> Map<TFrom, TTo, TResult>(
            this Expression<Func<TFrom, TResult>> e)
        {
            var param = Expression.Parameter(typeof(TTo), e.Parameters.First().Name);
            var visitor = new ReplaceVisitor<TFrom, TTo>(param);
            var exp = visitor.Visit(e.Body);

            return Expression.Lambda<Func<TTo, TResult>>(exp, param);
        }
    }
}
