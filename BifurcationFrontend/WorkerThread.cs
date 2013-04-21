using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BifurcationFrontend
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkerThread
    {
        public bool ShouldStop { get; set; }

        private Queue<Action> Actions { get; set; }

        public void QueueItem(Action action)
        {
            lock(this)
            {
                Actions.Enqueue(action);
            }
        }


        protected virtual void RunThread()
        {
            while (!ShouldStop)
            {
                if (!Actions.Any())
                {
                    Thread.Sleep(100);
                    continue;
                }

                var current = Actions.Dequeue();
                if (current != null)
                {
                    current();
                }

            }
        }

        private Thread _thread;

        public WorkerThread()
        {
            Actions = new Queue<Action>();
            _thread = new Thread(new ThreadStart(this.RunThread));
        }

        public void Start() { _thread.Start(); }
        public void Join() { _thread.Join(); }
        public void Abort() { _thread.Abort(); }
        public bool IsAlive { get { return _thread.IsAlive; } }

    }
}
