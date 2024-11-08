using Insurance_Management_System.Admin;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Insurance_Management_System.Staff
{
    public partial class ViewCar : Window
    {
        InsuranceManagementEntities db = new InsuranceManagementEntities();
        public ViewCar()
        {
            InitializeComponent();
            LoadCarData();
            LoadCarIDs();
        }

        private void LoadCarData()
        {
            var car = db.CarInsurance.ToList();
            CarData.ItemsSource = car;
            var carColumn = CarData.Columns.FirstOrDefault(c => c.Header.ToString() == "Insurance");
            if (carColumn != null)
            {
                CarData.Columns.Remove(carColumn);
            }
        }

        private void LoadCarIDs()
        {
            var CarIDs = db.CarInsurance.Select(c => c.CInsuranceID).ToList();
            cmbInsuranceID.ItemsSource = CarIDs;
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (cmbInsuranceID.SelectedItem == null)
            {
                MessageBox.Show("Please select an Insurance Number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                var car = db.CarInsurance.FirstOrDefault(c => c.CInsuranceID == selectedID);
                if (car != null)
                {
                    car.Colour = txtColour.Text;
                    car.Make = txtMake.Text;
                    car.License_Driver = txtLicenseD.Text;
                    car.License_Plate = txtLicenseP.Text;
                    car.Council = txtCouncil.Text;
                    car.Model = txtModel.Text;
                    car.Registration = txtRegistration.Text;
                    car.CoverageAmount = coverageAmount;
                    car.PolicyID = policyID;

                    db.SaveChanges();
                    MessageBox.Show("Car Insurance updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadCarData();
                }
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var car = db.CarInsurance.FirstOrDefault(c => c.CInsuranceID == selectedID);

                if (car != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Are you sure you want to delete this Insurance record?",
                        "Confirm deletion",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        db.CarInsurance.Remove(car);
                        db.SaveChanges();
                        MessageBox.Show("Insurance records deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadCarData();
                        LoadCarIDs();
                        ClearFormFields();
                    }
                }
            }
        }

        private void cmbInsuranceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var car = db.CarInsurance.FirstOrDefault(c => c.CInsuranceID == selectedID);

                if (car != null)
                {
                    txtColour.Text = car.Colour.ToString();
                    txtCouncil.Text = car.Council.ToString();
                    txtLicenseD.Text = car.License_Driver.ToString();
                    txtLicenseP.Text = car.License_Plate.ToString();
                    txtMake.Text = car.Make.ToString();
                    txtModel.Text = car.Model.ToString();
                    txtRegistration.Text = car.Registration.ToString();
                    txtCoverage.Text = car.CoverageAmount.ToString();

                    txtPolicyID.Content = car.PolicyID.ToString();
                    txtIsApproved.Content = car.IsApproverd ? "Approved" : "Not Approved";
                }
            }
        }

        private void CarData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarData.SelectedItem != null)
            {
                CarInsurance car = (CarInsurance)CarData.SelectedItem;

                txtColour.Text = car.Colour.ToString();
                txtCouncil.Text = car.Council.ToString();
                txtLicenseD.Text = car.License_Driver.ToString();
                txtLicenseP.Text = car.License_Plate.ToString();
                txtMake.Text = car.Make.ToString();
                txtModel.Text = car.Model.ToString();
                txtRegistration.Text = car.Registration.ToString();
                txtCoverage.Text = car.CoverageAmount.ToString();

                txtPolicyID.Content = car.PolicyID.ToString();
                txtIsApproved.Content = car.IsApproverd ? "Approved" : "Not Approved";
                cmbInsuranceID.SelectedItem = car.CInsuranceID;
            }
        }
    }
}
