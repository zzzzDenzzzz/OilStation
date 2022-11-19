using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

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

        // прибыль за день
        decimal sumDay;

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
            toPaySum = 0;
            sumDay = 0;
            comboBoxPetrol.SelectedIndex = 0;
            toolStripDropDownButtonCurrentDay.Text = DateTime.Now.ToShortDateString();
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

        /// <summary>
        /// Показывает общую сумму и запускает время в строке состояния
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (toPaySum == 0)
            {
                labelToPaySum.Text = "0.00";
            }
            else
            {
                labelToPaySum.Text = toPaySum.ToString();
            }
            timerStatusLabel.Tick += timer_StatusLabel;
            if (timerStatusLabel.Enabled == false)
            {
                timerStatusLabel.Start();
            }
            timer.Start();
        }

        /// <summary>
        /// Вызов сообщения о продолжении работы со старым клиентом или работа с новым
        /// </summary>
        DialogResult NewClient()
        {
            timer.Stop();

            return MessageBox.Show("Завершить работу с клиентом?", "Новый клиент",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Возвращение формы в исходное состояние
        /// </summary>
        void FormClear()
        {
            timerStatusLabel.Stop();

            sumDay += toPaySum;
            Text = sumDay.ToString();
            sumPetrol = 0;
            sumMiniCafe = 0;
            comboBoxPetrol.SelectedIndex = 0;
            textBoxPriceGasStation.Text = pricePetrol[0].ToString();
            radioButtonAmount.Focus();
            textBoxAmountGasStation.Text = "0.0";
            labelToPaySum.Text = "0.00";
            foreach (CheckBox checkBox in groupBoxMiniCafe.Controls.OfType<CheckBox>())
            {
                checkBox.Checked = false;
            }
        }

        /// <summary>
        /// После подсчета общей суммы запускает таймер=3 сек. 
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            DialogResult dialogResult = NewClient();
            if (dialogResult == DialogResult.Yes)
            {
                FormClear();
            }
        }

        /// <summary>
        /// При закрытии формы появляется сообщение с суммой прибыли
        /// если прибыли нет, сообщение не выводится
        /// </summary>
        private void FormGasStation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sumDay == 0)
            {
                return;
            }
            MessageBox.Show(Text, "Прибыль", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Запускает таймер и в строке состояния выводится время или дата с интервалом в 1 с
        Timer timerStatusLabel = new Timer() { Interval = 1000 };
        bool flagTimer = true;
        void timer_StatusLabel(object sender, EventArgs e)
        {
            if (flagTimer)
            {
                toolStripStatusLabelDateTime.Text = DateTime.Now.ToLongTimeString();
                flagTimer = false;
            }
            else
            {
                toolStripStatusLabelDateTime.Text = DateTime.Now.ToLongDateString();
                flagTimer = true;
            }
        }

        // Вызывает панель изменения цвета фона формы
        private void toolStripMenuItemChangeColor_Click(object sender, EventArgs e)
        {
            panelChangeColor.Visible = true;
        }

        byte r = 0;
        private void trackBarR_Scroll(object sender, EventArgs e)
        {
            r = (byte)trackBarR.Value;
        }

        byte g = 0;
        private void trackBarG_Scroll(object sender, EventArgs e)
        {
            g = (byte)trackBarG.Value;
        }

        byte b = 0;
        private void trackBarB_Scroll(object sender, EventArgs e)
        {
            b = (byte)trackBarB.Value;
        }

        /// <summary>
        /// Изменение цвета фона формы
        /// </summary>
        private void buttonChangeColor_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(r, g, b);
            panelChangeColor.Visible = false;
        }


        /// <summary>
        /// Изменение языка на русский
        /// </summary>
        private void toolStripMenuItemLang_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru");
            ComponentResourceManager resources = new ComponentResourceManager(GetType());
            resources.ApplyResources(this, "$this");
            foreach (Control control in Controls)
            {
                resources.ApplyResources(control, control.Name);
            }
        }
        /// <summary>
        /// Изменение языка на английский
        /// </summary>
        private void toolStripMenuItemLangEng_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            ComponentResourceManager resources = new ComponentResourceManager(GetType());
            resources.ApplyResources(this, "$this");
            foreach (Control control in Controls)
            {
                resources.ApplyResources(control, control.Name);
            }
        }

        /// <summary>
        /// Сворачивание в трей
        /// </summary>
        private void FormGasStation_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        /// <summary>
        /// Разворачивание из трея
        /// </summary>
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
    }
}
