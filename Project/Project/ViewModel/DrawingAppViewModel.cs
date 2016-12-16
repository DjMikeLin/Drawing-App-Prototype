using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System;
using System.Windows.Media.Imaging;
using System.IO;
using Project.Icons;
using Project.View;
using System.Windows.Input;

//add rounded rec, arcs?
namespace Project.ViewModel
{
    class DrawingAppViewModel : INotifyPropertyChanged
    {
        private List<SolidColorBrush> colors;
        private System.Windows.Shapes.Path myPath;
        private List<Shape> _shapesComboBox = new List<Shape>();
        private List<Shape> _fillColorComboBox = new List<Shape>();
        private List<Shape> _strokeColorComboBox = new List<Shape>();
        private int _selectedIndexShapes = 0;
        private int _selectedIndexFillColor = 0;
        private int _indexWhite;
        private int _lastIndexShapes;
        private int _lastIndexFill;
        private int _lastIndexStrokeColor;
        private int _lastStrokeThickness;
        private int _selectedIndexStrokeColor = 0;
        private int _strokeThicknessNumber = 1;
        private bool _stop = false;
        private string currentPath;
        private bool started = false;
        private Point originalStartPoint;
        private Point lastEndPoint;
        private bool _isChecked;
        private PathFigure pathFigure;
        private PointCollection polygonPoints = new PointCollection();
        public DrawingAppViewModel()
        {
            Eraser_UnChecked();
            addShapes();
            addColors();
            strokeThicknessNumber = 1;
            OnPropertyChanged();
        }
        private void addShapes()
        {
            ComboBoxShapesIcon icons = new ComboBoxShapesIcon();

            _shapesComboBox.Add(icons.getRectangle);
            _shapesComboBox.Add(icons.getEllipse);
            _shapesComboBox.Add(icons.getLine);
            _shapesComboBox.Add(icons.getDot);

            _shapesComboBox.Add(new Rectangle() { Fill = icons.getPencil, Height = 12, Width = 17.5, Stretch = Stretch.Uniform, ToolTip = "Pencil\n\n Draw a free-form line with the corresponding stroke thickness." });
            _shapesComboBox.Add(new Rectangle() { Fill = icons.getRTriangle, Height = 12, Width = 17.5, Stretch = Stretch.Uniform, ToolTip = "Right Triangle" });
            _shapesComboBox.Add(new Rectangle() { Fill = icons.getETriangle, Height = 12, Width = 17.5, Stretch = Stretch.Uniform, ToolTip = "Equilateral Triangle" });
            _shapesComboBox.Add(new Rectangle() { Fill = icons.getDiamond, Height = 12, Width = 17.5, Stretch = Stretch.Uniform, ToolTip = "Diamond" });
            _shapesComboBox.Add(new Rectangle() { Fill = icons.getPolygon, Height = 12, Width = 17.5, Stretch = Stretch.Uniform, ToolTip = "Polygon \n\n Change shape to close and fill the polygon" });
            _shapesComboBox.Add(new Rectangle() { Fill = icons.getRoundedRectangle, Height = 12, Width = 17.5, Stretch = Stretch.Uniform, ToolTip = "Rounded Rectangle" });
            _shapesComboBox.Add(new Rectangle() { Fill = icons.getArc, Height = 12, Width = 17.5, Stretch = Stretch.Uniform, ToolTip = "Arc" });
        }

        public IEnumerable<Shape> shapesComboBox
        {
            get
            {
                foreach (var opt in _shapesComboBox)
                {
                    yield return opt;
                }
            }
        }

        public int selectedIndexShapes
        {
            get { return _selectedIndexShapes; }
            set
            {
                _selectedIndexShapes = value;
                OnPropertyChanged(_selectedIndexShapes.ToString());
            }
        }

        private void addColors()
        {
            colors = typeof(Brushes).GetProperties().Select(p => p.GetValue(null, null) as SolidColorBrush).ToList();

            int count = 0;
            foreach (SolidColorBrush color in colors)
            {
                _fillColorComboBox.Add(new Rectangle() { Height = 12, Width = 17.5, Fill = color });
                _strokeColorComboBox.Add(new Rectangle() { Height = 12, Width = 17.5, Fill = color });

                if (color.Color == Colors.Red)
                    _selectedIndexFillColor = count;

                if (color.Color == Colors.Black)
                    _selectedIndexStrokeColor = count;

                if (color.Color == Colors.White)
                    _indexWhite = count;
                count++;
            }
        }

        public IEnumerable<Shape> fillColorComboBox
        {
            get
            {
                foreach (var opt in _fillColorComboBox)
                {
                    yield return opt;
                }
            }
        }

        public int selectedIndexFillColor
        {
            get { return _selectedIndexFillColor; }
            set
            {
                _selectedIndexFillColor = value;
                OnPropertyChanged(_selectedIndexFillColor.ToString());
            }
        }

