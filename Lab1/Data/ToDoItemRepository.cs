using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Lab1.Data
{
    /// <summary>
    /// Класс репозиторий для сохранения данных о задачах
    /// </summary>
    public class ToDoItemRepository
    {
        // Относительный путь к файлу с данными задач
        private const string TODO_FILE_PATH = "LocalDB/todoData.json";

        // Чтение данных задач из файла
        private List<ToDoItem> ReadToDoItems()
        {
            string todoDataJson = File.ReadAllText(TODO_FILE_PATH);
            List<ToDoItem>? todoItems = JsonSerializer.Deserialize<List<ToDoItem>>(todoDataJson);
            return todoItems ?? new List<ToDoItem>();
        }

        // Сохранение данных задач в файл
        private void SaveToDoItems(List<ToDoItem> todoItems)
        {
            string jsonString = JsonSerializer.Serialize(todoItems);
            File.WriteAllText(TODO_FILE_PATH, jsonString);
        }

        /// <summary>
        /// Возвращает все задачи
        /// </summary>
        /// <returns>Список задач</returns>
        public List<ToDoItem> GetAllItems()
        {
            lock (this)
            {
                return ReadToDoItems();
            }
        }

        /// <summary>
        /// Возвращает задачу по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        public ToDoItem? GetItemById(Guid id)
        {
            lock (this)
            {
                return ReadToDoItems().FirstOrDefault(item => item.Id == id);
            }
        }

        /// <summary>
        /// Добавляет новую задачу
        /// </summary>
        /// <param name="item">Новая задача</param>
        public void AddItem(ToDoItem item)
        {
            lock (this)
            {
                List<ToDoItem> todoItems = ReadToDoItems();

                // Находим максимальный идентификатор и увеличиваем на 1
                Guid maxId = todoItems.Any() ? todoItems.Max(i => i.Id) : Guid.Empty;
                item.Id = maxId == Guid.Empty ? Guid.NewGuid() : Guid.NewGuid(); // Генерируем новый уникальный идентификатор
                todoItems.Add(item);
                SaveToDoItems(todoItems);
            }
        }

        /// <summary>
        /// Обновляет задачу
        /// </summary>
        /// <param name="item">Задача для обновления</param>
        public void UpdateItem(ToDoItem item)
        {
            lock (this)
            {
                List<ToDoItem> todoItems = ReadToDoItems();
                var existingItem = todoItems.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.Topic = item.Topic;
                    existingItem.IsCompleted = item.IsCompleted;
                    existingItem.Priority = item.Priority;
                    SaveToDoItems(todoItems);
                }
            }
        }
    

        /// <summary>
        /// Удаляет задачу по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор задачи для удаления</param>
        public void DeleteItem(Guid id)
        {
            lock (this)
            {
                List<ToDoItem> todoItems = ReadToDoItems();
                ToDoItem? item = todoItems.Find(i => i.Id == id);
                if (item != null)
                {
                    todoItems.Remove(item);
                    SaveToDoItems(todoItems);
                }
            }
        }
    }
}

