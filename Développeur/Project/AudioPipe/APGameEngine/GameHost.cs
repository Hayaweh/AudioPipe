using System;
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
using System.Runtime.InteropServices;

namespace APGameEngine
{
    /// <summary>
    /// Interaction logic for GameHost.xaml
    /// </summary>
    public partial class GameHost : Window
    {
        #region Fields

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool m_doneRun;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Game m_game;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Game m_gameControl;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private Window m_topLevelControl;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal Window m_frontWindow;

        #endregion

        #region Events

        private void HookElementEvents()
        {

        }

        void m_topLevelControl_Closing(object sender, System.ComponentModel.CancelEventArgs args)
        {

        }

        void MainWindow_LocationChanged(object sender, EventArgs args)
        {

        }

        void Game_SizeChanged(object sender, SizeChangedEventArgs args)
        {

        }

        #endregion

        #region Properties

        internal object WPFHost
        {
            get
            {
                return this.m_frontWindow;
            }
            set
            {
                this.m_frontWindow.Content = value;
            }
        }

        public Window TopLevelWindow
        {
            get
            {
                if (this.m_topLevelControl != Window.GetWindow(this.m_game))
                {
                    this.m_topLevelControl = Window.GetWindow(this.m_game);
                    this.Owner = this.m_topLevelControl;
                    this.m_topLevelControl.Closing -= new System.ComponentModel.CancelEventHandler(m_topLevelControl_Closing);
                }
            }
        }

        public IntPtr Handle
        {
            get
            {
                return new System.Windows.Interop.WindowInteropHelper(this).Handle;
            }
        }

        #endregion
    }
}
