using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using AiLaTrieuPhu_DEMO.Model;
using System.Security.Cryptography;
using System.Text;

namespace AiLaTrieuPhu_DEMO.ViewModel
{
    public static class AccountService
    {
        // Đường dẫn tới accounts.json nằm ở thư mục project
        private static readonly string FilePath = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName,
            "AiLaTrieuPhu_DEMO", "accounts.json");

        public static Account CurrentAccount { get; set; }

        // ------------------ MD5 Helper ------------------
        public static string EncodeMD5(string input)

        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (var b in hashBytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
        // ------------------ End MD5 Helper ------------------

        // Load all accounts
        public static List<Account> LoadAccounts()
        {
            if (!File.Exists(FilePath))
                return new List<Account>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Account>>(json) ?? new List<Account>();
        }

        // Save all accounts
        private static void SaveAccounts(List<Account> accounts)
        {
            File.WriteAllText(FilePath, JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true }));
        }

        // -------- LOGIN --------
        public static Account Login(string username, string password)
        {

            var accounts = LoadAccounts();
            string encodedPw = EncodeMD5(password); // Mã hóa password nhập vào


            var acc = accounts.FirstOrDefault(a =>
                a.Username.Trim().Equals(username.Trim(), System.StringComparison.OrdinalIgnoreCase) &&
                a.Password == encodedPw);

            if (acc != null)
            {
                if (acc.IsLocked)
                {
                    MessageBox.Show("Tài khoản bạn đã bị khóa.", "Lỗi đăng nhập", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                CurrentAccount = acc;
            }
            return acc;
        }

        // -------- REGISTER --------
        public static bool Register(string username, string password, string email, string role = "Guest")
        {
            try
            {
                var accounts = LoadAccounts();
                if (accounts.Any(a => a.Username == username || a.Email == email))
                    return false;

                var newAcc = new Account
                {
                    Username = username,
                    Password = EncodeMD5(password),  // Mã hóa mật khẩu
                    Email = email,
                    Role = role
                };
                accounts.Add(newAcc);

                SaveAccounts(accounts);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // -------- FIND BY EMAIL --------
        public static Account FindByEmail(string email)
        {
            var accounts = LoadAccounts();
            return accounts.FirstOrDefault(a => a.Email == email);
        }

        // -------- CHANGE PASSWORD (FORGOT PASSWORD) --------
        public static bool ChangePassword(string email, string newPassword)
        {
            var accounts = LoadAccounts();
            var acc = accounts.FirstOrDefault(a => a.Email == email);
            if (acc == null) return false;
            acc.Password = EncodeMD5(newPassword); // Mã hóa mật khẩu mới!
            SaveAccounts(accounts);
            return true;
        }

        // Debug method
        public static string GetFilePath() => FilePath;

        public static bool ToggleLockAccount(string username)
        {
            var accounts = LoadAccounts();
            var account = accounts.FirstOrDefault(a => a.Username == username);
            if (account == null) return false;

            account.IsLocked = !account.IsLocked;
            SaveAccounts(accounts);
            return true;
        }

        public static bool DeleteAccount(string username)
        {
            var accounts = LoadAccounts();
            var account = accounts.FirstOrDefault(a => a.Username == username);
            if (account == null) return false;

            accounts.Remove(account);
            SaveAccounts(accounts);
            return true;
        }
    }
}
