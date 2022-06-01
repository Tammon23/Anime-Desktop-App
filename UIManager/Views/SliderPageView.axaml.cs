using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UIManager.Views
{
    public class SliderPageView: UserControl
    {
        public SliderPageView()
        {
            InitializeComponent();
            /*this.FindControl<TextBlock>("idk").Bind(TextBlock.TextProperty,
                StatusBarView.SearchBox.GetObservable(TextBox.TextProperty));*/
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}