using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Xamarin.Forms;
using MySqlConnector;
using System.Security.Cryptography;

namespace Kursinis
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        string connectionString = "Server=192.168.42.57;Port=3306;Database=db;Uid=root;Pwd=;";
        int count = 0;
        static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
        public async void Handle_Clicked(object sender, EventArgs e)
        {
            MySqlConnection sqlcon = new MySqlConnection(connectionString);
            string query = "Select UserID from accounts Where Username = '" + Username.Text + "' and Password = '" + Hash(Password.Text) + "'";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);

            //idvalue = Convert.ToInt32(UserID);
            if (dtbl.Rows.Count > 0)
            { 
                var idvalue = dtbl.AsEnumerable().Select(r => r.Field<int>("UserID")).ToArray();
                MySqlConnection sqlcon2 = new MySqlConnection(connectionString);
                string query2 = "Select * from users Where paskyra = '" + idvalue[0] + "'";
                MySqlDataAdapter sda2 = new MySqlDataAdapter(query2, sqlcon2);
                DataTable dtbl2 = new DataTable();
                sda2.Fill(dtbl2);
                var Vardas = dtbl2.AsEnumerable().Select(r => r.Field<string>("Vardas")).ToArray();
                App.vardass = Vardas[0];
                int convid = idvalue[0];
                App.paskyra = convid.ToString();
                MySqlConnection sqlcon55 = new MySqlConnection(connectionString);
                string query55 = "INSERT INTO logs(LogID, userid, date, vardas, action) VALUES (NULL, '" + App.paskyra + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + App.vardass + "', '" + "Prisijungė prie paskyros" + "')";
                MySqlCommand cmd55 = new MySqlCommand(query55, sqlcon55);
                sqlcon55.Open();
                cmd55.ExecuteNonQuery();
                DisplayAlert("Prisijungimas", "Prisijungimas sėkmingas", "OK");
                //Username.Text = App.paskyra;
                /*PridetiIrasa pi = new PridetiIrasa();
                pi.skaicius = paskyra;*/
                // Username.Text = a;
                await Navigation.PushAsync(new Pagrindinis());
            }
            else
            {
                DisplayAlert("Prisijungimas", "Prisijungimas nesėkmingas", "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registracija());
        }
    }
}
