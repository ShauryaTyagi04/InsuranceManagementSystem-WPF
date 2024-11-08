using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Insurance_Management_System.Admin
{
    public partial class UpdateStaff : Window
    {
        InsuranceManagementEntities db = new InsuranceManagementEntities();

        public UpdateStaff()
        {
            InitializeComponent();
            LoadEmployeeIDs();
            LoadRoles();
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            var employees = db.Employees.ToList();
            EmployeeData.ItemsSource = employees;
        }

        private void LoadEmployeeIDs()
        {
            var employeeIDs = db.Employees.Select(emp => emp.EmployeeID).ToList();
            Employee_ID.ItemsSource = employeeIDs;
        }
        private void LoadRoles()
        {
            cmbRole.Items.Add("staff");
            cmbRole.Items.Add("manager");
        }

        private void ID_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Employee_ID.SelectedItem != null)
            {
                int selectedID = (int)Employee_ID.SelectedItem;
                var employee = db.Employees.FirstOrDefault(emp => emp.EmployeeID == selectedID);

                if (employee != null)
                {
                    txtFName.Text = employee.FirstName;
                    txtLName.Text = employee.LastName;
                    txtEmail.Text = employee.EmailAddress;
                    txtPassword.Text = employee.Password;
                    txtUsername.Text = employee.UserName;
                    cmbRole.SelectedItem = employee.Role;
                    txtIsApproved.Content = employee.isApproved;
                }
            }

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

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Employee_ID.SelectedItem != null)
            {
                int selectedID = (int)Employee_ID.SelectedItem;
                var employee = db.Employees.FirstOrDefault(emp => emp.EmployeeID == selectedID);

                if (employee != null)
                {
                    employee.isApproved = true;
                    db.SaveChanges();
                    MessageBox.Show("Employee application approved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtIsApproved.Content = "Approved";
                    LoadEmployeeData();
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Employee_ID.SelectedItem != null)
            {
                int selectedID = (int)Employee_ID.SelectedItem;
                var employee = db.Employees.FirstOrDefault(emp => emp.EmployeeID == selectedID);

                if (employee != null)
                {
                    employee.FirstName = txtFName.Text;
                    employee.LastName = txtLName.Text;
                    employee.EmailAddress = txtEmail.Text;
                    employee.UserName = txtUsername.Text;
                    employee.Role = cmbRole.Text;
                    employee.isApproved = true;  

                    db.SaveChanges();
                    MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadEmployeeData();
                }
            }
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            if (Employee_ID.SelectedItem != null)
            {
                int selectedID = (int)Employee_ID.SelectedItem;
                var employee = db.Employees.FirstOrDefault(emp => emp.EmployeeID == selectedID);

                if (employee != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Are you sure you want to reject this employee application?",
                        "Confirm Rejection",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        db.Employees.Remove(employee);
                        db.SaveChanges();
                        MessageBox.Show("Employee application rejected successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        LoadEmployeeIDs();
                        LoadEmployeeData(); 
                        ClearFields(); 
                    }
                }
            }
        }


        private void ClearFields()
        {
            txtFName.Text = string.Empty;
            txtLName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
            cmbRole.SelectedIndex = -1; 
            Employee_ID.SelectedIndex = -1; 
        }

        private void EmployeeData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeData.SelectedItem != null)
            {
                Employees selectedEmployee = (Employees)EmployeeData.SelectedItem;

                Employee_ID.SelectedItem = selectedEmployee.EmployeeID; 
                txtFName.Text = selectedEmployee.FirstName;
                txtLName.Text = selectedEmployee.LastName;
                txtEmail.Text = selectedEmployee.EmailAddress;
                txtPassword.Text = selectedEmployee.Password;
                txtUsername.Text = selectedEmployee.UserName;
                cmbRole.SelectedItem = selectedEmployee.Role;
                txtIsApproved.Content = selectedEmployee.isApproved;
            }
        }
    }
}
