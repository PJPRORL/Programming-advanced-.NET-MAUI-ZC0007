namespace ProgrammingAdvancedOefeningen.Repositories
{
    public class InMemoryVentilatorRepository : IVentilatorRepository
    {
        private static List<Ventilator> ventilatoren = new List<Ventilator>
        {
            new Ventilator {Id = 1, AfmetingInMm = 120, Merk = "Noctua", Prijs = 34.99, Verlichting = "Geen"},
            new Ventilator {Id = 2, AfmetingInMm = 140, Merk = "be quiet!", Prijs = 24.99, Verlichting = "Geen"},
            new Ventilator {Id = 3, AfmetingInMm = 120, Merk = "Corsair", Prijs = 29.99, Verlichting = "RGB"},
            new Ventilator {Id = 4, AfmetingInMm = 140, Merk = "Lian Li", Prijs = 32.99, Verlichting = "ARGB"}
        };

        public List<Ventilator> GetAll()
        {
            return ventilatoren;
        }

        public Ventilator? GetVentilatorById(int id)
        {
            return ventilatoren.FirstOrDefault(fan => fan.Id == id);
        }

        public Ventilator Create(Ventilator nieuweVentilator)
        {
            int nieuweId = ventilatoren.Max(fan => fan.Id) + 1;
            nieuweVentilator.Id = nieuweId;

            ventilatoren.Add(nieuweVentilator);
            return nieuweVentilator;
        }

        public void Update(int id, Ventilator ventilator)
        {
            Ventilator? bestaandeVentilator = ventilatoren.FirstOrDefault(fan => fan.Id == id);

            if (bestaandeVentilator != null)
            {
                bestaandeVentilator.AfmetingInMm = ventilator.AfmetingInMm;
                bestaandeVentilator.Merk = ventilator.Merk;
                bestaandeVentilator.Prijs = ventilator.Prijs;
                bestaandeVentilator.Verlichting = ventilator.Verlichting;
            }
        }

        public void Delete(int id)
        {
            Ventilator? ventilator = ventilatoren.FirstOrDefault(fan => fan.Id == id);

            if (ventilator != null)
            {
                ventilatoren.Remove(ventilator);
            }
        }
    }
}
