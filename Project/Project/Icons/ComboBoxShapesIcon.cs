using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System;

namespace Project.Icons
{
    public class ComboBoxShapesIcon
    {
        private static double _height = 12;
        private static double _width = 17.5;
        private static Brush _stroke = Brushes.Black;

        public Rectangle getRectangle { get { return this.rectangle; } }

        public Ellipse getEllipse { get { return this.ellipse; } }

        public Polyline getLine { get { return this.line; } }

        public Ellipse getDot { get { return this.dot; } }

        public ImageBrush getPencil { get { return this.pencil; } }

        public ImageBrush getRTriangle { get { return this.rTriangle; } }

        public ImageBrush getETriangle { get { return this.eTriangle; } }

        public ImageBrush getDiamond { get { return this.diamond; } }

        public ImageBrush getPolygon { get { return this.polygon; } }

        public ImageBrush getArc { get { return this.arc; } }

        public ImageBrush getRoundedRectangle { get { return this.roundedRectangle; } } 

        public Rectangle rectangle = new Rectangle()
        {
            Height = _height,
            Width = _width,
            Stroke = _stroke,
            ToolTip = "Rectangle"
        };

        public Ellipse ellipse = new Ellipse()
        {
            Height = _height,
            Width = _width,
            Stroke = _stroke,
            ToolTip = "Ellipse"
        };

        public Polyline line = new Polyline()
        {
            Points = new PointCollection()
            {
                new Point { X = 0, Y = 7 },
                new Point { X = 25, Y = 7 }
            },

            Height = _height,
            Width = _width,
            Stroke = _stroke,
            ToolTip = "Line"
        };

        public Ellipse dot = new Ellipse()
        {
            Height = _height,
            Width = _height,
            Stroke = _stroke,
            Fill = _stroke,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            ToolTip = "Dot"
        };

        public ImageBrush pencil = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri(@"Images/Pencil.png", UriKind.Relative))
        };

        public ImageBrush rTriangle = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri(@"Images/rightTriangle.png", UriKind.Relative))
        };

        public ImageBrush eTriangle = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri(@"Images/equalateralTriangle.png", UriKind.Relative))
        };

        public ImageBrush diamond = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri(@"Images/Diamond.png", UriKind.Relative))
        };

        public ImageBrush polygon = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri(@"Images/Polygon.png", UriKind.Relative))
        };

        public ImageBrush roundedRectangle = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri(@"Images/RoundedRectangle.png", UriKind.Relative))
        };

        public ImageBrush arc = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri(@"Images/Arc.png", UriKind.Relative))
        };
    }
}