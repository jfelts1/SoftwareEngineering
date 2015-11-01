using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        private MainWindow _mainWindow;

        private void QuickSearchWindow1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                _mainWindow.closeQuickSearchWindow();
            }
        }
    }
}
