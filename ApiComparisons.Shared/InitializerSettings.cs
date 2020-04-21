namespace ApiComparisons.Shared
{
    public class InitializerSettings
    {
        public int Persons { get; set; }
        public int Stores { get; set; }
        public int Products { get; set; }
        public int Purchases { get; set; }
        public int Transactions { get; set; }

        public override string ToString()
        {
            return $"Persons: {Persons}, Stores: {Stores}, Products: {Products}, Purchases: {Purchases}, Transactions: {Transactions}";
        }
    }
}
