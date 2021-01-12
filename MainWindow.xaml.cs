using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BurgerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        private int mSusanBread = 0;
        private int mSimpleBread = 0;
        private int mMeatFilled = 0;
        private int mCheeseFilled = 0;
        private int mSaladFilled = 0;
        private double Total = 0;
        private BurgerType selectedBurger;
        public MainWindow()
        {
            InitializeComponent();
           
        }
       
        private void susanMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mSusanBread++;
            txtSusanBread.Text = ToString();
        }

        private void simpleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mSimpleBread++;
            txtSimpleBread.Text = ToString();
        }

        private void meatMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mMeatFilled++;
            txtMeatFilled.Text = ToString();
        }

        private void cheeseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mCheeseFilled++;
            txtCheeseFilled.Text = ToString();
        }

        private void saladMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mSaladFilled++;
            txtSaladFilled.Text = ToString();
        }

        private void FilledItemsShow_Click(object sender, RoutedEventArgs e)
        {
            string mesaj;
            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            mesaj = SelectedItem.Header.ToString() + "burgers are being cooked!";
            this.Title = mesaj;
        }

        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        KeyValuePair<BurgerType, double>[] PriceList = {
 new KeyValuePair<BurgerType, double>(BurgerType.Susan, 25),
 new KeyValuePair<BurgerType, double>(BurgerType.Simple,30),
 new KeyValuePair<BurgerType, double>(BurgerType.Meat,45),
 new KeyValuePair<BurgerType, double>(BurgerType.Cheese,40),
 new KeyValuePair<BurgerType, double>(BurgerType.Salad,35)
 };
        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            cmbType.ItemsSource = PriceList;
            cmbType.DisplayMemberPath = "Key";
            cmbType.SelectedValuePath = "Value";
        }
        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPrice.Text = cmbType.SelectedValue.ToString();
            KeyValuePair<BurgerType, double> selectedEntry =
           (KeyValuePair<BurgerType, double>)cmbType.SelectedItem;
            selectedBurger = selectedEntry.Key;
        }
        private int ValidateQuantity(BurgerType selectedBurger)
        {
            int q = int.Parse(txtQuantity.Text);
            int r = 1;

            switch (selectedBurger)
            {
                case BurgerType.Susan:
                    if (q > mSusanBread)
                        r = 0;
                    break;
                case BurgerType.Simple:
                    if (q > mSimpleBread)
                        r = 0;
                    break;
                case BurgerType.Meat:
                    if (q > mMeatFilled)
                        r = 0;
                    break;
                case BurgerType.Cheese:
                    if (q > mCheeseFilled)
                        r = 0;
                    break;
                case BurgerType.Salad:
                    if (q > mSaladFilled)
                        r = 0;
                    break;
            }
            return r;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateQuantity(selectedBurger) > 0)
            {
                lstSale.Items.Add(txtQuantity.Text + " " + selectedBurger.ToString() +
               ":" + txtPrice.Text + " " + double.Parse(txtQuantity.Text) *
               double.Parse(txtPrice.Text));
                Total = Total + double.Parse(txtQuantity.Text) * double.Parse(txtPrice.Text);
                txtTotal.Text = Total.ToString();
            }
            else
            {
                MessageBox.Show("Cantitatea introdusa nu este disponibila in stoc!");
            }
        }
        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            lstSale.Items.Remove(lstSale.SelectedItem);
        }
        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            txtTotal.Text = "0";
            foreach (string s in lstSale.Items)
            {
                switch (s.Substring(s.IndexOf(" ") + 1, s.IndexOf(":") - s.IndexOf(" ") -
               1))
                {
                    case "Ssusan":
                        mSusanBread = mSusanBread - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtSusanBread.Text = mSusanBread.ToString();
                        break;
                    case "Simple":
                        mSimpleBread = mSimpleBread - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtSimpleBread.Text = mSimpleBread.ToString();
                        break;
                    case "Meat":
                        mMeatFilled = mMeatFilled - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtMeatFilled.Text = mMeatFilled.ToString();
                        break;
                    case "Cheese":
                        mCheeseFilled = mCheeseFilled - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtCheeseFilled.Text = mCheeseFilled.ToString();
                        break;
                    case "Salad":
                        mSaladFilled = mSaladFilled - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtSaladFilled.Text = mSaladFilled.ToString();
                        break;
                }
            }
            lstSale.Items.Clear();
        }

        private void txtQuantity_KeyPress(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                MessageBox.Show("Numai cifre se pot introduce!", "Input Error", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
    }
}
