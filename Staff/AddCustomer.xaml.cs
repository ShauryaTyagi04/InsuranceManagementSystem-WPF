using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Insurance_Management_System.Staff
{

    public partial class AddCustomer : Window
    {
        private InsuranceManagementEntities db = new InsuranceManagementEntities();
        public AddCustomer()
        {
            InitializeComponent();
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

            ViewCustomer ViewWindow = new ViewCustomer();
            ViewWindow.Show();
            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            string email = txtEmail.Text;
            string ni = txtNI.Text;

            if (IsFormValid())
            {

                DateTime dob;
                if (!DateTime.TryParseExact(txtDOB.Text, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out dob))
                {
                    MessageBox.Show("Date of Birth must be in DD-MM-YYYY format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (dob > DateTime.Now)
                {
                    MessageBox.Show("Date of Birth cannot be a future date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var existingEmail = db.Customers
                         .FirstOrDefault(c => c.EmailAddress == email);

                if (existingEmail != null)
                {
                    MessageBox.Show("Email address already exists. Please use a different one.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var existingNI = db.Customers
                         .FirstOrDefault(c => c.NationalInsurance == ni);

                if (existingNI != null)
                {
                    MessageBox.Show("National Insurance already registered. Please use a different one.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newCustomer = new Customers
                {
                    FirstName = txtFName.Text,
                    LastName = txtLName.Text,
                    DOB = dob, 
                    ContactNumber = txtNumber.Text,
                    AlternateContactNumber = txtAltNumber.Text,
                    EmailAddress = txtEmail.Text,
                    Address = txtAddress.Text,
                    PostCode = txtPost.Text,
                    Sex = GetSelectedSex(),
                    MaritalStatus = GetSelectedMaritalStatus(),
                    Nationality = txtNationality.Text,
                    Occupation = txtOccupation.Text,
                    NationalInsurance = txtNI.Text,
                    isApproved = false 
                };

                if (db.Customers.Any(c => c.EmailAddress == newCustomer.EmailAddress))
                {
                    MessageBox.Show("Email address must be unique.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (db.Customers.Any(c => c.NationalInsurance == newCustomer.NationalInsurance))
                {
                    MessageBox.Show("National Insurance number must be unique.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                db.Customers.Add(newCustomer);
                db.SaveChanges();

                MessageBox.Show("Customer added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearForm();
            }
        }

        private bool IsFormValid()
        {
            if (string.IsNullOrWhiteSpace(txtFName.Text) ||
                string.IsNullOrWhiteSpace(txtLName.Text) ||
                string.IsNullOrWhiteSpace(txtDOB.Text) ||
                string.IsNullOrWhiteSpace(txtNumber.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtNI.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private string GetSelectedSex()
        {
            if (MaleRadioButton.IsChecked == true)
                return "M";
            if (FemaleRadioButton.IsChecked == true)
                return "F";
            return "O";
        }

        private string GetSelectedMaritalStatus()
        {
            if (MarriedRadioButton.IsChecked == true)
                return "married";
            if (SingleRadioButton.IsChecked == true)
                return "single";
            if (DivorcedRadioButton.IsChecked == true)
                return "divorced";
            return "engaged";
        }

        private void ClearForm()
        {
            txtFName.Text = "";
            txtLName.Text = "";
            txtDOB.Text = "";
            txtNumber.Text = "";
            txtAltNumber.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtPost.Text = "";
            txtNationality.Text = "";
            txtOccupation.Text = "";
            txtNI.Text = "";
            MaleRadioButton.IsChecked = false;
            FemaleRadioButton.IsChecked = false;
            RatherNotSayRadioButton.IsChecked = false;
            MarriedRadioButton.IsChecked = false;
            SingleRadioButton.IsChecked = false;
            DivorcedRadioButton.IsChecked = false;
            EngagedRadioButton.IsChecked = false;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}
