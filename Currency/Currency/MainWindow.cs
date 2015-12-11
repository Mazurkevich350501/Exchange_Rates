using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Currency
{
    public partial class MainWindow : Form
    {
        string[] currencyList = { "USD", "EUR", "RUB", "BYR", "PLN", "UAH", "GBP", "CZK", "CAD", "SEK", "CNY", "JPY" };
        CurrencyController CurrencyInfo = new CurrencyController();
        public MainWindow()
        {
            InitializeComponent();

            foreach(string currency in currencyList)
            {
                comboBoxCurrency.Items.Add(currency);
                comboBoxCurrencyFrom.Items.Add(currency);
                comboBoxCurrencyTo.Items.Add(currency);
            }
            dataGridView1.Rows.Clear();
            foreach (RateData data in CurrencyInfo.GetCurrencyInfo("USD"))
            {
                dataGridView1.Rows.Add(data.Symbol.Substring(4,3), data.Rate, data.Range);
            }
            
        }

        private void comboBoxCurrency_SelectionChange(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (RateData data in CurrencyInfo.GetCurrencyInfo(comboBoxCurrency.SelectedItem.ToString()))
            {
                dataGridView1.Rows.Add(data.Symbol.Substring(4, 3), data.Rate, data.Range);
            }
        }
        private void comboBoxCurrencyTo_SelectionChange(object sender, EventArgs e)
        {
            showConvertResult();
        }
        private void comboBoxCurrencyFrom_SelectionChange(object sender, EventArgs e)
        {
            showConvertResult();
        }
        private void textBoxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }
        private void textBoxResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            showConvertResult();
        }
        private void showConvertResult()
        {
            double result;
            try
            {
                result = ConvertCurrency.ConvertValue(textBoxValue.Text,
                      CurrencyInfo.GetRate(comboBoxCurrencyFrom.SelectedItem.ToString(), comboBoxCurrencyTo.SelectedItem.ToString()));
            }
            catch
            {
                result = 0;
            }
            textBoxResult.Text = result.ToString();
        }

    }
}
