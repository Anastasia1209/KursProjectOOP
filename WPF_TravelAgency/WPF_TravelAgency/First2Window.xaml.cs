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
    /// Логика взаимодействия для FirstWindow.xaml
    /// </summary>
    public partial class First2Window : Window
    {

        ObservableCollection<Hotel> Hotels;
        public First2Window(ObservableCollection<Hotel> hot)
        {
            InitializeComponent();
          //  Hotels = new ObservableCollection<Hotel>(hot);

          //  hotelList.ItemsSource = Hotels;
        }
    }
}
