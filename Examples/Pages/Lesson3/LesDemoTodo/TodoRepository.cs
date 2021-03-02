using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace Examples.Pages.Lesson3.LesDemoTodo
{
    public class TodoRepository
    {
        public IDbConnection Connect()
        {
            string connectionString = @"Server=127.0.0.1;
                                        Database=TodoLesDemo;
                                        Uid=root;
                                        Pwd=Test@1234!;";

            return new MySqlConnection(connectionString);
        }

        public List<Todo> Get()
        {
            using var connection = Connect();
            List<Todo> todos = connection.Query<Todo>("SELECT * FROM Todo").ToList();
            return todos;
        }

        public bool Delete(int todoId)
        {
            using var connection = Connect();
            int numRowEffected = connection.Execute("DELETE FROM Todo WHERE TodoId = @TodoId",
                new {TodoId = todoId}
            );
            return numRowEffected == 1;
        }
        
        	
        public List<Todo> GetWithSQLInjection(string filter)
        {
            var todos = new List<Todo>();
            try
            {
                //De code is anders, omdat SQL injectie anders alsnog wordt tegengehouden
                using (var connection = Connect())
                {
                    //Doe dit nooit zelf een querystring in elkaar zetten!
                    //!!!SQL INJECTIE!!! Alle gegevens kunnen gestolen worden, etc :-(
                    var sql = @"SELECT TodoId, Description, Done 
                        FROM Todo WHERE Description LIKE '%" + filter + "%'";
                    
                    //todos = connection.Query<Todo>(sql).ToList();
                    
                    var reader = connection.ExecuteReader(sql);
                    while (reader.Read())
                    {
                        todos.Add(new Todo()
                        {
                            TodoId = reader.GetInt32(0),
                            Description = reader.GetString(1),
                            Done = reader.GetBoolean(2)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
         
            return todos;
        }


        public Todo Add(Todo todo)
        {
            using var connect =  Connect();
            int numRowsEffected = connect.Execute(
                "INSERT INTO Todo (Description, Done) VALUES (@Description, @Done)"
                ,new {Description = todo.Description, Done = todo.Done});

            if (numRowsEffected == 1)
            {
                var newTodo = connect.QuerySingle<Todo>(
                    "SELECT * FROM Todo WHERE TodoId = LAST_INSERT_ID()");
                return newTodo;    
            }

            return null;
        }
    }
}