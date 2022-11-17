using System;
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
        // количество бензина
        decimal amountPetrol;

        // стоимость хот-догов
        decimal costHotDog;
        // стоимость гамбургеров
        decimal costGamburger;
        // стоимость фри
        decimal costFry;
        // стоимость coca-cola
        decimal costCocaCola;
        // общая стоимость товаров в мини-кафе
        decimal sumMiniCafe;

        // всего к оплате
        decimal toPaySum;

        public FormGasStation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Выполняется при загрузке формы
        /// </summary>
        private void LoadForm(object sender, EventArgs e)
        {
            sumPetrol = 0;
            sumMiniCafe = 0;
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
            SumChanged(sender, e);
        }

        /// <summary>
        /// Изменение суммы оплаты или количества бензина в зависимости от выбраннного radioButton
        /// </summary>
        void SumChanged(object obj, EventArgs e)
        {
            if (radioButtonAmount.Checked)
            {
                textBoxAmountGasStation_TextChanged(obj, e);
            }
            if (radioButtonSum.Checked)
            {
                textBoxSumGasStation_TextChanged(obj, e);
            }
        }

        /// <summary>
        /// Выбор radioButton Количество
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
        /// Выбор radioButton Сумма
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
                sumPetrol = 0;
                labelToPaySumGasStation.Text = "0.00";
            }
        }

        /// <summary>
        /// Вычисляет количество бензина
        /// </summary>
        private void textBoxSumGasStation_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBoxPriceGasStation.Text, out decimal price) &&
                decimal.TryParse(textBoxSumGasStation.Text, out decimal sum))
            {
                amountPetrol = Math.Abs(Math.Round(sum / price, 2));
                labelToPaySumGasStation.Text = amountPetrol.ToString();
            }
            else
            {
                labelToPaySumGasStation.Text = "0.00";
            }
        }

        /// <summary>
        /// Вычисляем стоимость хот-догов
        /// </summary>
        void CostHotDog()
        {
            if (decimal.TryParse(textBoxAmountHotDog.Text, out decimal amount))
            {
                costHotDog = Math.Abs(priceMiniCafe[0] * amount);
            }
        }

        /// <summary>
        /// Вычисляем стоимость гамбургеров
        /// </summary>
        void CostGamburger()
        {
            if (decimal.TryParse(textBoxAmountGamburger.Text, out decimal amount))
            {
                costGamburger = Math.Abs(priceMiniCafe[1] * amount);
            }
        }

        /// <summary>
        /// Вычисляем стоимость фри
        /// </summary>
        void CostFry()
        {
            if (decimal.TryParse(textBoxAmountFry.Text, out decimal amount))
            {
                costFry = Math.Abs(priceMiniCafe[2] * amount);
            }
        }

        /// <summary>
        /// Вычисляем стоимость coca-cola
        /// </summary>
        void CostCocaCola()
        {
            if (decimal.TryParse(textBoxAmountCocaCola.Text, out decimal amount))
            {
                costCocaCola = Math.Abs(priceMiniCafe[3] * amount);
            }
        }

        /// <summary>
        /// Открывает доступ к textBox количество хот-догов
        /// </summary>
        private void checkBoxHotDog_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHotDog.Checked)
            {
                textBoxAmountHotDog.ReadOnly = false;
            }
            else
            {
                textBoxAmountHotDog.Text = "0";
                textBoxAmountHotDog.ReadOnly = true;
            }
        }

        /// <summary>
        /// Открывает доступ к textBox количество гамбургеров
        /// </summary>
        private void checkBoxGamburger_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxGamburger.Checked)
            {
                textBoxAmountGamburger.ReadOnly = false;
            }
            else
            {
                textBoxAmountGamburger.Text = "0";
                textBoxAmountGamburger.ReadOnly = true;
            }
        }

        /// <summary>
        /// Открывает доступ к textBox количество фри
        /// </summary>
        private void checkBoxFry_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFry.Checked)
            {
                textBoxAmountFry.ReadOnly = false;
            }
            else
            {
                textBoxAmountFry.Text = "0";
                textBoxAmountFry.ReadOnly = true;
            }
        }

        /// <summary>
        /// Открывает доступ к textBox количество coca-cola
        /// </summary>
        private void checkBoxCocaCola_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCocaCola.Checked)
            {
                textBoxAmountCocaCola.ReadOnly = false;
            }
            else
            {
                textBoxAmountCocaCola.Text = "0";
                textBoxAmountCocaCola.ReadOnly = true;
            }
        }

        /// <summary>
        /// Стоимость товаров в мини-кафе
        /// </summary>
        void SumMiniCafe()
        {
            sumMiniCafe = costHotDog + costGamburger + costFry + costCocaCola;
            if (checkBoxHotDog.Checked || checkBoxGamburger.Checked ||
                checkBoxFry.Checked || checkBoxCocaCola.Checked)
            {
                labelToPaySumMiniCafe.Text = sumMiniCafe.ToString();
            }
            else
            {
                sumMiniCafe = 0;
                labelToPaySumMiniCafe.Text = "0.00";
            }
        }

        /// <summary>
        /// При изменении количества хот-догов считает их стоимость
        /// </summary>
        private void textBoxAmountHotDog_TextChanged(object sender, EventArgs e)
        {
            CostHotDog();
            SumMiniCafe();
        }

        /// <summary>
        /// При изменении количества гамбургеров считает их стоимость
        /// </summary>
        private void textBoxAmountGamburger_TextChanged(object sender, EventArgs e)
        {
            CostGamburger();
            SumMiniCafe();
        }

        /// <summary>
        /// При изменении количества фри считает ее стоимость
        /// </summary>
        private void textBoxAmountFry_TextChanged(object sender, EventArgs e)
        {
            CostFry();
            SumMiniCafe();
        }

        /// <summary>
        /// При изменении количества coca-cola считает ее стоимость
        /// </summary>
        private void textBoxAmountCocaCola_TextChanged(object sender, EventArgs e)
        {
            CostCocaCola();
            SumMiniCafe();
        }

        /// <summary>
        /// Общая стоимость
        /// </summary>
        void Sum()
        {
            if (radioButtonAmount.Checked)
            {
                toPaySum = sumPetrol + sumMiniCafe;
            }
            if (radioButtonSum.Checked)
            {
                if (decimal.TryParse(textBoxSumGasStation.Text, out decimal sum))
                {
                    toPaySum = sum + sumMiniCafe;
                }
                else
                {
                    toPaySum = sumMiniCafe;
                }
            }
            if (toPaySum == 0)
            {
                labelToPaySum.Text = "0.00";
            }
            else 
            {
                labelToPaySum.Text = toPaySum.ToString();
            }
        }

        /// <summary>
        /// Изменение суммы за бензин
        /// </summary>
        private void labelToPaySumGasStation_TextChanged(object sender, EventArgs e)
        {
            Sum();
        }

        /// <summary>
        /// Изменение суммы за товары мини-кафе
        /// </summary>
        private void labelToPaySumMiniCafe_TextChanged(object sender, EventArgs e)
        {
            Sum();
        }
    }
}
