using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Insurance_Management_System.Admin
{
    public partial class UpdatePolicy : Window
    {

        InsuranceManagementEntities db = new InsuranceManagementEntities();

        public UpdatePolicy()
        {
            InitializeComponent();
            LoadPolicyData();
            LoadPolicyIDs();
        }

        private void LoadPolicyData()
        {
            var policies = db.Policies.ToList();
            PolicyData.ItemsSource = policies;
            var customerColumn = PolicyData.Columns.FirstOrDefault(c => c.Header.ToString() == "Customer");
            if (customerColumn != null)
            {
                PolicyData.Columns.Remove(customerColumn);
            }
        }

        private void LoadPolicyIDs()
        {
            var policyIDs = db.Policies.Select(p => p.PolicyID).ToList();
            cmbPolicyID.ItemsSource = policyIDs;
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

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPolicyID.SelectedItem != null)
            {
                int selectedID = (int)cmbPolicyID.SelectedItem;
                var policy = db.Policies.FirstOrDefault(p => p.PolicyID == selectedID);

                if (policy != null)
                {
                    policy.IsApproved = true;
                    db.SaveChanges();
                    MessageBox.Show("Policy application approved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtIsApproved.Content = "Approved";
                    LoadPolicyData();
                }
            }
            else
            {
                MessageBox.Show("Please select a policy.", "Invalid Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtPAmount.Text, out double premiumAmount))
            {
                MessageBox.Show("Please enter a valid Premium Amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (!int.TryParse(txtCustomerID.Content.ToString(), out int customerID))
            {
                return;
            }

            if (cmbPolicyID.SelectedItem != null)
            {
                int selectedID = (int)cmbPolicyID.SelectedItem;
                var policy = db.Policies.FirstOrDefault(p => p.PolicyID == selectedID);
                if (policy != null)
                {
                    policy.StartDate = startD;
                    policy.EndDate = endD;
                    policy.CarInsurance = CarCheckbox.IsChecked ?? false;
                    policy.HomeInsurance = HomeCheckbox.IsChecked ?? false;
                    policy.LifeInsurance = LifeCheckbox.IsChecked ?? false;
                    policy.TravelInsurance = TravelCheckbox.IsChecked ?? false;
                    policy.PremiumAmount = premiumAmount;
                    policy.CustomerID = customerID;
                    policy.IsApproved = false;

                    db.SaveChanges();
                    MessageBox.Show("Policy updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadPolicyData();
                }
            }

            
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPolicyID.SelectedItem != null)
            {
                int selectedID = (int)cmbPolicyID.SelectedItem;
                var policy = db.Policies.FirstOrDefault(p => p.CustomerID == selectedID);

                if (policy != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Are you sure you want to reject this policy application?",
                        "Confirm Rejection",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        db.Policies.Remove(policy);
                        db.SaveChanges();
                        MessageBox.Show("Policy application rejected successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadPolicyData();
                        LoadPolicyIDs();
                        ClearForm();
                    }
                }
            }
        }

        private void ClearForm()
        {
            txtSDate.Text = "";
            txtEDate.Text = "";
            txtPAmount.Text = "";
            CarCheckbox.IsChecked = false;
            HomeCheckbox.IsChecked = false;
            LifeCheckbox.IsChecked = false;
            TravelCheckbox.IsChecked = false;
            txtCustomerID.Content = "";
            cmbPolicyID.SelectedIndex = -1;
        }

        private void PolicyData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PolicyData.SelectedItem != null)
            {
                Policies selectedPolicy = (Policies)PolicyData.SelectedItem;
                int selectedID = selectedPolicy.PolicyID;

                txtSDate.Text = selectedPolicy.StartDate.ToString("dd-MM-yyyy");
                txtEDate.Text = selectedPolicy.EndDate.ToString("dd-MM-yyyy");
                txtPAmount.Text = selectedPolicy.PremiumAmount.ToString();
                txtIsApproved.Content = selectedPolicy.IsApproved ? "Approved" : "Not Approved";
                txtCustomerID.Content = selectedPolicy.CustomerID;
                cmbPolicyID.SelectedItem = selectedPolicy.PolicyID.ToString();

                bool existsInCarInsurance = db.CarInsurance.Any(ci => ci.PolicyID == selectedID);
                CarCheckbox.IsChecked = existsInCarInsurance;

                bool existsInHomeInsurance = db.HomeInsurance.Any(hi => hi.PolicyID == selectedID);
                HomeCheckbox.IsChecked = existsInHomeInsurance;

                bool existsInLifeInsurance = db.LifeInsurance.Any(li => li.PolicyID == selectedID);
                LifeCheckbox.IsChecked = existsInLifeInsurance;

                bool existsInTravelInsurance = db.TravelInsurance.Any(ti => ti.PolicyID == selectedID);
                TravelCheckbox.IsChecked = existsInTravelInsurance;

                double totalCoverageAmount = 0;

                var carInsurance = db.CarInsurance.FirstOrDefault(ci => ci.PolicyID == selectedID); 
                if (carInsurance != null)
                {
                    totalCoverageAmount += carInsurance.CoverageAmount;
                }

                var homeInsurance = db.HomeInsurance.FirstOrDefault(hi => hi.PolicyID == selectedID);
                if (homeInsurance != null)
                {
                    totalCoverageAmount += homeInsurance.CoverageAmount;
                }

                var lifeInsurance = db.LifeInsurance.FirstOrDefault(li => li.PolicyID == selectedID);
                if (lifeInsurance != null)
                {
                    totalCoverageAmount += lifeInsurance.CoverageAmount;
                }

                var travelInsurance = db.TravelInsurance.FirstOrDefault(ti => ti.PolicyID == selectedID);
                if (travelInsurance != null)
                {
                    totalCoverageAmount += travelInsurance.CoverageAmount;
                }

                double premiumAmount = (totalCoverageAmount * 1.2)/12; // monthly

                txtPAmount.Text = premiumAmount.ToString("F2"); // 2 Decimal
            }
        }

        private void cmbPolicyID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPolicyID.SelectedItem != null)
            {
                int selectedID = (int)cmbPolicyID.SelectedItem;
                var policy = db.Policies.FirstOrDefault(p => p.PolicyID == selectedID);

                if (policy != null)
                {
                    txtSDate.Text = policy.StartDate.ToString("dd-MM-yyyy");
                    txtEDate.Text = policy.EndDate.ToString("dd-MM-yyyy");
                    txtPAmount.Text = policy.PremiumAmount.ToString();
                    txtIsApproved.Content = policy.IsApproved ? "Approved" : "Not Approved";
                    txtCustomerID.Content = policy.CustomerID.ToString();

                    bool existsInCarInsurance = db.CarInsurance.Any(ci => ci.PolicyID == selectedID);
                    CarCheckbox.IsChecked = existsInCarInsurance;

                    bool existsInHomeInsurance = db.HomeInsurance.Any(hi => hi.PolicyID == selectedID);
                    HomeCheckbox.IsChecked = existsInHomeInsurance;

                    bool existsInLifeInsurance = db.LifeInsurance.Any(li => li.PolicyID == selectedID);
                    LifeCheckbox.IsChecked = existsInLifeInsurance;

                    bool existsInTravelInsurance = db.TravelInsurance.Any(ti => ti.PolicyID == selectedID);
                    TravelCheckbox.IsChecked = existsInTravelInsurance;

                    double totalCoverageAmount = 0;

                    var carInsurance = db.CarInsurance.FirstOrDefault(ci => ci.PolicyID == selectedID);
                    if (carInsurance != null)
                    {
                        totalCoverageAmount += carInsurance.CoverageAmount;
                    }

                    var homeInsurance = db.HomeInsurance.FirstOrDefault(hi => hi.PolicyID == selectedID);
                    if (homeInsurance != null)
                    {
                        totalCoverageAmount += homeInsurance.CoverageAmount;
                    }

                    var lifeInsurance = db.LifeInsurance.FirstOrDefault(li => li.PolicyID == selectedID);
                    if (lifeInsurance != null)
                    {
                        totalCoverageAmount += lifeInsurance.CoverageAmount;
                    }

                    var travelInsurance = db.TravelInsurance.FirstOrDefault(ti => ti.PolicyID == selectedID);
                    if (travelInsurance != null)
                    {
                        totalCoverageAmount += travelInsurance.CoverageAmount;
                    }

                    double premiumAmount = (totalCoverageAmount * 1.2) / 12; // 20%

                    txtPAmount.Text = premiumAmount.ToString("F2"); // 2 Decimal
                }
            }
        }
    }
}
