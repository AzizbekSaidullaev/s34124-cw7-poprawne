namespace kolosNauka.Entities;

public class Components
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ComponentManufacturerId { get; set; }
    public int ComponentTypesId { get; set; }
    
    public virtual ComponentTypes ComponentTypes { get; set; }
    public virtual ComponentManufacturers ComponentManufacturers { get; set; }
    public virtual ICollection<PCComponents> PCComponents { get; set; }
}