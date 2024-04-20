namespace Lab1.Data;

/// <summary>
/// Простой POCO класс представляющий строку данных
/// </summary>
// TODO: переименуйте этот файл и класс, добавьте атрибуты
public class ToDoItem
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Тема задачи
    /// </summary>
    public string Topic { get; set; }
    /// <summary>
    /// флаг выполнения
    /// </summary>
    public bool IsCompleted { get; set; }
    /// <summary>
    /// приоритет
    /// </summary>
    public int Priority { get; set; }

    public ToDoItem(string topic, bool isCompleted, int priority)
    {
        Id = Guid.NewGuid(); // Генерируем новый уникальный идентификатор
        Topic = topic;
        IsCompleted = isCompleted;
        Priority = priority;
    }
}
