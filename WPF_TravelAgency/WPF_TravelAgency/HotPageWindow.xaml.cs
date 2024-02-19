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
using WPF_TravelAgency.Services;

namespace WPF_TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для HotPageWindow.xaml
    /// </summary>
    public partial class HotPageWindow : Window
    {
        Hotel hotel;
        User curUser;
        ObservableCollection<Comment> commetMass;

  //      ObservableCollection<Hotel> lovelyHotels = new ObservableCollection<Hotel>();

        AppContext db = new AppContext();

        DateTime dateT;
        int night;
        int person;

        public HotPageWindow(User user, Hotel current, DateTime date, int nightKol, int personKol)
        {
            InitializeComponent();
            CommandBinding binding = new CommandBinding(Commands.Visible);
            binding.Executed += CommandBinding_Executed;
            gridVis.Visibility = Visibility.Collapsed;
            this.CommandBindings.Add(binding);
            hotel = current;
            curUser = user;

            ByteToImg byteTo = new ByteToImg();

            dateT = date;
            night = nightKol;
            person = personKol;

            titleBlock.Text = hotel.title;
            cityBlock.Text = hotel.city;
            descriptionBlock.Text = hotel.descriptionHotel;
            photoHotel.Source = byteTo.ToImage(hotel.photo);

            commetMass = new ObservableCollection<Comment>(db.Comments.Where(p => p.idHotel == hotel.idHotel).ToList());

            commentsList.Items.Clear();
            commentsList.ItemsSource = commetMass;


        }

        private void Button_Book_CLick(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow(curUser, hotel, dateT, night, person);
            bookingWindow.Show();

        }
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (gridVis.Visibility == Visibility.Collapsed)
                gridVis.Visibility = Visibility.Visible;
            else
                gridVis.Visibility = Visibility.Collapsed;
        }

        private void Button_Com_CLick(object sender, RoutedEventArgs e)
        {
            CommentsWindow commentsWindow = new CommentsWindow(curUser, hotel);
            commentsWindow.Show();

        }

       
    }
}
