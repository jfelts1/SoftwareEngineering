using System.Windows.Input;

namespace FinalProjMediaPlayer
{
    /// <summary>
    /// Interaction logic for AdvancedSearchWindow.xaml
    /// </summary>
    public partial class AdvancedSearchWindow
    {
        private const string DefaultArtistTextBoxText = "Enter artist to search for";
        private const string DefaultGenreTextBoxText = "Enter genre to search for";
        public AdvancedSearchWindow(MainWindow window)
        {
            InitializeComponent();
            _mainWindow = window;
            TextBoxAdvancedSearchWindowArtist.Text = DefaultArtistTextBoxText;
            TextBoxAdvancedSearchWindowGenre.Text = DefaultGenreTextBoxText;
        }

        private readonly MainWindow _mainWindow;

        private void AdvancedSearchWindow1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                _mainWindow.closeAdvancedSearchWindow();
            }
            if (e.Key == Key.Enter)
            {
                ButtonAdvancedSearchWindowSearch_Click(sender,e);
            }
        }

        private void ButtonAdvancedSearchWindowSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TextBoxAdvancedSearchWindowGenre.Text == DefaultGenreTextBoxText && TextBoxAdvancedSearchWindowArtist.Text != DefaultArtistTextBoxText)
            {
                _mainWindow.searchDatabase(TextBoxAdvancedSearchWindowArtist.Text,null);
            }
            else if (TextBoxAdvancedSearchWindowGenre.Text != DefaultGenreTextBoxText &&
                     TextBoxAdvancedSearchWindowArtist.Text == DefaultArtistTextBoxText)
            {
                _mainWindow.searchDatabase(null,TextBoxAdvancedSearchWindowGenre.Text);
            }
            else if(TextBoxAdvancedSearchWindowGenre.Text!= DefaultGenreTextBoxText && TextBoxAdvancedSearchWindowArtist.Text != DefaultArtistTextBoxText)
            {
                _mainWindow.searchDatabase(TextBoxAdvancedSearchWindowArtist.Text,TextBoxAdvancedSearchWindowGenre.Text);
            }

            _mainWindow.closeAdvancedSearchWindow();
        }
    }
}
