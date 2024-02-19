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

namespace WPF_TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для FirstPageWindow.xaml
    /// </summary>
    public partial class FirstPageWindow : Window
    {

        User currentUser = null;
        private AppContext db = new AppContext();

        ObservableCollection<Hotel> lovelyHotels;

        public FirstPageWindow(User curUser)
        {
            currentUser = curUser;
            InitializeComponent();

            ComboCountry.ItemsSource = db.Hotels.Select(temp => temp.country).ToHashSet().ToArray();
        }
        public FirstPageWindow()
        {
            InitializeComponent();

            ComboCountry.ItemsSource = db.Hotels.Select(temp => temp.country).ToHashSet().ToArray();
        }

        public FirstPageWindow(ObservableCollection<Hotel> lovely)
        {
            InitializeComponent();
            lovelyHotels = lovely;
            ComboCountry.ItemsSource = db.Hotels.Select(temp => temp.country).ToHashSet().ToArray();
        }

        private void Button_Use_Click(object sender, RoutedEventArgs e)
        {
            UserInfoWindow userInfo = new UserInfoWindow(currentUser);
            userInfo.Show();
            Close();

        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            if (ComboCountry.Text == string.Empty  || Night.Text == string.Empty || Persons.Text == string.Empty)
            {
                MessageBox.Show("Заполните все поля");
            }
            else { 

            CountryWindow countryWindow = new CountryWindow(currentUser, ComboCountry.Text, (DateTime)Calendar.SelectedDate , int.Parse(Night.Text), int.Parse(Persons.Text));
            countryWindow.Show();
            Close();
            }
        }

        private void Show_Lovely(object sender, RoutedEventArgs e)
        {
            First2Window love = new First2Window(lovelyHotels);
        }
    }
}
