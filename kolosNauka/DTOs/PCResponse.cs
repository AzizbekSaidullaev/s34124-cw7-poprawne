namespace kolosNauka.DTOs;

public record PCResponse(
    int Id,
    string Name,
    float Weight,
    int Warranty,
    DateTime CreatedAt,
    int Stock 
    );