using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using EffectCache = System.Windows.Media.Effects.Effect;

namespace APGameEngine
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControls : UserControl
    {
        public UserControls()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            EffectCache cache = PlayButton.Effect;

            if (PlayButton.IsPressed)
                PlayButton.Effect = null;
            else
                PlayButton.Effect = cache;
        }
    }
}
