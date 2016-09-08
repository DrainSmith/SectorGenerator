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

namespace SectorGenerator
{
    /// <summary>
    /// Interaction logic for HexButton.xaml
    /// </summary>
    public partial class HexButton : UserControl
    {
        public PlanetProfile Planet;

        private string _sectorId = "";
        private string _profile = "";
        private string _name = "";
        private string _tradeCodes = "";
        public string SectorId { get { return _sectorId; } set { _sectorId = value; SubSectorNumber.Content = value; } }
        public string Profile { get { return _profile; } set { _profile = value; UniversalSectorDescriptor.Content = value; } }
        public string TradeCodes { get { return _tradeCodes; } set { _tradeCodes = value; TradeCodesLabel.Content = value; } }
        public string SubSectorName { get { return _name; } set { _name = value; NameLabel.Content = value; } }

        private EventHandler<RoutedEventArgs> _onButtonClickEvent;
        public event EventHandler<RoutedEventArgs> Click
        {
            add { _onButtonClickEvent += value; }
            remove { _onButtonClickEvent -= value; }
        }

        private void ClickEvent(RoutedEventArgs args)
        {
            if (_onButtonClickEvent != null)
            {
                _onButtonClickEvent(this, args);
            }
        }

        public HexButton(PlanetProfile p)
        {
            InitializeComponent();
            Planet = p;
            SectorId = p.SectorId;
            if (p.hasPlanet)
            {
                var s = p.Profile;
                Profile = s;
                TradeCodes = p.TradeCodes;
                SubSectorName = p.Name;
                if (p.isAmber)
                    SetAmber();
                if (p.isRed)
                    SetRed();
                if (p.HasGasGiant)
                    SetGasGiant();
                if (p.Hydrographics > 3)
                    SetWaterPlanet();
                else SetDryPlanet();

                if (p.HasScoutBase)
                    SetScoutBase();
                if (p.HasTas)
                    SetTas();
                if (p.HasNavalBase)
                    SetNavalBase();
            }
        }

        public void SetScoutBase()
        {
            ScoutBaseImage.Visibility = Visibility.Visible;
        }

        public void SetTas()
        {
            TasImage.Visibility = Visibility.Visible;
        }

        public void SetNavalBase()
        {
            NavalBaseImage.Visibility = Visibility.Visible;
        }

        public void SetAmber()
        {
            img.Source = Resources["HexagonAmberImage"] as ImageSource;
        }

        public void SetRed()
        {
            img.Source = Resources["HexagonRedImage"] as ImageSource;
        }

        public void SetWhite()
        {
            img.Source = Resources["HexagonImage"] as ImageSource;
        }

        public void SetWaterPlanet()
        {
            WaterImage.Visibility = Visibility.Visible;
        }

        public void SetDryPlanet()
        {
            DryImage.Visibility = Visibility.Visible;
        }

        public void SetGasGiant()
        {
            GasGiantImage.Visibility = Visibility.Visible;
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            ClickEvent(e);
        }
    }
}
