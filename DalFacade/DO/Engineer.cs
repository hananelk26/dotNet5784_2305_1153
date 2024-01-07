namespace DO;
public record Engineer
(
    int Id,
    string Email,
    double Cost,
    string Name,
    DO.EngineerExperience Level

)
{
    public Engineer() : this(0, "", 0, "",0) { }

}