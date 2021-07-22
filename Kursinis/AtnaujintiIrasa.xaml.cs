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

namespace Kursinis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AtnaujintiIrasa : ContentPage
    {
        public AtnaujintiIrasa()
        {
            InitializeComponent();
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            if (Svarbumas.SelectedIndex == -1 || Statusas.SelectedIndex == -1 || Projektas.Text == null || Problema.Text == null)
            {

                DisplayActionSheet("Klaida", "OK", null, "Negali būti tuščių langelių");
            }
            else
            {
                string connectionString = "Server=192.168.42.57;Port=3306;Database=db;Uid=root;Pwd=;";
                MySqlConnection sqlcon = new MySqlConnection(connectionString);
                string query = "update tickets set projektas='" + Projektas.Text + "',priority='" + Svarbumas.SelectedItem.ToString() + "',problema='" + Problema.Text + "',statusas='" + Statusas.SelectedItem.ToString() + "'  where TicketID='" + App.choice + "';";
                MySqlCommand cmd = new MySqlCommand(query, sqlcon);
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                MySqlConnection sqlcon55 = new MySqlConnection(connectionString);
                string query55 = "INSERT INTO logs(LogID, userid, date, vardas, action) VALUES (NULL, '" + App.paskyra + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + App.vardass + "', '" + "Paspaustas įrašo atnaujinimo mygtukas" + "')";
                MySqlCommand cmd55 = new MySqlCommand(query55, sqlcon55);
                sqlcon55.Open();
                cmd55.ExecuteNonQuery();
                DisplayActionSheet("Patvirtinimas", "OK", null, "Įrašas atnaujintas");
                await Navigation.PushAsync(new Pagrindinis());
            }
        }
    }
}