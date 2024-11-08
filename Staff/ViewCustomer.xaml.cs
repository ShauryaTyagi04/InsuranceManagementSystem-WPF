using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Insurance_Management_System.Staff
{
    public partial class ViewCustomer : Window
    {

        InsuranceManagementEntities db = new InsuranceManagementEntities();

        public ViewCustomer()
        {
            InitializeComponent();
            LoadCustomerData();
            LoadCustomerIDs();
        }

        private void LoadCustomerData()
        {
            var customers = db.Customers.ToList();
            CustomerData.ItemsSource = customers;
        }

        private void LoadCustomerIDs()
        {
            var customerIDs = db.Customers.Select(c => c.CustomerID).ToList();
            cmbCustomerID.ItemsSource = customerIDs;
        }

        private void cmbCustomerID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCustomerID.SelectedItem != null)
            {
                int selectedID = (int)cmbCustomerID.SelectedItem;
                var customer = db.Customers.FirstOrDefault(c => c.CustomerID == selectedID);

                if (customer != null)
                {
                    txtFName.Text = customer.FirstName;
                    txtLName.Text = customer.LastName;
                    txtDOB.Text = customer.DOB.ToString("dd-MM-yyyy"); 
                    txtNumber.Text = customer.ContactNumber;
                    txtAltNumber.Text = customer.AlternateContactNumber;
                    txtEmail.Text = customer.EmailAddress;
                    txtAddress.Text = customer.Address;
                    txtPost.Text = customer.PostCode;
                    SetSelectedSex(customer.Sex);
                    SetSelectedMaritalStatus(customer.MaritalStatus);
                    txtNationality.Text = customer.Nationality;
                    txtOccupation.Text = customer.Occupation;
                    txtNI.Text = customer.NationalInsurance;
                    txtIsApproved.Content = customer.isApproved ? "Approved" : "Not Approved";
                }
            }
        }

        private void SetSelectedSex(string sex)
        {
            switch (sex)
            {
                case "M":
                    MaleRadioButton.IsChecked = true;
                    break;
                case "F":
                    FemaleRadioButton.IsChecked = true;
                    break;
                case "O":
                    RatherNotSayRadioButton.IsChecked = true;
                    break;
            }
        }

        private void SetSelectedMaritalStatus(string status)
        {
            switch (status)
            {
                case "married":
                    MarriedRadioButton.IsChecked = true;
                    break;
                case "single":
                    SingleRadioButton.IsChecked = true;
                    break;
                case "divorced":
                    DivorcedRadioButton.IsChecked = true;
                    break;
                case "engaged":
                    EngagedRadioButton.IsChecked = true;
                    break;
            }
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

        private void CustomerData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerData.SelectedItem != null)
            {
                Customers selectedCustomer = (Customers)CustomerData.SelectedItem;

                cmbCustomerID.SelectedItem = selectedCustomer.CustomerID;
                txtFName.Text = selectedCustomer.FirstName;
                txtLName.Text = selectedCustomer.LastName;
                txtDOB.Text = selectedCustomer.DOB.ToString("dd-MM-yyyy"); 
                txtNumber.Text = selectedCustomer.ContactNumber;
                txtAltNumber.Text = selectedCustomer.AlternateContactNumber;
                txtEmail.Text = selectedCustomer.EmailAddress;
                txtAddress.Text = selectedCustomer.Address;
                txtPost.Text = selectedCustomer.PostCode;
                SetSelectedSex(selectedCustomer.Sex);
                SetSelectedMaritalStatus(selectedCustomer.MaritalStatus);
                txtNationality.Text = selectedCustomer.Nationality;
                txtOccupation.Text = selectedCustomer.Occupation;
                txtNI.Text = selectedCustomer.NationalInsurance;
                txtIsApproved.Content = selectedCustomer.isApproved ? "Approved" : "Not Approved";
            }
        }

        private void ClearForm()
        {
            txtFName.Clear();
            txtLName.Clear();
            txtDOB.Clear();
            txtNumber.Clear();
            txtAltNumber.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtPost.Clear();
            txtNationality.Clear();
            txtOccupation.Clear();
            txtNI.Clear();
            txtIsApproved.Content = "";
            cmbCustomerID.SelectedIndex = -1;
            CustomerData.SelectedItem = -1;
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

            DateTime dob;
            if (!DateTime.TryParseExact(txtDOB.Text, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out dob))
            {
                MessageBox.Show("Date of Birth must be in DD-MM-YYYY format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cmbCustomerID.SelectedItem != null)
            {
                int selectedID = (int)cmbCustomerID.SelectedItem;
                var customer = db.Customers.FirstOrDefault(c => c.CustomerID == selectedID);
                if (customer != null)
                {

                    customer.FirstName = txtFName.Text;
                    customer.LastName = txtLName.Text;
                    customer.DOB = dob;
                    customer.ContactNumber = txtNumber.Text;
                    customer.AlternateContactNumber = txtAltNumber.Text;
                    customer.EmailAddress = txtEmail.Text;
                    customer.Address = txtAddress.Text;
                    customer.PostCode = txtPost.Text;
                    customer.Sex = GetSelectedSex();
                    customer.MaritalStatus = GetSelectedMaritalStatus();
                    customer.Nationality = txtNationality.Text;
                    customer.Occupation = txtOccupation.Text;
                    customer.NationalInsurance = txtNI.Text;
                    customer.isApproved = txtIsApproved.Content.ToString() == "Approved";

                    db.SaveChanges();
                    MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadCustomerData();
                }
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCustomerID.SelectedItem != null)
            {
                int selectedID = (int)cmbCustomerID.SelectedItem;
                var customer = db.Customers.FirstOrDefault(c => c.CustomerID == selectedID);

                if (customer != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Are you sure you want to delete this customer's record?",
                        "Confirm deletion",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        db.Customers.Remove(customer);
                        db.SaveChanges();
                        MessageBox.Show("Customer records deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadCustomerData();
                        LoadCustomerIDs();
                        ClearForm();
                    }
                }
            }
        }
    }
}
