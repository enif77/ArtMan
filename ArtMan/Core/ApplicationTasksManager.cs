/* (C) 2016 Premysl Fara */

namespace ArtMan.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    /// <summary>
    /// This singleton class is used for notifying user about background processes from data layer (in Ezpada.Controls.BottomPanel control).
    /// Class is thread-safe.
    /// </summary>
    public sealed class ApplicationTasksManager
    {
        #region fields
        private readonly List<ApplicationTask> _tasks = new List<ApplicationTask>();
        private static readonly object Lock = new object();
        private static volatile ApplicationTasksManager _instance;
        #endregion

        #region properties
        /// <summary>
        /// Gets instance to class
        /// </summary>
        public static ApplicationTasksManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                            _instance = new ApplicationTasksManager();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Gets names of currently processing tasks
        /// </summary>
        public string[] CurrentTasks
        {
            get
            {
                lock (Lock)
                {
                    return _tasks.Select(i => i.Name).ToArray();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether some tasks are running
        /// </summary>
        public bool IsRunning
        {
            get
            {
                lock (Lock)
                {
                    return _tasks.Count > 0;
                }
            }
        }

        /// <summary>
        /// Gets summary text for output
        /// </summary>
        public string SummaryText
        {
            get
            {
                lock (Lock)
                {
                    if (_tasks.Count == 0) return "Done";
                    var res = String.Join(", ", CurrentTasks);
                    return res;
                }
            }
        }
        #endregion

        #region events
        /// <summary>
        /// Event fired when there are some changes in tasks
        /// </summary>
        public event EventHandler Change;
        #endregion

        #region ctor
        /// <summary>
        /// Private constructor - class is singleton
        /// </summary>
        private ApplicationTasksManager()
        { }
        #endregion

        #region public methods
        /// <summary>
        /// Starts new task and returns disposable class representing current task.
        /// When task is finished, Dispose method have to be called, what change task to finished.
        /// Example:
        /// using (ApplicationTasksManager.Instance.StartTask(String.Format("Saving...")))
        /// {
        ///     ...
        /// }
        /// </summary>
        /// <param name="name">Name of task</param>
        /// <returns>Instance to new task</returns>
        public ApplicationTask StartTask(string name)
        {
            lock (Lock)
            {
                var task = new ApplicationTask { Id = Guid.NewGuid(), Name = name, Started = DateTime.Now };
                _tasks.Add(task);
                InvokeChange();
                return task;
            }
        }
        #endregion

        #region non-public methods
        /// <summary>
        /// Sets task as complete. This method is called when disposing ApplicationTask class.
        /// </summary>
        /// <param name="task">Instance to task class</param>
        internal void SetComplete(ApplicationTask task)
        {
            lock (Lock)
            {
                if (!_tasks.Contains(task)) throw new InvalidOperationException();

                _tasks.Remove(task);
                InvokeChange();
            }
        }

        /// <summary>
        /// Asynchronously invokes Change event
        /// </summary>
        private void InvokeChange()
        {
            Task.Factory.StartNew(() =>
                {
                    if (Change != null) Change(this, EventArgs.Empty);
                });
        }
        #endregion
    }
}
