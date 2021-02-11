using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class SessionStoreObject : PageModel
    {
        public ActionCounter ActionCounter { get; set; }
        public void OnGet(string action = "")
        {
            if (action == "removeSession")
            {
                HttpContext.Session.Remove("actionCounter");
            }

            ActionCounter actionCounter = HttpContext.Session.Get<ActionCounter>("actionCounter");
            if (actionCounter == null)
            {
                actionCounter = new ActionCounter();
            }

            if (action != "" && action != "removeSession")
            {
                actionCounter.AddAction(action);
            }

            HttpContext.Session.Set<ActionCounter>("actionCounter", actionCounter);

            ActionCounter = actionCounter;
        }
    }

    public class ActionCounter
    {
        public List<string> Actions { get; set; } = new List<string>();

        public void AddAction(string action)
        {
            Actions.Add(action.ToLower());
        }

        public int CalculateResult()
        {
            int count = 0;

            foreach (var action in Actions)
            {
                if (action == "increment")
                {
                    count++;
                }
                else if (action == "decrement")
                {
                    count--;
                } else if (action == "reset")
                {
                    count = 0;
                }
            }

            return count;
        }
    }


}
