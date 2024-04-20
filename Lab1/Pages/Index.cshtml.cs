using Lab1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace Lab1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ToDoItemRepository _toDoItemRepository;

        public List<ToDoItem> ToDoItems { get; set; }

        public IndexModel(ToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
            ToDoItems = new List<ToDoItem>();
        }

        public void OnGet()
        {
            ToDoItems = _toDoItemRepository.GetAllItems();
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPostDelete(Guid id)
        {
            try
            {
                _toDoItemRepository.DeleteItem(id);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                // Обработка ошибок удаления задачи
                return RedirectToPage("/Error");
            }
        }
    }
}
