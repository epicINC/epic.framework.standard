using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Epic
{
    public class Wrapper
    {

        /// <summary>
        /// Wrapper STA To MTA
        /// </summary>
        /// <remarks>
        /// https://weblog.west-wind.com/posts/2012/Sep/18/Creating-STA-COM-compatible-ASPNET-Applications
        /// </remarks>
        /// <param name="action"></param>
        public static Thread STAToMTA(ThreadStart action)
        {
            var result = new Thread(action);
            result.SetApartmentState(ApartmentState.STA);
            return result;
        }
    }
}