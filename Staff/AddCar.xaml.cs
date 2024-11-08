using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Insurance_Management_System.Staff
{
    public partial class AddCar : Window
    {
        private InsuranceManagementEntities db = new InsuranceManagementEntities();

        public AddCar()
        {
            InitializeComponent();
            LoadPolicyIDs();
        }

        private void LoadPolicyIDs()
        {
            var policyIDs = db.Policies.Select(c => c.PolicyID).ToList();
            cmbPolicyNumber.ItemsSource = policyIDs;
        }

        private bool IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(txtColour.Text) ||
                string.IsNullOrWhiteSpace(txtCouncil.Text) ||
                string.IsNullOrWhiteSpace(txtCoverage.Text) ||
                string.IsNullOrWhiteSpace(txtLicenseD.Text) ||
                string.IsNullOrWhiteSpace(txtLicenseP.Text) ||
                string.IsNullOrWhiteSpace(txtMake.Text) ||
                string.IsNullOrWhiteSpace(txtModel.Text) ||
                string.IsNullOrWhiteSpace(txtRegistration.Text))
            {
                MessageBox.Show("Please fill in all the required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void ClearFormFields()
        {
            txtColour.Text = string.Empty;
            txtCouncil.Text = string.Empty;
            txtCoverage.Text = string.Empty;
            txtLicenseD.Text = string.Empty;
            txtLicenseP.Text = string.Empty;
            txtMake.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtRegistration.Text = string.Empty;

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

            ViewCar ViewWindow = new ViewCar();
            ViewWindow.Show();
            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsFormValid())
            {
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

                var newCarInsurance = new CarInsurance
                {
                    Colour = txtColour.Text,
                    Make = txtMake.Text,
                    License_Driver = txtLicenseD.Text,
                    License_Plate = txtLicenseP.Text,
                    Council = txtCouncil.Text,
                    Model = txtModel.Text,
                    Registration = txtRegistration.Text,
                    CoverageAmount = coverageAmount,
                    IsApproverd = false,
                    PolicyID = selectedPolicyID,
                };


                db.CarInsurance.Add(newCarInsurance);
                db.SaveChanges();

                MessageBox.Show("Car insurance added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearFormFields();
            }
        }

        private void cmbPolicyNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