        public IEnumerable<Shape> strokeColorComboBox
        {
            get
            {
                foreach (var opt in _strokeColorComboBox)
                {
                    yield return opt;
                }
            }
        }

        public int selectedIndexStrokeColor
        {
            get
            {
                return _selectedIndexStrokeColor;
            }
            set
            {
                _selectedIndexStrokeColor = value;
                OnPropertyChanged(_selectedIndexStrokeColor.ToString());
            }
        }

        public void shapeChanged(ComboBox shapeComboBox, Canvas canvas)
        {
            selectedIndexShapes = shapeComboBox.SelectedIndex;
            closePolygon(canvas);
        }
        
        public void fillChanged(ComboBox fillColorComboBox)
        {
            selectedIndexFillColor = fillColorComboBox.SelectedIndex;
        }

        public void strokeChanged(ComboBox strokeColorComboBox)
        {
            selectedIndexStrokeColor = strokeColorComboBox.SelectedIndex;
        }
        public int strokeThicknessNumber
        {
            get
            {
                return _strokeThicknessNumber;
            }
            set
            {
                _strokeThicknessNumber = value;
            }
        }
        public void thicknessChanged(TextBox textBox)
        {
            //regex where any string of chars besides numbers
            Regex pattern = new Regex(@"^[0-9]*$", RegexOptions.Compiled);

            Match result = pattern.Match(textBox.Text);
            if (textBox.Text.ToString() == string.Empty)
                return;
            else if (!result.Success)
            {
                MessageBox.Show("Invalid character entered. Integer numbers only. Stroke Thickness will be reseted to a default of 1.");
                strokeThicknessNumber = 1;
                OnPropertyChanged();
                textBox.SelectAll();
            }
            else
            {
                int x;
                if (int.TryParse(textBox.Text, out x))
                    strokeThicknessNumber = int.Parse(textBox.Text);
            }
        }

        public void mouseDown(Canvas canvas, Point start)
        {
            myPath = new System.Windows.Shapes.Path();
            setPath(canvas);

            switch (_selectedIndexShapes)
            {
                case 0:
                    myPath.Data = new RectangleGeometry();
                    break;
                case 1:
                    myPath.Data = new EllipseGeometry();
                    break;
                case 2:
                    myPath.Data = new LineGeometry() { StartPoint = start };
                    break;
                case 3:
                    drawDot(start);
                    break;
                case 4:
                    lastEndPoint = start;//glitches if used first
                    myPath.Data = new LineGeometry();
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
                case 8:
                    if (started == false)
                    {
                        myPath.Data = new LineGeometry() { StartPoint = start };
                        originalStartPoint = start;
                        started = true;
                    }
                    else if (started == true)
                    {
                        myPath.Data = new LineGeometry() { StartPoint = lastEndPoint };
                        polygonPoints.Add(lastEndPoint);
                    }
                    break;
                case 9:
                    myPath.Data = new RectangleGeometry();
                    break;
                case 10:
                    myPath.Data = new PathGeometry();
                    pathFigure = new PathFigure() { StartPoint = start };
                    break;
            }
        }

        public void mouseMove(Canvas canvas, Point start, Point currPoint)
        {
            cursorCords = currPoint.X + "," + currPoint.Y;
            OnPropertyChanged();
            if (_stop == true)
                return;

            switch (_selectedIndexShapes)
            {
                case 0:
                    rectangleResize(start, currPoint);
                    break;
                case 1:
                    ellipseResize(start, currPoint);
                    break;
                case 2:
                    lineResize(start, currPoint);
                    break;
                case 3:
                    break;
                case 4:
                    drawLine(start, currPoint, canvas);
                    break;
                case 5:
                    rightTriangleResize(start, currPoint);
                    break;
                case 6:
                    triangleResize(start, currPoint);
                    break;
                case 7:
                    diamondResize(start, currPoint);
                    break;
                case 8:
                    drawPolygon(currPoint, canvas);
                    break;
                case 9:
                    roundedRectangleResize(start, currPoint);
                    break;
                case 10:
                    arcResize(start, currPoint);
                    break;
            }
        }

        public void mouseUp()
        {
            _stop = true;
        }
        private void setPath(Canvas canvas)
        {
            _stop = false;
            myPath.Fill = _fillColorComboBox.ElementAt(_selectedIndexFillColor).Fill;
            myPath.Stroke = _strokeColorComboBox.ElementAt(_selectedIndexStrokeColor).Fill;
            myPath.StrokeThickness = strokeThicknessNumber;
            myPath.HorizontalAlignment = HorizontalAlignment.Stretch;
            myPath.VerticalAlignment = VerticalAlignment.Stretch;
            myPath.Stretch = Stretch.None;
            myPath.SetValue(Grid.ColumnProperty, 1);
            canvas.Children.Add(myPath);
        }

