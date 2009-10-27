using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Bus.Implementation;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using StructureMap;

namespace Test.Fohjin.DDD.Bus
{
    public class When_a_single_command_gets_published_to_the_bus_containing_an_sinlge_command_handler : BaseTestFixture<DirectCommandBus>
    {
        private FirstTestCommandHandler _handler;
        private TestCommand _command;

        protected override void SetupDependencies()
        {
            _handler = new FirstTestCommandHandler();
            OnDependency<IContainer>()
                .Setup(x => x.GetAllInstances<ICommandHandler<TestCommand>>())
                .Returns(new List<ICommandHandler<TestCommand>> {_handler});
        }

        protected override void Given()
        {
            _command = new TestCommand(Guid.NewGuid());
        }

        protected override void When()
        {
            SubjectUnderTest.Publish(_command);
        }

        [Then]
        public void Then_the_execute_method_on_the_returned_command_handler_is_invoked_with_the_provided_command()
        {
            _handler.Ids.First().WillBe(_command.Id);
        }
    }

    public class When_a_single_command_gets_published_to_the_bus_containing_multiple_command_handlers : BaseTestFixture<DirectCommandBus>
    {
        private FirstTestCommandHandler _handler;
        private SecondTestCommandHandler _secondHandler;
        private TestCommand _command;

        protected override void SetupDependencies()
        {
            _handler = new FirstTestCommandHandler();
            _secondHandler = new SecondTestCommandHandler();
            OnDependency<IContainer>()
                .Setup(x => x.GetAllInstances<ICommandHandler<TestCommand>>())
                .Returns(new List<ICommandHandler<TestCommand>> { _handler, _secondHandler });
        }

        protected override void Given()
        {
            _command = new TestCommand(Guid.NewGuid());
        }

        protected override void When()
        {
            SubjectUnderTest.PublishMultiple(new List<IMessage> { _command });
        }

        [Then]
        public void Then_the_execute_method_on_the_first_returned_command_handler_is_invoked_with_the_first_provided_command()
        {
            _handler.Ids.First().WillBe(_command.Id);
        }

        [Then]
        public void Then_the_execute_method_on_the_second_returned_command_handler_is_invoked_with_the_first_provided_command()
        {
            _secondHandler.Ids.First().WillBe(_command.Id);
        }
    }

    public class When_multiple_commands_gets_published_to_the_bus_containing_multiple_command_handlers : BaseTestFixture<DirectCommandBus>
    {
        private FirstTestCommandHandler _handler;
        private SecondTestCommandHandler _otherHandler;
        private TestCommand _command;
        private TestCommand _otherCommand;

        protected override void SetupDependencies()
        {
            _handler = new FirstTestCommandHandler();
            _otherHandler = new SecondTestCommandHandler();
            OnDependency<IContainer>()
                .Setup(x => x.GetAllInstances<ICommandHandler<TestCommand>>())
                .Returns(new List<ICommandHandler<TestCommand>> {_handler, _otherHandler});
        }

        protected override void Given()
        {
            _command = new TestCommand(Guid.NewGuid());
            _otherCommand = new TestCommand(Guid.NewGuid());
        }

        protected override void When()
        {
            SubjectUnderTest.PublishMultiple(new List<IMessage>{ _command, _otherCommand });
        }

        [Then]
        public void Then_the_execute_method_on_the_first_returned_command_handler_is_invoked_with_the_first_provided_command()
        {
            _handler.Ids[0].WillBe(_command.Id);
        }

        [Then]
        public void Then_the_execute_method_on_the_first_returned_command_handler_is_invoked_with_the_second_provided_command()
        {
            _handler.Ids[1].WillBe(_otherCommand.Id);
        }

        [Then]
        public void Then_the_execute_method_on_the_second_returned_command_handler_is_invoked_with_the_first_provided_command()
        {
            _otherHandler.Ids[0].WillBe(_command.Id);
        }

        [Then]
        public void Then_the_execute_method_on_the_second_returned_command_handler_is_invoked_with_the_second_provided_command()
        {
            _otherHandler.Ids[1].WillBe(_otherCommand.Id);
        }
    }

    public class TestCommand : Command
    {
        public TestCommand(Guid id) : base(id)
        {
        }
    }

    public class FirstTestCommandHandler : ICommandHandler<TestCommand>
    {
        public List<Guid> Ids;

        public FirstTestCommandHandler()
        {
            Ids = new List<Guid>();
        }

        public void Execute(TestCommand command)
        {
            Ids.Add(command.Id);
        }
    }

    public class SecondTestCommandHandler : ICommandHandler<TestCommand>
    {
        public List<Guid> Ids;

        public SecondTestCommandHandler()
        {
            Ids = new List<Guid>();
        }

        public void Execute(TestCommand command)
        {
            Ids.Add(command.Id);
        }
    }
}