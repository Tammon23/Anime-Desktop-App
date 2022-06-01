using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UIManager.Views
{
    public partial class NotificationView : UserControl
    {
        public NotificationView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}