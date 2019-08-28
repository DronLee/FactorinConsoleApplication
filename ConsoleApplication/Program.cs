using Autofac;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("XUnitTestProject")]
namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = TypesRegistration.BuildContainer())
            {
                Console.Write("Укажите каталог, в котором нужно отслеживать новые файлы:");
                var processor = container.Resolve<IFilesProcessor>();
                processor.Run(Console.ReadLine());
                Console.WriteLine("Нажмите любую клавишу, чтобы остановить программу...");
                Console.ReadKey();
                processor.Stop();
            }
        }
    }
}