using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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

namespace CurrencyExchange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Currency1.Items.Add("USD");
            Currency1.Items.Add("EUR");
            Currency1.Items.Add("SEK");
            Currency1.Items.Add("NOK");
            Currency1.Items.Add("CAD");
            Currency1.Items.Add("GBP");
            Currency1.Items.Add("DKK");

            Currency2.Items.Add("DKK");
            Currency2.Items.Add("USD");
            Currency2.Items.Add("EUR");
            Currency2.Items.Add("SEK");
            Currency2.Items.Add("NOK");
            Currency2.Items.Add("CAD");
            Currency2.Items.Add("GBP");

            fillLabels();

            amount.Text = "1";

        }

        private float hentCurrency(string api, string conversion)
        {
            try
            {
                string Json = new WebClient().DownloadString(api);
                var result = JsonConvert.DeserializeObject<dynamic>(Json);
                return result["rates"][conversion];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Oops");
            }
            return 0;
        }

        private void fillLabels()
        {
            string api = "https://api.exchangeratesapi.io/latest?base=DKK";

            USD.Content = $"USD {hentCurrency(api, "USD")}";
            EUR.Content = $"EUR {hentCurrency(api, "EUR")}";
            SEK.Content = $"SEK {hentCurrency(api, "SEK")}";
            NOK.Content = $"NOK {hentCurrency(api, "NOK")}";
            CAD.Content = $"CAD {hentCurrency(api, "CAD")}";
            GBP.Content = $"GBP {hentCurrency(api, "GBP")}";

        }

        private void amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (amount.Text == "")
            {
                ConversionResult.Content = "";
            }
            else
            {

                string apiRequest = "https://api.exchangeratesapi.io/latest?base=" + Currency1.SelectedItem;

                string c2 = Currency2.SelectedItem.ToString();

                float textValue = 1;
                float currencyValue = hentCurrency(apiRequest, c2);
                try
                {
                    textValue = float.Parse(amount.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Input must be a valid number");
                }


                ConversionResult.Content = $"{textValue} {Currency1.SelectedItem} = {currencyValue * textValue} {c2}";
            }
        }
    }
}
