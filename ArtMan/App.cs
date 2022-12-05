/* (C) 2016 Premysl Fara */

namespace ArtMan
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;

    using ArtMan.Core;
    using ArtMan.Core.Configuration;
    using ArtMan.Core.Injektor;
    using ArtMan.Core.Templates;
    using ArtMan.Datalayer;
    using ArtMan.Forms;


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

                // Set the localdb data directory.
                AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());

                // Connect to the localdb.
                var anaDb = new Core.Data.Database(ConfigurationManager.ConnectionStrings["ARTMANDB"].ConnectionString);

                Initializer.InitializeLayers(anaDb);

                // Run application.
                var window = new MainWindow
                {
                    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
                };
                
                app.Run(window);
                
                // Save base configuration values.
                GetUserConfigPartFromFile(GetConfigurationInstance());
                SaveAppConfig(window);
            }
            finally
            {
                ;
            }
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //ArgsParser.RegisterArgument(new ArgumentTypeInfo(ArgumentTypes.Analysis, typeof(AnalysisArgument), "Analysis"));
            //ArgsParser.RegisterArgument(new ArgumentTypeInfo(ArgumentTypes.Headless, typeof(HeadlessArgument), "Headless"));
            //ArgsParser.RegisterArgument(new ArgumentTypeInfo(ArgumentTypes.Output, typeof(OutputArgument), "Output"));

            //ArgsParser.Initialize(e.Args);

            Config = LoadUserConfig(GetConfigurationInstance());

            //var window = Registry.Get<MainWindow>();

            //var analysisArg = ArgsParser.GetArgument(ArgumentTypes.Analysis) as AnalysisArgument;
            //if (analysisArg != null)
            //{
            //    window.AnalysisToOpenOnStart = analysisArg.Value;
            //}
        }


        /// <summary>
        /// A lock object for thread synchronizing/locking.
        /// </summary>
        protected static readonly object AppLocker = new object();

        //private static bool _headlessMode;
        private static IApplicationConfiguration _config;


        /// <summary>
        /// An application config loaded from a configuration template.
        /// </summary>
        public static IApplicationConfiguration Config
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
        /// Returns an instance of a IApplicationConfiguration.
        /// </summary>
        /// <returns>An instance of a IApplicationConfiguration.</returns>
        public static IApplicationConfiguration GetConfigurationInstance()
        {
            return new ApplicationConfiguration();
        }

        /// <summary>
        /// Loads again user config parts that are not being changed (forms that are not opened)
        /// </summary>
        /// <param name="config">Initialized instance of a config</param>
        protected static void GetUserConfigPartFromFile(IApplicationConfiguration config)
        {
            //var fileConfig = (ApplicationConfiguration)LoadUserConfig(config);
            //var actualConfig = (ApplicationConfiguration)Config;

            // TODO: Apply settings in the config file to the in memory config.
        }

        /// <summary>
        /// Loads user config from the file
        /// </summary>
        /// <param name="config">Initialized instance of a config</param>
        protected static IApplicationConfiguration LoadUserConfig(IApplicationConfiguration config)
        {
            Template configurationTemplate = null;
            try
            {
                configurationTemplate = TemplatesManager.Instance.LoadTemplate(
                ConfigurationHelper.AppUserConfigTemplatesCategoryName,
                config.GetTemplateName());
            }
            catch (Exception)
            {
                ;  // Exceptions ignored

                // TODO: Log exception.
            }

            if (configurationTemplate != null)
            {
                TemplatesManager.Instance.ApplyTemplate(config, configurationTemplate);
            }

            return config;
        }

        /// <summary>
        /// Saves current app. state to the configuration template.
        /// </summary>
        /// <param name="window">A MainWindow instance.</param>
        protected static void SaveAppConfig(Window window)
        {
            Config.IsMaximized = window.WindowState == WindowState.Maximized;
            Config.Top = window.Top;
            Config.Left = window.Left;
            Config.Width = window.Width;
            Config.Height = window.Height;

            var configurationTemplate = TemplatesManager.Instance.CreateTemplate(Config, ConfigurationHelper.AppUserConfigTemplatesCategoryName);

            TemplatesManager.Instance.SaveTemplate(configurationTemplate, Config.GetTemplateName());
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
