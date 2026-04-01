using SmartTask.Core.Models;
using SmartTask.Core.Services;

class Program
{
    static async Task Main()
    {
        var manager = new UserTaskManager();
        await manager.LoadTasksAsync();

        while (true)
        {
            Console.WriteLine("\n1.Add 2.View 3.Update 4.Delete 5.Exit");
            int ch = int.Parse(Console.ReadLine());

            try
            {
                if (ch == 1)
                {
                    Console.Write("Title: ");
                    string t = Console.ReadLine();

                    Console.Write("Desc: ");
                    string d = Console.ReadLine();

                    Console.Write("Category: ");
                    string c = Console.ReadLine();

                    Console.Write("Due Date: ");
                    DateTime date = DateTime.Parse(Console.ReadLine());

                    manager.AddTask(new TaskItem
                    {
                        Title = t,
                        Description = d,
                        Category = c,
                        DueDate = date
                    });

                    await manager.SaveTasksAsync();
                }
                else if (ch == 2)
                {
                    foreach (var task in manager.ListTasks())
                        Console.WriteLine($"{task.Id} {task.Title} {task.Category}");
                }
                else if (ch == 3)
                {
                    Console.Write("ID: ");
                    int id = int.Parse(Console.ReadLine());

                    Console.Write("New Title: ");
                    string t = Console.ReadLine();

                    Console.Write("New Desc: ");
                    string d = Console.ReadLine();

                    Console.Write("New Category: ");
                    string c = Console.ReadLine();

                    Console.Write("New Date: ");
                    DateTime date = DateTime.Parse(Console.ReadLine());

                    manager.UpdateTask(id, new TaskItem
                    {
                        Title = t,
                        Description = d,
                        Category = c,
                        DueDate = date
                    });

                    await manager.SaveTasksAsync();
                }
                else if (ch == 4)
                {
                    Console.Write("ID: ");
                    int id = int.Parse(Console.ReadLine());

                    manager.DeleteTask(id);
                    await manager.SaveTasksAsync();
                }
                else break;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}   