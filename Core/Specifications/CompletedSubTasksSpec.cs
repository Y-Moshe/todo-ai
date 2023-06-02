using Core.Entities;

namespace Core.Specifications;

public class CompletedSubTasksSpec : BaseSpecification<SubTask>
{
    public CompletedSubTasksSpec()
        : base(st => st.IsDone == true) { }
}
