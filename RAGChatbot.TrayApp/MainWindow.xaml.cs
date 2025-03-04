using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Web.WebView2.Wpf;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows.Interop;

namespace RAGChatbot.TrayApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private const int WM_HOTKEY = 0x0312;
        private const int MOD_CONTROL = 0x0002; // Ctrl key
        private const int hotkeyId = 1;         // Unique ID for the hotkey
        private static MainWindow instance;

        public MainWindow()
        {
            InitializeComponent();
            instance = this;

            // Position the window in the lower-right
            var workArea = SystemParameters.WorkArea;
            this.Left = workArea.Right - this.Width;
            this.Top = workArea.Bottom - this.Height;

            // Start with the window visible on first run
            this.WindowState = WindowState.Normal; // Show normally, not minimized
            this.ShowInTaskbar = false;            // Keep it out of the taskbar
            this.Show();                           // Display the window

            InitializeTray();
            LoadMiniChat();
        }

        private void InitializeTray()
        {
            notifyIconMiniChat.TrayMouseDoubleClick += NotifyIconMiniChat_TrayMouseDoubleClick;
        }

        private void LoadMiniChat()
        {
            webView2MiniChat.CoreWebView2InitializationCompleted += (sender, e) =>
            {
                if (e.IsSuccess)
                {
                    webView2MiniChat.CoreWebView2.Navigate("https://localhost:7043/mini-chat");
                }
                else
                {
                    MessageBox.Show($"WebView2 initialization failed: {e.InitializationException.Message}");
                }
            };
            webView2MiniChat.EnsureCoreWebView2Async(null);
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var helper = new WindowInteropHelper(this);
            IntPtr handle = helper.Handle;
            if (handle == IntPtr.Zero)
            {
                MessageBox.Show("Invalid window handle!");
                return;
            }

            bool success = RegisterHotKey(handle, hotkeyId, MOD_CONTROL, KeyInterop.VirtualKeyFromKey(Key.M));
            if (!success)
            {
                int errorCode = Marshal.GetLastWin32Error();
                MessageBox.Show($"Failed to register Ctrl+M. Error code: {errorCode}. It might be in use by another app.");
            }

            HwndSource source = HwndSource.FromHwnd(handle);
            if (source == null)
            {
                MessageBox.Show("Failed to create HwndSource!");
                return;
            }
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY && wParam.ToInt32() == hotkeyId)
            {
                ToggleVisibility();
                handled = true;
            }
            return IntPtr.Zero;
        }

        private void ToggleVisibility()
        {
            if (this.Visibility == Visibility.Visible && this.WindowState != WindowState.Minimized)
            {
                this.Hide();
            }
            else
            {
                var workArea = SystemParameters.WorkArea;
                this.Left = workArea.Right - this.Width;
                this.Top = workArea.Bottom - this.Height;
                this.Show();
                this.WindowState = WindowState.Normal;
                this.Activate();
            }
        }

        private void NotifyIconMiniChat_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ToggleVisibility();
        }

        private void ShowMiniChat_Click(object sender, RoutedEventArgs e)
        {
            var workArea = SystemParameters.WorkArea;
            this.Left = workArea.Right - this.Width;
            this.Top = workArea.Bottom - this.Height;
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Activate();
        }

        private void HideMiniChat_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var helper = new WindowInteropHelper(this);
            UnregisterHotKey(helper.Handle, hotkeyId);
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
