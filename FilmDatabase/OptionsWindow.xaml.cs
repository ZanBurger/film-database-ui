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

namespace FilmDatabase
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
            listOfGenres.ItemsSource = Properties.Settings.Default.Genre;
            removeGenre.IsEnabled = false;
            editGenre.IsEnabled = false;
        }

        private void addGenre_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Genre.Add(newGenreTextBox.Text);
            Properties.Settings.Default.Save();
            listOfGenres.Items.Refresh();
            newGenreTextBox.Clear();
        }

        private void removeGenre_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Genre.RemoveAt(listOfGenres.SelectedIndex);
            Properties.Settings.Default.Save();
            listOfGenres.Items.Refresh();
        }

        private void editGenre_Click(object sender, RoutedEventArgs e)
        {
            newGenreTextBox.Text = listOfGenres.SelectedValue.ToString();
            Properties.Settings.Default.Genre.RemoveAt(listOfGenres.SelectedIndex);
        }

        private void listOfGenres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listOfGenres.SelectedItems.Count <= 0)
            {
                removeGenre.IsEnabled = false;
                editGenre.IsEnabled = false;
            }
            else {
                removeGenre.IsEnabled = true;
                editGenre.IsEnabled = true;
            }
        }
    }
}
