namespace BlastAce.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Property1 { get; set; }
        public string? Property2 { get; set; }
        public int Progress { get; set; }
        public bool Deleted { get; set; }
    }
}
