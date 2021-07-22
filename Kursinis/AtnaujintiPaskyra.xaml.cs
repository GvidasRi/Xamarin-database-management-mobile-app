using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography;

namespace Kursinis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AtnaujintiPaskyra : ContentPage
    {
        public AtnaujintiPaskyra()
        {
            InitializeComponent();


        }

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

        private async void Update_Clicked(object sender, EventArgs e)
        {
            if (Privilegijos.SelectedIndex == -1 || Vardas.Text == null || Pavarde.Text == null || Username.Text == null || Password.Text == null || App.choice2 == null)
            {

                DisplayActionSheet("Klaida", "OK", null, "Negali būti tuščių langelių");
            }
            else
            {
                string connectionString = "Server=192.168.42.57;Port=3306;Database=db;Uid=root;Pwd=;";
                MySqlConnection sqlcon = new MySqlConnection(connectionString);
                string query = "update accounts set Username='" + Username.Text + "',Password='" + Hash(Password.Text) + "',Priv='" + Privilegijos.SelectedItem.ToString() + "' where UserID='" + App.choice2 + "';";
                MySqlCommand cmd = new MySqlCommand(query, sqlcon);
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                MySqlConnection sqlcon2 = new MySqlConnection(connectionString);
                string query2 = "update users set Vardas='" + Vardas.Text + "',Pavarde='" + Pavarde.Text + "',Privilegijos='" + Privilegijos.SelectedItem.ToString() + "' where paskyra='" + App.choice2 + "';";
                MySqlCommand cmd2 = new MySqlCommand(query2, sqlcon2);
                sqlcon2.Open();
                cmd2.ExecuteNonQuery();
                DisplayActionSheet("Patvirtinimas", "OK", null, "Įrašas atnaujintas");
                await Navigation.PushAsync(new Pagrindinis());
            }
        }
    }
}