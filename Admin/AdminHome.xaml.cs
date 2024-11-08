using Insurance_Management_System.Staff;
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

namespace Insurance_Management_System.Admin
{
    /// <summary>
    /// Interaction logic for AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Window
    {
        public AdminHome()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {

            AdminHome HomeWindow = new AdminHome();
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

            UpdateCustomer CustomerWindow = new UpdateCustomer();
            CustomerWindow.Show();
            this.Close();
        }

        private void PolicyIcon_Click(object sender, RoutedEventArgs e)
        {

            UpdatePolicy PolicyWindow = new UpdatePolicy();
            PolicyWindow.Show();
            this.Close();
        }

        private void CarIcon_Click(object sender, RoutedEventArgs e)
        {

            UpdateCar CarWindow = new UpdateCar();
            CarWindow.Show();
            this.Close();
        }

        private void HomeIcon_Click(object sender, RoutedEventArgs e)
        {

            UpdateHome HomeWindow = new UpdateHome();
            HomeWindow.Show();
            this.Close();
        }

        private void LifeIcon_Click(object sender, RoutedEventArgs e)
        {

            UpdateLife LifeWindow = new UpdateLife();
            LifeWindow.Show();
            this.Close();
        }

        private void TravelIcon_Click(object sender, RoutedEventArgs e)
        {

            UpdateTravel TravelWindow = new UpdateTravel();
            TravelWindow.Show();
            this.Close();
        }

        private void StaffIcon_Click(object sender, RoutedEventArgs e)
        {

            AddStaff StaffWindow = new AddStaff();
            StaffWindow.Show();
            this.Close();
        }

    }
}
