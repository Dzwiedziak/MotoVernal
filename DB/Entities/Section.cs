namespace DB.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Section? Parent { get; set; }
        public int? ParentId { get; set; }
        public File? Image { get; set; }

        public Section() { }

        public Section(int id, string title, Section parent, File? image)
        {
            Id = id;
            Title = title;
            Parent = parent;
            Image = image;
        }

        public Section(string title, Section parent, File? image)
            : this(0, title, parent, image) { }
    }
}
