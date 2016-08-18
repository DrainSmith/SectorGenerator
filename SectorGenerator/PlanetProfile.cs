using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SectorGenerator
{
    [Serializable]
    public class PlanetProfile
    {
        public string Profile { get { if (!hasPlanet) return ""; else return StarPort.ToString() + Size.ToString("X") + Atmosphere.ToString("X") + Hydrographics.ToString("X") + Population.ToString("X") + Government.ToString("X") + LawLevel.ToString("X") + "-" + TechLevel.ToString("X"); } }
        public string Name { get; set; }

        public int Size { get; set; }
        public int Atmosphere { get; set; }
        public int Hydrographics { get; set; }
        public int Population { get; set; }
        public int Government { get; set; }
        public int LawLevel { get; set; }
        public int TechLevel { get; set; }
        public char StarPort { get; set; }
        public int Temperature { get; set; }
        public bool HasGasGiant { get; set; }
        public string TradeCodes { get; set; }
        public string SectorId { get { return (Column + 1).ToString().PadLeft(2, '0') + (Row + 1).ToString().PadLeft(2, '0'); } }
        public int Column { get; set; }
        public int Row { get; set; }
        public string Notes { get; set; }
        public bool isRed { get; set; }
        public bool HasTas { get; set; }
        public bool HasNavalBase { get; set; }
        public bool HasScoutBase { get; set; }
        Random r;

        public bool hasPlanet = false;

        public bool IsAgricultural { get
            {
                if (Atmosphere > 3 && Atmosphere < 10 && Hydrographics > 3 && Hydrographics < 9 && Population > 4 && Population < 8)
                    return true;
                else return false;

            } }
        public bool isAsteroid {
            get {
                if (Size == 0 && Atmosphere == 0 && Hydrographics == 0)
                    return true;
                else return false;
            }
        }

        public bool isBarren { get {
                if (Population == 0 && Government == 0 && LawLevel == 0)
                    return true;
                else return false;
            } }

        public bool isDesert
        {
            get {
                if (Atmosphere > 1 && Hydrographics == 0)
                    return true;
                else return false;
            }

        }

        public bool isFluidOceans
        {
            get
            {
                if (Atmosphere > 10 && Hydrographics > 0)
                    return true;
                else return false;

            }
        }

        public bool isGarden
        {
            get
            {
                if (Size > 5 && Size < 9 && (Atmosphere == 5 || Atmosphere == 6 || Atmosphere == 8) && Hydrographics > 4 && Hydrographics < 8)
                    return true;
                else return false;

            }
        }

        public bool isHighPopulation
        {
            get
            {
                if (Population > 8)
                    return true;
                else return false;

            }
        }

        public bool isHighTech
        {
            get
            {
                if (TechLevel > 11)
                    return true;
                else return false;

            }
        }

        public bool isIceCapped
        {
            get
            {
                if ((Atmosphere == 0 || Atmosphere == 1) && Hydrographics > 0)
                    return true;
                else return false;

            }
        }

        public bool isIndustrial
        {
            get
            {
                if ((Atmosphere == 0 || Atmosphere == 1 || Atmosphere == 2 || Atmosphere == 4 || Atmosphere == 7 || Atmosphere == 9) && Population > 8)
                    return true;
                else return false;

            }
        }

        public bool isLowPopulation
        {
            get
            {
                if (Population < 4)
                    return true;
                else return false;

            }
        }

        public bool isLowTech
        {
            get
            {
                if (TechLevel < 6)
                    return true;
                else return false;

            }
        }

        public bool isNonAgricultural
        {
            get
            {
                if (Atmosphere < 4 && Hydrographics < 4 && Population > 5)
                    return true;
                else return false;

            }
        }

        public bool isNonIndustrial
        {
            get
            {
                if (Population < 7)
                    return true;
                else return false;

            }
        }

        public bool isPoor
        {
            get
            {
                if (Atmosphere > 1 && Atmosphere < 6 && Hydrographics < 4)
                    return true;
                else return false;

            }
        }

        public bool isRich
        {
            get
            {
                if ((Atmosphere == 6 || Atmosphere == 8) && Population >5 && Population< 9 && Government > 3 && Government < 10)
                    return true;
                else return false;

            }
        }

        public bool isVacuum
        {
            get
            {
                if (Atmosphere == 0)
                    return true;
                else return false;

            }
        }

        public bool isWaterWorld
        {
            get
            {
                if (Hydrographics > 9)
                    return true;
                else return false;

            }
        }

        public bool isAmber
        { get {
                if (Atmosphere > 9 && (Government == 0 || Government == 7 || Government == 10) && (LawLevel == 0 || LawLevel > 8))
                    return true;
                else return false;
            } }

        // for serialization
        public PlanetProfile()
        { }

        public PlanetProfile(Random r, WorldFrequency freq)
        {
            this.r = r;
            int val = r.Next(10, 60);
            val = val + (int)freq;
            Name = string.Empty;
            if (val >= 40)
            {
                GenerateProfile();
                hasPlanet = true;
            }
        }

        private void GenerateProfile()
        {
            Size = GenerateSize();
            Atmosphere = GenerateAtmosphere(Size);
            Hydrographics = GenerateHydrographics(Size, Atmosphere);
            Population = GeneratePopulation();
            Government = GenerateGovernment(Population);
            LawLevel = GenerateLawLevel(Population, Government);
            StarPort = GenerateStarport(Population);
            TechLevel = GenerateTechLevel(StarPort, Size, Atmosphere, Hydrographics, Population, Government);
            Temperature = GenerateTemperature();
            GenerateBases();
            Name = RandomName.GetName(r);
            var i = r.Next(2, 12);
            if (i > 9)
                HasGasGiant = false;
            else HasGasGiant = true;
            GenerateTradeCodes();
        }

        private void GenerateBases()
        {
            if (StarPort == 'A')
            {
                HasTas = true;
                int val = r.Next(2, 12);
                if (val > 7)
                    HasNavalBase = true;
                val = r.Next(2, 12);
                if (val > 9)
                    HasScoutBase = true;
                
            }
            else if (StarPort == 'B')
            {
                HasTas = true;
                int val = r.Next(2, 12);
                if (val > 7)
                    HasNavalBase = true;
                val = r.Next(2, 12);
                if (val > 7)
                    HasScoutBase = true;
                
            }
            else if (StarPort == 'C')
            {
                int val = r.Next(2, 12);
                if (val > 7)
                    HasScoutBase = true;
                val = r.Next(2, 12);
                if (val > 9)
                    HasTas = true;
            }
            if (StarPort == 'D')
            {
                int val = r.Next(2, 12);
                if (val > 6)
                    HasScoutBase = true;
            }
        }

        private void GenerateTradeCodes()
        {
            TradeCodes = string.Empty;
            if (IsAgricultural)
                TradeCodes += "Ag ";
            if (isAsteroid)
                TradeCodes += "As ";
            if (isBarren)
                TradeCodes += "Ba ";
            if (isDesert)
                TradeCodes += "De ";
            if (isFluidOceans)
                TradeCodes += "Fl ";
            if (isGarden)
                TradeCodes += "Ga ";
            if (isHighPopulation)
                TradeCodes += "Hi ";
            if (isHighTech)
                TradeCodes += "Ht ";
            if (isIceCapped)
                TradeCodes += "Ie";
            if (isIndustrial)
                TradeCodes += "In ";
            if (isLowPopulation)
                TradeCodes += "Lo ";
            if (isLowTech)
                TradeCodes += "Lt ";
            if (isNonAgricultural)
                TradeCodes += "Na ";
            if (isNonIndustrial)
                TradeCodes += "Ni ";
            if (isPoor)
                TradeCodes += "Po ";
            if (isRich)
                TradeCodes += "Ri ";
            if (isVacuum)
                TradeCodes += "Va ";
            if (isWaterWorld)
                TradeCodes += "Wa ";
        }

        private int GenerateTemperature()
        {
            var i = r.Next(2, 12);
            if (Atmosphere == 2 || Atmosphere == 3)
                i -= 2;
            else if (Atmosphere == 4 || Atmosphere == 5 || Atmosphere == 14)
                i -= 1;
            else if (Atmosphere == 8 || Atmosphere == 9)
                i += 1;
            else if (Atmosphere == 10 || Atmosphere == 13 || Atmosphere == 15)
                i += 2;
            else if (Atmosphere == 11 || Atmosphere == 12)
                i += 6;
            if (i < 0)
                i = 0;
            if (i > 15)
                i = 15;
            return i;
        }

        private int GenerateSize()
        {
            var i = r.Next(0, 10);
            return i;
        }

        private int GenerateAtmosphere(int Size)
        {
            var i = r.Next(2, 12);
            i = i - 7 + Size;
            if (i < 0)
                i = 0;
            return i;
        }

        private int GenerateHydrographics(int Size, int Atmosphere)
        {
            if ((Size == 0) || (Size == 1))
                return 0;
            var i = r.Next(2, 12);
            i = i - 7 + Atmosphere;
            if ((Atmosphere <= 1) || (Atmosphere > 9 && Atmosphere < 13))
                i = i - 4;
            if (i < 0)
                i = 0;
            if (i > 10)
                i = 10;
            return i;
        }

        private int GeneratePopulation()
        {
            var i = r.Next(0, 10);
            return i;
        }

        private int GenerateGovernment(int Population)
        {
            if (Population == 0)
                return 0;
            var i = r.Next(2, 12);
            i = i - 7 + Population;
            if (i < 0)
                i = 0;
            return i;
        }

        private int GenerateLawLevel(int Population, int Government)
        {
            if (Population == 0)
                return 0;
            var i = r.Next(2, 12);
            i = i - 7 + Government;
            if (i < 0)
                i = 0;
            return i;
        }

        private char GenerateStarport(int Population)
        {
            var i = r.Next(2, 12);
            var mod = 0;
            if (Population >= 10)
                mod = 2;
            else if (Population >= 8)
                mod = 1;
            else if (Population <= 2)
                mod = -2;
            else if (Population <= 4)
                mod = -4;
            i = i + mod;
            if (i <= 2)
                return 'X';
            else if (i <= 4)
                return 'E';
            else if (i <= 6)
                return 'D';
            else if (i <= 8)
                return 'C';
            else if (i <= 10)
                return 'B';
            else return 'A';
        }

        private int GenerateTechLevel(char Starport, int Size, int Atmosphere, int Hydrographics, int Population, int Government)
        {
            if (Population == 0)
                return 0;
            var i = r.Next(2, 12);
            var mod = 0;
            if (Starport == 'X')
                mod += -4;
            else if (Starport == 'C')
                mod += 2;
            else if (Starport == 'B')
                mod += 4;
            else if (Starport == 'A')
                mod += 6;

            if (Size <= 1)
                mod += 2;
            else if (Size <= 4)
                mod += 1;

            if ((Atmosphere <= 3) || Atmosphere >= 10)
                mod += 1;

            if ((Hydrographics == 1) || (Hydrographics == 9))
                mod += 1;
            else if (Hydrographics == 10)
                mod += 2;

            if (((Population <= 5) && (Population > 0)) || (Population == 8))
                mod += 1;
            else if (Population == 9)
                mod += 2;
            else if (Population == 10)
                mod += 4;

            if ((Government == 0) || (Government == 5))
                mod += 1;
            else if (Government == 7)
                mod += 2;
            else if ((Government == 13) || (Government == 14))
                mod += -2;

            i = i + mod;
            if (i > 16)
                i = 15;
            else if (i < 0)
                i = 0;
            return i;
        }
    }

    public enum WorldFrequency : int
    {
        Normal = 0,
        Rift = -15,
        Sparse = -10,
        Dense = +10
    }
}
