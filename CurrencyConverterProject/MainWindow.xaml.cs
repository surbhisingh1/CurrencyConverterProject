using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConverterProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Root val = new Root();

        public MainWindow()
        {
            InitializeComponent();
            GetValue();
            

        }
        private async void GetValue()
        {
            val = await GetData<Root>("https://openexchangerates.org/api/latest.json?app_id=639a09d3bd384793bbcb205b3eb2cf59"); //API Link
          BindCurrency();

        }
        private void BindCurrency()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Text");
            table.Columns.Add("Rate");
            table.Rows.Add("--SELECT--", 0);
            table.Rows.Add("INR", val.rates.INR);
            table.Rows.Add("USD", val.rates.USD);
            table.Rows.Add("EUR", val.rates.EUR);
            table.Rows.Add("SAR", val.rates.SAR);
            table.Rows.Add("GBP", val.rates.GBP);
            table.Rows.Add("ISK", val.rates.ISK);
            table.Rows.Add("CAD", val.rates.CAD);
            table.Rows.Add("php", val.rates.PHP);
            table.Rows.Add("NZD", val.rates.NZD);
            table.Rows.Add("DKK", val.rates.DKK);
            cmbFromCurrency.ItemsSource = table.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath = "Rate";
            cmbFromCurrency.SelectedIndex = 0;
            cmbToCurrency.ItemsSource = table.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Rate";
            cmbToCurrency.SelectedIndex = 0;
        }

        //Method to get the data from the web
            public static async Task<Root> GetData<t>(string url)
        {
            Root root = new Root();
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(1);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responsestring = await response.Content.ReadAsStringAsync();
                    var responseobject = JsonConvert.DeserializeObject<Root>(responsestring);
                    return responseobject;
                }
                return root;
            }
            catch
            {
                return root;
            }


        }
        private void txtCurrency_TextChanged(object sender, TextChangedEventArgs e)
        {
            string pattern = @"[^0-9]";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(txtCurrency.Text))
            {
                txtCurrency.Text = null;
                //MessageBox.Show("Please Enter the Amount", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter the Amount", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCurrency.Focus();

            }
            else if (cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Currency From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbFromCurrency.Focus();
                // return;
            }
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Currency To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbToCurrency.Focus();
                //return;
            }
            else
            {

                double Amount = double.Parse(txtCurrency.Text);
                double result = Amount * double.Parse(cmbToCurrency.SelectedValue.ToString()) / double.Parse(cmbFromCurrency.SelectedValue.ToString());
                DisplayCurrency.Content = cmbToCurrency.Text + " " + result.ToString("N2");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtCurrency.Text = null;
            DisplayCurrency.Content = "";
            cmbToCurrency.SelectedIndex = 0;
            cmbFromCurrency.SelectedIndex = 0;
            txtCurrency.Focus();
        }

      
    }
}
