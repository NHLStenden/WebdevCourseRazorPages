using System;
using System.Net;
using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Exercises.Pages.Lesson0
{
    public class Exercises : CarterModule
    {
        public Exercises()
        {
            Get("/Lesson0/assignment0", Assignment0);

            Get("/Lesson0/assignment1", Assignment1);

            Get("/Lesson0/assignment2", Assignment2);

            Get("/Lesson0/assignment3", Assignment3);

            Get("/Lesson0/assignment4", Assignment4);

            Get("/Lesson0/assignment5", Assignment5Get);

            Post("/Lesson0/assignment5", Assignment5Post);

            //Get("/Lesson1/assignment6", Assignment6Get);
        }

        private Task Assignment0(HttpRequest request, HttpResponse response)
        {
            //Voor uitleg wat je moet maken, zie README.md
            throw new NotImplementedException();

            //return Task.CompletedTask;
        }

        private Task Assignment1(HttpRequest request, HttpResponse response)
        {
            throw new NotImplementedException();

            //return Task.CompletedTask;
        }

        private Task Assignment2(HttpRequest request, HttpResponse response)
        {
            throw new NotImplementedException();

            //return Task.CompletedTask;
        }

        private Task Assignment3(HttpRequest request, HttpResponse response)
        {
            throw new NotImplementedException();

            //return Task.CompletedTask;
        }

        private Task Assignment4(HttpRequest request, HttpResponse response)
        {
            throw new NotImplementedException();

            //return Task.CompletedTask;
        }

        private Task Assignment5Get(HttpRequest request, HttpResponse response)
        {
            throw new NotImplementedException();

            //return Task.CompletedTask;
        }

        private Task Assignment5Post(HttpRequest request, HttpResponse response)
        {
            throw new NotImplementedException();

            //return Task.CompletedTask;
        }

        private string FormAssignment5(string firstName = "", string lastName = "", string age = "",
            string firstNameError = "",
            string lastNameError = "", string ageError = "")
        {
            throw new NotImplementedException();

//             var form = $@"
//
//
//                 ";
//             return form;
        }

        // private Task Assignment6Get(HttpRequest request, HttpResponse response)
        // {
        //
        // }


    }
}
