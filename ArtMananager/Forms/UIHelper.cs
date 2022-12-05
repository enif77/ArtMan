/* (C) 2016 Premysl Fara */

namespace ArtMananager.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    using ArtMananager.DataObjects;


    public static class UIHelper
    {
        #region Autor

        public static Autor EditAutor(DispatcherObject dob, GlobalDataObject gdo, object source, bool isNew = false,
            bool updateColection = true)
        {
            Autor entity;

            if (isNew)
            {
                entity = new Autor();
            }
            else
            {
                var item = source as Autor;
                if (item == null)
                {
                    MessageBox.Show("Vyberte položku k editaci.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                if (item.Id == 0)
                {
                    MessageBox.Show("Tuto položku nelze editovat.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                entity = gdo.GetAutor(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybranou položku nelze načíst k editaci.", "Art Manager - Chyba",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                    return null;
                }
            }

            if (AutorEditor.Open(gdo, entity))
            {
                gdo.SaveAutor(entity);

                if (updateColection) UpdateAutorCollection(dob, gdo);
            }

            return entity;
        }


        public static void UpdateAutorCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateAutorCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region Technika

        public static Technika EditTechnika(DispatcherObject dob, GlobalDataObject gdo, object source,
            bool isNew = false, bool updateColection = true)
        {
            Technika entity;

            if (isNew)
            {
                entity = new Technika();
            }
            else
            {
                var item = source as Technika;
                if (item == null)
                {
                    MessageBox.Show("Vyberte položku k editaci.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                if (item.Id == 0)
                {
                    MessageBox.Show("Tuto položku nelze editovat.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                entity = gdo.GetTechnika(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybranou položku nelze načíst k editaci.", "Art Manager - Chyba",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                    return null;
                }
            }

            if (TechnikaEditor.Open(entity))
            {
                gdo.SaveTechnika(entity);

                if (updateColection) UpdateTechnikaCollection(dob, gdo);
            }

            return entity;
        }


        public static void UpdateTechnikaCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateTechnikaCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region TypDila

        public static TypDila EditTypDila(DispatcherObject dob, GlobalDataObject gdo, object source, bool isNew = false,
            bool updateColection = true)
        {
            TypDila entity;

            if (isNew)
            {
                entity = new TypDila();
            }
            else
            {
                var item = source as TypDila;
                if (item == null)
                {
                    MessageBox.Show("Vyberte položku k editaci.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                if (item.Id == 0)
                {
                    MessageBox.Show("Tuto položku nelze editovat.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                entity = gdo.GetTypDila(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybranou položku nelze načíst k editaci.", "Art Manager - Chyba",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                    return null;
                }
            }

            if (TypDilaEditor.Open(entity))
            {
                gdo.SaveTypDila(entity);

                if (updateColection) UpdateTypDilaCollection(dob, gdo);
            }

            return entity;
        }


        public static void UpdateTypDilaCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateTypDilaCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region Mena

        public static Mena EditMena(DispatcherObject dob, GlobalDataObject gdo, object source, bool isNew = false,
            bool updateColection = true)
        {
            Mena entity;

            if (isNew)
            {
                entity = new Mena();
            }
            else
            {
                var item = source as Mena;
                if (item == null)
                {
                    MessageBox.Show("Vyberte položku k editaci.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                if (item.Id == 0)
                {
                    MessageBox.Show("Tuto položku nelze editovat.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                entity = gdo.GetMena(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybranou položku nelze načíst k editaci.", "Art Manager - Chyba",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                    return null;
                }
            }

            if (MenaEditor.Open(entity))
            {
                gdo.SaveMena(entity);

                if (updateColection) UpdateMenaCollection(dob, gdo);
            }

            return entity;
        }


        public static void UpdateMenaCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateMenaCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region ProdejniMisto

        public static ProdejniMisto EditProdejniMisto(DispatcherObject dob, GlobalDataObject gdo, object source,
            bool isNew = false, bool updateColection = true)
        {
            ProdejniMisto entity;

            if (isNew)
            {
                entity = new ProdejniMisto();
            }
            else
            {
                var item = source as ProdejniMisto;
                if (item == null)
                {
                    MessageBox.Show("Vyberte položku k editaci.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                if (item.Id == 0)
                {
                    MessageBox.Show("Tuto položku nelze editovat.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                entity = gdo.GetProdejniMisto(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybranou položku nelze načíst k editaci.", "Art Manager - Chyba",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                    return null;
                }
            }

            if (ProdejniMistoEditor.Open(entity))
            {
                gdo.SaveProdejniMisto(entity);

                if (updateColection) UpdateProdejniMistoCollection(dob, gdo);
            }

            return entity;
        }


        public static void UpdateProdejniMistoCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateProdejniMistoCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region Majitel

        public static Majitel EditMajitel(DispatcherObject dob, GlobalDataObject gdo, object source, bool isNew = false,
            bool updateColection = true)
        {
            Majitel entity;

            if (isNew)
            {
                entity = new Majitel();
            }
            else
            {
                var item = source as Majitel;
                if (item == null)
                {
                    MessageBox.Show("Vyberte položku k editaci.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                if (item.Id == 0)
                {
                    MessageBox.Show("Tuto položku nelze editovat.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                entity = gdo.GetMajitel(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybranou položku nelze načíst k editaci.", "Art Manager - Chyba",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                    return null;
                }
            }

            if (MajitelEditor.Open(entity))
            {
                gdo.SaveMajitel(entity);

                if (updateColection) UpdateMajitelCollection(dob, gdo);
            }

            return entity;
        }


        public static void UpdateMajitelCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateMajitelCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        #region Umisteni

        public static Umisteni EditUmisteni(DispatcherObject dob, GlobalDataObject gdo, object source,
            bool isNew = false, bool updateColection = true)
        {
            Umisteni entity;

            if (isNew)
            {
                entity = new Umisteni();
            }
            else
            {
                var item = source as Umisteni;
                if (item == null)
                {
                    MessageBox.Show("Vyberte položku k editaci.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                if (item.Id == 0)
                {
                    MessageBox.Show("Tuto položku nelze editovat.", "Art Manager - Upozornění", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);

                    return null;
                }

                entity = gdo.GetUmisteni(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybranou položku nelze načíst k editaci.", "Art Manager - Chyba",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                    return null;
                }
            }

            if (UmisteniEditor.Open(entity))
            {
                gdo.SaveUmisteni(entity);

                if (updateColection) UpdateUmisteniCollection(dob, gdo);
            }

            return entity;
        }


        public static void UpdateUmisteniCollection(DispatcherObject dob, GlobalDataObject gdo)
        {
            dob.InvokeIfRequired(() =>
            {
                gdo.UpdateUmisteniCollection();
            }, DispatcherPriority.Render);
        }

        #endregion


        public static bool IsValidUrl(string url)
        {
            if (String.IsNullOrWhiteSpace(url)) return false;

            try
            {
                Uri uriResult;

                return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                       (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static bool IsPathValid(string path)
        {
            try
            {
                return Path.GetFullPath(path) == path;
            }
            catch
            {
                return false;
            }
        }


        public static string GetFullPath(string path)
        {
            // Return the BaseResourcesDirectoryPath instead of an empty path.
            if (String.IsNullOrWhiteSpace(path))
            {
                return path;
            }

            // No base BaseResourcesDirectoryPath set, return the path itself.
            if (String.IsNullOrWhiteSpace(App.Config.BaseResourcesDirectoryPath))
            {
                return (path.StartsWith("@")) ? path.Substring(1) : path;
            }

            // Is a relative path?
            return path.StartsWith("@")
                ? Path.Combine(App.Config.BaseResourcesDirectoryPath, path.Substring(1))
                : path;
        }


        public static string GetRelativePath(string path)
        {
            // No BaseResourcesDirectoryPath set. Return the unmodified path.
            if (String.IsNullOrWhiteSpace(App.Config.BaseResourcesDirectoryPath) || String.IsNullOrWhiteSpace(path))
            {
                return path;
            }

            // Make the path relative.
            var relativePath = path.StartsWith(App.Config.BaseResourcesDirectoryPath)
                ? path.Replace(App.Config.BaseResourcesDirectoryPath, String.Empty)
                : path;

            // Remove the leading '\', to make the Path.Combine() happy.
            relativePath = (relativePath.StartsWith("\\"))
                ? relativePath.Substring(1)
                : relativePath;

            return relativePath.StartsWith("@") ? relativePath : ("@" + relativePath);
        }


        public static ImageSource LoadImage(string fileName)
        {
            var image = new BitmapImage();

            using (var stream = File.OpenRead(fileName))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }

            return image;
        }


        public static void LoadPreviewImage(Autor autor, Image previewDisplay, Label previewDescription)
        {
            LoadPreviewImage(autor.Id, autor.ResourcesDir, autor.ThumbnailName, previewDisplay, previewDescription);
        }


        public static void LoadPreviewImage(Dilo dilo, Image previewDisplay, Label previewDescription)
        {
            LoadPreviewImage(dilo.Id, dilo.ResourcesDir, dilo.ThumbnailName, previewDisplay, previewDescription);
        }


        public static void LoadPreviewImage(int id, string resourcesDir, string thumbnailName, Image previewDisplay, Label previewDescription)
        {
            var imageName = GetThumbnailImageName(thumbnailName);
            var imgagePath = GetThumbnailImagePath(id, resourcesDir, thumbnailName);
            previewDisplay.Source = File.Exists(imgagePath) ? UIHelper.LoadImage(imgagePath) : new BitmapImage();
            previewDescription.Content = "<Dokumenty>\\" + imageName;
        }


        public static string GetThumbnailImagePath(int id, string resourcesDir, string thumbnailName)
        {
            var imageName = GetThumbnailImageName(thumbnailName);

            var generatedThumbnailPath = GetThumbnailImagePath(GetThumbnailsDir(id), imageName);
            if (File.Exists(generatedThumbnailPath))
            {
                return generatedThumbnailPath;
            }

            return GetThumbnailImagePath(GetFullPath(resourcesDir), imageName);
        }


        public static string GetThumbnailImagePath(string resourcesDir, string fileName)
        {
            return Path.Combine(resourcesDir, fileName);
        }


        public static string GetThumbnailImageName(string fileName)
        {
            return String.IsNullOrWhiteSpace(fileName)
                ? "Thumbnail.jpg"
                : fileName;
        }


        public static string GetThumbnailsDir(int diloId)
        {
            var thubnailsDirPath = String.Format("Resources\\Thumbnails\\{0}", diloId);

            try
            {
                Directory.CreateDirectory(thubnailsDirPath);
            }
            catch (Exception ex)
            {
                ShowErrors(new List<string>() { ex.Message });

                return null;
            }

            return thubnailsDirPath;
        }
        
        /// <summary>
        /// Shows MessageBox with OK button and list of errors
        /// </summary>
        /// <param name="errors">List of displayed errors</param>
        public static void ShowErrors(IEnumerable<string> errors)
        {
            MessageBox.Show(String.Join(Environment.NewLine, errors), "Errors", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        public static T GetChildOfType<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }


        public static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);

            var numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;

                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }

                if (child != null)
                {
                    break;
                }
            }

            return child;
        }


        public static IEnumerable<T> GetVisualChildren<T>(DependencyObject parent) where T : Visual
        {
            List<T> children = new List<T>();
            T child = default(T);

            var numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;

                if (child == null)
                {
                    children.AddRange(GetVisualChildren<T>(v));
                }

                if (child != null)
                {
                    children.Add(child);
                }
            }

            return children;
        }


        public static TChildItem FindVisualChild<TChildItem>(DependencyObject obj) where TChildItem : DependencyObject
        {
            ////Debug.WriteLine(String.Format("pre ch: {0:mm:ss.fff}", DateTime.Now));

            // Search immediate children
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                if (child is TChildItem)
                {
                    ////Debug.WriteLine(String.Format("post ch 1: {0:mm:ss.fff}", DateTime.Now));
                    
                    return (TChildItem) child;
                }
                else
                {
                    var childOfChild = FindVisualChild<TChildItem>(child);

                    if (childOfChild != null)
                    {
                        ////Debug.WriteLine(String.Format("post ch 2: {0:mm:ss.fff}", DateTime.Now));

                        return childOfChild;
                    }
                }
            }

            ////Debug.WriteLine(String.Format("post ch 3: {0:mm:ss.fff}", DateTime.Now));

            return null;
        }


        public static List<T> GetVisualChildCollection<T>(object parent) where T : Visual
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }


        private static void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                else if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }


        public static DataGridColumnHeader GetColumnHeaderFromColumn(DataGrid dataGrid, DataGridColumn column)
        {
            // dataGrid is the name of your DataGrid. In this case Name="dataGrid"
            var columnHeaders = GetVisualChildCollection<DataGridColumnHeader>(dataGrid);
            foreach (var columnHeader in columnHeaders)
            {
                if (Equals(columnHeader.Column, column))
                {
                    return columnHeader;
                }
            }

            return null;
        }


        public static DataGridColumnHeader GetColumnHeaderFromColumn(IEnumerable<DataGridColumnHeader> columnHeaders, DataGridColumn column)
        {
            foreach (var columnHeader in columnHeaders)
            {
                if (Equals(columnHeader.Column, column))
                {
                    return columnHeader;
                }
            }

            return null;
        }


        public static bool DesignMode
        {
            get { return DesignerProperties.GetIsInDesignMode(new DependencyObject()); }
        }


        public static void InvokeIfRequired(this DispatcherObject control, Action methodcall, DispatcherPriority priorityForCall)
        {
            //see if we need to Invoke call to Dispatcher thread
            if (control.Dispatcher.Thread != Thread.CurrentThread)
                control.Dispatcher.Invoke(priorityForCall, methodcall);
            else
                methodcall();
        }


        public static void InvokeIfRequired(this Dispatcher control, Action methodcall)
        {
            if (control.CheckAccess())
            {
                methodcall();
            }
            else
            {
                control.Invoke(DispatcherPriority.Normal, methodcall);
            }
        }

        public static void InvokeIfRequired<T>(this Dispatcher control, Action<T> methodcall, T param)
        {
            if (control.CheckAccess())
            {
                methodcall(param);
            }
            else
            {
                control.Invoke(DispatcherPriority.Normal, methodcall, param);
            }
        }

        /// <summary>
        /// Sets a text into a clipboard
        /// </summary>
        /// <param name="text">Text pasted to a clipboard</param>
        /// <remarks>
        /// Since Clipboard.SetText can fail to CLIPBRD_E_CANT_OPEN exception, it has to be retried several times
        /// http://stackoverflow.com/questions/68666/clipbrd-e-cant-open-error-when-setting-the-clipboard-from-net
        /// </remarks>
        public static void SetClipboard(string text)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Clipboard.SetText(text);
                    return;
                }
                catch { }
                System.Threading.Thread.Sleep(10);
            } 
        }

        /// <summary>
        /// Sets a text into a clipboard
        /// </summary>
        /// <param name="text">Text pasted to a clipboard</param>
        /// <remarks>
        /// Since Clipboard.SetText can fail to CLIPBRD_E_CANT_OPEN exception, ignore it
        /// http://stackoverflow.com/questions/68666/clipbrd-e-cant-open-error-when-setting-the-clipboard-from-net
        /// (SetCliboard method may take too long to go through all the loops, this is the faster alternative)
        /// </remarks>
        public static void SetClipboardOnce(string text)
        {
            try
            {
                Clipboard.SetText(text);
            }
            catch { }
        }
    }
}
