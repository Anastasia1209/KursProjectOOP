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

namespace WPF_TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        private AppContext db = new AppContext();


        Hotel curHotel;
        User curUser;

        DateTime dateT;
        int night;
        int person;

        public BookingWindow(User user, Hotel hotel, DateTime date, int nightKol, int personKol)
        {
            InitializeComponent();
            curUser = user;
            curHotel = hotel;

            dateT = date;
            night = nightKol;
            person = personKol;

            NameUser.Text = curUser.name;
            Surname.Text = curUser.surname;
            Passport.Text = curUser.passport;
            Email.Text = curUser.email;
        }


        private void Button_Book_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = new Booking
            {
                idHotel = curHotel.idHotel,
                id = curUser.id,
                dateBoking = dateT,
                numNights = night,
                numPerson = person,
                finalPrice = curHotel.price * person * night
            };

            db.Bookings.Add(booking);
            db.SaveChanges();

        }
    }
}
