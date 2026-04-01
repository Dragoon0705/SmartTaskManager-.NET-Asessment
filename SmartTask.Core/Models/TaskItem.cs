using System;

namespace SmartTask.Core.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public virtual void MarkComplete()
        {
            IsCompleted = true;
        }

        public virtual void UpdateDetails(string title, string description, string category, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            Title = title;
            Description = description;
            Category = category;
            DueDate = dueDate;
        }
    }
}