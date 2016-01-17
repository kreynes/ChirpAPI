using System;
using System.Threading.Tasks;

namespace ChirpLib
{
    public static class ParallelExtension
    {
        /// <summary>
        /// Invoke delegates when an event is fired..
        /// </summary>
        /// <param name="md">Method Delegate.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="ea">Event Args</param>
        public static void ParallelInvoke<TEventArgs>(this EventHandler<TEventArgs> md, object sender, TEventArgs ea)
        {
            foreach (Delegate d in md.GetInvocationList())
            {
                Task.Run(() =>
                    {
                        ((EventHandler<TEventArgs>)d).Invoke(sender, ea);
                    });
            }
        }
    }
}

