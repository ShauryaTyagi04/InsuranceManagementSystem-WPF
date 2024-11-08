using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Insurance_Management_System.Staff
{

    public partial class AddPolicy : Window
    {
        private InsuranceManagementEntities db = new InsuranceManagementEntities();
        public AddPolicy()
        {
            InitializeComponent();
            LoadAvailableCustomerIDs();
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

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {

            ViewPolicy ViewWindow = new ViewPolicy();
            ViewWindow.Show();
            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsFormValid())
            {

                if (cmbCustomerID.SelectedItem == null)
                {
                    MessageBox.Show("Please select a Customer ID.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DateTime startD;
                if (!DateTime.TryParseExact(txtSDate.Text, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out startD))
                {
                    MessageBox.Show("Date of Birth must be in YYYY-MM-DD format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DateTime endD;
                if (!DateTime.TryParseExact(txtEDate.Text, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out endD))
                {
                    MessageBox.Show("Date of Birth must be in YYYY-MM-DD format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int selectedCustomerId = (int)cmbCustomerID.SelectedItem;

                if (db.Policies.Any(p => p.CustomerID == selectedCustomerId))
                {
                    MessageBox.Show("This Customer ID already has a policy assigned.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newPolicy = new Policies
                {
                    StartDate = startD,
                    EndDate = endD,
                    CarInsurance = CarCheckbox.IsChecked ?? false,
                    HomeInsurance = HomeCheckbox.IsChecked ?? false,
                    LifeInsurance = LifeCheckbox.IsChecked ?? false,
                    TravelInsurance = TravelCheckbox.IsChecked ?? false,
                    PremiumAmount = 0.0,
                    CustomerID = selectedCustomerId,
                    IsApproved = false
                };

                db.Policies.Add(newPolicy);
                db.SaveChanges();

                MessageBox.Show("Policy assigned to customer successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearForm();
                LoadAvailableCustomerIDs();
            }
        }

        private void LoadAvailableCustomerIDs()
        {
            try
            {
                var allCustomers = db.Customers.ToList();

                var availableCustomerIDs = allCustomers
                    .Where(c => !db.Policies.Any(p => p.CustomerID == c.CustomerID))
                    .Select(c => c.CustomerID)
                    .ToList();

                if (availableCustomerIDs.Any())
                {
                    MessageBox.Show($"{availableCustomerIDs.Count} customers without a policy assigned.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No available customer.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                cmbCustomerID.ItemsSource = availableCustomerIDs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading Customer IDs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void LoadAvailableCustomerIDs(object sender, RoutedEventArgs e)
        {
            LoadAvailableCustomerIDs();
        }


        private void cmbCustomerID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private bool IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(txtSDate.Text) ||
                string.IsNullOrWhiteSpace(txtEDate.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtSDate.Text = "";
            txtEDate.Text = "";
            txtPAmmount.Text = "";
            CarCheckbox.IsChecked = false;
            HomeCheckbox.IsChecked = false;
            LifeCheckbox.IsChecked = false;
            TravelCheckbox.IsChecked = false;
            cmbCustomerID.SelectedIndex = -1;    
        }

    }
}
