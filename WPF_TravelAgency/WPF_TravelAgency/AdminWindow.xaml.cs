using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SQLite;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private AppContext db = new AppContext();
        bool photoLoaded = false;

        public AdminWindow()
        {
            InitializeComponent();
            //  db = new AppContext();
            (tableBooking.Columns[3] as DataGridTextColumn).Binding.StringFormat = "dd:mm:yyyy";

            Show();
        }

        private OpenFileDialog openFileDialog;
        private Uri fileUri;

        private byte[] B64Encode()
        {
            byte[] imageBytes;
            using (FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    imageBytes = reader.ReadBytes((int)stream.Length);
                }
            }

            return imageBytes;
        }

        private void Button_Main_Click(object sender, RoutedEventArgs e)
        {
            FirstPageWindow firstWindow = new FirstPageWindow();
            firstWindow.Show();
            Close();
        }

        private void Button_Photo_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image|*.jpg;*.jpeg;*.png;";
            if (openFileDialog.ShowDialog() == true)
            {
                fileUri = new Uri(openFileDialog.FileName);
                photoLoaded = true;
            }
        }

        private void Show()
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var query = from hotel in db.Hotels
                                select new
                                {
                                    hotel.idHotel,
                                    hotel.title,
                                    hotel.country,
                                    hotel.city,
                                    hotel.descriptionHotel,
                                    hotel.stars,
                                    hotel.photo,
                                    hotel.price,
                                    hotel.numRooms

                                };

                    tableHotels.ItemsSource = query.ToList();

                    var queryUs = from user in db.Users
                                  select new
                                  {
                                      user.id,
                                      user.name,
                                      user.surname,
                                      user.passport,
                                      user.email,
                                      user.password
                                  };
                    tableUser.ItemsSource = queryUs.ToList();

                    var queryBook = from booking in db.Bookings
                                    select new
                                    {
                                        booking.idBooking,
                                        booking.idHotel,
                                        booking.id,
                                        booking.dateBoking,
                                        booking.numNights,
                                        booking.numPerson,
                                        booking.finalPrice
                                    };
                    tableBooking.ItemsSource = queryBook.ToList();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            string nameHot = Title.Text;
            string country = Country.Text;
            string adress = City.Text;
            TextRange txtRich = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd);
            string description = txtRich.Text;
            int stars = int.Parse(Stars.Text);
            byte[] photo = B64Encode();
            int price = int.Parse(Price.Text);
            int numRooms = int.Parse(NumRooms.Text);
            if (nameHot != String.Empty || country != String.Empty || adress != String.Empty || description != String.Empty)
            {
                MessageBox.Show("Успешно");

                Hotel hotel = new Hotel
                {
                    title = nameHot,
                    country = country,
                    city = adress,
                    descriptionHotel = description,
                    stars = stars,
                    photo = photo,
                    price = price,
                    numRooms = numRooms
                };
                db.Hotels.Add(hotel);
                db.SaveChanges();
                Show();
            }
        }

        private void Button_Del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tableHotels.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < tableHotels.SelectedItems.Count; i++)
                    {

                        int id = (int)tableHotels.SelectedItems[i].GetType().GetProperty("idHotel").GetValue(tableHotels.SelectedItems[i]);
                        Hotel delHotel = db.Hotels.Where(temp => temp.idHotel == id).FirstOrDefault();

                        if (delHotel != null)
                        {
                            db.Hotels.Remove(delHotel);
                        }
                    }
                    db.SaveChanges();
                    Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Upd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Button_Photo_Click(sender, e);
                int id = (int)tableHotels.SelectedItems[0].GetType().GetProperty("idHotel").GetValue(tableHotels.SelectedItems[0]);
                Hotel hotel = db.Hotels.Where(temp => temp.idHotel == id).FirstOrDefault();

                TextRange txtRich = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd);
                string description = txtRich.Text;

                /*if (fileUri == null)
                {
                    *//*string base64String = Convert.ToBase64String(hotel.photo);
                    string dataUri = $"data:image;base64,{base64String}";
                    fileUri = new Uri(dataUri);*//*

                    hotel.photo = B64Encode();
                }*/

                hotel.title = Title.Text;
                hotel.city = City.Text;
                hotel.descriptionHotel = txtRich.Text;
                hotel.price = int.Parse(Price.Text);
                hotel.stars = int.Parse(Stars.Text);
                hotel.numRooms = int.Parse(NumRooms.Text);
                if (photoLoaded)
                {
                    hotel.photo = B64Encode();
                }
                else
                {
                    hotel.photo = hotel.photo;
                }

                db.SaveChanges();
                Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void tableHotels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tableHotels.SelectedItem != null)
            {
                TextRange txtRich = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd);
                string description = txtRich.Text;
                int id = (int)tableHotels.SelectedItems[0].GetType().GetProperty("idHotel").GetValue(tableHotels.SelectedItems[0]);
                Hotel selectedItem = db.Hotels.Where(temp => temp.idHotel == id).FirstOrDefault();

                string selectTitle = selectedItem.title;
                string selectCity = selectedItem.city;
                string selectCountry = selectedItem.country;
                string selectDesc = selectedItem.descriptionHotel;
                string selectStar = Convert.ToString(selectedItem.stars);
                string selectPrice = Convert.ToString(selectedItem.price);
                string selectNumRoom = Convert.ToString(selectedItem.numRooms);


                Title.Text = selectTitle;
                City.Text = selectCity;
                Country.Text = selectCountry;
                txtRich.Text = selectDesc;

                Stars.Text = selectStar;
                Price.Text = selectPrice;
                NumRooms.Text = selectNumRoom;

            }
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            TextRange txtRich = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd);
            string description = txtRich.Text;


            Title.Text = string.Empty;
            City.Text = string.Empty;
            Country.Text = string.Empty;
            txtRich.Text = string.Empty;
            Stars.SelectedItem = null;
            Price.Text = string.Empty;
            NumRooms.Text = string.Empty;


        }
    }
}
