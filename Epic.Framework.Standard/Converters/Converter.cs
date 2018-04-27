using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Converters
{

    public delegate bool TryParser<T, K>(T value, out K result);

    public delegate bool ArrayTryParser<T, K>(T[] value, out K[] result, bool skipFail = false);


    public static class CommonConverter
    {
        public static TResult Converter<T, TResult>(T value, TryParser<T, TResult> parser)
        {
            parser(value, out TResult result);
            return result;
        }


        public static IEnumerable<TResult> ArrayConverter<T, TResult>(T[] value, TryParser<T, TResult> parser, bool skipFail = false)
        {
            TResult result;
            if (skipFail)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    if (parser(value[i], out result)) yield return result;
                }
            }
            else
            {
                for (var i = 0; i < value.Length; i++)
                {
                    parser(value[i], out result);
                    yield return result;
                }
            }
        }
    }

}
