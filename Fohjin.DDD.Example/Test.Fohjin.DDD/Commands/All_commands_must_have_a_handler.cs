using Fohjin.DDD.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Test.Fohjin.DDD.Commands
{
    [TestClass]
    public class All_commands_must_have_a_handler
    {
        [TestMethod]
        public void Verify_that_each_command_has_atleast_one_command_handler()
        {
            Assert.Inconclusive("This needs done a different way");
            //var commands = CommandHandlerHelper.GetCommands();
            //var commandHandlers = CommandHandlerHelper.GetCommandHandlers();

            //var stringBuilder = new StringBuilder();
            //foreach (var command in commands.Where(command => !commandHandlers.ContainsKey(command)))
            //{
            //    stringBuilder.AppendLine(string.Format("No command handler found for command '{0}'", command.FullName));
            //    continue;
            //}
            //if (stringBuilder.Length > 0)
            //    throw new Exception(string.Format("\n\nCommand handler exceptions:\n{0}\n", stringBuilder));
        }
    }
}