using SmartTask.Core.Exceptions;
using SmartTask.Core.Models;
using System.Text.Json;

namespace SmartTask.Core.Services
{
    public class UserTaskManager
    {
        private List<TaskItem> tasks = new();
        private int currentId = 1;
        private readonly string filePath = "tasks.json";

        public async Task LoadTasksAsync()
        {
            if (File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
                currentId = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
            }
        }

        public async Task SaveTasksAsync()
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }

        public void AddTask(TaskItem task)
        {
            task.Id = currentId++;
            tasks.Add(task);
        }

        public void UpdateTask(int id, TaskItem updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                throw new TaskNotFoundException("Task not found");

            task.UpdateDetails(updatedTask.Title, updatedTask.Description,
                               updatedTask.Category, updatedTask.DueDate);
        }

        public void DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                throw new TaskNotFoundException("Task not found");

            tasks.Remove(task);
        }

        public List<TaskItem> ListTasks() => tasks;

        public List<TaskItem> ListByCategory(string category)
        {
            return tasks.Where(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}