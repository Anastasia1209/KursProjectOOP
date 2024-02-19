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
    /// Логика взаимодействия для CheckWindow.xaml
    /// </summary>
    public partial class CheckWindow : Window
    {
        AppContext db = new AppContext();
        User currentUser;
        public CheckWindow()
        {
            InitializeComponent();
            db = new AppContext();
        }
        public CheckWindow(User user)
        {
            InitializeComponent();
            currentUser = user;

            Check.Text = BookingCheck();
        }

        public string BookingCheck()
        {
            Booking curBooking = db.Bookings.Where(p => p.User.name == currentUser.name).FirstOrDefault();
            Hotel curHotel = curBooking.Hotel;

            string check = currentUser.name + currentUser.surname + "\n" +
                curHotel.title + "\n" +
                curHotel.city + curHotel.country + "\n" +
                curBooking.dateBoking + curBooking.numNights + "\n" +
                curBooking.numPerson + "\n" +
                curBooking.finalPrice;
            return check;

        }

        private void Button_Del_Booking_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Booking delBooking = db.Bookings.Where(temp => temp.User.email == currentUser.email).FirstOrDefault();

               
                if (delBooking != null)
                {
                    db.Bookings.Remove(delBooking);
                }

                db.SaveChanges();
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
