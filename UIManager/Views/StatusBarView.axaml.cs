using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UIManager.Views
{
    public class StatusBarView : UserControl
    {
        /*public static TextBox SearchBox;*/
        
        public StatusBarView()
        {
            InitializeComponent();
            /*SearchBox = this.FindControl<TextBox>("SearchTextBox");*/
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}