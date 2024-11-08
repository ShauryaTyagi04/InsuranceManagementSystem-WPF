using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Insurance_Management_System.Staff
{
    public partial class ViewLife : Window
    {
        InsuranceManagementEntities db = new InsuranceManagementEntities();
        public ViewLife()
        {
            InitializeComponent();
            LoadLifeData();
            LoadLifeIDs();
        }

        private void LoadLifeData()
        {
            var life = db.LifeInsurance.ToList();
            LifeData.ItemsSource = life;
            var lifeColumn = LifeData.Columns.FirstOrDefault(l => l.Header.ToString() == "Insurance");
            if (lifeColumn != null)
            {
                LifeData.Columns.Remove(lifeColumn);
            }
        }

        private void LoadLifeIDs()
        {
            var LifeIDs = db.LifeInsurance.Select(l => l.LInsuranceID).ToList();
            cmbInsuranceID.ItemsSource = LifeIDs;
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

            cmbInsuranceID.SelectedIndex = -1;
            txtPolicyID.Content = string.Empty;
            txtIsApproved.Content = string.Empty;

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
            RadioUnemployment.IsChecked = false;
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

            bool family = CheckboxFamily.IsChecked ?? false;
            bool debt = CheckboxDebt.IsChecked ?? false;
            bool estate = CheckboxEstate.IsChecked ?? false;
            bool business = CheckboxBusiness.IsChecked ?? false;
            bool instability = CheckboxInstability.IsChecked ?? false;

            if (CheckboxAll.IsChecked == true)
            {
                family = debt = estate = business = instability = true;
            }

            if (!int.TryParse(txtPolicyID.Content.ToString(), out int policyID))
            {
                return;
            }

            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var life = db.LifeInsurance.FirstOrDefault(p => p.LInsuranceID == selectedID);
                if (life != null)
                {
                    life.Height_cm = txtHeight.Text;
                    life.Weight_kg = txtWeight.Text;
                    life.NHSNumber = txtNHS.Text;
                    life.Health_Issues = txtHealth.Text;
                    life.Income = txtAnnual.Text;
                    life.Benefitiary = txtBenefitiary.Text;
                    life.Relationship_B = txtRelationship.Text;
                    life.Family = family;
                    life.Debt = debt;
                    life.Business = business;
                    life.Estate = estate;
                    life.Gaurantee = instability;
                    life.Year_10 = Radio10_yr.IsChecked ?? false;
                    life.Year_20 = Radio20_yr.IsChecked ?? false;
                    life.Year_30 = Radio30_yr.IsChecked ?? false;
                    life.Univesal = RadioUniversal.IsChecked ?? false;
                    life.Whole = RadioWhole.IsChecked ?? false;
                    life.Variable = RadioVariable.IsChecked ?? false;
                    life.Employee = RadioEmployee.IsChecked ?? false;
                    life.Unemployed = RadioUnemployment.IsChecked ?? false;
                    life.Self_E = RadioSelf.IsChecked ?? false;
                    life.CoverageAmount = coverageAmount;
                    life.PolicyID = policyID;

                    db.SaveChanges();
                    MessageBox.Show("Life Insurance updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadLifeData();
                }
            }

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var life = db.LifeInsurance.FirstOrDefault(t => t.LInsuranceID == selectedID);

                if (life != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Are you sure you want to delete this Insurance record?",
                        "Confirm deletion",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        db.LifeInsurance.Remove(life);
                        db.SaveChanges();
                        MessageBox.Show("Insurance records deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadLifeData();
                        LoadLifeIDs();
                        ClearFormFields();
                    }
                }
            }
        }

        private void LifeData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LifeData.SelectedItem != null)
            {
                LifeInsurance life = (LifeInsurance)LifeData.SelectedItem;

                txtHeight.Text = life.Height_cm.ToString();
                txtWeight.Text = life.Weight_kg.ToString();
                txtNHS.Text = life.NHSNumber.ToString();
                txtHealth.Text = life.Health_Issues.ToString();
                txtAnnual.Text = life.Income.ToString();
                txtBenefitiary.Text = life.Benefitiary.ToString();
                txtRelationship.Text = life.Relationship_B.ToString();
                txtCoverage.Text = life.CoverageAmount.ToString();

                Radio10_yr.IsChecked = life?.Year_10 ?? false;
                Radio20_yr.IsChecked = life?.Year_20 ?? false;
                Radio30_yr.IsChecked = life?.Year_30 ?? false;
                RadioUniversal.IsChecked = life?.Univesal ?? false;
                RadioWhole.IsChecked = life?.Whole ?? false;
                RadioVariable.IsChecked = life?.Variable ?? false;

                CheckboxFamily.IsChecked = life?.Family ?? false;
                CheckboxDebt.IsChecked = life?.Debt ?? false;
                CheckboxEstate.IsChecked = life?.Estate ?? false;
                CheckboxBusiness.IsChecked = life?.Business ?? false;
                CheckboxInstability.IsChecked = life?.Gaurantee ?? false;

                RadioEmployee.IsChecked = life?.Employee ?? false;
                RadioUnemployment.IsChecked = life?.Unemployed ?? false;
                RadioSelf.IsChecked = life?.Self_E ?? false;


                txtPolicyID.Content = life.PolicyID.ToString();
                txtIsApproved.Content = life.IsApproverd ? "Approved" : "Not Approved";
                cmbInsuranceID.SelectedItem = life.LInsuranceID;
            }
        }

        private void cmbInsuranceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInsuranceID.SelectedItem != null)
            {
                int selectedID = (int)cmbInsuranceID.SelectedItem;
                var life = db.LifeInsurance.FirstOrDefault(l => l.LInsuranceID == selectedID);

                if (life != null)
                {
                    txtHeight.Text = life.Height_cm.ToString();
                    txtWeight.Text = life.Weight_kg.ToString();
                    txtNHS.Text = life.NHSNumber.ToString();
                    txtHealth.Text = life.Health_Issues.ToString();
                    txtAnnual.Text = life.Income.ToString();
                    txtBenefitiary.Text = life.Benefitiary.ToString();
                    txtRelationship.Text = life.Relationship_B.ToString();
                    txtCoverage.Text = life.CoverageAmount.ToString();

                    Radio10_yr.IsChecked = life?.Year_10 ?? false;
                    Radio20_yr.IsChecked = life?.Year_20 ?? false;
                    Radio30_yr.IsChecked = life?.Year_30 ?? false;
                    RadioUniversal.IsChecked = life?.Univesal ?? false;
                    RadioWhole.IsChecked = life?.Whole ?? false;
                    RadioVariable.IsChecked = life?.Variable ?? false;

                    CheckboxFamily.IsChecked = life?.Family ?? false;
                    CheckboxDebt.IsChecked = life?.Debt ?? false;
                    CheckboxEstate.IsChecked = life?.Estate ?? false;
                    CheckboxBusiness.IsChecked = life?.Business ?? false;
                    CheckboxInstability.IsChecked = life?.Gaurantee ?? false;

                    RadioEmployee.IsChecked = life?.Employee ?? false;
                    RadioUnemployment.IsChecked = life?.Unemployed ?? false;
                    RadioSelf.IsChecked = life?.Self_E ?? false;


                    txtPolicyID.Content = life.PolicyID.ToString();
                    txtIsApproved.Content = life.IsApproverd ? "Approved" : "Not Approved";
                }
            }
        }
    }
}
