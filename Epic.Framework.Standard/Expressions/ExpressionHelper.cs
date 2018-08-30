using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Epic.Expressions
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyExpression"></param>
        /// <example>e => e.ID</example>
        /// <returns>default null</returns>
        public static PropertyInfo Property(LambdaExpression value)
        {
            if (value == null) return null;
            var expression = RemoveConvert(value) as MemberExpression;
            if (expression == null) return null;
            return expression.Member as PropertyInfo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string PropertyName(LambdaExpression value)
        {
            return Property(value)?.Name;
        }



        public static Expression RemoveConvert(Expression expression)
        {
            while ((expression != null) && ((expression.NodeType == ExpressionType.Convert) || (expression.NodeType == ExpressionType.ConvertChecked)))
            {
                expression = RemoveConvert(((UnaryExpression)expression).Operand);
            }
            return expression;
        }

    }
}
