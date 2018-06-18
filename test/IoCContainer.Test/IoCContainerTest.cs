using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoCContainer.Test
{
    [TestClass]
    public class IoCContainerTest
    {
        [TestMethod]
        public void CreateAnInstanceWithParams()
        {
            var Container = new Container();
            var subject = (Logger)Container.GetInstance(typeof(Logger));
            Assert.IsNotNull(subject);
        }

        [TestMethod]
        public void Container_RegisterGeneric()
        {
            var Container = new Container();
            Container.Register<ILogger, Logger>();
            Container.Register(typeof(IService<>), typeof(EfService<>));
            ///  Container.Register<EfService<Product>, ProductService>();
            var subject = Container.GetInstance<ProductService>();
            Assert.IsNotNull(subject);
        }

        [TestMethod]
        public void Container_RegisterInterface()
        {
            var Container = new Container();
            Container.Register<ILogger, Logger>();
            var subject = Container.GetInstance<ILogger>();
            Assert.IsNotNull(subject);
        }

        [TestMethod]
        public void Container_RegisterSingleton()
        {
            var Container = new Container();
            
            Container.Register<ILogger>(new Logger());
            var subject = Container.GetInstance<ILogger>();
            Assert.IsNotNull(subject);
        }
    }
}