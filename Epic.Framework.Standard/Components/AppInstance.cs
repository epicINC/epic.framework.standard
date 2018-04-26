using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Components
{
    public static class AppInstance
    {
        public static void Lazy<T>()
        {

        }
    }


    public static class AppInstance<T> where T : class
    {

        static AppInstance()
        {

        }

        public static Lazy<T> Init(Func<T> value)
        {
            return Item = new Lazy<T>(value);
        }

        static Lazy<T> Item
        {
            get;
            set;
        }

        public static T Value
        {
            get
            {
                if (Item != null) return Item.Value;
                return (Item = new Lazy<T>()).Value;
            }
        }
    }
}
