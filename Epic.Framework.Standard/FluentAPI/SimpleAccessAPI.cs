using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Epic.FluentAPI
{
    public static class SimpleAccess
    {
        public static MemberInfo Find(this LambdaExpression value)
        {


            switch (value.Body.NodeType)
            {
                case ExpressionType.MemberAccess :
                    return Property(value);
                case ExpressionType.Call :
                    return Method(value);
                default:
                    throw new ArgumentOutOfRangeException("输入表达式类型不正确!");
            }
        }



        public static PropertyInfo Property(this LambdaExpression value)
        {
            return MatchPropertyAccess(value.Body);
        }


        public static MethodInfo Method(this LambdaExpression value)
        {
            return MatchMethodCall(value.Body);
        }

        static MethodInfo MatchMethodCall(this Expression value)
        {
            var expression = value.RemoveConvert() as MethodCallExpression;
            if (expression == null) return null;

            return expression.Method;
        }



        static PropertyInfo MatchPropertyAccess(this Expression value)
        {
            var expression = value.RemoveConvert() as MemberExpression;
            if (expression == null) return null;

            return expression.Member as PropertyInfo;
        }


    }
}
