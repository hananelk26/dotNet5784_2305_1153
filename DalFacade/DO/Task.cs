

namespace DO;

public record Task
(
    int id,
    string? Alias,
    string? Description,
    DateTime?  createdAtDate,
    TimeSpan? RequiredEffortTime,
    bool? isMileStone,
    DO.EngineerExperience? Copmlexity,
    DateTime? StartDate,
    DateTime? ScheduledDate,
    DateTime? DeadlineDate,
    DateTime? CompleteDate,
    string? Deliverables,
    string? Remarks,
    int Engineerld
)
{
    public Task() : this() { };
   

}