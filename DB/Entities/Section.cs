namespace DB.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Section? Parent { get; set; }
        public int? ParentId { get; set; }

        public Section() { }

        public Section(int id, string title, Section parent)
        {
            Id = id;
            Title = title;
            Parent = parent;
        }

        public Section(string title, Section parent)
            : this(0, title, parent) { }
    }
}
