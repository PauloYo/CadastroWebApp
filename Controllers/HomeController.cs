using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CadastroWebApp.Models;
using Microsoft.Data.SqlClient;

namespace CadastroWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Database _database;

    public HomeController(ILogger<HomeController> logger, Database database)
    {
        _logger = logger;
        _database = database;
    }

    public IActionResult Index()
    {
        try 
        {
            using (SqlConnection conn = _database.GetConnection())
            {
                conn.Open();
                Console.WriteLine("Conexão com o banco de dados foi bem-sucedida!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao conectar: " + ex.Message);
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
