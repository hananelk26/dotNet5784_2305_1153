namespace DO;

public record Task
(
    int Id,
    string Alias,
    string Description,
    DateTime  createdAtDate,
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
    public Task() : this(0,"","",DateTime.Now) { }

  
    public  bool ShouldSerializeEngineerld()
    { 
        return Engineerld.HasValue; 
    }
    public bool ShouldSerializeRemarks()
    {
        return !string.IsNullOrEmpty(Remarks);
    }

    public bool ShouldSerializeDeliverables()
    {
        return !string.IsNullOrEmpty(Deliverables);
    }
    public bool ShouldSerializeCompleteDate()
    {
        return !string.IsNullOrEmpty(CompleteDate.ToString());
    }
    public bool ShouldSerializeDeadlineDate()
    {
        return !string.IsNullOrEmpty(DeadlineDate.ToString());
    }
    public bool ShouldSerializeScheduledDate()
    {
        return !string.IsNullOrEmpty(ScheduledDate.ToString());
    }
    public bool ShouldSerializeStartDate()
    {
        return !string.IsNullOrEmpty(StartDate.ToString());
    }
    public bool ShouldSerializeCopmlexity()
    {
        return !string.IsNullOrEmpty(Copmlexity.ToString());
    }
    public bool ShouldSerializeRequiredEffortTime()
    {
        return !string.IsNullOrEmpty(RequiredEffortTime.ToString());
    }

}