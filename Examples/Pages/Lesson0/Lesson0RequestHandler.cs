// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.RegularExpressions;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using WebdevCourseCarter.LessonExamples.Lesson1;
//
// namespace Examples.Pages.Lesson0
// {
//     public class Lesson0RequestHandler
//     {
//         private class RouteRequest
//         {
//             public string Method { get; }
//             public string Path { get; }
//
//             public Func<HttpRequest, HttpResponse, Task> Action { get; set; }
//
//             public List<string> RouteTemplateSegements { get; set; }
//
//             public RouteRequest(string path, Func<HttpRequest, HttpResponse, Task> action, string method = "GET")
//             {
//                 Path = new Regex(@"([^/]+)").Match(path).Groups[0].Value;
//
//                 Regex r = new Regex(@"{(.+?)}");
//                 MatchCollection mc = r.Matches(path);
//
//                 foreach (Match match in mc)
//                 {
//                     if (RouteTemplateSegements == null)
//                     {
//                         RouteTemplateSegements = new List<string>();
//                     }
//
//                     RouteTemplateSegements.Add(match.Groups[1].Value);
//                 }
//
//                 Action = action;
//                 Method = method;
//             }
//
//             protected bool Equals(RouteRequest other)
//             {
//                 return string.Equals(Method, other.Method, StringComparison.OrdinalIgnoreCase) &&
//                        string.Equals(Path, other.Path, StringComparison.OrdinalIgnoreCase);
//             }
//
//             public override bool Equals(object obj)
//             {
//                 if (ReferenceEquals(null, obj)) return false;
//                 if (ReferenceEquals(this, obj)) return true;
//                 if (obj.GetType() != this.GetType()) return false;
//                 return Equals((RouteRequest) obj);
//             }
//
//             public override int GetHashCode()
//             {
//                 var hashCode = new HashCode();
//                 hashCode.Add(Method, StringComparer.OrdinalIgnoreCase);
//                 hashCode.Add(Path, StringComparer.OrdinalIgnoreCase);
//                 int result = hashCode.ToHashCode();
//                 Console.WriteLine($"Path: {Path} HashCode: {result}");
//                 return result;
//             }
//         }
//
//         private static List<RouteRequest> RouteRequests { get; set; } =
//             new List<RouteRequest>()
//             {
//                 //Get Request
//                 new RouteRequest("Get", GetRequest.Get),
//                 new RouteRequest("GetWithOneParameter", GetRequest.GetWithOneParameter),
//
//                 //Get Request with parameters
//                 new RouteRequest("GetWithMultipleParameters",GetRequest.GetWithMultipleParameters),
//
//                 //Routing (can also be used in combination with POST)
//                 new RouteRequest("OneRouteValue/{id}", RouteValues.WithOneRouteValue),
//                 new RouteRequest("TwoRouteValue/{category}/{subcategory}", RouteValues.WithTwoRouteValue),
//
//
//                 //POST Request
//                 new RouteRequest("PostRequest", PostRequest.Get),
//                 new RouteRequest( "PostRequest", PostRequest.Post, "POST"),
//
//                 new RouteRequest("PostRequestMultipleButtonsOneForm", PostRequestMultipleButtonsOneForm.Get),
//                 new RouteRequest("PostRequestMultipleButtonsOneForm", PostRequestMultipleButtonsOneForm.Post, "POST"),
//
//                 new RouteRequest("PostRequestMultipleButtonsMultipleForms", PostRequestMultipleButtonsMultipleForms.Get),
//                 new RouteRequest( "PostRequestMultipleButtonsMultipleForms", PostRequestMultipleButtonsMultipleForms.Post, "POST"),
//
//                 //Prevent Hackers
//                 new RouteRequest("XSSHacked", XSSHacked.Get),
//                 new RouteRequest("XSSPrevent", XSSPrevent.Get),
//
//                 //Input validation
//                 new RouteRequest("SimpleInputValidation", SimpleInputValidation.GetSimpleInputValidation),
//
//                 //Redirect
//                 new RouteRequest("Redirect", Redirect.Get)
//             };
//
//         public static Task HandleRequest(HttpContext context, string prefix = "/Lesson0/")
//         {
//             string requestPath = context.Request.Path.Value.ToLower();
//             requestPath = requestPath.Replace(prefix.ToLower(), string.Empty);
//
//             string method = context.Request.Method;
//
//             var requestParts = requestPath.Split("/");
//             string firstPart = requestParts.First().ToLower();
//
//             var routeRequests = RouteRequests.Where(x =>
//                         requestParts[0].ToLower() == x.Path.ToLower()
//                         && string.Compare(x.Method, method, StringComparison.OrdinalIgnoreCase) == 0).ToList();
//
//             if (routeRequests.Count() == 1)
//             {
//                 var route = routeRequests[0];
//
//                 try
//                 {
//                     for (int i = 1; i < requestParts.Length; i++)
//                     {
//                         context.Request.RouteValues.Add(route.RouteTemplateSegements[i-1], requestParts[i]);
//                     }
//                 }
//                 catch (ArgumentOutOfRangeException e)
//                 {
//                     return Task.FromException(new NotSupportedException("Invalid number of route segements"));
//                 }
//
//                 return route.Action(context.Request, context.Response);
//             }
//             else if (routeRequests.Count() > 1)
//             {
//                 //Todo create error message
//                 return Task.FromException(
//                     new NotSupportedException(
//                 $@"The following routes are ambiguous: {
//                             string.Join(",", routeRequests.Select(x => x.Path))
//                         }")
//                 );
//             }
//             else
//             {
//                 return Task.FromException(new NotImplementedException("Page Not Found 404"));
//             }
//         }
//     }
//
//
// }
