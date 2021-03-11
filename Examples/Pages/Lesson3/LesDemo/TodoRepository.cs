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
                "Uid=root;Pwd=Test@1234!;" +
                "Allow User Variables=True;"    //anders kan je geen SQL Variabelen (@newPrio) gebruiken, b.v.
                                                //SET @newPrio = (SELECT Max(Prio) FROM Kado) + 1;
                                                //INSERT INTO Kado (Description, Prio) VALUES ('Item 1', @newPrio);
            );
        }

        public IEnumerable<Todo> Get()
        {
            using var connection = Connect();
            var todos = connection
                .Query<Todo>("SELECT * FROM Todo");
            return todos;
        }

        public Todo Get(int todoId)
        {
            using var connection = Connect();
            var todo = connection.QuerySingleOrDefault<Todo>(
                "SELECT * FROM Todo WHERE TodoId = @TodoId",
                
                new {TodoId = todoId});
            return todo;
        }

        public IEnumerable<Todo> GetWithSQLInjection(string filter)
        {
            using var connection = Connect();
            //Doe dit nooit zelf een querystring in elkaar zetten!
            //!!!SQL INJECTIE!!! Alle gegevens kunnen gestolen worden, etc :-(
            var todos = connection.Query<Todo>(
                "SELECT * FROM Todo WHERE Description = " + filter
            );

            return todos.ToList();
        }

        public bool AddSimple(Todo todo)
        {
            using var connection = Connect();
            int numRowEffected = connection.Execute(
                @"INSERT INTO Todo (Description, Done) VALUES (@Description, @Done)"
                , todo);
            return numRowEffected == 1;
        }
        
        
        public Todo AddPlusSelect(Todo todo)
        {
            using var connection = Connect();
            Todo addedTodo = connection.QuerySingle<Todo>(
                @"INSERT INTO Todo (Description, Done) VALUES (@Description, @Done); 
                      SELECT * FROM Todo WHERE TodoId = LAST_INSERT_ID()"
                ,todo);
            return addedTodo;    
        }

        public Todo Update(Todo todo)
        {
            using var connection = Connect();
            Todo updatedTodo = connection.QuerySingle<Todo>(@"
                UPDATE Todo SET Description = @Description, Done = @Done
                WHERE TodoId = @TodoId; 
                SELECT * FROM Todo WHERE TodoId = @TodoId", 
                todo
            );

            return updatedTodo;
        }
        
        //Ik had verwacht dat dit zou werken! Maar helaas niet
        //SELECT * FROM Todo WHERE TodoId = LAST_INSERT_ID()
        //uit de officiele documentatie blijkt LAST_INSERT_ID() ook alleen te werken met INSERTS
        //https://dev.mysql.com/doc/refman/8.0/en/information-functions.html#function_last-insert-id

        public bool Delete(int todoId)
        {
            using var connect =  Connect();
            int numRowsEffected = connect.Execute("DELETE FROM Todo WHERE TodoId = @TodoId",
                new {TodoId = todoId});
            return numRowsEffected == 1;
        }
    }
}