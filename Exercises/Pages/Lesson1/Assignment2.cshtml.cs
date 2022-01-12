using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercises.Pages.Lesson1
{


    public class Exercise2 : PageModel
    {
        public enum Direction
        {
            Left, Right, Forward, Backward
        }

        public void OnGet([FromQuery] List<Direction> directions)
        {
            Directions = directions;
        }

        public List<Direction> Directions { get; set; }
    }
}
