using System.ComponentModel.DataAnnotations;

namespace Examples.Pages.Lesson3.LesDemo
{
    // CREATE DATABASE IF NOT EXISTS TodoDBExample;
    // USE TodoDBExample;
    // CREATE TABLE Todo (
    //      TodoId INT PRIMARY KEY AUTO_INCREMENT,
    //      Description VARCHAR(20) NOT NULL,
    //      Done BOOLEAN DEFAULT FALSE
    // );

    // INSERT INTO Todo (Description, Done) VALUES ('Item 1', FALSE);
    // INSERT INTO Todo (Description, Done) VALUES ('Item 2', TRUE);
    //SELECT * FROM Todo;

    public class Todo
    {
        public int TodoId { get; set; }
        [Required(), MinLength(2), MaxLength(20)]
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}