        public void Eraser_Checked()
        {
            _lastIndexFill = selectedIndexFillColor;
            _lastIndexShapes = selectedIndexShapes;
            _lastIndexStrokeColor = selectedIndexStrokeColor;
            _lastStrokeThickness = strokeThicknessNumber;

            comboBoxEnabled = false;
            canvasCursor = Cursors.SizeAll;

            selectedIndexShapes = 4;
            selectedIndexStrokeColor = _indexWhite;
            OnPropertyChanged();
            isChecked = true;
        }

        public void Eraser_UnChecked()
        {
            comboBoxEnabled = true;
            canvasCursor = Cursors.Arrow;

            selectedIndexShapes = _lastIndexShapes;
            selectedIndexFillColor = _lastIndexFill;
            selectedIndexStrokeColor = _lastIndexStrokeColor;
            strokeThicknessNumber = _lastStrokeThickness;
            OnPropertyChanged();
            isChecked = false;
        }

        public bool isChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(_isChecked.ToString());
            }
        }

        private void rectangleResize(Point start, Point currPoint)
        {
            try
            {
                var x = myPath.Data as RectangleGeometry;
                x.Rect = new Rect(start, currPoint);
                myPath.Data = x;
            }
            catch (NullReferenceException) { return; }
        }
        private void ellipseResize(Point start, Point currPoint)
        {
            try
            {
                var x = myPath.Data as EllipseGeometry;
                x = new EllipseGeometry(new Rect(start, currPoint));
                myPath.Data = x;
            }
            catch (NullReferenceException) { return; }
        }
        private void lineResize(Point start, Point currPoint)
        {
            try
            {
                var z = myPath.Data as LineGeometry;
                z.EndPoint = currPoint;
            }
            catch (NullReferenceException) { return; }
        }
        private void drawDot(Point point)
        {
            try
            {
                myPath.Data = new EllipseGeometry(point, 1, 1);
            }
            catch (InvalidOperationException) { return; }
        }
        private void drawLine(Point start, Point end, Canvas canvas)
        {
            myPath = new System.Windows.Shapes.Path();
            myPath.Data = new LineGeometry(lastEndPoint, end);
            setPath(canvas);
            lastEndPoint = end;
        }
        private void rightTriangleResize(Point start, Point currPoint)
        {
            try
            {
                var x = myPath.Data as StreamGeometry;
                using (StreamGeometryContext ctx = x.Open())
                {
                    ctx.BeginFigure(start, true, true);
                    ctx.LineTo(currPoint, true, false);
                    ctx.LineTo(new Point(start.X, currPoint.Y), true, false);
                    ctx.Close();
                }
                myPath.Data = x;
            }
            catch (NullReferenceException) { return; }
        }
        private void triangleResize(Point start, Point currPoint)
        {
            try
            {
                var x = myPath.Data as StreamGeometry;
                using (StreamGeometryContext ctx = x.Open())
                {
                    ctx.BeginFigure(start, true, true);
                    ctx.LineTo(currPoint, true, false);

                    var temp = 0;
                    int.TryParse(currPoint.X.ToString(), out temp);
                    var point_X = Math.Abs((temp - (2 * start.X)));
                    ctx.LineTo(new Point(point_X, currPoint.Y), true, false);
                    ctx.Close();
                }
            }
            catch (NullReferenceException) { return; }
        }
        private void diamondResize(Point start, Point currPoint)
        {
            try
            {
                var x = myPath.Data as StreamGeometry;

                int startX, startY, currX, currY;
                startX = int.Parse(start.X.ToString());
                startY = int.Parse(start.Y.ToString());
                currX = int.Parse(currPoint.X.ToString());
                currY = int.Parse(currPoint.Y.ToString());

                Point midpoint = new Point((startX + currX) / 2, (startY + currY) / 2);
                Point right = new Point(start.X, 2 * midpoint.Y);
                Point bot = new Point(midpoint.X, currY + midpoint.Y);
                Point top = new Point(midpoint.X, currY - midpoint.Y);

                PointCollection points = new PointCollection
                {
                    top,
                    currPoint,
                    bot
                };

                using (StreamGeometryContext ctx = x.Open())
                {
                    ctx.BeginFigure(start, true, true);
                    ctx.PolyLineTo(points, true, false);
                }
            }
            catch (NullReferenceException) { return; }
        }

        private void drawPolygon(Point currPoint, Canvas canvas)
        {
            try
            {
                var z = myPath.Data as LineGeometry;
                z.EndPoint = currPoint;
                lastEndPoint = z.EndPoint;
            }
            catch (NullReferenceException) { return; }
        }

        private void buildPolygon(Canvas canvas)
        {
            PointCollection temp = new PointCollection();

            var x = myPath.Data as StreamGeometry;
            using (StreamGeometryContext ctx = x.Open())
            {
                ctx.BeginFigure(originalStartPoint, true, true);
                ctx.PolyLineTo(polygonPoints, true, false);
            }
            setPath(canvas);
        }

        private void closePolygon(Canvas canvas)
        {
            if (started == true)
            {
                myPath = new System.Windows.Shapes.Path();
                setPath(canvas);
                myPath.Data = new LineGeometry(lastEndPoint, originalStartPoint);
                polygonPoints.Add(lastEndPoint);

                myPath = new System.Windows.Shapes.Path();
                myPath.Data = new StreamGeometry();
                buildPolygon(canvas);

                lastEndPoint = new Point(0, 0);
                originalStartPoint = new Point(0, 0);
                polygonPoints = new PointCollection();
                started = false;
                _stop = true;
            }
            else
                return;
        }

        private void roundedRectangleResize(Point start, Point currPoint)
        {
            try
            {
                var x = myPath.Data as RectangleGeometry;
                x.RadiusX = 5;
                x.RadiusY = 5;
                x.Rect = new Rect(start, currPoint);
                myPath.Data = x;
            }
            catch (NullReferenceException) { return; }
        }

        private void arcResize(Point start, Point currPoint)
        {
            try
            {
                var x = myPath.Data as PathGeometry;

                PathSegmentCollection segCollection = new PathSegmentCollection();
                segCollection.Add(new ArcSegment()
                {
                    Size = new Size(50, 50),
                    RotationAngle = 30,
                    IsLargeArc = false,
                    SweepDirection = SweepDirection.Clockwise,
                    Point = currPoint
                });
                pathFigure.Segments = segCollection;

                PathFigureCollection figureCollection = new PathFigureCollection();
                figureCollection.Add(pathFigure);
                x.Figures = figureCollection;
                myPath.Data = x;
            }
            catch(NullReferenceException) { return; }
        }

        public string cursorCords { get; set; }

        public bool comboBoxEnabled { get; set; }

        public Cursor canvasCursor { get; set; }
        public void Reset(Canvas canvas)
        {
            if (_isChecked == true)
            {
                MessageBoxResult mssg = MessageBox.Show("Please Un Check Eraser before using reset.");
                return;
            }
            canvas.Background = Brushes.White;
            canvas.Children.Clear();
            lastEndPoint = new Point(0,0);
            started = false;
        }

        public void Exit() { Environment.Exit(1); }

        public void Save_As(Canvas canvas)
        {
            Microsoft.Win32.SaveFileDialog dlg = saveDialogWindow();
            dlg.ShowDialog();

            if (dlg.FileName != "")
            {
                currentPath = System.IO.Path.Combine("c:\\Documents\\", dlg.FileName);
                saveBitmap(canvas);
            }
        }

        public void Save(Canvas canvas)
        {
            if (currentPath == null)
                Save_As(canvas);

            Microsoft.Win32.SaveFileDialog dlg = saveDialogWindow();
            saveBitmap(canvas);
        }

        private Microsoft.Win32.SaveFileDialog saveDialogWindow()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.RestoreDirectory = true;
            dlg.FileName = "Untitled"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG (.png)|*.png"; // Filter files by extension
            dlg.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";

            return dlg;
        }

        private void saveBitmap (Canvas canvas)
        {
            Transform transform = canvas.LayoutTransform;
            canvas.LayoutTransform = null;

            Size size = new Size(canvas.ActualWidth, canvas.ActualHeight);

            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            // Create a render bitmap and render canvas to it
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(canvas);

            try
            {
                using (FileStream outStream = new FileStream(currentPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    // Use png encoder for our data
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    // push the rendered bitmap to it
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    // save the data to the stream
                    encoder.Save(outStream);
                }
                // Restore previously saved layout
                canvas.LayoutTransform = transform;
            }
            catch (DirectoryNotFoundException) { return; }
        }
        
        public void Open(Canvas canvas)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.FileName = ""; // No default file
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG (.png)|*.png"; // Filter files by extension
            dlg.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
            dlg.ShowDialog();
            Reset(canvas);

            if (dlg.FileName != "")
            {
                currentPath = System.IO.Path.Combine("c:\\Documents\\", dlg.FileName);
 
                ImageBrush brush = new ImageBrush();
                BitmapImage imgTemp = new BitmapImage();
                imgTemp.BeginInit();
                imgTemp.CacheOption = BitmapCacheOption.OnLoad;
                imgTemp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                imgTemp.UriSource = new Uri(currentPath, UriKind.Relative);
                imgTemp.EndInit();
                brush.ImageSource = imgTemp;
                canvas.Background = brush;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}