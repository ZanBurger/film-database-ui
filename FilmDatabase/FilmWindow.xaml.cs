using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static FilmDatabase.MainWindow;

namespace FilmDatabase
{
    /// <summary>
    /// Interaction logic for FilmWindow.xaml
    /// </summary>
    public partial class FilmWindow : Window
    {
        public ObservableCollection<Movie> moviesList = new ObservableCollection<Movie>();
        public MainWindow mainWindow;
        public FilmWindow()
        {
            InitializeComponent();
            releaseDate.SelectedDate = DateTime.Today;
            comboBoxGenre.ItemsSource = Properties.Settings.Default.Genre;
        }

        private void uploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            var result = openFileDialog.ShowDialog();
            if (result == true) {
                var image = new Image();
                image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                uploadImageButton.Content = image;
                actualImage = image;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

        }

        private void comboBoxGenre_DropDownOpened(object sender, EventArgs e)
        {
            comboBoxGenre.Items.Refresh();
        }

        private void newRating(object sender, RatingUserControl.SomeChangeEventArgs e)
        {
            currentRating.Text = ratingUserControl.actualRating.Text;
        }
    }
}
