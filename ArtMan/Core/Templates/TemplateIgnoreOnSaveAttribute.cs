/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Templates
{
    using System;


    /// <summary>
    /// Attribute using to indicate TemplatesManager that property 
    /// should not be used while generating the template.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TemplateIgnoreOnSaveAttribute : Attribute
    { }
}
