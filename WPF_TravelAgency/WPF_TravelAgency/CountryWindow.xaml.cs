using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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

namespace WPF_TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для CountryWindow.xaml
    /// </summary>
    public partial class CountryWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Hotel> Hotels { get; set; }
        private AppContext db = new AppContext();
        Hotel hotelUnit;
        ObservableCollection<Hotel> lovelyHotels = new ObservableCollection<Hotel>();

        User curUser;

        string country;
        DateTime date;
        int night;
        int person;

        public CountryWindow()
        {
            InitializeComponent();


            Hotels = new ObservableCollection<Hotel>(db.Hotels);

            hotelList.ItemsSource = Hotels;
            Show_Country();
        }

        public CountryWindow(User user, string countr, DateTime dat, int nigh, int perso)
        {
            InitializeComponent();

            country = countr;
            date = dat;
            night = nigh;
            person = perso;
            curUser = user;
            Show_Country();
        }

        private void Filter_Cost(object sender, RoutedEventArgs e)
        {
            Hotels = new ObservableCollection<Hotel>(Hotels.OrderBy(p => p.price));
            hotelList.ItemsSource = Hotels;
        }


        private void Filter_Rating(object sender, RoutedEventArgs e)
        {
            Hotels = new ObservableCollection<Hotel>(Hotels.OrderBy(p => p.stars));
            hotelList.ItemsSource = Hotels;
        }



        private void Show_Country()
        {
            Hotels = new ObservableCollection<Hotel>(db.Hotels.Where(p => p.country == country && p.numRooms > person));
            hotelList.ItemsSource = Hotels;
        }



        private void Button_Hotel_CLick(object sender, RoutedEventArgs e)
        {

            hotelUnit = hotelList.SelectedItem as Hotel;

            if (hotelUnit is null)
            {
                MessageBox.Show("Выберите отель!");
            }
            else
            {
                lovelyHotels.Add(hotelUnit);
                HotPageWindow hotel = new HotPageWindow(curUser, hotelUnit, date, night, person);
                hotel.Show();

            }


        }

        private void Button_Main_Click(object sender, RoutedEventArgs e)
        {
            FirstPageWindow firstPage = new FirstPageWindow(curUser);
            firstPage.Show();
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
