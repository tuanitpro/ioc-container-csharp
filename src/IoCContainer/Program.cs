using System;

namespace IoCContainer
{
    internal class Program
    {
        private static void Main()
        {
            var Container = new Container();
            Container.Register<ILogger, Logger>();
            Container.Register(typeof(IService<>), typeof(EfService<>));
            /// Container.Register<IService<Product>, ProductService>();
            var productService = Container.GetInstance<ProductService>();
            Console.WriteLine(productService.Add(new Product()));

            var logger = Container.GetInstance<ILogger>();
            Console.WriteLine(logger.Write("tuanitpro"));

            Console.ReadKey();
        }
    }
}