using System;
using System.Threading.Tasks;

namespace ChirpLib
{
    public static class ParallelExtension
    {
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

