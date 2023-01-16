using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace RoomEnvironmentMonitor_.Behaviors
{
    class MainViewBehaviors : Behavior<Window>
    {
        protected override void OnAttached()
        {
            if (this.AssociatedObject is Window window)
            {
                window.MouseDown += Window_MouseDown;
                window.MouseUp += Window_MouseUp;
                window.MouseMove += Window_MouseMove;
            }
        }

        #region DragAndDropLogic

        private Point WindowStartPosition;
        private bool isWindowMoving = false;

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!isWindowMoving & e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Window window = (Window)sender;
                isWindowMoving = true;
                WindowStartPosition = e.GetPosition(window);
            }
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isWindowMoving)
            {
                Window window = (Window)sender;
                Point CursorLocation = e.GetPosition(window);
                Vector move = CursorLocation - WindowStartPosition;
                window.Top += move.Y;
                window.Left += move.X;
            }
        }

        private void Window_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (isWindowMoving & e.LeftButton == System.Windows.Input.MouseButtonState.Released)
                isWindowMoving = false;
        }
        #endregion
    }
}
