namespace DB.Entities
{
    public class Phone
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Extension { get; set; }

        public Phone() { }

        public Phone(int id, string number, string extension)
        {
            Id = id;
            Number = number;
            Extension = extension;
        }

        public Phone(string number, string extension)
           : this(0, number, extension) { }
    }
}
