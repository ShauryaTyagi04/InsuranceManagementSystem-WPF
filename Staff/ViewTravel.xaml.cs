using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Insurance_Management_System.Staff
{
    public partial class ViewTravel : Window
    {
        InsuranceManagementEntities db = new InsuranceManagementEntities();
        public ViewTravel()
        {
            InitializeComponent();
            LoadTravelData();
            LoadTravelIDs();
        }

        private void LoadTravelData()
        {
            var travel = db.TravelInsurance.ToList();
            TravelData.ItemsSource = travel;
            var travelColumn = TravelData.Columns.FirstOrDefault(t => t.Header.ToString() == "Insurance");
            if (travelColumn != null)
            {
                TravelData.Columns.Remove(travelColumn);
            }
        }

        private void LoadTravelIDs()
        {
            var travelIDs = db.TravelInsurance.Select(t => t.TInsuranceID).ToList();
            cmbInsuranceID.ItemsSource = travelIDs;
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
            if (!DateTime.TryParseExact(txtSDate.Text, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime travelDate))
            {
                MessageBox.Show("Please enter a valid Travel Date in DD-MM-YYYY format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!DateTime.TryParseExact(txtEDate.Text, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime returnDate))
            {
                MessageBox.Show("Please enter a valid Return Date in DD-MM-YYYY format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(txtCoverage.Text, out double coverageAmount))
            {
                MessageBox.Show("Please enter a valid Coverage Amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool lostBags = CheckboxBag.IsChecked ?? false;
            bool legalCosts = CheckboxLegal.IsChecked ?? false;
            bool emergencyMedicalExpenses = CheckboxEmergency.IsChecked ?? false;
            bool cancellationCost = CheckboxCancellation.IsChecked ?? false;

            if (CheckboxAll.IsChecked == true)
            {
                lostBags = legalCosts = emergencyMedicalExpenses = cancellationCost = true;
            }

            if (!int.TryParse(txtPolicyID.Content.ToString(), out int policyID))
            {
                return;
            }

            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var travel = db.TravelInsurance.FirstOrDefault(p => p.TInsuranceID == selectedID);
                if (travel != null)
                {
                    travel.Destination = txtDestination.Text;
                    travel.TravelDate = travelDate;
                    travel.ReturnDate = returnDate;
                    travel.InternationalContactNumber = txtContact.Text;
                    travel.CoverageAmount = coverageAmount;
                    travel.PolicyID = policyID;
                    travel.LostBags = lostBags;
                    travel.LegalCosts = legalCosts;
                    travel.EmergencyMedicalExpenses = emergencyMedicalExpenses;
                    travel.Single = CheckboxSingle.IsChecked ?? false;
                    travel.Solo = CheckboxSolo.IsChecked ?? false;
                    travel.European = CheckboxEuropean.IsChecked ?? false;
                    travel.Annual = CheckboxAnnual.IsChecked ?? false;
                    travel.Family = CheckboxFamily.IsChecked ?? false;
                    travel.Summer = CheckboxSummer.IsChecked ?? false;
                    travel.Gap = CheckboxGap.IsChecked ?? false;
                    travel.Worldwide = CheckboxWorldwide.IsChecked ?? false;
                    travel.Winter = CheckboxWinter.IsChecked ?? false;
                    travel.CancellationCost = cancellationCost;

                    db.SaveChanges();
                    MessageBox.Show("Travel Insurance updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTravelData();
                }
            }    
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var travel = db.TravelInsurance.FirstOrDefault(t => t.TInsuranceID == selectedID);

                if (travel != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Are you sure you want to delete this Insurance record?",
                        "Confirm deletion",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        db.TravelInsurance.Remove(travel);
                        db.SaveChanges();
                        MessageBox.Show("Insurance records deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadTravelData();
                        LoadTravelIDs();
                        ClearFormFields();
                    }
                }
            }
        }

        private void TravelData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TravelData.SelectedItem != null)
            {
                TravelInsurance travel = (TravelInsurance)TravelData.SelectedItem;

                txtDestination.Text = travel.Destination.ToString();
                txtSDate.Text = travel.TravelDate.ToString("dd-MM-yyyy");
                txtEDate.Text = travel.ReturnDate.ToString("dd-MM-yyyy");
                txtContact.Text = travel.InternationalContactNumber.ToString();
                txtCoverage.Text = travel.CoverageAmount.ToString();
                CheckboxBag.IsChecked = travel?.LostBags ?? false;
                CheckboxLegal.IsChecked = travel?.LegalCosts ?? false;
                CheckboxEmergency.IsChecked = travel?.EmergencyMedicalExpenses ?? false;
                CheckboxCancellation.IsChecked = travel?.CancellationCost ?? false;
                CheckboxSingle.IsChecked = travel?.Single ?? false;
                CheckboxSolo.IsChecked = travel?.Solo ?? false;
                CheckboxEuropean.IsChecked = travel?.European ?? false;
                CheckboxFamily.IsChecked = travel?.Family ?? false;
                CheckboxSummer.IsChecked = travel?.Summer ?? false;
                CheckboxGap.IsChecked = travel?.Gap ?? false;
                CheckboxWorldwide.IsChecked = travel?.Worldwide ?? false;
                CheckboxWinter.IsChecked = travel?.Winter ?? false;
                txtPolicyID.Content = travel.PolicyID.ToString();
                txtIsApproved.Content = travel.IsApproverd ? "Approved" : "Not Approved";
                cmbInsuranceID.SelectedItem = travel.TInsuranceID;
            }
        }

        private void cmbInsuranceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var travel = db.TravelInsurance.FirstOrDefault(t => t.TInsuranceID == selectedID);

                if (travel != null)
                {
                    txtDestination.Text = travel.Destination.ToString();
                    txtSDate.Text = travel.TravelDate.ToString("dd-MM-yyyy");
                    txtEDate.Text = travel.ReturnDate.ToString("dd-MM-yyyy");
                    txtContact.Text = travel.InternationalContactNumber.ToString();
                    txtCoverage.Text = travel.CoverageAmount.ToString();
                    CheckboxBag.IsChecked = travel?.LostBags ?? false;
                    CheckboxLegal.IsChecked = travel?.LegalCosts ?? false;
                    CheckboxEmergency.IsChecked = travel?.EmergencyMedicalExpenses ?? false;
                    CheckboxCancellation.IsChecked = travel?.CancellationCost ?? false;
                    CheckboxSingle.IsChecked = travel?.Single ?? false;
                    CheckboxSolo.IsChecked = travel?.Solo ?? false;
                    CheckboxEuropean.IsChecked = travel?.European ?? false;
                    CheckboxFamily.IsChecked = travel?.Family ?? false;
                    CheckboxSummer.IsChecked = travel?.Summer ?? false;
                    CheckboxGap.IsChecked = travel?.Gap ?? false;
                    CheckboxWorldwide.IsChecked = travel?.Worldwide ?? false;
                    CheckboxWinter.IsChecked = travel?.Winter ?? false;
                    txtPolicyID.Content = travel.PolicyID.ToString();
                    txtIsApproved.Content = travel.IsApproverd ? "Approved" : "Not Approved";
                }
            }
        }

        private void ClearFormFields()
        {
            txtDestination.Text = string.Empty;
            txtSDate.Text = string.Empty;
            txtEDate.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtCoverage.Text = string.Empty;
            CheckboxBag.IsChecked = false;
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
            cmbInsuranceID.SelectedIndex = -1;
            txtPolicyID.Content = string.Empty;
        }
    }
}
