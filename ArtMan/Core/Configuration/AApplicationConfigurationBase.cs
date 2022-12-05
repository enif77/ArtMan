/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Configuration
{
    public abstract class AApplicationConfigurationBase : IApplicationConfiguration
    {
        #region CONSTANTS =====================================================

        /// <summary>
        /// The default if the main app. window was maximized.
        /// </summary>
        public const bool DefaultMaximized = false;

        /// <summary>
        /// The default position of the top endge of the main app. window.
        /// </summary>
        public const double DefaultTop = 0;

        /// <summary>
        /// The default position of the left endge of the main app. window.
        /// </summary>
        public const double DefaultLeft = 0;

        /// <summary>
        /// The default width of the main app. window.
        /// </summary>
        public const double DefaultWidth = 1024;

        /// <summary>
        /// The default height of the main app. window.
        /// </summary>
        public const double DefaultHeight = 768;

        #endregion


        #region CONSTRUCTORS ==================================================

        /// <summary>
        /// Creates an ApplicationConfigurationBase with default settings.
        /// </summary>
        protected AApplicationConfigurationBase()
        {
            this.IsMaximized = DefaultMaximized;
            this.Top = DefaultTop;
            this.Left = DefaultLeft;
            this.Width = DefaultWidth;
            this.Height = DefaultHeight;
        }

        #endregion


        #region IApplicationConfiguration =====================================

        /// <summary>
        /// Gets or sets the code of an aplication for which this configuration is.
        /// </summary>
        public string AppCode { get; set; }


        /// <summary>
        /// Gets or sets if the main app. window was maximized.
        /// </summary>
        public bool IsMaximized { get; set; }

        /// <summary>
        /// Gets or sets the position of the top edge of the main app. window.
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Gets or sets the position of the left edge of the main app. window.
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Gets or sets the width of the main app. window.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the main app. window.
        /// </summary>
        public double Height { get; set; }


        /// <summary>
        /// Returns the name of a template generated from this configuration.
        /// </summary>
        /// <returns>A name of a template generated from this configuration.</returns>
        public string GetTemplateName()
        {
            return ConfigurationHelper.GetAppUserConfigTemplateName(this.AppCode);
        }

        #endregion
    }
}
