/* (C) 2016 Premysl Fara */

namespace ArtMan.Core.Templates
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Xml.Serialization;

    using ArtMan.Core.Collections;


    /// <summary>
    /// This singleton class is used for managing templates
    /// </summary>
    public sealed class TemplatesManager
    {
        #region fields
        private static readonly object Lock = new object();
        private static volatile TemplatesManager _instance;
        private const string FileExtension = ".etm";

        // TODO: Replace this with the [TemplateIgnore] attribute.
        private static readonly string[] ExcludedProperties = new[] { "Item", "Error", "UpdateDate", "UpdatedBy", "CreateDate", "CreatedBy" };

        private static readonly XmlSerializer XmlSerializer = new XmlSerializer(typeof(Template));
        private const string ValueTypePropertyName = "*Value";
        public const string SystemCategory = "System";
        #endregion

        #region properties
        /// <summary>
        /// Gets instance to object
        /// </summary>
        public static TemplatesManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                            _instance = new TemplatesManager();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Gets or privately sets a value indicating whether template is applying to object in current moment
        /// </summary>
        public bool ApplyingTemplate
        {
            get;
            private set;
        }
        #endregion

        #region events
        public event EventHandler TemplateApplied;
        #endregion

        #region ctor
        /// <summary>
        /// Private constructor - class is singleton
        /// </summary>
        private TemplatesManager()
        { }
        #endregion

        #region public methods
        /// <summary>
        /// Returns all template names for specified category
        /// </summary>
        /// <param name="category">Category name (required)</param>
        /// <returns>Array of template names</returns>
        public string[] GetTemplatesFiles(string category)
        {
            if (String.IsNullOrEmpty(category)) throw new ArgumentNullException("category");

            var rootPath = GetRootPath();
            var catPath = EnsureCategory(rootPath, category);
            var fileNames = Directory.GetFiles(catPath, "*" + FileExtension);
            return fileNames.Select(Path.GetFileNameWithoutExtension).ToArray();
        }

        /// <summary>
        /// Creates template from object
        /// </summary>
        /// <param name="obj">Instance to object for templating</param>
        /// <param name="category">Optional category name</param>
        /// <returns>Created template</returns>
        public Template CreateTemplate(object obj, string category)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var res = new Template();
            res.Category = category;
            var items = SerializeMembers(obj, null);
            res.Data = items.ToArray();
            return res;
        }

        /// <summary>
        /// Saves template to user storage.
        /// </summary>
        /// <param name="template">Template instance</param>
        /// <param name="name">Template name without file extension</param>
        public void SaveTemplate(Template template, string name)
        {
            if (template == null) throw new ArgumentNullException("template");
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (name.Contains(FileExtension)) throw new ArgumentException("name can't contain file extension");

            var rootPath = GetRootPath();
            var catPath = EnsureCategory(rootPath, template.Category);
            using (var stream = File.Open(Path.Combine(catPath, name + FileExtension), FileMode.Create))
            {
                XmlSerializer.Serialize(stream, template);
            }
        }

        /// <summary>
        /// Saves template to temp folder and returns path to created file
        /// </summary>
        /// <param name="template">Template instance</param>
        /// <returns>Path to created file (in temp folder)</returns>
        public string SaveTemplateToTemp(Template template)
        {
            if (template == null) throw new ArgumentNullException("template");

            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + FileExtension);

            using (var stream = File.Open(tempFile, FileMode.Create))
            {
                XmlSerializer.Serialize(stream, template);
            }

            return tempFile;
        }

        /// <summary>
        /// Returns real full path to user storage
        /// </summary>
        /// <returns>Path to storage</returns>
        //public string GetRealPath()
        //{
        //    var store = GetStore();
        //    // ReSharper disable PossibleNullReferenceException
        //    var res = store.GetType().GetField("m_RootDir", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(store).ToString();
        //    // ReSharper restore PossibleNullReferenceException
        //    return res;
        //}

        /// <summary>
        /// Loads template from specified path. When file content is not valid, method returns null without exception.
        /// </summary>
        /// <param name="path">Path to template file</param>
        /// <returns>Deserialized template instance</returns>
        public Template LoadTemplateFromPath(string path)
        {
            if (String.IsNullOrEmpty(path)) throw new ArgumentException("Path is null or empty");
            if (!File.Exists(path)) throw new FileNotFoundException("File not found", path);

            try
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var inputStream = File.Open(path, FileMode.Open))
                    {
                        CopyStream(inputStream, outputStream);
                    }
                    outputStream.Position = 0;
                    var res = (Template)XmlSerializer.Deserialize(outputStream);
                    return res;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Loads template from user storage by category and name. When there are problems with deserializing, TemplateException is thrown.
        /// </summary>
        /// <param name="category">Category name (required)</param>
        /// <param name="name">Template name (required)</param>
        /// <returns>Template instance</returns>
        public Template LoadTemplate(string category, string name)
        {
            if (String.IsNullOrEmpty(category)) throw new ArgumentNullException("category");
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (name.Contains(FileExtension)) throw new ArgumentException("name can't contain file extension");

            var rootPath = GetRootPath();
            var catPath = EnsureCategory(rootPath, category);
            try
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var inputStream = File.Open(Path.Combine(catPath, name + FileExtension), FileMode.Open))
                    {
                        CopyStream(inputStream, outputStream);
                    }
                    outputStream.Position = 0;
                    var res = (Template)XmlSerializer.Deserialize(outputStream);
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new TemplateException(String.Format("Can't deserialize template with category '{0}' and name '{1}'!", category, name), ex);
            }
        }

        /// <summary>
        /// Deletes template by category and name
        /// </summary>
        /// <param name="category">Category name (required)</param>
        /// <param name="name">Template name (required)</param>
        public void DeleteTemplate(string category, string name)
        {
            if (String.IsNullOrEmpty(category)) throw new ArgumentNullException("category");
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (name.Contains(FileExtension)) throw new ArgumentException("name can't contain file extension");

            var rootPath = GetRootPath();
            var categoryPath = EnsureCategory(rootPath, category);
            var fullPath = Path.Combine(categoryPath, name + FileExtension);
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }

        /// <summary>
        /// Applies template to object. When there are problems, no exception is thrown.
        /// </summary>
        /// <param name="obj">Instance to object</param>
        /// <param name="template">Instance to template</param>
        public void ApplyTemplate(object obj, Template template)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (template == null) throw new ArgumentNullException("template");

            ApplyingTemplate = true;
            try
            {
                DeserializeMembers(obj, template, null);
            }
            finally
            {
                ApplyingTemplate = false;
                if (TemplateApplied != null) TemplateApplied(this, EventArgs.Empty);
            }
        }
        #endregion

        #region non-public methods
        /// <summary>
        /// Recursive method for deserializing and applying template to object
        /// </summary>
        /// <param name="obj">Instance to object</param>
        /// <param name="template">Instance to template</param>
        /// <param name="parent">Parent name (optional)</param>
        private static void DeserializeMembers(object obj, Template template, string parent)
        {
            var type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (SkipProperty(propertyInfo)) continue;

                if (template.IsParent(propertyInfo.Name))
                {
                    if (propertyInfo.PropertyType.IsGenericType && IsCollectionType(propertyInfo))
                    {
                        Type genericType = propertyInfo.PropertyType.GetGenericArguments().First();
                        var collection = (IList)propertyInfo.GetValue(obj, new object[0]);
                        var count = template.GetParentsCount(propertyInfo.Name);
                        if (HasAttribute(propertyInfo, typeof(TemplateCollectionClearAttribute))) collection.Clear();

                        for (int i = 0; i < count; i++)
                        {
                            if (genericType.IsValueType || genericType == typeof(string))
                            {
                                var fieldItem = template.GetByPropertyName(ValueTypePropertyName, propertyInfo.Name + "." + i);
                                if (fieldItem == null) continue;
                                var item = Deserialize(fieldItem.Value, genericType);
                                collection.Add(item);
                            }
                            else
                            {
                                var item = Activator.CreateInstance(genericType);
                                DeserializeMembers(item, template, propertyInfo.Name + "." + i);
                                collection.Add(item);
                            }
                        }
                    }
                    else
                    {
                        DeserializeMembers(propertyInfo.GetValue(obj, new object[0]), template, propertyInfo.Name);
                    }
                }
                var templateItem = template.GetByPropertyName(propertyInfo.Name, parent);
                if (templateItem == null) continue;
                propertyInfo.SetValue(obj, Deserialize(templateItem.Value, propertyInfo.PropertyType), new object[0]);
            }
        }

        /// <summary>
        /// Recursive method for serializing object to template items
        /// </summary>
        /// <param name="obj">Instance to object</param>
        /// <param name="parent">Parent name (optional)</param>
        /// <returns>Array of template items</returns>
        private static TemplateItem[] SerializeMembers(object obj, string parent)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var type = obj.GetType();
            if (type.IsValueType || type == typeof(string))
            {
                return new[] { new TemplateItem(parent, ValueTypePropertyName, obj) };
            }

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var items = new List<TemplateItem>();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (SkipPropertyOnSave(propertyInfo)) continue;

                var value = propertyInfo.GetValue(obj, new object[0]);
                if (propertyInfo.PropertyType.IsGenericType && IsCollectionType(propertyInfo))
                {
                    var collection = (ICollection)value;
                    var index = 0;
                    foreach (var collectionItem in collection)
                    {
                        var subItems = SerializeMembers(collectionItem, propertyInfo.Name + "." + index);
                        if (subItems.Length > 0) items.AddRange(subItems);

                        index++;
                    }
                }
                else if (parent == null && IsComplexType(propertyInfo, value))
                {
                    var subItems = SerializeMembers(value, propertyInfo.Name);
                    if (subItems.Length > 0) items.AddRange(subItems);
                }
                else
                {
                    if (parent == null) items.Add(new TemplateItem(propertyInfo.Name, value));
                    else items.Add(new TemplateItem(parent, propertyInfo.Name, value));
                }
            }
            return items.ToArray();
        }

        /// <summary>
        /// Returns a value indicating whether skip property when (de)serializing
        /// </summary>
        /// <param name="propertyInfo">PropertyInfo instance</param>
        /// <returns>Boolean</returns>
        private static bool SkipProperty(PropertyInfo propertyInfo)
        {
            if (ExcludedProperties.Contains(propertyInfo.Name)) return true;
            if (propertyInfo.GetSetMethod() == null &&
                (!propertyInfo.PropertyType.IsGenericType || !IsCollectionType(propertyInfo)))
            {
                return true;
            }
            if (HasAttribute(propertyInfo, typeof(TemplateIgnoreAttribute))) return true;

            return false;
        }
        
        /// <summary>
        /// Returns a value indicating whether skip property when (de)serializing
        /// </summary>
        /// <param name="propertyInfo">PropertyInfo instance</param>
        /// <returns>Boolean</returns>
        private static bool SkipPropertyOnSave(PropertyInfo propertyInfo)
        {
            if (ExcludedProperties.Contains(propertyInfo.Name)) return true;
            if (propertyInfo.GetSetMethod() == null &&
                (!propertyInfo.PropertyType.IsGenericType || !IsCollectionType(propertyInfo)))
            {
                return true;
            }
            if (HasAttribute(propertyInfo, typeof(TemplateIgnoreAttribute))) return true;
            if (HasAttribute(propertyInfo, typeof(TemplateIgnoreOnSaveAttribute))) return true;

            return false;
        }

        /// <summary>
        /// Tests, if a property is one of the supported collection types.
        /// </summary>
        /// <param name="propertyInfo">A property info.</param>
        /// <returns>True, if the property is one of the supported types.</returns>
        private static bool IsCollectionType(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ObservableCollection<>)
                || propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ExtendedObservableCollection<>);
        }

        /// <summary>
        /// Returns a value indicating whether property has specified attribute
        /// </summary>
        /// <param name="propertyInfo">PropertyInfo instance</param>
        /// <param name="attributeType">Attribute Type</param>
        /// <returns>Boolean</returns>
        private static bool HasAttribute(PropertyInfo propertyInfo, Type attributeType)
        {
            var attr = propertyInfo.GetCustomAttributes(attributeType, false);
            return attr.Length > 0;
        }

        /// <summary>
        /// Returns a value indicating whether property is complex type
        /// </summary>
        /// <param name="propertyInfo">PropertyInfo instance</param>
        /// <param name="value">Property value</param>
        /// <returns>Boolean</returns>
        private static bool IsComplexType(PropertyInfo propertyInfo, object value)
        {
            return !propertyInfo.PropertyType.IsValueType && propertyInfo.PropertyType != typeof(string) && value != null;
        }

        /// <summary>
        /// Deserializes string value to object. When type is not supported, NotSupportedException is thrown.
        /// </summary>
        /// <param name="value">Serialized value</param>
        /// <param name="type">Value type</param>
        /// <returns>Converted value</returns>
        private static object Deserialize(string value, Type type)
        {
            if (type == typeof(string) || type == typeof(object))
            {
                return value;
            }

            if (String.IsNullOrEmpty(value)) return type.IsClass ? null : Activator.CreateInstance(type);

            if (type == typeof(int))
            {
                return int.Parse(value);
            }
            if (type == typeof(double))
            {
                return double.Parse(value);
            }
            if (type == typeof(decimal))
            {
                return decimal.Parse(value);
            }
            if (type == typeof(decimal?))
            {
                return String.IsNullOrEmpty(value) ? null : (decimal?)decimal.Parse(value);
            }
            if (type == typeof(long))
            {
                return long.Parse(value);
            }
            if (type == typeof(DateTime))
            {
                return DateTime.Parse(value);
            }
            if (type == typeof(DateTime?))
            {
                return String.IsNullOrEmpty(value) ? null : (DateTime?)DateTime.Parse(value);
            }
            if (type == typeof(TimeSpan))
            {
                return TimeSpan.Parse(value);
            }
            if (type == typeof(bool))
            {
                return bool.Parse(value);
            }
            if (type == typeof(bool?))
            {
                try
                {
                    return bool.Parse(value);
                }
                catch
                {
                    return (bool?)null;
                }
            }
            if (type == typeof(char))
            {
                return char.Parse(value);
            }
            if (type.IsEnum)
            {
                return Enum.Parse(type, value);
            }
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns isolated storage for user
        /// </summary>
        /// <returns>IsolatedStorageFile instance</returns>
        //private static IsolatedStorageFile GetStore()
        //{
        //    IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForDomain();
        //    return store;
        //}

        private static string GetRootPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CTRM", "Templates");
        }

        /// <summary>
        /// Ensures that category exists in storage
        /// </summary>
        /// <param name="rootPath">Path to root</param>
        /// <param name="category">Category name</param>
        private string EnsureCategory(string rootPath, string category)
        {
            var fullPath = Path.Combine(rootPath, category);
            if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
            return fullPath;
        }
        //private void EnsureCategory(IsolatedStorageFile store, string category)
        //{
        //    if (!store.DirectoryExists(category)) store.CreateDirectory(category);
        //}

        /// <summary>
        /// Helper method to copy input stream to output stream
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        private static void CopyStream(Stream input, Stream output)
        {
            const int bufferSize = 4096;
            var buffer = new byte[bufferSize];
            while (true)
            {
                int read = input.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                {
                    return;
                }
                output.Write(buffer, 0, read);
            }
        }
        #endregion
    }
}
