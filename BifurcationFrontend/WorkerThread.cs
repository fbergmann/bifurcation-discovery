using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BifurcationFrontend
{
    /// <summary>
    /// </summary>
    public class WorkerThread
    {
        private readonly Thread _thread;

        public WorkerThread()
        {
            Actions = new Queue<Action>();
            _thread = new Thread(RunThread);
        }

        public bool ShouldStop { get; set; }

        private Queue<Action> Actions { get; set; }

        public bool IsAlive
        {
            get { return _thread.IsAlive; }
        }

        public void Abort()
        {
            _thread.Abort();
        }

        public void Join()
        {
            _thread.Join();
        }

        public void QueueItem(Action action)
        {
            lock (this)
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

                Action current = Actions.Dequeue();
                if (current != null)
                {
                    current();
                }
            }
        }

        public void Start()
        {
            _thread.Start();
        }
    }
}