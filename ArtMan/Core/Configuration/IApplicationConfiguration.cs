/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Configuration
{
    public interface IApplicationConfiguration
    {
        /// <summary>
        /// Gets or sets if the main app. window was maximized.
        /// </summary>
        bool IsMaximized { get; set; }

        /// <summary>
        /// Gets or sets the position of the top edge of the main app. window.
        /// </summary>
        double Top { get; set; }

        /// <summary>
        /// Gets or sets the position of the left edge of the main app. window.
        /// </summary>
        double Left { get; set; }

        /// <summary>
        /// Gets or sets the width of the main app. window.
        /// </summary>
        double Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the main app. window.
        /// </summary>
        double Height { get; set; }


        /// <summary>
        /// Returns the name of a template generated from this configuration.
        /// </summary>
        /// <returns>A name of a template generated from this configuration.</returns>
        string GetTemplateName();
    }
}
