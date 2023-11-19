using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace FilmDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class MoviesObservable{

            public ObservableCollection<Movie> moviesList = new ObservableCollection<Movie>();

        }
        public MoviesObservable movies = new MoviesObservable();
        
        public Movie selectedMovie { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            removeMenuItem.IsEnabled = false;
            comboBoxGenre.ItemsSource = Properties.Settings.Default.Genre;

            // Icons for Menu Items
            fileMenuItem.Icon = new Image {
                Source = new BitmapImage(new Uri("Icons/iconFile.png", UriKind.Relative))
            };
            filmsMenuItem.Icon = new Image
            {
                Source = new BitmapImage(new Uri("Icons/iconFilm.png", UriKind.Relative))
            };
            toolsMenuItem.Icon = new Image
            {
                Source = new BitmapImage(new Uri("Icons/iconTools.png", UriKind.Relative))
            };
            addMenuItem.Icon = new Image
            {
                Source = new BitmapImage(new Uri("Icons/iconAdd.png", UriKind.Relative))
            };
            removeMenuItem.Icon = new Image
            {
                Source = new BitmapImage(new Uri("Icons/iconRemove.png", UriKind.Relative))
            };
            exitMenuItem.Icon = new Image
            {
                Source = new BitmapImage(new Uri("Icons/iconExit.png", UriKind.Relative))
            };
            optionMenuItem.Icon = new Image
            {
                Source = new BitmapImage(new Uri("Icons/iconOptions.png", UriKind.Relative))
            };

            // ListView DataBinding
            DateOnly d1 = new DateOnly(2020, 12, 5);
            DateOnly d2 = new DateOnly(2016, 8, 26);
            DateOnly d3 = new DateOnly(2019, 7, 19);
            
            var image1 = new BitmapImage(new Uri("https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx131573-rpl82vDEDRm6.jpg", UriKind.Absolute));
            movies.moviesList.Add(new Movie("Jujutsu Kaisen 0", d1, "Movie Description of JJK", image1, "Action", "actor1, actor43", "Completed", "120", "None", false, 0));
            var image2 = new BitmapImage(new Uri("https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx21519-XIr3PeczUjjF.png", UriKind.Absolute));
            movies.moviesList.Add(new Movie("Your Name", d2, "Movie Description of Your Name", image2, "Romance", "actor43, actor323, actor411", "Completed", "130", "Cool tag, another cool tag", false, 0));
            var image3 = new BitmapImage(new Uri("https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx106286-5COcpd0J9VbL.png", UriKind.Absolute));
            movies.moviesList.Add(new Movie("Weathering With You", d3, "Movie Description of Weathering With You", image3, "Romance", "unknown actors", "Completed", "110", "Very cool tag", false, 0));
            var image4 = new BitmapImage(new Uri("https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx20954-UMb6Kl7ZL8Ke.jpg", UriKind.Absolute));
            movies.moviesList.Add(new Movie("A Silent Voice", d1, "Movie Description of Silent Voice", image4, "Romance", "actor32, actor12, actor04", "Completed", "120", "None", false, 0));
           
            listOfFilms.ItemsSource = movies.moviesList;

        }

        // Movie Class //       
        public class Movie : INotifyPropertyChanged {
            string name;
            DateOnly releaseDate;
            string description;
            [XmlIgnore]
            ImageSource image;
            string genre;
            string actors;
            string status;
            string length;
            string tags;
            int rating;

            private bool _isFavorite;
            private int _rating;
            private ImageSource _image;

            public event PropertyChangedEventHandler PropertyChanged;

            public Movie() { 
            
            }
            
            // ON PROPERTY CHANGED METHOD //
            protected virtual void OnPropertyChanged(string propertyName) {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public Movie(string name, DateOnly releaseDate, string description, ImageSource image, string genre, string actors, string status, string length, string tags, bool isFavorite, int rating)
            {
                this.name = name;
                this.releaseDate = releaseDate;
                this.description = description;
                this.image = image;
                this.genre = genre;
                this.actors = actors;
                this.status = status;
                this.length = length;
                this.tags = tags;
                this.rating = rating;
            }    

            public string Name { get { return name; } set { name = value; } }
            public DateOnly ReleaseDate { get { return releaseDate; } set { releaseDate = value; } }
            public string Description { get { return description; } set { description = value; } }
            [XmlIgnore]
            public ImageSource Image { get { return image; } set { image = value; OnPropertyChanged("Image"); } }
            public string Genre { get { return genre; } set { genre = value; } }
            public string Actors { get { return actors; } set { actors = value; } }
            public string Status { get { return status; } set { status = value; } }
            public string Length { get { return length; } set { length = value; } }
            public string Tags { get { return tags; } set { tags = value; } }
            public bool IsFavorite { get { return _isFavorite; } set { _isFavorite = value; OnPropertyChanged("IsFavorite"); } }

            public int Rating { get { return _rating; } set { _rating = value; OnPropertyChanged("Rating"); } }

        }

        private void ExitProgram(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddEntry(object sender, RoutedEventArgs e)
        {
            var filmWindow = new FilmWindow();
            DateOnly releaseDate;
            if (filmWindow.ShowDialog() == true) {
                releaseDate = new DateOnly(filmWindow.releaseDate.SelectedDate.Value.Year, filmWindow.releaseDate.SelectedDate.Value.Month,
                                    filmWindow.releaseDate.SelectedDate.Value.Day);

                movies.moviesList.Add(new Movie(filmWindow.movieName.Text, releaseDate, filmWindow.description.Text, filmWindow.actualImage.Source, filmWindow.comboBoxGenre.Text, filmWindow.actors.Text, filmWindow.comboBoxStatus.Text, filmWindow.length.Text, filmWindow.tags.Text, false, int.Parse(filmWindow.currentRating.Text)));
            }
        }

        private void RemoveEntry(object sender, RoutedEventArgs e)
        {
            movies.moviesList.RemoveAt(listOfFilms.SelectedIndex);    
        }

        private void listOfFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (listOfFilms.SelectedItem is Movie m) {
                comboBoxGenre.DataContext = m;
            }
            
            if (listOfFilms.SelectedItems.Count <= 0)
            {
                removeMenuItem.IsEnabled = false;
            }
            else {
                removeMenuItem.IsEnabled = true;
            }
        }

        private void OpenOptions(object sender, RoutedEventArgs e) {
            var optionsWindow = new OptionsWindow();
            optionsWindow.Show();
        }

        FilmWindow filmWindow = null;
        private void listOfFilms_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e) { 
        
            var SelectedItem = (Movie)listOfFilms.SelectedItem;

            filmWindow = new FilmWindow();
            filmWindow.DataContext = SelectedItem;
            filmWindow.Show();

        }

        private void buttonFavorite_Click(object sender, RoutedEventArgs e)
        {
            Movie selectedMovie = (Movie)listOfFilms.SelectedItem;
         
            selectedMovie.IsFavorite = !selectedMovie.IsFavorite;              
        }

        private void comboBoxGenre_DropDownOpened(object sender, EventArgs e)
        {
            comboBoxGenre.Items.Refresh();
        }

        private void ImportFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*xml)|*xml";
            if (openFileDialog.ShowDialog() == true) {
                using (TextReader textReader = new StreamReader(openFileDialog.FileName))
                {
                    XmlSerializer xmlSer = new XmlSerializer(typeof(ObservableCollection<Movie>));
                    movies.moviesList = (ObservableCollection<Movie>) xmlSer.Deserialize(textReader);
                    listOfFilms.ItemsSource = movies.moviesList;
                }
            }
        }
        private void ExportFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*xml)|*xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (TextWriter textWriter = new StreamWriter(saveFileDialog.FileName))
                {
                    XmlSerializer xmlSer = new XmlSerializer(typeof(ObservableCollection<Movie>));
                    xmlSer.Serialize(textWriter, movies.moviesList);
                }
            }
        }

        private void newRating(object sender, RatingUserControl.SomeChangeEventArgs e)
        {
            Movie selectedMovie = (Movie)listOfFilms.SelectedItem;
            selectedMovie.Rating = int.Parse(ratingUserControl.actualRating.Text); 
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            SearchMovies(search.Text);

        }

        private void SearchMovies(string searchString)
        {
            // Filter the movies list to only show movies that contain the search string in their title
            var filteredMovies = movies.moviesList.Where(movie => movie.Name.Contains(searchString)).ToList();
            // Update the listview to display the filtered movies
            listOfFilms.ItemsSource = filteredMovies;
        }

        private void SortAlphabetically(object sender, RoutedEventArgs e)
        {
            var moviesA = listOfFilms.ItemsSource as ObservableCollection<Movie>;
            var sortedMovies = new ObservableCollection<Movie>(moviesA.OrderBy(x => x.Name));
            listOfFilms.ItemsSource = sortedMovies;
        }

        private void movieImage_MouseEnter(object sender, MouseEventArgs e)
        {
            movieImage.Margin = new Thickness(0, 0, 0, 0);
            movieImage.Opacity = 0;
        }
    }
}
