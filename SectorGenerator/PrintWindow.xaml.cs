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
using System.Windows.Shapes;

namespace SectorGenerator
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {
        private FixedDocumentSequence _document;
        public PrintWindow(FixedDocumentSequence document)
        {
            InitializeComponent();
            _document = document;
            PreviewD.Document = _document;
            // Hides the search bar at the bottom of the window because it is useless for us
            ContentControl cc = PreviewD.Template.FindName("PART_FindToolBarHost", PreviewD) as ContentControl;
            cc.Visibility = Visibility.Collapsed;
            PreviewD.ApplyTemplate();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
