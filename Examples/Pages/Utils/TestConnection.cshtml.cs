using System;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace Examples.Pages.Utils
{
    public class TestConnection : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string ConnectionString { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                MySqlConnection connection = null;
                try
                {
                    connection = new MySqlConnection(ConnectionString);
                    connection.Open();
                    connection.QuerySingle<int>("SELECT 1");
                    Message = "Succesvol verbonden";
                }
                catch (Exception exception)
                {
                    Message = exception.Message;
                }
                finally
                {
                    connection?.Close();
                }
            }
        }
    }
}
