using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UIManager.ViewModels;

namespace UIManager.Views
{
    public class AnimeListPageView : UserControl
    {
        public AnimeListPageView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}