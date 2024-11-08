using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Insurance_Management_System.Admin
{
    public partial class UpdateTravel : Window
    {
        InsuranceManagementEntities db = new InsuranceManagementEntities();
        public UpdateTravel()
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
                CheckboxBag.IsChecked = travel?.LostBags;
                CheckboxLegal.IsChecked = travel?.LegalCosts;
                CheckboxEmergency.IsChecked = travel?.EmergencyMedicalExpenses;
                CheckboxCancellation.IsChecked = travel?.CancellationCost;
                CheckboxSingle.IsChecked = travel?.Single;
                CheckboxSolo.IsChecked = travel?.Solo;
                CheckboxEuropean.IsChecked = travel?.European;
                CheckboxFamily.IsChecked = travel?.Family;
                CheckboxSummer.IsChecked = travel?.Summer;
                CheckboxGap.IsChecked = travel?.Gap;
                CheckboxWorldwide.IsChecked = travel?.Worldwide;
                CheckboxWinter.IsChecked = travel?.Winter;
                txtPolicyID.Content = travel.PolicyID.ToString();
                txtIsApproved.Content = travel.IsApproverd ? "Approved" : "Not Approved";
                cmbInsuranceID.SelectedItem = travel.TInsuranceID;
            }
        }

        private void cmbInsuranceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    CheckboxBag.IsChecked = travel?.LostBags;
                    CheckboxLegal.IsChecked = travel?.LegalCosts;
                    CheckboxEmergency.IsChecked = travel?.EmergencyMedicalExpenses;
                    CheckboxCancellation.IsChecked = travel?.CancellationCost;
                    CheckboxSingle.IsChecked = travel?.Single;
                    CheckboxSolo.IsChecked = travel?.Solo;
                    CheckboxEuropean.IsChecked = travel?.European;
                    CheckboxFamily.IsChecked = travel?.Family;
                    CheckboxSummer.IsChecked = travel?.Summer;
                    CheckboxGap.IsChecked = travel?.Gap;
                    CheckboxWorldwide.IsChecked = travel?.Worldwide;
                    CheckboxWinter.IsChecked = travel?.Winter;
                    txtPolicyID.Content = travel.PolicyID.ToString();
                    txtIsApproved.Content = travel.IsApproverd ? "Approved" : "Not Approved";
                }
            }
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var travel = db.TravelInsurance.FirstOrDefault(t => t.TInsuranceID == selectedID);

                if (travel != null)
                {
                    travel.IsApproverd = true;
                    db.SaveChanges();
                    MessageBox.Show("Insurance application approved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtIsApproved.Content = "Approved";
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
                        MessageBox.Show("Insuranec application rejected successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadTravelData();
                        LoadTravelIDs();
                        ClearFormFields();
                    }
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

        private void Update_Click(object sender, RoutedEventArgs e)
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
                    travel.IsApproverd = false;

                    db.SaveChanges();
                    MessageBox.Show("Travel Insurance updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTravelData();
                }
            }
            else
            {
                MessageBox.Show("Please select an Insurance Number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
    }
}
