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
using System.Windows.Shapes;

namespace Insurance_Management_System.Staff
{
    /// <summary>
    /// Interaction logic for StaffHome.xaml
    /// </summary>
    public partial class StaffHome : Window
    {
        public StaffHome()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {

            StaffHome HomeWindow = new StaffHome();
            HomeWindow.Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            Login LoginWindow = new Login();
            LoginWindow.Show();
            this.Close();
        }

        private void CustomerIcon_Click(object sender, RoutedEventArgs e)
        {

            AddCustomer CustomerWindow = new AddCustomer();
            CustomerWindow.Show();
            this.Close();
        }

        private void PolicyIcon_Click(object sender, RoutedEventArgs e)
        {

            AddPolicy PolicyWindow = new AddPolicy();
            PolicyWindow.Show();
            this.Close();
        }

        private void CarIcon_Click(object sender, RoutedEventArgs e)
        {

            AddCar CarWindow = new AddCar();
            CarWindow.Show();
            this.Close();
        }

        private void HomeIcon_Click(object sender, RoutedEventArgs e)
        {

            AddHome HomeWindow = new AddHome();
            HomeWindow.Show();
            this.Close();
        }

        private void LifeIcon_Click(object sender, RoutedEventArgs e)
        {

            AddLife LifeWindow = new AddLife();
            LifeWindow.Show();
            this.Close();
        }

        private void TravelIcon_Click(object sender, RoutedEventArgs e)
        {

            AddTravel TravelWindow = new AddTravel();
            TravelWindow.Show();
            this.Close();
        }

    }
}