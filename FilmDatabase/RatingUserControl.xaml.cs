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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FilmDatabase.MainWindow;

namespace FilmDatabase
{
    /// <summary>
    /// Interaction logic for RatingUserControl.xaml
    /// </summary>
    
    public partial class RatingUserControl : UserControl
    {
        
        public class SomeChangeEventArgs : System.EventArgs
        {
            public string TextValue { get; set; }
        }

        public event EventHandler<SomeChangeEventArgs> SomethingHasChanged;
        public RatingUserControl()
        {
            InitializeComponent();
            actualRating.Visibility = Visibility.Hidden;
        }

        
        private void toggleStar1_Click(object sender, RoutedEventArgs e)
        {
            actualRating.Text = "1";
            this.SomethingHasChanged?.Invoke(
            this, new SomeChangeEventArgs()
            {
                TextValue = this.actualRating.Text              

            }
         ); 
      }

        private void toggleStar2_Click(object sender, RoutedEventArgs e)
        {
            actualRating.Text = "2";
            this.SomethingHasChanged?.Invoke(
            this, new SomeChangeEventArgs()
            {
                TextValue = this.actualRating.Text

            }
         );
        }
        private void toggleStar3_Click(object sender, RoutedEventArgs e)
        {
            actualRating.Text = "3";
            this.SomethingHasChanged?.Invoke(
            this, new SomeChangeEventArgs()
            {
                TextValue = this.actualRating.Text

            }
         );
        }
        private void toggleStar4_Click(object sender, RoutedEventArgs e)
        {
            actualRating.Text = "4";
            this.SomethingHasChanged?.Invoke(
            this, new SomeChangeEventArgs()
            {
                TextValue = this.actualRating.Text

            }
         );
        }
        private void toggleStar5_Click(object sender, RoutedEventArgs e)
        {
            actualRating.Text = "5";
            this.SomethingHasChanged?.Invoke(
            this, new SomeChangeEventArgs()
            {
                TextValue = this.actualRating.Text

            }
         );
        }

    }
}
