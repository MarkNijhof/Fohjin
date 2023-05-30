using Fohjin.DDD.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Commands
{
    [TestClass]
    public class All_commands_must_be_Serializable
    {
        [TestMethod]
        public void All_commands_will_have_the_Serializable_attribute_assigned()
        {
            Assert.Inconclusive("Not using binary formatter so this no longer applies");
            //var domainEventTypes = typeof(Command).Assembly.GetExportedTypes().Where(x => x.BaseType == typeof(Command)).ToList();
            //foreach (var commandType in domainEventTypes)
            //{
            //    if (commandType.IsSerializable)
            //        continue;

            //    throw new Exception(string.Format("Command '{0}' is not Serializable", commandType.FullName));
            //}
        }
    }
}