﻿using AiLaTrieuPhu_DEMO;
using AiLaTrieuPhu_DEMO.Model;
using AiLaTrieuPhu_DEMO.View;
using AiLaTrieuPhu_DEMO.ViewModel;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace AiLaTrieuPhu_DEMO.View 
{
    /// <summary>
    /// Interaction logic for MenuGame.xaml
    /// </summary>
    public partial class MenuGameWindow : Window
    {
        public MenuGameWindow()
        {
            InitializeComponent();
            PlayMenuMusicOnce(); // 🔁 Chỉ gọi nhạc 1 lần
        }

        private void PlayMenuMusicOnce()
        {
            if (MenuMusicController.MenuMusicPlayer == null)
            {
                try
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Music", "menu.mp3");
                    if (File.Exists(path))
                    {
                        MediaPlayer player = new MediaPlayer();
                        player.Open(new Uri(path, UriKind.Absolute));
                        player.Volume = 0.7;

                        // ❌ Không lặp lại nhạc
                        // player.MediaEnded += (s, e) => player.Position = TimeSpan.Zero;

                        player.Play();

                        MenuMusicController.MenuMusicPlayer = player; // Lưu lại player tĩnh
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy file menu_theme.mp3 trong thư mục Music!", "Lỗi âm thanh", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi phát nhạc menu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            SoundHelper.PlayClick();
            var loginWin = new AiLaTrieuPhu_DEMO.View.LoginWindow();
            var result = loginWin.ShowDialog();

            if (result == true)
            {
                var currentUser = AccountService.CurrentAccount;

                // 🛑 Tắt nhạc khi vào chơi game
                MenuMusicController.StopMenuMusic();

                if (currentUser != null && currentUser.Role == "Admin")
                {
                    MessageBox.Show("Đây là tài khoản Admin. Chuyển sang dashboard admin.");
                    this.Close();
                }
                else if (currentUser != null && currentUser.Role == "Guest")
                {
                    MainWindow menu = new MainWindow();
                    menu.Show();
                    this.Close();
                }
            }
        }

        private void AboutUs_Click(object sender, RoutedEventArgs e)
        {
            SoundHelper.PlayClick();
            MenuMusicController.StopMenuMusic();
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Close();
        }

        private void HowToPlay_Click(object sender, RoutedEventArgs e)
        {
            SoundHelper.PlayClick();
            MenuMusicController.StopMenuMusic();
            HowToPlay howToPlay = new HowToPlay();
            howToPlay.Show();
            this.Close();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            SoundHelper.PlayClick();
            this.Close();
        }

        private void HuongDan_Click(object sender, RoutedEventArgs e)
        {
            // Nếu có phần hướng dẫn
        }
    }

    // ✅ Controller giữ nhạc tĩnh 1 lần duy nhất
    public static class MenuMusicController
    {
        public static MediaPlayer MenuMusicPlayer;

        public static void StopMenuMusic()
        {
            if (MenuMusicPlayer != null)
            {
                MenuMusicPlayer.Stop();
                MenuMusicPlayer.Close();
                MenuMusicPlayer = null;
            }
        }
    }
}
