using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Linq;

namespace Epic.Hardware.WMI
{
    internal class WMISearcher
    {
        public static IEnumerable<ManagementBaseObject> Query(string query)
        {
            if (String.IsNullOrWhiteSpace(query)) yield break;
            using (var searcher = new ManagementObjectSearcher(query))
            {
                using (var collection = searcher.Get())
                {
                    //try
                    //{
                    var enumerator = collection.GetEnumerator();
                    while (enumerator.MoveNext())
                        yield return enumerator.Current;

                    //}
                    //catch (System.Management.ManagementException ex)
                    //{
                    //    yield break;
                    //}
                    //catch (Exception e)
                    //{
                    //    yield break;
                    //}
                }
            }
        }

        public static ManagementBaseObject Find(string query)
        {
            return Query(query).FirstOrDefault();
        }
    }
}
