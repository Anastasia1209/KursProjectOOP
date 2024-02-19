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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        public UserPage()
        {
            InitializeComponent();

            AppContext db = new AppContext();
            List<User> users = db.Users.ToList();

            listOfUsers.ItemsSource = users;
        }
/*        private void UserListSelectionChange(object sender, SelectedCellsChangedEventArgs e)
        {
            var selectionItem = (sender as ListView).SelectedItem as User;

            if(selectionItem != null)
            {
               *//* UserPage accPage = new UserPage(selectionItem.Name);
                accPage.Show();
                Hide();*//*
            }
        }*/
    }
}
