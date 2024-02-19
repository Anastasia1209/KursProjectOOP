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

namespace WPF_TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext db;

        public MainWindow()
        {
            InitializeComponent();
            db = new AppContext();
        }
        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text.Trim();
            string surname = textBoxSurname.Text.Trim();
            string email = textBoxEmail.Text.Trim().ToLower();
            string passport = textBoxPassport.Text.Trim();
            string password = passBox.Text.Trim();
            string pass2 = passBox2.Text.Trim();

            if (password.Length < 6 || password.Length > 12 || password != pass2 || name == string.Empty || surname == string.Empty || !email.Contains("@") || !email.Contains("."))
            {
                
                passBox.ToolTip = "Некорректные данные";
                
                passBox2.ToolTip = "Некорректные данные";
                
                MessageBox.Show("Проверьте данные");
            }
            else
            {
                textBoxName.Background = Brushes.White;
                textBoxSurname.Background = Brushes.White;
                textBoxEmail.Background = Brushes.White;
                passBox.Background = Brushes.White;
                passBox2.Background = Brushes.White;

                MessageBox.Show("Успешно");

                User user = new User
                {
                    name = name,
                    surname = surname,
                    passport = passport,
                    email = email,
                    password = password
                };

                db.Users.Add(user);
                db.SaveChanges();

                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                Close();
            }

            /* 
             if (pass.Length > 6)
             {
                 passBox.ToolTip = "Некорректные данные";
                 passBox.Background = Brushes.LightSalmon;
             }
             else
             {
                 passBox.Background = Brushes.White;
             }
             if (pass != pass2)
             {
                 passBox2.ToolTip = "Некорректные данные";
                 passBox2.Background = Brushes.LightSalmon;
             }
             else
             {
                 passBox2.Background = Brushes.White;
             }*/

        }

        private void Button_Win_Auth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
            /* UserPage userPage = new UserPage();
             userPage.Show();
             Close();*/
        }
    }
}
