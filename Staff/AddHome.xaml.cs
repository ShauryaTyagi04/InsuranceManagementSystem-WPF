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
    /// Interaction logic for AddHome.xaml
    /// </summary>
    public partial class AddHome : Window
    {
        private InsuranceManagementEntities db = new InsuranceManagementEntities();

        public AddHome()
        {
            InitializeComponent();
            LoadPolicyIDs();
        }

        private void LoadPolicyIDs()
        {
            var policyIDs = db.Policies.Select(h => h.PolicyID).ToList();
            cmbPolicyNumber.ItemsSource = policyIDs;
        }

        private bool IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtCoverage.Text) ||
                string.IsNullOrWhiteSpace(txtOContact.Text) ||
                string.IsNullOrWhiteSpace(txtOEmail.Text) ||
                string.IsNullOrWhiteSpace(txtOwner.Text) ||
                string.IsNullOrWhiteSpace(txtValue.Text))
            {
                MessageBox.Show("Please fill in all the required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void ClearFormFields()
        {
            txtAddress.Text = string.Empty;
            txtCoverage.Text = string.Empty;
            txtLease.Text = string.Empty;
            txtOContact.Text = string.Empty;
            txtOEmail.Text = string.Empty;
            txtOwner.Text = string.Empty;
            txtValue.Text = string.Empty;

            cmbPolicyNumber.SelectedIndex = -1;
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

            ViewHome ViewWindow = new ViewHome();
            ViewWindow.Show();
            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsFormValid())
            {
                DateTime Ldate;
                if (!DateTime.TryParseExact(txtLease.Text, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out Ldate))
                {
                    MessageBox.Show("Date must be in DD-MM-YYYY format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Ldate < DateTime.Now)
                {
                    MessageBox.Show("Lease date should be a future date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cmbPolicyNumber.SelectedItem == null)
                {
                    MessageBox.Show("Please select a Policy Number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(txtCoverage.Text, out double coverageAmount))
                {
                    MessageBox.Show("Please enter a valid Coverage Amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int selectedPolicyID = (int)cmbPolicyNumber.SelectedItem;

                var newHomeInsurance = new HomeInsurance
                {
                    Lease = Ldate,
                    Legal_Owner = txtOwner.Text,
                    Owner_Contact = txtOContact.Text,
                    Owner_Email = txtOEmail.Text,
                    Property_Address = txtAddress.Text,
                    Property_Value = txtValue.Text,
                    CoverageAmount = coverageAmount,
                    IsApproverd = false,
                    PolicyID = selectedPolicyID,
                };


                db.HomeInsurance.Add(newHomeInsurance);
                db.SaveChanges();

                MessageBox.Show("Home insurance added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearFormFields();
            }
        }

        private void cmbPolicyNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
