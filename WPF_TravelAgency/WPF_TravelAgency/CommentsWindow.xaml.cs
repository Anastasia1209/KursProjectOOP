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
    /// Логика взаимодействия для CommentsWindow.xaml
    /// </summary>
    public partial class CommentsWindow : Window
    {
        User curUser;
        Hotel curHotel;

        AppContext db = new AppContext();

        public CommentsWindow(User user, Hotel hotels)
        {
            InitializeComponent();

            curUser = user;
            curHotel = hotels;
        }

        private void Button_Com_Click(object sender, RoutedEventArgs e)
        {
            TextRange txtDesc = new TextRange(CommentText.Document.ContentStart, CommentText.Document.ContentEnd);
            Comment commentUnit = new Comment
            {
                id = curUser.id,
                idHotel = curHotel.idHotel,
                text = txtDesc.Text
            };

            db.Comments.Add(commentUnit);
            db.SaveChanges();
            
        }
    }
}
