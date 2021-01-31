namespace Exercises.Pages.Lesson3.Models
{
    public class OrderLine
    {
        public int TableNr { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public Product Product { get; set; }

        //Het hoeft niet, maar je mag de "Foreign Key" natuurlijk toevoegen aan je model, indien je dit nodig bent!
        //Let op de ? achter de int (Nullable), dit geeft aan dat het CostObjectId leeg kan zijn en indirect dus
        //ook dat property CostObject null kan zijn! Zie ook het UML diagram (let op 0..1)
        //blijkbaar was deze proprety niet nodig (ik gebruik het niet in de C# code)
        //public int? CostObjectId { get; set; }

        public CostObject CostObject { get; set; }
    }
}
