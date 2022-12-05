/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Templates
{
    using System.Xml.Serialization;


    /// <summary>
    /// Class representing one property in template. Property can be have parent (complex types)
    /// </summary>
    [XmlRoot("Item")]
    public sealed class TemplateItem
    {
        #region properties
        /// <summary>
        /// Gets or sets parent property name (used for storing complex types)
        /// </summary>
        [XmlAttribute("ParentPropertyName")]
        public string ParentPropertyName { get; set; }

        /// <summary>
        /// Gets or sets property name
        /// </summary>
        [XmlAttribute("PropertyName")]
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets value stored as string
        /// </summary>
        [XmlAttribute("Value")]
        public string Value { get; set; }
        #endregion

        #region ctor
        /// <summary>
        /// Creates new empty instance (used for deserializer)
        /// </summary>
        public TemplateItem()
        { }

        /// <summary>
        /// Creates new instance with property name and value
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="value">Stored value</param>
        public TemplateItem(string propertyName, object value)
        {
            PropertyName = propertyName;
            Value = value != null ? value.ToString() : string.Empty;
        }

        /// <summary>
        /// Creates new instance with parent, property name and value
        /// </summary>
        /// <param name="parentName">Parent name</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="value">Stored value</param>
        public TemplateItem(string parentName, string propertyName, object value)
        {
            ParentPropertyName = parentName;
            PropertyName = propertyName;
            Value = value != null ? value.ToString() : string.Empty;
        }
        #endregion

        #region public methods
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(ParentPropertyName)) return string.Format("{0}.{1} = {2}", ParentPropertyName, PropertyName, Value);

            return string.Format("{0} = {1}", PropertyName, Value);
        }
        #endregion
    }
}
