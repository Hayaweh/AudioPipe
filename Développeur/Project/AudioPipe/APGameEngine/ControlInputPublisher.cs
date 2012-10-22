using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace APGameEngine
{
    /// <summary>
    /// Service allowing object to monitor user input for a form or a control
    /// </summary>
    public interface IInputPublisherService
    {
        /// <summary>
        /// Fired when the mouse has been clicked
        /// </summary>
        event MouseEventHandler MouseClick;
        /// <summary>
        /// Fired when a mouse button is pressed down
        /// </summary>
        event MouseEventHandler MouseDown;
        /// <summary>
        /// Fired when a mouse button is released again (after being clicked)
        /// </summary>
        event MouseEventHandler MouseUp;
        /// <summary>
        /// Fired when the mouse has been moved
        /// </summary>
        event MouseEventHandler MouseMove;

        /// <summary>
        /// Fired when a key is pressed down
        /// </summary>
        event KeyEventHandler KeyDown;
        /// <summary>
        /// Fired when a key is released again (after being pressed)
        /// </summary>
        event KeyEventHandler keyUP;
    }

    /// <summary>
    /// Service allowing object to monitor user input for a form or a control
    /// </summary>
    internal class ControlInputPublisher : IInputPublisherService, IDisposable
    {
        /// <summary>
        /// User contol whose input events this publisher makes public
        /// </summary>
        private FrameworkElement control;

        /// <summary>
        /// Fired when the mouse has been clicked
        /// </summary>
        event MouseEventHandler MouseClick;
        /// <summary>
        /// Fired when a mouse button is pressed down
        /// </summary>
        event MouseEventHandler MouseDown;
        /// <summary>
        /// Fired when a mouse button is released again (after being clicked)
        /// </summary>
        event MouseEventHandler MouseUp;
        /// <summary>
        /// Fired when the mouse has been moved
        /// </summary>
        event MouseEventHandler MouseMove;

        /// <summary>
        /// Fired when a key is pressed down
        /// </summary>
        event KeyEventHandler KeyDown;
        /// <summary>
        /// Fired when a key is released again (after being pressed)
        /// </summary>
        event KeyEventHandler KeyUp;

        /// <summary>
        /// Initializes a new user control input event publisher
        /// </summary>
        /// <param name="control">USer control whose input events to publish</param>
        public ControlInputPublisher(FrameworkElement control)
        {
            this.control = control;

            control.MouseDown += new MouseButtonEventHandler(mouseDown);
            control.MouseUp += new MouseButtonEventHandler(mouseUp);
            control.MouseMove += new MouseEventHandler(mouseMove);
            control.KeyDown += new KeyEventHandler(keyDown);
            control.KeyUp += new KeyEventHandler(keyUp);
        }

        /// <summary>
        /// Immediately releases all resources owned by the object
        /// </summary>
        public void Dispose()
        {
            if (this.control != null)
            {
                this.control.MouseDown -= new MouseButtonEventHandler(mouseDown);
                this.control.MouseUp -= new MouseButtonEventHandler(mouseUp);
                this.control.MouseMove -= new MouseEventHandler(mouseMove);
                this.control.KeyDown -= new KeyEventHandler(keyDown);
                this.control.KeyUp -= new KeyEventHandler(keyUp);

                GC.SuppressFinalize(this);
            }
        }

        private void mouseDown(object sender, MouseButtonEventArgs args)
        {

        }

        private void mouseUp(object sender, MouseButtonEventArgs args)
        {

        }

        private void mouseMove(object sender, MouseEventArgs args)
        {

        }

        private void keyDown(object sender, KeyEventArgs args)
        {

        }

        private void keyUp(object sender, KeyEventArgs args)
        {

        }
    }
}
