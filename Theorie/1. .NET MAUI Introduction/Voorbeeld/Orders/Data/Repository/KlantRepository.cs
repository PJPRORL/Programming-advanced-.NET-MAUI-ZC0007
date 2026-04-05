

namespace Orders.Data.Repository
{
    public class KlantRepository : BaseRepository, IKlantRepository
    {
        public IEnumerable<Klant> KlantenOphalenMetOrders()
        {
            string sql = @"SELECT K.*, O.*
                           FROM Klanten K
                           INNER JOIN Orders O ON K.Id = O.KlantId
                           ORDER BY K.Bedrijf, O.Id";

            using IDbConnection db = new SqlConnection(ConnectionString);
            var klanten = db.Query<Klant, Order, Klant>(
                sql,
                (klant, order) =>
                {
                    order.Klant = klant;
                    klant.Orders = new List<Order>() { order };
                    return klant;
                }
            );

            return GroepeerKlanten(klanten);
        }

        public IEnumerable<Klant> KlantenOphalenMetOrdersEnOrderlijnen()
        {
            string sql = @"SELECT K.*, O.*, OL.*
                           FROM Klanten K
                           INNER JOIN Orders O ON K.Id = O.KlantId
                           INNER JOIN Orderlijnen OL ON O.Id = OL.OrderId
                           ORDER BY K.Bedrijf, O.Id";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var klanten = db.Query<Klant, Order, Orderlijn, Klant>(
                    sql,
                    (klant, order, orderlijn) =>
                    {
                        order.Klant = klant;
                        orderlijn.Order = order;
                        order.Orderlijnen = new List<Orderlijn>() { orderlijn };
                        klant.Orders = new List<Order>() { order };
                        return klant;
                    }
                );

                var gegroepeerdeKlanten = GroepeerKlantenAlternatief(klanten);

                return gegroepeerdeKlanten.Select(k =>
                {
                    k.Orders = GroepeerOrders(k.Orders);
                    return k;
                });
            }
        }

        public IEnumerable<Klant> KlantenOphalenMetOrdersEnOrderlijnenEnProduct()
        {
            string sql = @"SELECT K.*, O.*, OL.*, P.*
                           FROM Klanten K
                           INNER JOIN Orders O ON K.Id = O.KlantId
                           INNER JOIN Orderlijnen OL ON O.Id = OL.OrderId
                           INNER JOIN Producten P ON OL.ProductId = P.Id
                           ORDER BY K.Bedrijf, O.Id";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var klanten = db.Query<Klant, Order, Orderlijn, Product, Klant>(
                    sql,
                    (klant, order, orderlijn, product) =>
                    {
                        order.Klant = klant;
                        orderlijn.Order = order;
                        orderlijn.Product = product;
                        order.Orderlijnen = new List<Orderlijn>() { orderlijn };
                        klant.Orders = new List<Order>() { order };
                        return klant;
                    }

                );

                var gegroepeerdeKlanten = GroepeerKlanten(klanten);

                // De property Orderlijnen van de Order objecten bevat nog steeds
                // een list met één element. We moeten nogmaals groeperen.
                return gegroepeerdeKlanten.Select(k =>
                {
                    k.Orders = GroepeerOrders(k.Orders);
                    return k;
                });
            }
        }

        public IEnumerable<Klant> KlantenOphalenMetOrdersEnOrderlijnenEnProductEnWerknemers()
        {
            string sql = @"SELECT K.*, O.*, OL.*, P.*, W.*
                           FROM Klanten K
                           INNER JOIN Orders O ON K.Id = O.KlantId
                           INNER JOIN Orderlijnen OL ON O.Id = OL.OrderId
                           INNER JOIN Producten P ON OL.ProductId = P.Id
                           INNER JOIN Werknemers W ON O.WerknemerId = W.Id
                           ORDER BY K.Bedrijf, O.Id";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var klanten = db.Query<Klant, Order, Orderlijn, Product, Werknemer, Klant>(
                    sql,
                    (klant, order, orderlijn, product, werknemer) =>
                    {
                        order.Klant = klant;
                        order.Werknemer = werknemer;
                        orderlijn.Order = order;
                        orderlijn.Product = product;
                        order.Orderlijnen = new List<Orderlijn>() { orderlijn };
                        klant.Orders = new List<Order>() { order };
                        return klant;
                    }

                );

                var gegroepeerdeKlanten = GroepeerKlanten(klanten);

                // De property Orderlijnen van de Order objecten bevat nog steeds
                // een list met één element. We moeten nogmaals groeperen.
                return gegroepeerdeKlanten.Select(k =>
                {
                    k.Orders = GroepeerOrders(k.Orders);
                    return k;
                });
            }
        }

        private static ICollection<Klant> GroepeerKlanten(IEnumerable<Klant> klanten)
        {
            var gegroepeerd = klanten.GroupBy(k => k.Id);

            List<Klant> klantenMetOrders = new List<Klant>();
            foreach (var groep in gegroepeerd)
            {
                var klant = groep.First();
                List<Order> alleOrders = new List<Order>();
                foreach (var k in groep)
                {
                    alleOrders.Add(k.Orders.First());
                }
                klant.Orders = alleOrders;
                klantenMetOrders.Add(klant);
            }

            return klantenMetOrders;
        }

        private static ICollection<Klant> GroepeerKlantenAlternatief(IEnumerable<Klant> klanten)
        {
            var gegroepeerd = klanten.GroupBy(k => k.Id);
            return gegroepeerd.Select(g =>
            {
                var klant = g.First();
                klant.Orders = g.Select(k => k.Orders.Single()).ToList();
                return klant;
            }).ToList();
        }

        private static ICollection<Order> GroepeerOrders(IEnumerable<Order> orders)
        {
            return orders.GroupBy(o => o.Id).Select(g =>
            {
                var order = g.First();
                order.Orderlijnen = g.Select(o => o.Orderlijnen.Single()).ToList();
                return order;
            }).ToList();
        }
    }
}