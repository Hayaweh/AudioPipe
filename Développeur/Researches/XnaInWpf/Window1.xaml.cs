using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Demo;


namespace AvalonDockTest
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {

        public Window1()
        {
            InitializeComponent();
        }

        private void DockableContent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RotatingCubeGame map = new RotatingCubeGame();
            Pouet.Content = map;

            map.Run();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.labelHello.Content = "Hello !";
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

    }
}