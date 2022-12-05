/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Configuration
{
    using System;


    public static class ConfigurationHelper
    {
        #region CONSTANTS =====================================================

        /// <summary>
        /// The name of the application user configuration templats category.
        /// </summary>
        public const string AppUserConfigTemplatesCategoryName = "USER_CONFIG";

        /// <summary>
        /// The base of the application user configuration template name.
        /// Should be used as "APP-CODE" + AppUserConfigTemplateNameBase.
        /// </summary>
        public const string AppUserConfigTemplateNameBase = "_user_config";
       
        #endregion


        #region PROPERTIES ====================================================
 
        #endregion


        #region PUBLIC METHODS ================================================

        /// <summary>
        /// Returns an application user configuration template name specific to
        /// a given application code.
        /// </summary>
        /// <param name="appCode">An unique application code.</param>
        /// <returns>A user configuration template name.</returns>
        public static string GetAppUserConfigTemplateName(string appCode)
        {
            if (String.IsNullOrEmpty(appCode))
            {
                throw new ArgumentException("The appCode parameter can not be null or empty.");
            }

            return appCode + AppUserConfigTemplateNameBase;
        }

        #endregion
    }
}
