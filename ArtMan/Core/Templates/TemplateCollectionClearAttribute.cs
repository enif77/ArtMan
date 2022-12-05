/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Templates
{
    using System;


    /// <summary>
    /// Attribute using to indicate TemplatesManager that collection property have to be cleared before templating
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TemplateCollectionClearAttribute : Attribute
    { }
}
