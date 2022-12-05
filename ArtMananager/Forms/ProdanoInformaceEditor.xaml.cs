/* (C) 2016 Premysl Fara */

namespace ArtMananager.Forms
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using ArtMananager.DataObjects;


    /// <summary>
    /// Interaction logic for ProdanoInformaceEditor.xaml
    /// </summary>
    public partial class ProdanoInformaceEditor : Window
    {   
        #region properties

        public DialogResultStateType DialogResultState { get; private set; }


        public Dilo DataObject
        {
            get { return DataContext as Dilo; }
        }

        public GlobalDataObject Gdo { get; set; }

        #endregion


        public ProdanoInformaceEditor(GlobalDataObject gdo)
        {
            Gdo = gdo;

            InitializeComponent();

            if (!UIHelper.DesignMode)
            {
                _prodejMenaSingleSelect.ItemsSource = Gdo.MenaCollection;
                _prodanoKdeSingleSelect.ItemsSource = Gdo.ProdejniMistoCollection;
            }

            DialogResultState = DialogResultStateType.Ok;
        }


        #region public methods

        public static bool Open(Window owner, GlobalDataObject gdo, Dilo dataObject)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new ProdanoInformaceEditor(gdo)
            {
                DataContext = dataObject,
                Owner = owner,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
               
            dialog.ShowDialog();

            return dialog.DialogResult.GetValueOrDefault();
        }

        #endregion


        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (SaveClick())
            {
                Close();
            }
        }


        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            CancelClick();
            Close();
        }


        private bool SaveClick()
        {
            // TODO: Castecna validace?

            DialogResult = true;
            DialogResultState = DialogResultStateType.Ok;

            return true;
        }


        private void CancelClick()
        {
            DialogResult = false;
            DialogResultState = DialogResultStateType.Cancel;
        }


        private void EditMena_OnClick(object sender, RoutedEventArgs e)
        {
            UIHelper.EditMena(this, Gdo, _prodejMenaSingleSelect.SelectedItem, false, false);
        }

        private void AddMena_OnClick(object sender, RoutedEventArgs e)
        {
            UIHelper.EditMena(this, Gdo, null, true);
        }


        private void EditProdejniMisto_OnClick(object sender, RoutedEventArgs e)
        {
            UIHelper.EditProdejniMisto(this, Gdo, _prodanoKdeSingleSelect.SelectedItem, false, false);
        }

        private void AddProdejniMisto_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = new ProdejniMisto();
            if (ProdejniMistoEditor.Open(obj))
            {
                Gdo.SaveProdejniMisto(obj);
                Gdo.UpdateProdejniMistoCollection();
            }
        }


        private void ProdanoInformaceEditor_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }


        private void _prodejCena_OnLostFocus(object sender, RoutedEventArgs e)
        {
            // zneviditelnit
            _prodejCena.Visibility = Visibility.Hidden;

            // zviditelnit _dnesniCenaView
            _prodejCenaView.Visibility = Visibility.Visible;
        }

        private void _prodejCenaView_OnGotFocus(object sender, RoutedEventArgs e)
        {
            // zneviditelnit
            _prodejCenaView.Visibility = Visibility.Hidden;

            // zviditelnit _prodejCena
            _prodejCena.Visibility = Visibility.Visible;

            // focus do _prodejCena
            _prodejCena.Focus();
        }
    }
}
