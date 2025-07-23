using System.Windows;
using AiLaTrieuPhu_DEMO.ViewModel;

namespace AiLaTrieuPhu_DEMO.View
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            SoundHelper.PlayClick();
            if (DataContext is RegisterViewModel vm)
            {
                vm.Password = passwordBox.Password;
                vm.ConfirmPassword = confirmBox.Password;
            }
        }
        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            SoundHelper.PlayClick();
            this.Close();
        }

    }
}
