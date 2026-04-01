using Microsoft.AspNetCore.Mvc;
using SmartTask.Core.Models;
using SmartTask.Core.Services;

namespace SmartTask.API.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TasksController : ControllerBase
    {
        private readonly UserTaskManager manager;

        public TasksController(UserTaskManager m)
        {
            manager = m;
        }

        [HttpGet]
        public IActionResult Get() => Ok(manager.ListTasks());

        [HttpPost]
        public async Task<IActionResult> Post(TaskItem t)
        {
            manager.AddTask(t);
            await manager.SaveTasksAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TaskItem t)
        {
            manager.UpdateTask(id, t);
            await manager.SaveTasksAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            manager.DeleteTask(id);
            await manager.SaveTasksAsync();
            return Ok();
        }

        [HttpGet("category/{category}")]
        public IActionResult GetByCategory(string category)
        {
            var result = manager.ListByCategory(category);
            return Ok(result);
        }

        [HttpPost("risk")]
        public async Task<IActionResult> GetRiskScore([FromBody] TaskItem task)
        {
            var client = new HttpClient();

            var payload = new
            {
                category = 1,
                estimated = 2,
                priority = 1
            };

            var response = await client.PostAsJsonAsync("http://localhost:5000/ml/risk-score", payload);

            var result = await response.Content.ReadAsStringAsync();

            return Ok(result);
        }
    }
}