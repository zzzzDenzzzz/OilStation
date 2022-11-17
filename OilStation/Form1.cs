using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OilStation
{
    public partial class FormGasStation : Form
    {
        // цена бензина {А-92, А-95, А-98}
        decimal[] pricePetrol = { 46.70M, 50.20M, 51.50M };
        // цена {хот-дога, гамбургера, фри, cola}
        decimal[] priceMiniCafe = { 170M, 220M, 90M, 100M };
        // стоимость бензина
        decimal sumPetrol;

        public FormGasStation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Выполняется при загрузке формы
        /// </summary>
        private void LoadForm(object sender, EventArgs e)
        {
            comboBoxPetrol.SelectedIndex = 0;
            textBoxPriceGasStation.Text = pricePetrol[0].ToString();
            textBoxPriceHotDog.Text = priceMiniCafe[0].ToString();
            textBoxPiceGamburger.Text = priceMiniCafe[1].ToString();
            textBoxPriceFry.Text = priceMiniCafe[2].ToString();
            textBoxPriceCocaCola.Text = priceMiniCafe[3].ToString();
        }

        /// <summary>
        /// При выборе марки бензина меняется цена в TextBox
        /// </summary>
        private void comboBoxPetrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    textBoxPriceGasStation.Text = pricePetrol[0].ToString();
                    break;
                case 1:
                    textBoxPriceGasStation.Text = pricePetrol[1].ToString();
                    break;
                case 2:
                    textBoxPriceGasStation.Text = pricePetrol[2].ToString();
                    break;
            }
        }

        /// <summary>
        /// radioButton Количество
        /// </summary>
        private void radioButtonAmount_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSumGasStation.ReadOnly = true;
            textBoxAmountGasStation.ReadOnly = false;
            textBoxSumGasStation.Text = "0.00";
            labelToPaySumGasStation.Text = "0.00";
            groupBoxGasStationToPay.Text = "К оплате";
            label3Rub.Text = "руб.";
        }

        /// <summary>
        /// RadioButton Сумма
        /// </summary>
        private void radioButtonSum_CheckedChanged(object sender, EventArgs e)
        {
            textBoxAmountGasStation.ReadOnly = true;
            textBoxSumGasStation.ReadOnly = false;
            textBoxAmountGasStation.Text = "0.0";
            labelToPaySumGasStation.Text = "0.00";
            groupBoxGasStationToPay.Text = "К выдаче";
            label3Rub.Text = "л.";
        }

        /// <summary>
        /// Вычисляет стоимость бензина
        /// </summary>
        private void textBoxAmountGasStation_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBoxPriceGasStation.Text, out decimal price) &&
                decimal.TryParse(textBoxAmountGasStation.Text, out decimal amount))
            {
                sumPetrol = Math.Abs(price * amount);
                labelToPaySumGasStation.Text = sumPetrol.ToString();
            }
            else
            {
                labelToPaySumGasStation.Text = "0.00";
            }
        }
    }
}
