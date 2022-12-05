/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Templates
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;


    /// <summary>
    /// Class representing one template file. Enables to store complex objects in xml.
    /// </summary>
    [XmlRoot("Template")]
    public sealed class Template
    {
        #region properties
        /// <summary>
        /// Stored properties
        /// </summary>
        [XmlArray("Data")]
        [XmlArrayItem("Item")]
        public TemplateItem[] Data { get; set; }

        /// <summary>
        /// Template category - commodity internal code. In some cases, category can be empty (like storing only deal block template to folder).
        /// </summary>
        [XmlIgnore]
        public string Category { get; set; }
        #endregion

        #region ctor
        public Template()
        {
            Data = new TemplateItem[0];
        }
        #endregion

        #region public methods
        /// <summary>
        /// Returns template item by property name and parent name
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="parent">Parent name (optional)</param>
        /// <returns>Template item</returns>
        public TemplateItem GetByPropertyName(string propertyName, string parent)
        {
            return Data.Where(i => i.PropertyName.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault(i => (!String.IsNullOrEmpty(parent) && parent.Equals(i.ParentPropertyName)) || (String.IsNullOrEmpty(parent) && String.IsNullOrEmpty(i.ParentPropertyName)));
        }

        /// <summary>
        /// Returns a indication whether property name is name of property with complex type.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <returns>Boolean</returns>
        public bool IsParent(string propertyName)
        {
            return Data.Any(i => propertyName.Equals(i.ParentPropertyName, StringComparison.InvariantCultureIgnoreCase) || (i.ParentPropertyName ?? String.Empty).StartsWith(propertyName + ".", StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Returns a count of parents in template by property name
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <returns>Count</returns>
        public int GetParentsCount(string propertyName)
        {
            var names = Data.Where(i => !String.IsNullOrEmpty(i.ParentPropertyName) && i.ParentPropertyName.StartsWith(propertyName + ".")).Select(i => i.ParentPropertyName).Distinct();
            var count = names.Count();
            return count;
        }

        public void AddTemplateItem(TemplateItem item)
        {
            if (item == null) throw new ArgumentNullException("item");
            Data = (Data ?? new TemplateItem[0]).Concat(new[] { item }).ToArray();
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var item in Data)
            {
                result.AppendLine(item.ToString());
            }
            return result.ToString();
        }
        #endregion
    }
}
