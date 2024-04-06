namespace Todo.Service;

public interface ITodoService
{
    Task<bool> Delete(int id);
    Task<bool> MarkAsDone(int id);
    IEnumerable<TodoModel> GetAll();
    Task<int> Add(AddTodoModel model);
    Task<int> UpdateDescription(int id, string description);
    
}