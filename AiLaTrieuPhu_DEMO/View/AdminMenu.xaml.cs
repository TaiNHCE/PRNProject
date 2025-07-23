using System.Windows;
using AiLaTrieuPhu_DEMO.View;
using AiLaTrieuPhu_DEMO.ViewModel;  // Bạn phải Add Reference sang project Account
using AiLaTrieuPhu_DEMO.Model;



namespace AiLaTrieuPhu_DEMO.View
{
    /// <summary>
    /// Interaction logic for MenuGame.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        public AdminMenu()
        {
            InitializeComponent();
        }


        private void acc_Click(object sender, RoutedEventArgs e)
        {
            SoundHelper.PlayClick();
            PlayerManager aboutUs = new PlayerManager();
            aboutUs.Show();
            this.Close();
        }

        private void hoi_Click(object sender, RoutedEventArgs e)
        {
            SoundHelper.PlayClick();
            AdminManagementWindow howToPlay = new AdminManagementWindow();
            howToPlay.Show();
            this.Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            SoundHelper.PlayClick();
            MenuGameWindow menu = new MenuGameWindow();
            menu.Show();
            this.Close();
        }

    }
}
