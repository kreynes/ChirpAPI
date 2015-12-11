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
                ((EventHandler<TEventArgs>)d).BeginInvoke(sender, ea, null, null);
            }
        }
    }
}

