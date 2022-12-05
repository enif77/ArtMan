/* (C) 2016 Premysl Fara */

namespace ArtMananager
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;

    using Injektor;

    using ArtMananager.Core;
    using ArtMananager.Datalayer;
    using ArtMananager.DataObjects;
    using ArtMananager.Forms;


    public class App : Application
    {
        #region methods

        [STAThread]
        static void Main()
        {
            // create app and subscribe to unhandled exception
            var app = new App();

            app.DispatcherUnhandledException += DispatcherUnhandledExceptionHandler;

            try
            {
                // Setup date time format for application.
                Thread.CurrentThread.CurrentCulture = Culture.GetDefaultApplicationCultureInfo();

                // Connect to the localdb.
                Initializer.InitializeLayers(new SimpleDb.Files.Database(DataDirectoryPath));

                // Run application.
                var window = new MainWindow
                {
                    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
                };
                
                app.Run(window);
                
                // Save base configuration values.
                SaveAppConfig(window);
            }
            finally
            {
                ;
            }
        }


        public static string DataDirectoryPath
        {
            get { return Path.Combine(Directory.GetCurrentDirectory(), "Data"); }
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Config = LoadUserConfig();
        }
             

        /// <summary>
        /// A lock object for thread synchronizing/locking.
        /// </summary>
        protected static readonly object AppLocker = new object();

        //private static bool _headlessMode;
        private static Configuration _config;


        /// <summary>
        /// An application config loaded from a configuration template.
        /// </summary>
        public static Configuration Config
        {
            get
            {
                lock (AppLocker)
                {
                    return _config;
                }
            }

            set
            {
                lock (AppLocker)
                {
                    _config = value;
                }
            }
        }

        /// <summary>
        /// Loads user config from the file
        /// </summary>
        private static Configuration LoadUserConfig()
        {
            Configuration configuration = null;

            try
            {
                configuration = Registry.Get<ConfigurationDataLayer>().Get(1);
                if (configuration == null)
                {
                    configuration = new Configuration();
                    Registry.Get<ConfigurationDataLayer>().Save(configuration);
                }
            }
            catch (Exception)
            {
                ;  // Exceptions ignored

                // TODO: Log exception.
            }

            return configuration;
        }

        /// <summary>
        /// Saves current app. state to the configuration template.
        /// </summary>
        /// <param name="window">A MainWindow instance.</param>
        private static void SaveAppConfig(Window window)
        {
            Config.IsMaximized = window.WindowState == WindowState.Maximized;
            Config.Top = (int)window.Top;
            Config.Left = (int)window.Left;
            Config.Width = (int)window.Width;
            Config.Height = (int)window.Height;

            Registry.Get<ConfigurationDataLayer>().Save(Config);
        }
            

        private static void DispatcherUnhandledExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            // ignore datagrid clipboard copy error (content is copied sucessfully anyway):
            // http://stackoverflow.com/questions/12769264/openclipboard-failed-when-copy-pasting-data-from-wpf-datagrid
            var comException = e.Exception as System.Runtime.InteropServices.COMException;
            if (comException != null && comException.ErrorCode == -2147221040) return;

            UnhandledErrorWindow.Open(e.Exception);
        }

        #endregion
    }
}
