using System;
using LibraryManagement.Interfaces;

namespace LibraryManagement.Services
{
    public class HtmlPrinter : IPrinter
    {
        public void PrintPage(string page)
        {
            Console.WriteLine($"<div class='single-page'>{page}</div>");
        }
    }
}
