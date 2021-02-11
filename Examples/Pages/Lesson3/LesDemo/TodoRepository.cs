using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace Examples.Pages.Lesson3.LesDemo
{
    public class TodoRepository
    {
        private IDbConnection Connect()
        {
            return new MySqlConnection(
                "Server=127.0.0.1;Port=3306;" +
                "Database=TodoDBExample;" +
                "Uid=root;Pwd=Test@1234!;"
            );
        }

        public IEnumerable<Todo> Get()
        {
            using var connect = Connect();
            var todos = connect
                .Query<Todo>("SELECT * FROM Todo");
            return todos;
        }

        public Todo Get(int todoId)
        {
            using var connect =  Connect();
            var todo = connect.QuerySingleOrDefault<Todo>(
                "SELECT * FROM Todo WHERE TodoId = @TodoId",
                
                new {TodoId = todoId});
            return todo;
        }

        public IEnumerable<Todo> GetWithSQLInjection(string filter)
        {
            using var connect = Connect();
            //Doe dit nooit zelf een querystring in elkaar zetten!
            //!!!SQL INJECTIE!!! Alle gegevens kunnen gestolen worden, etc :-(
            var todos = connect.Query<Todo>(
                "SELECT * FROM Todo WHERE Description = " + filter
            );

            return todos.ToList();
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

        public bool Delete(int todoId)
        {
            using var connect =  Connect();
            int numRowsEffected = connect.Execute("DELETE FROM Todo WHERE TodoId = @TodoId",
                new {TodoId = todoId});
            return numRowsEffected == 1;
        }
    }
}