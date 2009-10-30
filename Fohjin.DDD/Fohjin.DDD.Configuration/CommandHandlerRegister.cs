using System;
using System.Collections.Generic;
using System.Linq;
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

            foreach (var command in commands)
            {
                foreach (var commandHandler in commandHandlers[command])
                {
                    ForRequestedType(typeof(ICommandHandler<>).MakeGenericType(command))
                        .TheDefaultIsConcreteType(commandHandler)
                        .WithName(commandHandler.Name);
                }
            }
        }

        public static IDictionary<Type, IList<Type>> GetCommandHandlers()
        {
            IDictionary<Type, IList<Type>> commands = new Dictionary<Type, IList<Type>>();
            typeof(ICommandHandler<>)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                .ToList()
                .ForEach(x => AddItem(commands, x));
            return commands;
        }

        public static IEnumerable<Type> GetCommands()
        {
            return typeof(Command)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType == typeof(Command))
                .ToList();
        }

        private static void AddItem(IDictionary<Type, IList<Type>> dictionary, Type type)
        {
            var command = type.GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                .First()
                .GetGenericArguments()
                .First();

            if (!dictionary.ContainsKey(command))
                dictionary.Add(command, new List<Type>());

            dictionary[command].Add(type);
        }
    }
}