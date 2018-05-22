using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Epic.Extensions;

namespace Epic.FluentAPI
{
    /// <summary>
    /// 表达式扩展方法
    /// </summary>
    public static class FluentAPIExtensions
    {
        


        public static PropertyPath GetComplexPropertyAccess(this LambdaExpression propertyAccessExpression)
        {
            var path = propertyAccessExpression.Parameters.Single<ParameterExpression>().MatchComplexPropertyAccess(propertyAccessExpression.Body);
            if (path == null) Errors.InvalidComplexPropertyExpression(propertyAccessExpression);
            return path;
        }

        internal static PropertyPath MatchComplexPropertyAccess(this Expression parameterExpression, Expression propertyAccessExpression)
        {
            var source = parameterExpression.MatchPropertyAccess(propertyAccessExpression);
            if (!source.Any<PropertyInfo>()) return null;
            return source;
        }

        internal static PropertyPath MatchPropertyAccess(this Expression parameterExpression, Expression propertyAccessExpression)
        {
            MemberExpression expression;
            PropertyInfo member;

            List<PropertyInfo> result = new List<PropertyInfo>();
            do
            {
                expression = propertyAccessExpression.RemoveConvert() as MemberExpression;
                if (expression == null) return null;

                member = expression.Member as PropertyInfo;
                if (member == null) return null;

                result.Add(member);
                propertyAccessExpression = expression.Expression;
            }
            while (propertyAccessExpression != parameterExpression);
            result.Reverse();
            return new PropertyPath(result);
        }



        public static Expression RemoveConvert(this Expression expression)
        {
            while ((expression != null) && ((expression.NodeType == ExpressionType.Convert) || (expression.NodeType == ExpressionType.ConvertChecked)))
            {
                expression = ((UnaryExpression)expression).Operand.RemoveConvert();
            }
            return expression;
        }

        public static IEnumerable<PropertyPath> GetSimplePropertyAccessList(this LambdaExpression propertyAccessExpression)
        {
            var enumerable = propertyAccessExpression.MatchPropertyAccessList((Expression p, Expression e) => e.MatchSimplePropertyAccess(p));
            if (enumerable == null) Errors.InvalidPropertiesExpression(propertyAccessExpression);
            return enumerable;
        }

        static PropertyPath MatchSimplePropertyAccess(this Expression parameterExpression, Expression propertyAccessExpression)
        {
            var result = parameterExpression.MatchPropertyAccess(propertyAccessExpression);
            if (result.Count != 1)
                return null;
            return result;
        }


        static IEnumerable<PropertyPath> MatchPropertyAccessList(this LambdaExpression lambdaExpression, Func<Expression, Expression, PropertyPath> propertyMatcher)
        {
            NewExpression newExpression = lambdaExpression.Body.RemoveConvert() as NewExpression;
            if (newExpression != null)
            {
                var parameterExpression = lambdaExpression.Parameters.Single<ParameterExpression>();
                IEnumerable<PropertyPath> enumerable =
                    from a in newExpression.Arguments
                    select propertyMatcher(a, parameterExpression) into p
                    where p != null
                    select p;
                if (enumerable.Count() == newExpression.Arguments.Count)
                {
                    if (!newExpression.HasDefaultMembersOnly(enumerable))
                        return null;
                    return enumerable;
                }
            }
            var propertyPath = propertyMatcher(lambdaExpression.Body, lambdaExpression.Parameters.Single<ParameterExpression>());
            if (propertyPath == null)
                return null;

            return propertyPath.Cast<PropertyPath>();
        }


        static bool HasDefaultMembersOnly(this NewExpression newExpression, IEnumerable<PropertyPath> propertyInfos)
        {
            return !newExpression.Members.Where((MemberInfo t, int i) => !String.Equals(t.Name, propertyInfos.ElementAt(i).Last<PropertyInfo>().Name, StringComparison.Ordinal)).Any<MemberInfo>();
        }


    }
}
