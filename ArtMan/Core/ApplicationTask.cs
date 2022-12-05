/* (C) 2016 premysl Fara */

namespace ArtMan.Core
{
    using System;


    /// <summary>
    /// Class representing background process (task)
    /// </summary>
    public sealed class ApplicationTask : IDisposable
    {
        #region properties
        /// <summary>
        /// Gets or sets task id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a task name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets start DateTime
        /// </summary>
        public DateTime Started
        {
            get;
            set;
        }
        #endregion

        #region public methods
        /// <summary>
        /// When task is finished, dispose method have to be called to set task as complete
        /// </summary>
        public void Dispose()
        {
            ApplicationTasksManager.Instance.SetComplete(this);
        }
        #endregion
    }
}
