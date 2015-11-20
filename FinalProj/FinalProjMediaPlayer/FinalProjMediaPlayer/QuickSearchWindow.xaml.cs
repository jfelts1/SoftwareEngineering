using System.Windows.Input;

namespace FinalProjMediaPlayer
{
    /// <summary>
    /// Interaction logic for QuickSearchWindow.xaml
    /// </summary>
    public partial class QuickSearchWindow
    {
        public QuickSearchWindow(MainWindow window)
        {
            InitializeComponent();
            _mainWindow = window;
        }
        private readonly MainWindow _mainWindow;

        private void QuickSearchWindow1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                _mainWindow.closeQuickSearchWindow();
            }
        }
    }
}
