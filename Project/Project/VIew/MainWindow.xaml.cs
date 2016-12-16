using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project.View
{
    public partial class MainWindow : Window
    {
        private Point _start;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _start = Mouse.GetPosition((UIElement)sender);
            DrawingAppViewModel.mouseDown(canvas, _start);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point currPoint = Mouse.GetPosition((UIElement)sender);

            DrawingAppViewModel.mouseMove(canvas, _start, currPoint);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DrawingAppViewModel.mouseUp();
        }
        private void Thickness_TextChanged(object sender, TextChangedEventArgs e)
        {
            DrawingAppViewModel.thicknessChanged(TextBox_Thickness);
        }

        private void SelectionChanged_Shape(object sender, SelectionChangedEventArgs e)
        {
            DrawingAppViewModel.shapeChanged(ComboBox_Shape, canvas);
        }

        private void SelectionChanged_Fill(object sender, SelectionChangedEventArgs e)
        {
            DrawingAppViewModel.fillChanged(ComboBox_FillColor);
        }

        private void SelectionChanged_Stroke(object sender, SelectionChangedEventArgs e)
        {
            DrawingAppViewModel.strokeChanged(ComboBox_StrokeColor);
        }

        private void MenuItem_Click_Reset(object sender, RoutedEventArgs e)
        {
            DrawingAppViewModel.Reset(canvas);
        }

        private void MenuItem_Click_Save_As(object sender, RoutedEventArgs e)
        {
            DrawingAppViewModel.Save_As(canvas);
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            DrawingAppViewModel.Save(canvas);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                DrawingAppViewModel.Reset(canvas);
            else if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                DrawingAppViewModel.Save_As(canvas);
            else if (e.Key == Key.O && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                DrawingAppViewModel.Open(canvas);
            else if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                DrawingAppViewModel.Save(canvas);
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            DrawingAppViewModel.Exit();
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (Key.Escape == e.Key)
                DrawingAppViewModel.Exit();
        }

        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            DrawingAppViewModel.Open(canvas);
        }

        private void CheckBox_Eraser_Checked(object sender, RoutedEventArgs e)
        {
            DrawingAppViewModel.Eraser_Checked();
        }

        private void CheckBox_Eraser_Unchecked(object sender, RoutedEventArgs e)
        {
            DrawingAppViewModel.Eraser_UnChecked();
        }
    }
}