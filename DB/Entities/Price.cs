namespace DB.Entities
{
    public class Price
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }

        public Price() { }

        public Price(int id, int amount, string currency)
        {
            Id = id;
            Amount = amount;
            Currency = currency;
        }

        public Price(int amount, string currency)
            : this(0, amount, currency) { }
    }
}
