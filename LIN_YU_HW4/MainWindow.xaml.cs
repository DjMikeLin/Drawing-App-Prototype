using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace LIN_YU_HW4
{
    public partial class MainWindow : Window
    {
        private int shapeIndex;
        private SolidColorBrush fillColor;
        private SolidColorBrush strokeColor;
        private List<SolidColorBrush> colors;
        private int fillIndex;
        private int strokeIndex;
        private int strokeThickness = 1;
        private int fillColorDefault;
        private int strokeColorDefault;
        StreamGeometry geometry;
        private Path myPath;
        private Point? _start = null;
        public MainWindow()
        {
            InitializeComponent();
            addShapes();
            addColors();
            textBox.Text = strokeThickness.ToString();
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e){Environment.Exit(1);}

        private void Window_KeyUp_ESC(object sender, KeyEventArgs e)
        {
            if (Key.Escape == e.Key)
                MenuItem_Click_Exit(sender, e);
        }

        private void MenuItem_Click_Reset(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            ComboBox_Shape.SelectedIndex = 0;
            ComboBox_FillColor.SelectedIndex = fillColorDefault;
            ComboBox_StrokeColor.SelectedIndex = strokeColorDefault;
            textBox.Text = 1.ToString();
            strokeThickness = 1;
        }
        private void Window_KeyDown_CTRL_X(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.X && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                MenuItem_Click_Reset(sender, e);
        }
        private void MenuItem_Click_Save_As(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Untitled"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG (.png)|*.png"; // Filter files by extension
            dlg.InitialDirectory = @"c:\Pictures\";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
            }
        }
        private void Window_KeyDown_CTRL_S(object sender, KeyEventArgs e)
        {

        }
        private void addShapes()
        {
            ComboBox_Shape.Items.Add(new Rectangle() { Height = 12, Width = 17.5, Stroke = Brushes.Black });
            ComboBox_Shape.Items.Add(new Ellipse() { Height = 12, Width = 17.5, Stroke = Brushes.Black });

            PointCollection temp = new PointCollection();
            temp.Add(new Point { X = 0, Y = 7 });
            temp.Add(new Point { X = 25, Y = 7 });
            ComboBox_Shape.Items.Add(new Polyline() { Height = 12, Width = 17.5, Stroke = Brushes.Black, Points = temp });
            
            ComboBox_Shape.Items.Add(new Ellipse() { Height = 12, Width = 17.5, Stroke = Brushes.Black, Fill = Brushes.Black, ToolTip = "Dot\n\n Draw a dot at the location of the clicker."});
            ComboBox_Shape.Items.Add(new Polyline() { Height = 12, Width = 17.5, Stroke = Brushes.Black, Points = temp });
            ComboBox_Shape.Items.Add(new Polyline() { Height = 12, Width = 17.5, Stroke = Brushes.Black, Points = temp });
            ComboBox_Shape.Items.Add(new Polyline() { Height = 12, Width = 17.5, Stroke = Brushes.Black, Points = temp });
            ComboBox_Shape.SelectedIndex = 0;
            shapeIndex = 0;
        }

        private void addColors()
        {
            colors = typeof(Brushes).GetProperties().Select(p => p.GetValue(null, null) as SolidColorBrush).ToList();

            int count = 0;
            foreach(SolidColorBrush color in colors)
            {
                ComboBox_FillColor.Items.Add(new Rectangle() { Height = 12, Width = 17.5, Fill = color });
                ComboBox_StrokeColor.Items.Add(new Rectangle() { Height = 12, Width = 17.5, Fill = color });

                if (color.Color == Colors.Red)
                {
                    fillIndex = count;
                    fillColor = colors[fillIndex];
                    ComboBox_FillColor.SelectedIndex = count;
                    fillColorDefault = count;
                }

                if (color.Color == Colors.Black)
                {
                    strokeIndex = count;
                    strokeColor = colors[strokeIndex];
                    ComboBox_StrokeColor.SelectedIndex = count;
                    strokeColorDefault = count;
                }

                count++;
            }
        }

        private void ComboBox_Shape_Selected(object sender, RoutedEventArgs e){ shapeIndex = ComboBox_Shape.SelectedIndex; }

        private void ComboBox_FillColor_Selected(object sender, RoutedEventArgs e){ fillIndex = ComboBox_FillColor.SelectedIndex; fillColor = colors[fillIndex]; }

        private void ComboBox_StrokeColor_Selected(object sender, RoutedEventArgs e){ strokeIndex = ComboBox_StrokeColor.SelectedIndex; strokeColor = colors[strokeIndex]; }

        private void setPath()
        {
            myPath.Fill = fillColor;
            myPath.Stroke = strokeColor;
            myPath.StrokeThickness = strokeThickness;
            myPath.HorizontalAlignment = HorizontalAlignment.Stretch;
            myPath.VerticalAlignment = VerticalAlignment.Stretch;
            myPath.Stretch = Stretch.None;
            myPath.SetValue(Grid.ColumnProperty, 1);
            canvas.Children.Add(myPath);
        }
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            myPath = new Path();
            setPath();

            if (_start == null) _start = Mouse.GetPosition((UIElement)sender);

            switch (shapeIndex)
            {
                case 0:
                    myPath.Data = new RectangleGeometry();
                    break;
                case 1:
                    myPath.Data = new EllipseGeometry();
                    break;
                case 2:
                    myPath.Data = new LineGeometry { StartPoint = _start.Value };
                    break;
                case 3:
                    drawDot(_start.Value);
                    break;
                case 4:
                    myPath.Data = new EllipseGeometry();
                    break;
                case 5:
                    myPath.Data = new StreamGeometry();
                    break;
                case 6:
                    myPath.Data = new StreamGeometry();
                    break;
                case 7:
                    myPath.Data = new StreamGeometry();
                    break;
            }
        }
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //regex where any string of chars besides numbers
            Regex pattern = new Regex(@"^([^0-9]*)$", RegexOptions.Compiled);


            Match result = pattern.Match(textBox.Text);
            if (textBox.Text.ToString() == string.Empty)
                return;
            else if (result.Success)
            {
                MessageBox.Show("Invalid character entered. Integer numbers only. Stroke Thickness will be reseted to a default of 1.");
                strokeThickness = 1;
                textBox.Text = strokeThickness.ToString();
                textBox.SelectAll();
            }
            else
            {
                int x;
                if (int.TryParse(textBox.Text, out x))
                    strokeThickness = int.Parse(textBox.Text);
            }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_start == null) return;
            var p = Mouse.GetPosition((UIElement)sender);
            
            switch (shapeIndex)
            {
                case 0:
                    rectangleResize(p);
                    break;
                case 1:
                    ellipseResize(p);
                    break;
                case 2:
                    lineResize(p);
                    break;
                case 4:
                    drawLine(p);
                    break;
                case 5:
                    rightTriangleResize(p);
                    break;
                case 6:
                    triangleResize(p);
                    break;
            }
        }
        private void rectangleResize(Point point)
        {
            try
            {
                var x = myPath.Data as RectangleGeometry;
                x.Rect = new Rect(_start.Value, point);
                myPath.Data = x;
            }
            catch (InvalidOperationException) { return; }
        }
        private void ellipseResize(Point point)
        {
            try
            {
                myPath.Data = new EllipseGeometry(new Rect(_start.Value, point));
            }
            catch (InvalidOperationException) { return; }
        }
        private void lineResize(Point point)
        {
            try
            {
                var z = myPath.Data as LineGeometry;
                z.EndPoint = point;
            }
            catch (InvalidOperationException) { return; }
        }
        private void drawDot(Point point)
        {
            try
            {
                myPath.Data = new EllipseGeometry(_start.Value, 1, 1);
            }
            catch (InvalidOperationException) { return; }
        }
        private void drawLine(Point point)
        {
            try
            {
                myPath = new Path();
                setPath();
                myPath.Data = new EllipseGeometry(point, 1, 1);
            }
            catch (InvalidOperationException) { return; }
        }
        private void rightTriangleResize(Point point)
        {
            try
            {
                var x = myPath.Data as StreamGeometry;
                using (StreamGeometryContext ctx = x.Open())
                {
                    ctx.BeginFigure(_start.Value, true, true);
                    ctx.LineTo(point, true, false);
                    ctx.LineTo(new Point(_start.Value.X, point.Y), true, false);
                    ctx.Close();
                }
                myPath.Data = x;
            }
            catch (InvalidOperationException) { return; }
        }
        private void triangleResize(Point point)
        {
            try
            {
                var x = myPath.Data as StreamGeometry;
                using (StreamGeometryContext ctx = x.Open())
                {
                    ctx.BeginFigure(_start.Value, true, true);
                    ctx.LineTo(point, true, false);

                    var temp = 0;
                    int.TryParse(point.X.ToString(), out temp);
                    var point_X = Math.Abs((temp - (2 * _start.Value.X)));
                    ctx.LineTo(new Point(point_X, point.Y), true, false);
                    ctx.Close();
                }
            }
            catch (InvalidOperationException) { return; }
        }
        private void diamondResize(Point point)
        {
            try
            {

            }
            catch (InvalidOperationException) { return; }
        }
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_start == null) return;
            _start = null;
            //myPath. StrokeDashArray = new DoubleCollection() { 4, 4 };
            //myPath.Cursor = 
            //myPath.Data.Bounds.se
        }
    }
}