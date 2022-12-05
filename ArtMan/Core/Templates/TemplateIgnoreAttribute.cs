/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Templates
{
    using System;


    /// <summary>
    /// Attribute using to indicate TemplatesManager that property can't be used in templating
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TemplateIgnoreAttribute : Attribute
    { }
}
