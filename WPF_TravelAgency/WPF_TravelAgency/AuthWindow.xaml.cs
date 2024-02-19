using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPF_TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {

            string email = textBoxEmail.Text.Trim().ToLower();
            string password = passBox.Text.Trim();

            string adminEmail = "aaa@aa.a";
            string adminPass = "admin11";

            if (email == adminEmail && password == adminPass)
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                /*UserPage userPage = new UserPage();
                userPage.Show();*/
                Close();
            }
            else
            {

                if (password == string.Empty || !email.Contains("@"))
                {

                    passBox.ToolTip = "Некорректные данные";

                    MessageBox.Show("Некорректные данные");

                }
                else
                {
                    textBoxEmail.Background = Brushes.White;
                    passBox.Background = Brushes.White;


                    //  MessageBox.Show("Успешно");
                    //находим пользоваеля
                    User authUser = null;
                    using (AppContext db = new AppContext())
                    {
                        authUser = db.Users.Where(b => b.email == email && b.password == password).FirstOrDefault();

                    }
                    if (authUser != null)
                    {
                        MessageBox.Show("Все хорошо!");
                        /* UserPage userPage = new UserPage();
                         userPage.Show();*/
                        FirstPageWindow firstPage = new FirstPageWindow(authUser);
                        firstPage.Show();
                        Close();

                    }
                    else
                        MessageBox.Show("Некорректные данные");
                }

            }

        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();

        }
    }
}
