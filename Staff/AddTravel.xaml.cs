using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Insurance_Management_System.Staff
{
    public partial class AddTravel : Window
    {
        private InsuranceManagementEntities db = new InsuranceManagementEntities();
        public AddTravel()
        {
            InitializeComponent();
            LoadPolicyIDs();
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

            ViewTravel ViewWindow = new ViewTravel();
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

                if (!DateTime.TryParseExact(txtSDate.Text, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime travelDate))
                {
                    MessageBox.Show("Please enter a valid Travel Date in DD-MM-YYYY format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!DateTime.TryParseExact(txtEdate.Text, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime returnDate))
                {
                    MessageBox.Show("Please enter a valid Return Date in DD-MM-YYYY format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (returnDate > travelDate)
                {
                    MessageBox.Show("Return date should be greater than travel date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(txtCoverage.Text, out double coverageAmount))
                {
                    MessageBox.Show("Please enter a valid Coverage Amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int selectedPolicyID = (int)cmbPolicyNumber.SelectedItem;

                bool lostBags = CheckboxBags.IsChecked ?? false;
                bool legalCosts = CheckboxLegal.IsChecked ?? false;
                bool emergencyMedicalExpenses = CheckboxEmergency.IsChecked ?? false;
                bool cancellationCost = CheckboxCancellation.IsChecked ?? false;

                if (CheckboxAll.IsChecked == true)
                {
                    lostBags = legalCosts = emergencyMedicalExpenses = cancellationCost = true;
                }

                var newTravelInsurance = new TravelInsurance
                {
                    Destination = txtDestination.Text,
                    TravelDate = travelDate,
                    ReturnDate = returnDate,
                    InternationalContactNumber = txtNumber.Text,
                    CoverageAmount = coverageAmount,
                    PolicyID = selectedPolicyID,
                    LostBags = lostBags,
                    LegalCosts = legalCosts,
                    EmergencyMedicalExpenses = emergencyMedicalExpenses,
                    Single = CheckboxSingle.IsChecked ?? false,
                    Solo = CheckboxSolo.IsChecked ?? false,
                    European = CheckboxEuropean.IsChecked ?? false,
                    Annual = CheckboxAnnual.IsChecked ?? false,
                    Family = CheckboxFamily.IsChecked ?? false,
                    Summer = CheckboxSummer.IsChecked ?? false,
                    Gap = CheckboxGap.IsChecked ?? false,
                    Worldwide = CheckboxWorldwide.IsChecked ?? false,
                    Winter = CheckboxWinter.IsChecked ?? false,
                    CancellationCost = cancellationCost,
                    IsApproverd = false,
                };


                db.TravelInsurance.Add(newTravelInsurance);
                db.SaveChanges();

                MessageBox.Show("Travel insurance added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearFormFields();
            }
        }

        private void ClearFormFields()
        {
            txtDestination.Text = string.Empty;
            txtSDate.Text = string.Empty;
            txtEdate.Text = string.Empty;
            txtNumber.Text = string.Empty;
            txtCoverage.Text = string.Empty;
            CheckboxBags.IsChecked = false;
            CheckboxLegal.IsChecked = false;
            CheckboxEmergency.IsChecked = false;
            CheckboxCancellation.IsChecked = false;
            CheckboxAll.IsChecked = false;
            CheckboxSingle.IsChecked = false;
            CheckboxSolo.IsChecked = false;
            CheckboxEuropean.IsChecked = false;
            CheckboxFamily.IsChecked = false;
            CheckboxSummer.IsChecked = false;
            CheckboxGap.IsChecked = false;
            CheckboxWorldwide.IsChecked = false;
            CheckboxWinter.IsChecked = false;
            cmbPolicyNumber.SelectedIndex = -1;
        }

        private void LoadPolicyIDs()
        {
            var policyIDs = db.Policies.Select(p => p.PolicyID).ToList();
            cmbPolicyNumber.ItemsSource = policyIDs;
        }

        private bool IsFormValid()
        {
            if (
                string.IsNullOrWhiteSpace(txtDestination.Text) ||
                string.IsNullOrWhiteSpace(txtCoverage.Text)) 
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void cmbPolicyNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}