using Core.Entities;

namespace Core.Specifications;

public class CompletedUserSubTasksSpec : BaseSpecification<SubTask>
{
    public CompletedUserSubTasksSpec(string userId)
        : base(st => st.AppUserId == userId && st.IsDone == true) { }
}
