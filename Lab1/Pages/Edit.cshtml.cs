using Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Lab1.Pages
{
    public class EditModel : PageModel
    {
        private readonly ToDoItemRepository _toDoItemRepository;

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public string Topic { get; set; }

        [BindProperty]
        public bool IsCompleted { get; set; }

        [BindProperty]
        public int Priority { get; set; }

        public EditModel(ToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public IActionResult OnGet()
        {
            var task = _toDoItemRepository.GetItemById(Id);
            if (task == null)
            {
                return RedirectToPage("/Index");
            }

            Topic = task.Topic;
            IsCompleted = task.IsCompleted;
            Priority = task.Priority;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var task = new ToDoItem(Topic, IsCompleted, Priority);
            task.Id = Id; // Присваиваем существующий идентификатор
            _toDoItemRepository.UpdateItem(task);

            return RedirectToPage("/Index");
        }
    }
}
