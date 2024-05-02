using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TesteSPA.API.DTO;
using TesteSPA.API.Model;

namespace TesteSPA.RazorPages.Pages.Task.Create
{
    public class Index : PageModel
    {

        public List<TaskDTO> taskModels{ get; set; } = new();

        public void OnGet()
        {
        }

       public async Task<IActionResult> OnPostAsync(string title, string description, string dateTime, string time)
        {
            var httpClient = new HttpClient();
            var url = "http://localhost:5031/api/Task";
            var task = new TaskDTO { Title = title, Description = description, dateTime = DateTime.Parse(dateTime), time = time };

            var jsonContent = JsonConvert.SerializeObject(task);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, contentString);
            if (response.IsSuccessStatusCode)
            {
                ViewData["SuccessMessage"] = "Tarefa criada com sucesso!";
            }
            else
            {
                ViewData["ErrorMessage"] = "Erro ao criar a tarefa.";
            }

            return Page();
        }
    }
}