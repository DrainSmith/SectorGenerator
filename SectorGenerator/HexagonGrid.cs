using System;
using System.Windows;
using System.Windows.Controls;

namespace SectorGenerator
{
    public class HexagonGrid : Grid
    {
        /**
         * If the length of 1 side of the hexagon = S, then:
         * Width = 2 x S
         * Height = S x SQRT(3)
         * Column C starts at C x (0.75 x Width)
         * Row R starts at R x Height
         * A row's uneven columns have an vertical offset of 0.5 x Height 
         **/
        #region HexagonSideLength

        /// <summary>
        /// HexagonSideLength Dependency Property
        /// </summary>
        public static readonly DependencyProperty HexagonSideLengthProperty =
            DependencyProperty.Register("HexagonSideLength", typeof(double), typeof(HexagonGrid),
                new FrameworkPropertyMetadata((double)0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the HexagonSideLength property. This dependency property 
        /// represents the length of 1 side of the hexagon.
        /// </summary>
        public double HexagonSideLength
        {
            get { return (double)GetValue(HexagonSideLengthProperty); }
            set { SetValue(HexagonSideLengthProperty, value); }
        }

        #endregion

        #region Rows

        /// <summary>
        /// Rows Dependency Property
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(HexagonGrid),
                new FrameworkPropertyMetadata((int)1,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the Rows property.
        /// </summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        #endregion

        #region Columns

        /// <summary>
        /// Columns Dependency Property
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(HexagonGrid),
                new FrameworkPropertyMetadata((int)1,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the Columns property.
        /// </summary>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        #endregion

        protected override Size MeasureOverride(Size constraint)
        {
            double side = HexagonSideLength;
            double width = 2 * side;
            double height = side * Math.Sqrt(3.0);
            double colWidth = 0.78 * width;
            double rowHeight = height;

            Size availableChildSize = new Size(width, height);

            foreach (FrameworkElement child in this.InternalChildren)
            {
                child.Measure(availableChildSize);
            }

            double totalHeight = Rows * rowHeight;
            if (Columns > 1)
                totalHeight += (0.5 * rowHeight);

            double totalWidth = Columns * (colWidth);

            Size totalSize = new Size(totalWidth, totalHeight);

            return totalSize; 
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            double side = HexagonSideLength;
            double width = 2 * side;
            double height = side * Math.Sqrt(3.0);
            double colWidth = 0.75 * width;
            double rowHeight = height;

            Size childSize = new Size(width, height);

            foreach (FrameworkElement child in this.InternalChildren)
            {
                int row = GetRow(child);
                int col = GetColumn(child);

                double left = col * colWidth - 1;
                double top = row * rowHeight - 1;
                bool isUnevenCol = (col % 2 != 0);
                if (isUnevenCol)
                    top += (0.5 * rowHeight);

                child.Arrange(new Rect(new Point(left, top), childSize));
            }

            return arrangeSize;
        }
    }
}
