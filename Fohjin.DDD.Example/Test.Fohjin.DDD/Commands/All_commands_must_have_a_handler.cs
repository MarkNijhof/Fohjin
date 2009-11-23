using System;
using System.Linq;
using System.Text;
using Fohjin.DDD.Configuration;
using NUnit.Framework;

namespace Test.Fohjin.DDD.Commands
{
    [TestFixture]
    public class All_commands_must_have_a_handler
    {
        [Test]
        public void Verify_that_each_command_has_atleast_one_command_handler()
        {
            var commands = CommandHandlerHelper.GetCommands();
            var commandHandlers = CommandHandlerHelper.GetCommandHandlers();

            var stringBuilder = new StringBuilder();
            foreach (var command in commands.Where(command => !commandHandlers.ContainsKey(command)))
            {
                stringBuilder.AppendLine(string.Format("No command handler found for command '{0}'", command.FullName));
                continue;
            }
            if (stringBuilder.Length > 0)
                throw new Exception(string.Format("\n\nCommand handler exceptions:\n{0}\n", stringBuilder));
        }
    }
}