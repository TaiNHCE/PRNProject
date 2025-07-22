using System.Windows;
using AdminManagement.View;
using AiLaTrieuPhu_DEMO.ViewModel;  // Bạn phải Add Reference sang project Account
using AiLaTrieuPhu_DEMO.Model;



namespace AdminManagement
{
    /// <summary>
    /// Interaction logic for MenuGame.xaml
    /// </summary>
    public partial class admindb : Window
    {
        public admindb()
        {
            InitializeComponent();
        }

       
        private void acc_Click(object sender, RoutedEventArgs e)
        {
            PlayerManager aboutUs = new PlayerManager();
            aboutUs.Show();
            this.Close();
        }
      
        private void hoi_Click(object sender, RoutedEventArgs e)
        {
            AdminManagementWindow howToPlay = new AdminManagementWindow();
            howToPlay.Show();
            this.Close();
        }
        
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MenuGameWindow menu = new MenuGameWindow();
            menu.Show();
            this.Close();
        }

    }
}
