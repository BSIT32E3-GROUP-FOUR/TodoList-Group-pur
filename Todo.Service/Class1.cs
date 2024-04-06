using Todolist.Models;

namespace Todo.Service;

public record TodoModel
{
    public int? Id { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime? DueDate { get; init; }
    public Category Category { get; init; } = Category.Adventure;
    public bool IsActive { get; init; } = true;
    public Status Status { get; init; } = Status.New;
    public bool Overdue => this.Status == Status.InProgress && DueDate < DateTime.Today;
    public bool IsNew() => this.Status == Status.New;
    public bool IsNotSaved() => this.Id.HasValue || this.Id > 0;
}

public record AddTodoModel
{
    public string Description { get; init; } = string.Empty;
    public DateTime? DueDate { get; init; }
    public Category Category { get; init; } = Category.Adventure;
}