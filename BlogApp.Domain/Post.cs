namespace BlogApp.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        // public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(Content)}: {Content}";
        }
    }
}