using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using StructureMap.Configuration.DSL;

namespace Fohjin.DDD.Configuration
{
    public class CommandHandlerRegister : Registry
    {
        public CommandHandlerRegister()
        {
            var commands = GetCommands();
            var commandHandlers = GetCommandHandlers();

            var stringBuilder = new StringBuilder();
            foreach (var command in commands)
            {
                if (!commandHandlers.ContainsKey(command))
                {
                    stringBuilder.AppendLine(string.Format("No command handler found for command '{0}'", command.FullName));
                    continue;
                }

                ForRequestedType(typeof(ICommandHandler<>).MakeGenericType(command))
                    .TheDefaultIsConcreteType(commandHandlers[command]);
            }
            if (stringBuilder.Length > 0)
                throw new Exception(string.Format("\n\nCommand handler exceptions:\n{0}\n", stringBuilder));
        }

        private static IDictionary<Type, Type> GetCommandHandlers()
        {
            IDictionary<Type, Type> commands = new Dictionary<Type, Type>();
            typeof(ICommandHandler<>)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                .ToList()
                .ForEach(x => AddItem(commands, x));
            return commands;
        }

        private static void AddItem(ICollection<KeyValuePair<Type, Type>> dictionary, Type type)
        {
            dictionary
                .Add(new KeyValuePair<Type, Type>(
                    type.GetInterfaces()
                        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (ICommandHandler<>))
                        .First()
                        .GetGenericArguments()
                        .First(), 
                    type));
        }

        private static List<Type> GetCommands()
        {
            return typeof(Command)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType == typeof(Command))
                .ToList();
        }
    }
}