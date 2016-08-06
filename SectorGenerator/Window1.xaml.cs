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
using System.Xml.Serialization;
using System.IO;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;

namespace SectorGenerator
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    public partial class Window1 : Window
    {
        WorldFrequency _lastUsed;
        List<PlanetProfile> _profiles = new List<PlanetProfile>();
        HexButton _currentButton;

        public Window1()
        {
            InitializeComponent();
            CreateGrid(WorldFrequency.Normal);
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Top = 0;
            this.Left = 0;
        }

        public void CreateGrid(WorldFrequency freq)
        {
            HexGrid.Children.Clear();
            _profiles.Clear();
            _lastUsed = freq;
            int columns = 10;
            int rows = 8;
            Random r = new Random();
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    PlanetProfile p = new PlanetProfile(r, freq);
                    p.Column = x;
                    p.Row = y;
                    AddPlanetToGrid(p, HexGrid);
                }
            }
        }

        private void AddPlanetToGrid(PlanetProfile p, HexagonGrid grid)
        {
            HexButton b = new HexButton(p);
            //b.clic += B_MouseDown;
            b.Click += B_Click;
            _profiles.Add(p);
            grid.Children.Add(b);
            Grid.SetRow(b, p.Column);
            Grid.SetColumn(b, p.Row);
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            HexButton source = (HexButton)sender;
            if (source.Planet.Name != string.Empty)
            {
                _currentButton = source;
                PlanetProfileGrid.Visibility = Visibility.Visible;
                PlanetNameLabel.Content = source.Planet.Name;
                UWPLabel.Content = source.Planet.Profile + "    " + source.Planet.TradeCodes;
                StarPortLabel.Content = (source.Planet.StarPort == 'X') ? "No starport" : source.Planet.StarPort.ToString();
                SizeLabel.Content = GetSizeDescription(source.Planet.Size);
                AtmosphereLabel.Content = GetAtmosphereDescription(source.Planet.Atmosphere);
                HydrographicsLabel.Content = GetHydrographicsDescription(source.Planet.Hydrographics);
                PopulationLabel.Content = (source.Planet.Population == 0) ? "None" : string.Format("{0:n0}", Math.Pow(1 * 10, source.Planet.Population)) + "+";
                GovernmentTextBox.Text = GetGovernmentDescription(source.Planet.Government);
                LawLevelLabel.Text = GetLawLevelDescription(source.Planet.LawLevel);
                TechTextBox.Content = source.Planet.TechLevel.ToString();
                TemperatureTextBox.Text = GetTemperatureDescription(source.Planet.Temperature);
                NotesTextBox.Text = source.Planet.Notes;
                RedZonedCheckbox.IsChecked = source.Planet.isRed;
            }
            else
            {
                PlanetProfileGrid.Visibility = Visibility.Hidden;
            }
        }
      

        private string GetSizeDescription(int size)
        {
            switch (size)
            {
                case 0: return "< 1000 km, negligible G"; 
                case 1: return "1600 km, 0.05 G"; 
                case 2: return "3200 km, 0.15 G";
                case 3: return "4800 km, 0.25 G";
                case 4: return "6400 km, 0.35 G";
                case 5: return "8000 km, 0.45 G";
                case 6: return "9600 km, 0.7 G";
                case 7: return "11200 km, 0.9 G";
                case 8: return "12800 km, 1.0 G";
                case 9: return "14400 km, 1.25 G";
                case 10: return "16000 km, 1.4 G";
                default: return ""; 
            }
        }

        private string GetAtmosphereDescription(int atmo)
        {
            switch (atmo)
            {
                case 0: return "None, 0.00, Vacc Suit";
                case 1: return "Trace, 0.001-0.09, Vacc Suit";
                case 2: return "Very Thin, Tainted, 0.1-0.42, Respirator, Filter";
                case 3: return "Very Thin, 0.1-0.42, Respirator";
                case 4: return "Thin, Tainted, 0.43-0.7, Filter";
                case 5: return "Thin, 0.43-0.7";
                case 6: return "Standard, 0.71-1.49";
                case 7: return "Standard, Tainted, 0.71-1.49, Filter";
                case 8: return "Dense, 1.5-2.49";
                case 9: return "Dense, Tainted, 1.5-2.49, Filter";
                case 10: return "Exotic, Varies, Air Supply";
                case 11: return "Corrosive, Varies, Vacc Suit";
                case 12: return "Insidious, Varies, Vacc Suit";
                case 13: return "Very Dense, 2.5+";
                case 14: return "Low, <0.5";
                case 15: return "Unusual";
                default: return "";
            }
        }

        private string GetHydrographicsDescription(int hydro)
        {
            switch (hydro)
            {
                case 0: return "0%-5%, Desert World";
                case 1: return "6%-15%, Dry World";
                case 2: return "16%-25%, A few small seas";
                case 3: return "26%-35%, Small seas and oceans";
                case 4: return "36%-45%, Wet World";
                case 5: return "46%-55%, Large Oceans";
                case 6: return "56%-65%, Large Oceans";
                case 7: return "66%-75%, Earth-like";
                case 8: return "76%-85%, Water world";
                case 9: return "86%-95%, Few small islands";
                case 10: return "96%-100%, Almost entirely water";
                default: return "";
            }
            //switch (hydro)
            //{
            //    case 0: return "";
            //    case 1: return "";
            //    case 2: return "";
            //    case 3: return "";
            //    case 4: return "";
            //    case 5: return "";
            //    case 6: return "";
            //    case 7: return "";
            //    case 8: return "";
            //    case 9: return "";
            //    case 10: return "";
            //    case 11: return "";
            //    case 12: return "";
            //    case 13: return "";
            //    case 14: return "";
            //    case 15: return "";
            //    default: return "";
            //}
        }

        private string GetGovernmentDescription(int govt)
        {
            switch (govt)
            {
                case 0: return "None\r\nNo government structure. In many cases, family bonds predominate";
                case 1: return "Company/Corporation\r\nRuling functions are assumed by a company managerial elite, and most citizenry are company employees or dependants";
                case 2: return "Participating Democracy\r\nRuling functions are reached by the advice and consent of the citizenry directly";
                case 3: return "Self-Perpetuating Oligarchy\r\nRuling functions are performed by a restricted minority, with little or no input from the mass of citizenry";
                case 4: return "Representative Democracy\r\nRuling functions are performed by elected representatives";
                case 5: return "Feudal Technocracy\r\nRuling functions are performed by specific individuals for persons who agree to be ruled by them.Relationships are based on the performance of technical activities which are mutually beneficial";
                case 6: return "Captive Government\r\nRuling functions are performed by an imposed leadership answerable to an outside group";
                case 7: return "Balkanisation\r\nNo central authority exists; rival governments compete for control. Law level refers to the government nearest the starport";
                case 8: return "Civil Service Bureaucracy\r\nRuling functions are performed by government agencies employing individuals selected for their expertise";
                case 9: return "Impersonal Bureaucracy\r\nRuling functions are performed by agencies which have become insulated from the governed citizens";
                case 10: return "Charismatic Dictator\r\nRuling functions are performed by agencies directed by a single leader who enjoys the overwhelming confidence of the citizens";
                case 11: return "Non-Charismatic Leader\r\nA previous charismatic dictator has been replaced by a leader through normal channels";
                case 12: return "Charismatic Oligarchy\r\nRuling functions are performed by a select group of members of an organisation or class which enjoys the overwhelming confidence of the citizenry";
                case 13: return "Religious Dictatorship\r\nRuling functions are performed by a religious organisation without regard to the specific individual needs of the citizenry";
                case 14: return "Religious Autocracy\r\nGovernment by a single religious leader having absolute power over the citizenry";
                case 15: return "Totalitarian Oligarchy\r\nGovernment by an all-powerful minority which maintains absolute control through widespread coercion and oppresion";
                default: return "";
            }
        }

        private string GetLawLevelDescription(int law)
        {
            switch (law)
            {
                case 0: return "No restrictions – heavy armor and a handy weapon recommended";
                case 1: return "Poison gas, explosives, undetectable weapons, WMD; Battle Dress";
                case 2: return "Portable energy and laser weapons; Combat armor";
                case 3: return "Military weapons; Flak";
                case 4: return "Light assault weapons and submachine guns; Cloth";
                case 5: return "Personal concealable weapons; Mesh";
                case 6: return "All firearms except shotguns & stunners; carrying weapons discouraged";
                case 7: return "Shotguns";
                case 8: return "All bladed weapons, stunners; All visible armor";
                default: return "All weapons; All armor";
            }
        }

        private string GetTemperatureDescription(int temp)
        {
            switch (temp)
            {
                case 0: 
                case 1: 
                case 2: return "Frozen: < -51°, No liquid water, very dry atmosphere";
                case 3: 
                case 4: return "Cold: 51° - 0°, Icy, little liquid water, extensive ice caps, few clouds";
                case 5: 
                case 6: 
                case 7: 
                case 8:
                case 9: return "Temperate: 0° - 30°, Earth-like, liquid and vaporized water, moderate ice caps";
                case 10:
                case 11: return "Hot: 31° - 80°, Small or no ice caps, little liquid water. Most water in clouds";
                default: return "Boiling: > 80°, No ice caps, little liquid water";
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = System.IO.Path.GetRandomFileName();
            using (XpsDocument doc = new XpsDocument(fileName, FileAccess.ReadWrite))
            {
                var writer = XpsDocument.CreateXpsDocumentWriter(doc);
                //Grid g = new Grid();
                //g.Width = 816;
                //g.Height = 1056;
                //Viewbox v = new Viewbox();
                
                //Border b = new Border();
                //b.BorderBrush = new SolidColorBrush(Colors.Black);
                //b.BorderThickness = new Thickness(2);
                //b.Margin = new Thickness(25);
                //HexagonGrid hexgrid = new HexagonGrid();
                //foreach (var planet in _profiles)
                //{
                //    HexButton hb = new HexButton(planet);
                //    hexgrid.Children.Add(hb);
                //    Grid.SetRow(hb, planet.Column);
                //    Grid.SetColumn(hb, planet.Row);
                //}
                //v.Child = b;
                //b.Child = hexgrid;
                //g.Children.Add(v);
                //g.UpdateLayout();
                //g.Arrange(new Rect(new Point(0,0), new Size(816, 1056)));
                writer.Write(MainViewbox);
                FixedDocumentSequence fds = doc.GetFixedDocumentSequence();
                
                var p = new PrintWindow(fds);
                p.ShowDialog();
            }
            File.Delete(fileName);
        }


        private void NormalRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CreateGrid(WorldFrequency.Normal);
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            CreateGrid(_lastUsed);
        }

        private void RiftRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CreateGrid(WorldFrequency.Rift);
        }

        private void SparseRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CreateGrid(WorldFrequency.Sparse);
        }

        private void DenseRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CreateGrid(WorldFrequency.Dense);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            dlg.Filter = "XML file (*.xml)|*.xml";
            dlg.DefaultExt = ".xml";
            if (dlg.ShowDialog() == true)
            {
                string settingsPath = dlg.FileName;

                //XmlSerializer xmlSerial = new XmlSerializer(typeof(LocalSensorSettings));
                XmlSerializer xmlSerial = XmlSerializer.FromTypes(new[] { typeof(List<PlanetProfile>) })[0];
                try
                {
                    using (Stream fStream = new FileStream(settingsPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        xmlSerial.Serialize(fStream, _profiles);
                    }

                }
                catch (Exception)
                {

                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            dlg.Filter = "XML file (*.xml)|*.xml";
            dlg.DefaultExt = ".xml";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    string settingsPath = dlg.FileName;
                    List<PlanetProfile> XmlSave;
                    XmlSerializer xmlSerial = XmlSerializer.FromTypes(new[] { typeof(List<PlanetProfile>) })[0];
                    using (Stream fStream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        XmlSave = (List<PlanetProfile>)xmlSerial.Deserialize(fStream);
                        HexGrid.Children.Clear();
                        _profiles.Clear();
                        foreach (PlanetProfile p in XmlSave)
                        {
                            AddPlanetToGrid(p, HexGrid);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An exception occured while reading the save file.\r\nYou were messing with it, weren't you?");
                }
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void titlebar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow h = new HelpWindow();
            h.Show();
        }

        private void NotesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _currentButton.Planet.Notes = NotesTextBox.Text;
        }

        private void RedZonedCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            _currentButton.Planet.isRed = true;
            _currentButton.SetRed();
        }

        private void RedZonedCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            _currentButton.Planet.isRed = false;
            if (_currentButton.Planet.isAmber)
                _currentButton.SetAmber();
            else _currentButton.SetWhite();
        }
    }
}