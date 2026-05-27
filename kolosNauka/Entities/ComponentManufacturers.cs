namespace kolosNauka.Entities;

public class ComponentManufacturers
{
    public int Id { get; set; }
    public string Abbreviation { get; set; }
    public string FullName { get; set; }
    public DateOnly FoundationDate { get; set; }
    
    public virtual ICollection<Components> Components { get; set; }
}