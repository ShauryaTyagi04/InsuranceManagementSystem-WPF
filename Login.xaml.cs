using Insurance_Management_System.Admin;
using Insurance_Management_System.Staff;
using System.Linq; 
using System.Windows;
using System.Windows.Input;

namespace Insurance_Management_System
{
    public partial class Login : Window
    {
        InsuranceManagementEntities db = new InsuranceManagementEntities();

        public Login()
        {
            InitializeComponent();
        }

        private void SignUpRedirect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Signup signupWindow = new Signup();
            signupWindow.Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string user = txtEmail.Text;
            string password = txtPassword.Text;

            var employee = db.Employees
                             .FirstOrDefault(emp => emp.UserName == user && emp.Password == password);

            if (employee != null)
            {
                if (employee.isApproved.HasValue && employee.isApproved.Value)
                {
                    MessageBox.Show("Successfully logged in.", "Login Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (employee.Role == "staff")
                    {
                        StaffHome staffHomeWindow = new StaffHome();
                        staffHomeWindow.Show();
                    }
                    else if (employee.Role == "manager")
                    {
                        AdminHome adminHomeWindow = new AdminHome();
                        adminHomeWindow.Show();
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Your account is not approved. Please contact the administrator.", "Account Not Approved", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
