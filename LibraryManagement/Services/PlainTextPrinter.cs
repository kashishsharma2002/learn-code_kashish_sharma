using System;
using LibraryManagement.Interfaces;

namespace LibraryManagement.Services
{
    public class PlainTextPrinter : IPrinter
    {
        public void PrintPage(string page)
        {
            Console.WriteLine(page);
        }
    }
}
