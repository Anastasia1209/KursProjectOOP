using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Xml;
using WPF_TravelAgency.Properties;

namespace WPF_TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        User currentUser = null;

        AppContext db = new AppContext();

        public UserInfoWindow(User curUser)
        {
            currentUser = curUser;
            InitializeComponent();

            NameUser.Text = currentUser.name;
            Surname.Text = currentUser.surname;
            Passport.Text = currentUser.passport;
            Email.Text = currentUser.email;
            Password.Text = currentUser.password;

        }
        public UserInfoWindow() { }

        private void Button_Main_Click(object sender, RoutedEventArgs e)
        {
            FirstPageWindow firstWindow = new FirstPageWindow(currentUser);
            firstWindow.Show();
            Close();

        }

        private void Button_Like_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Del_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Удалить аккаунт?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (AppContext db = new AppContext())
                {
                    //var account = db.Users.FirstOrDefault(u => u.name == Name);
                    var account = db.Users.Where(temp => temp.email == currentUser.email).FirstOrDefault();

                    if (account != null)
                    {
                        int userID = account.id;
                        db.Users.Remove(account);
                        db.SaveChanges();
                    }
                }
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неуспешно..");
            }




        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var userDB = db.Users.Where(temp => temp.id == currentUser.id).FirstOrDefault(); 


                userDB.name = NameUser.Text;
                userDB.surname = Surname.Text;
                userDB.passport = Passport.Text;
                userDB.email = Email.Text;
                userDB.password = Password.Text;
                
                db.SaveChanges();
                currentUser = userDB;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("успех");

            }




        }

        private void Button_Book_Click(object sender, RoutedEventArgs e)
        {

            if (db.Bookings.Any(temp => temp.id == currentUser.id))
            {
                CheckWindow checkWindow = new CheckWindow(currentUser);
                checkWindow.Show();
            }
            else
            {
                MessageBox.Show("У вас нет броней");
            }


        }
    }



}

