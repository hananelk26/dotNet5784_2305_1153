namespace DO;

public record Task
(
    int Id,
    string Alias,
    string Description,
    DateTime  createdAtDate,
    bool isMileStone=false,
    TimeSpan? RequiredEffortTime = null,
    DO.EngineerExperience? Copmlexity = null,
    DateTime? StartDate = null,
    DateTime? ScheduledDate = null,
    DateTime? DeadlineDate = null,
    DateTime? CompleteDate = null,
    string? Deliverables = null,
    string? Remarks = null,
    int? Engineerld = null
)
{
    public Task() : this(0,"","",DateTime.Now) { }/// empty ctor

}