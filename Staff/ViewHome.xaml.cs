using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Windows;
using System.Windows.Controls;

namespace Insurance_Management_System.Staff
{
    public partial class ViewHome : Window
    {
        InsuranceManagementEntities db = new InsuranceManagementEntities();
        public ViewHome()
        {
            InitializeComponent();
            LoadHomeData();
            LoadHomeIDs();
        }

        private void LoadHomeData()
        {
            var home = db.HomeInsurance.ToList();
            HomeData.ItemsSource = home;
            var homeColumn = HomeData.Columns.FirstOrDefault(h => h.Header.ToString() == "Insurance");
            if (homeColumn != null)
            {
                HomeData.Columns.Remove(homeColumn);
            }
        }

        private void LoadHomeIDs()
        {
            var HomeIDs = db.HomeInsurance.Select(h => h.HInsuranceID).ToList();
            cmbInsuranceID.ItemsSource = HomeIDs;
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

            txtPolicyID.Content = string.Empty;
            cmbInsuranceID.SelectedIndex = -1;
            txtIsApproved.Content = string.Empty;
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

        private void Submit_Click(object sender, RoutedEventArgs e)
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

            if (!double.TryParse(txtCoverage.Text, out double coverageAmount))
            {
                MessageBox.Show("Please enter a valid Coverage Amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(txtPolicyID.Content.ToString(), out int policyID))
            {
                return;
            }

            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var home = db.HomeInsurance.FirstOrDefault(h => h.HInsuranceID == selectedID);
                if (home != null)
                {
                    home.Lease = Ldate;
                    home.Legal_Owner = txtOwner.Text;
                    home.Owner_Contact = txtOContact.Text;
                    home.Owner_Email = txtOEmail.Text;
                    home.Property_Address = txtAddress.Text;
                    home.Property_Value = txtValue.Text;
                    home.CoverageAmount = coverageAmount;
                    home.PolicyID = policyID;

                    db.SaveChanges();
                    MessageBox.Show("Home Insurance updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadHomeData();
                }
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var home = db.HomeInsurance.FirstOrDefault(h => h.HInsuranceID == selectedID);

                if (home != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Are you sure you want to delete this Insurance record?",
                        "Confirm deletion",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        db.HomeInsurance.Remove(home);
                        db.SaveChanges();
                        MessageBox.Show("Insurance records deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadHomeData();
                        LoadHomeIDs();
                        ClearFormFields();
                    }
                }
            }
        }

        private void HomeData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HomeData.SelectedItem != null)
            {
                HomeInsurance home = (HomeInsurance)HomeData.SelectedItem;

                txtLease.Text = home.Lease.ToString();
                txtOContact.Text = home.Owner_Contact.ToString();
                txtOEmail.Text = home.Owner_Email.ToString();
                txtOwner.Text = home.Legal_Owner.ToString();
                txtValue.Text = home.Property_Value.ToString();
                txtAddress.Text = home.Property_Address.ToString();
                txtCoverage.Text = home.CoverageAmount.ToString();

                txtPolicyID.Content = home.PolicyID.ToString();
                txtIsApproved.Content = home.IsApproverd ? "Approved" : "Not Approved";
            }
        }

        private void cmbInsuranceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var home = db.HomeInsurance.FirstOrDefault(h => h.HInsuranceID == selectedID);

                if (home != null)
                {
                    txtLease.Text = home.Lease.ToString();
                    txtOContact.Text = home.Owner_Contact.ToString();
                    txtOEmail.Text = home.Owner_Email.ToString();
                    txtOwner.Text = home.Legal_Owner.ToString();
                    txtValue.Text = home.Property_Value.ToString();
                    txtAddress.Text = home.Property_Address.ToString();
                    txtCoverage.Text = home.CoverageAmount.ToString();

                    txtPolicyID.Content = home.PolicyID.ToString();
                    txtIsApproved.Content = home.IsApproverd ? "Approved" : "Not Approved";
                }
            }
        }
    }
}
