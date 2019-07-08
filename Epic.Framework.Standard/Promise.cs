using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Epic
{
    public delegate void PromiseCall<T, K>(Action<T> resolve, Action<K> reject);

    public class Promise
    {



        public static Task<T> Taskify<T, K>(PromiseCall<T, K> action) where K : Exception
        {
            var tcs = new TaskCompletionSource<T>();
            try
            {
                action(e => tcs.SetResult(e), k => tcs.SetException(k));
            }
            catch (Exception e)
            {
                tcs.SetException(e);
            }
            return tcs.Task;
        }
    }

    public class Promise<T, K> where K : Exception
    {

        public Promise(PromiseCall<T, K> action)
        {
            var tcs = new TaskCompletionSource<T>();

            try
            {
                action(e => tcs.SetResult(e), k => tcs.SetException(k));
            }
            catch (Exception e)
            {
                tcs.SetException(e);
            }
            this.Task = tcs.Task;
        }

        public Task<T> Task { get; private set; }

        public bool Timeout(int millisecondsTimeout)
        {
            return !this.Task.Wait(millisecondsTimeout);
            //return (await Race(this.Current, Task.Delay(millisecondsTimeout))) != this.Current;
        }

        public bool Timeout(TimeSpan timeout)
        {
            return !this.Task.Wait(timeout);
            //return (await Race(this.Current, Task.Delay(timeout))) != this.Current;
        }




        public void Catch()
        {
        }


     

        public static Task All(params Task[] tasks)
        {
            return System.Threading.Tasks.Task.WhenAll(tasks);
        }

        public static Task<T[]> All(params Task<T>[] tasks)
        {
            return System.Threading.Tasks.Task.WhenAll(tasks);
        }

        public static Task<Task> Race(params Task[] tasks)
        {
            return System.Threading.Tasks.Task.WhenAny(tasks);
        }


        public static Task<Task<T>> Race(params Task<T>[] tasks)
        {
            return System.Threading.Tasks.Task.WhenAny(tasks);
        }



        public static void Reject()
        {

            

        }

        public static void Resolve()
        {

        }

    }
}
