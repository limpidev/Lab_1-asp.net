using Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages;

public class CreateModel : PageModel
{
    private readonly ToDoItemRepository _toDoItemRepository;

    [BindProperty]
    public string Topic { get; set; }

    [BindProperty]
    public bool IsCompleted { get; set; }

    [BindProperty]
    public int Priority { get; set; }

    public CreateModel(ToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
        Topic = string.Empty;
        IsCompleted = false;
        Priority = 0;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPostAdd()
    {
        ToDoItem item = new ToDoItem(Topic, IsCompleted, Priority);
        _toDoItemRepository.AddItem(item);
        return RedirectToPage("/Index");
    }
}
