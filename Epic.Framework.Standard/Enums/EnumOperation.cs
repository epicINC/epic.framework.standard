using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;
using Epic.Components;

namespace Epic.Enums
{

    internal class EnumOperation<T, K> : IEnumOperation<T, K> where T : struct, IEnumConstraint
    {

        internal EnumOperation()
        {
            var leftType = typeof(T);
            var rightType = typeof(K);
            var underlyingType = leftType.GetEnumUnderlyingType();

            var left = Expression.Parameter(leftType, "left");
            var right = Expression.Parameter(rightType, "right");
            var convertedParam1 = Expression.Convert(left, underlyingType);

            Expression convertedParam2;
            if (rightType == underlyingType)
                convertedParam2 = right;
            else
                convertedParam2 = Expression.Convert(right, underlyingType);

            this.Not = Expression.Lambda<Func<K, T>>(Expression.Convert(Expression.Not(convertedParam2), leftType), right).Compile();
            this.IsEmpty = Expression.Lambda<Func<T, bool>>(Expression.Equal(convertedParam1, Expression.Constant(Activator.CreateInstance(underlyingType))), left).Compile();

            this.Equality = Expression.Lambda<Func<T, K, bool>>(Expression.Equal(convertedParam1, convertedParam2), left, right).Compile();
            this.Or = Expression.Lambda<Func<T, K, T>>(Expression.Convert(Expression.Or(convertedParam1, convertedParam2), leftType), left, right).Compile();
            this.And = Expression.Lambda<Func<T, K, T>>(Expression.Convert(Expression.And(convertedParam1, convertedParam2), leftType), left, right).Compile();

            this.Set = this.Xor = Expression.Lambda<Func<T, K, T>>(Expression.Convert(Expression.ExclusiveOr(convertedParam1, convertedParam2), leftType), left, right).Compile();
            this.Add = this.Or;
            this.Remove = Expression.Lambda<Func<T, K, T>>(Expression.Convert(Expression.And(convertedParam1, Expression.Not(convertedParam2)), leftType), left, right).Compile();
            this.HasValue = Expression.Lambda<Func<T, K, bool>>(Expression.Equal(Expression.And(convertedParam1, convertedParam2), convertedParam2), left, right).Compile();

        }

        public Func<T, K, bool> HasValue { get; private set; }

        public Func<T, K, bool> Equality { get; private set; }

        public Func<T, K, T> Or { get; private set; }

        public Func<T, K, T> And { get; private set; }

        public Func<T, K, T> Xor { get; private set; }

        public Func<K, T> Not { get; private set; }

        public Func<T, K, T> Set { get; private set; }

        public Func<T, K, T> Add { get; private set; }

        public Func<T, K, T> Remove { get; private set; }

        public Func<T, bool> IsEmpty { get; private set; }
    }
}
