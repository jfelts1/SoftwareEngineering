using System.Windows.Input;

namespace FinalProjMediaPlayer
{
    /// <summary>
    /// Interaction logic for AdvancedSearchWindow.xaml
    /// </summary>
    public partial class AdvancedSearchWindow
    {
        public AdvancedSearchWindow(MainWindow window)
        {
            InitializeComponent();
            _mainWindow = window;
        }

        private readonly MainWindow _mainWindow;

        private void AdvancedSearchWindow1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                _mainWindow.closeAdvancedSearchWindow();
            }
        }
    }
}
