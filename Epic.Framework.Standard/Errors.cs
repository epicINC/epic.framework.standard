using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;

namespace Epic
{
    public class Errors
    {
        public static void Argument(string message, string paramName)
        {
            throw new ArgumentException(message, paramName);
        }

        #region ArgumentNull

        [SecuritySafeCritical]
        public static void ArgumentNull(string paramName, string message)
        {
            throw new ArgumentNullException(paramName, message);
        }


        [SecuritySafeCritical]
        public static void ArgumentNull(string paramName, object argument)
        {
            if (argument == null)
                throw new ArgumentNullException(paramName);
        }


        [SecuritySafeCritical]
        public static void ArgumentNull(string paramName, string message, object argument)
        {
            if (argument == null)
                throw new ArgumentNullException(paramName, message);
        }



        #endregion


        public static void InvalidComplexPropertyExpression(object p0)
        {
            //return new InvalidOperationException(Strings.InvalidComplexPropertyExpression(p0));
            if (p0 == null) throw new InvalidOperationException();
        }

        public static void InvalidPropertiesExpression(object p0)
        {
            if (p0 == null) throw new InvalidOperationException();
        }

    }


    internal static class ErrorExtensions
    {
        public static void Throw(this Exception value)
        {
            if (value != null) throw value;
        }
    }

}

