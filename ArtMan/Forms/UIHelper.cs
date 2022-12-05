/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms
{
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;


    public static class UIHelper
    {
        public static bool IsValidUrl(string url)
        {
            if (String.IsNullOrWhiteSpace(url)) return false;

            try
            {
                Uri uriResult;

                return Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
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
