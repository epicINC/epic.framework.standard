using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Components;

namespace Epic.Enums
{
    internal interface IEnumOperation<T, K> where T : struct, IEnumConstraint
    {
        Func<T, K, bool> HasValue
        {
            get;
        }

        Func<T, K, bool> Equality
        {
            get;
        }

        Func<T, K, T> Or
        {
            get;
        }

        Func<T, K, T> And
        {
            get;
        }

        Func<T, K, T> Xor
        {
            get;
        }

        Func<T, K, T> Set
        {
            get;
        }


        Func<T, K, T> Add
        {
            get;
        }

        Func<T, K, T> Remove
        {
            get;
        }

        Func<K, T> Not
        {
            get;
        }

    }

}
