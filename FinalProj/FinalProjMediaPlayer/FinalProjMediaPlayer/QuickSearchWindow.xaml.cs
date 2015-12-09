using System.Windows.Input;

namespace FinalProjMediaPlayer
{
    /// <summary>
    /// Interaction logic for QuickSearchWindow.xaml
    /// </summary>
    public partial class QuickSearchWindow
    {
        private const string DefaultTextBoxtext = "Enter name of a artist to search for";
        public QuickSearchWindow(MainWindow window)
        {
            InitializeComponent();
            TextBoxQuickSearchWindowSearchTextBox.Text = DefaultTextBoxtext;
            _mainWindow = window;
        }
        private readonly MainWindow _mainWindow;

        private void QuickSearchWindow1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                _mainWindow.closeQuickSearchWindow();
            }
            if (e.Key == Key.Enter)
            {
                ButtonQuickSearchWindowSearch_Click(sender,e);
            }
        }

        private void ButtonQuickSearchWindowSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TextBoxQuickSearchWindowSearchTextBox.Text != DefaultTextBoxtext)
            {
                _mainWindow.searchDatabase(TextBoxQuickSearchWindowSearchTextBox.Text,null);
            }
            _mainWindow.closeQuickSearchWindow();
        }
    }
}
