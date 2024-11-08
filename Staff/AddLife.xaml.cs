using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Insurance_Management_System.Staff
{

    public partial class AddLife : Window
    {
        private InsuranceManagementEntities db = new InsuranceManagementEntities();
        public AddLife()
        {
            InitializeComponent();
            LoadPolicyIDs();
        }

        private void LoadPolicyIDs()
        {
            var policyIDs = db.Policies.Select(l => l.PolicyID).ToList();
            cmbPolicyNumber.ItemsSource = policyIDs;
        }

        private bool IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(txtHeight.Text) ||
                string.IsNullOrWhiteSpace(txtWeight.Text) ||
                string.IsNullOrWhiteSpace(txtNHS.Text) ||
                string.IsNullOrWhiteSpace(txtAnnual.Text) ||
                string.IsNullOrWhiteSpace(txtBenefitiary.Text) ||
                string.IsNullOrWhiteSpace(txtRelationship.Text))
            {
                MessageBox.Show("Please fill in all the required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!(Radio10_yr.IsChecked == true || Radio20_yr.IsChecked == true || Radio30_yr.IsChecked == true ||
                  RadioUniversal.IsChecked == true || RadioWhole.IsChecked == true || RadioVariable.IsChecked == true))
            {
                MessageBox.Show("Please select a policy type.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!(RadioEmployee.IsChecked == true || RadioUnemployed.IsChecked == true || RadioSelf.IsChecked == true))
            {
                MessageBox.Show("Please select an employment status.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void ClearFormFields()
        {
            txtHeight.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtNHS.Text = string.Empty;
            txtHealth.Text = string.Empty;
            txtAnnual.Text = string.Empty;
            txtBenefitiary.Text = string.Empty;
            txtRelationship.Text = string.Empty;
            txtCoverage.Text = string.Empty;

            cmbPolicyNumber.SelectedIndex = -1;

            CheckboxFamily.IsChecked = false;
            CheckboxDebt.IsChecked = false;
            CheckboxEstate.IsChecked = false;
            CheckboxBusiness.IsChecked = false;
            CheckboxInstability.IsChecked = false;
            CheckboxAll.IsChecked = false;

            Radio10_yr.IsChecked = false;
            Radio20_yr.IsChecked = false;
            Radio30_yr.IsChecked = false;
            RadioUniversal.IsChecked = false;
            RadioWhole.IsChecked = false;
            RadioVariable.IsChecked = false;
            RadioEmployee.IsChecked = false;
            RadioUnemployed.IsChecked = false;
            RadioSelf.IsChecked = false;
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

            ViewLife ViewWindow = new ViewLife();
            ViewWindow.Show();
            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string nhs = txtNHS.Text;

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

                var existingNHS = db.LifeInsurance
                         .FirstOrDefault(l => l.NHSNumber == nhs);

                if (existingNHS != null)
                {
                    MessageBox.Show("National Health Insurance already registered. Please use a different one.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int selectedPolicyID = (int)cmbPolicyNumber.SelectedItem;

                bool family = CheckboxFamily.IsChecked ?? false;
                bool debt = CheckboxDebt.IsChecked ?? false;
                bool estate = CheckboxEstate.IsChecked ?? false;
                bool business = CheckboxBusiness.IsChecked ?? false;
                bool instability = CheckboxInstability.IsChecked ?? false;

                if (CheckboxAll.IsChecked == true)
                {
                    family = debt = estate = business = instability = true;
                }

                var newLifeInsurance = new LifeInsurance
                {
                    Height_cm = txtHeight.Text,
                    Weight_kg = txtWeight.Text,
                    NHSNumber = txtNHS.Text,
                    Health_Issues = txtHealth.Text,
                    Income = txtAnnual.Text,
                    Benefitiary = txtBenefitiary.Text,
                    Relationship_B = txtRelationship.Text,
                    CoverageAmount = coverageAmount,
                    PolicyID = selectedPolicyID,
                    Family = family,
                    Debt = debt,
                    Business = business, 
                    Estate = estate, 
                    Gaurantee = instability,
                    Year_10 = Radio10_yr.IsChecked ?? false,
                    Year_20 = Radio20_yr.IsChecked ?? false,
                    Year_30 = Radio30_yr.IsChecked ?? false,
                    Univesal = RadioUniversal.IsChecked ?? false,
                    Whole = RadioWhole.IsChecked ?? false,
                    Variable = RadioVariable.IsChecked ?? false,
                    Employee = RadioEmployee.IsChecked ?? false,
                    Unemployed = RadioUnemployed.IsChecked ?? false,
                    Self_E = RadioSelf.IsChecked ?? false,
                    IsApproverd = false,
                };


                db.LifeInsurance.Add(newLifeInsurance);
                db.SaveChanges();

                MessageBox.Show("Life insurance added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearFormFields();
            }
        }

        private void cmbPolicyNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
