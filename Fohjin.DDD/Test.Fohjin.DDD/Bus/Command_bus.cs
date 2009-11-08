using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;

namespace Test.Fohjin.DDD.Bus
{
    public class When_a_single_command_gets_published_to_the_bus_containing_an_sinlge_command_handler : BaseTestFixture<DirectBus>
    {
        private FirstTestCommandHandler _handler;
        private TestCommand _command;

        protected override void SetupDependencies()
        {
            _handler = new FirstTestCommandHandler();
            var messageRouter = new MessageRouter();
            messageRouter.Register<TestCommand>(x => _handler.Execute(x));
            DoNotMock.Add(typeof (IRouteMessages), messageRouter);
        }

        protected override void Given()
        {
            _command = new TestCommand(Guid.NewGuid());
        }

        protected override void When()
        {
            SubjectUnderTest.Publish(_command);
            SubjectUnderTest.Commit();
        }

        [Then]
        public void Then_the_execute_method_on_the_returned_command_handler_is_invoked_with_the_provided_command()
        {
            _handler.Ids.First().WillBe(_command.Id);
        }
    }

    public class When_a_single_command_gets_published_to_the_bus_containing_multiple_command_handlers : BaseTestFixture<DirectBus>
    {
        private FirstTestCommandHandler _handler;
        private SecondTestCommandHandler _secondHandler;
        private TestCommand _command;

        protected override void SetupDependencies()
        {
            _handler = new FirstTestCommandHandler();
            _secondHandler = new SecondTestCommandHandler();
            var messageRouter = new MessageRouter();
            messageRouter.Register<TestCommand>(x => _handler.Execute(x));
            messageRouter.Register<TestCommand>(x => _secondHandler.Execute(x));
            DoNotMock.Add(typeof(IRouteMessages), messageRouter);
        }

        protected override void Given()
        {
            _command = new TestCommand(Guid.NewGuid());
        }

        protected override void When()
        {
            SubjectUnderTest.Publish(new List<object> { _command });
            SubjectUnderTest.Commit();
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

    public class When_multiple_commands_gets_published_to_the_bus_containing_multiple_command_handlers : BaseTestFixture<DirectBus>
    {
        private FirstTestCommandHandler _handler;
        private SecondTestCommandHandler _secondHandler;
        private TestCommand _command;
        private TestCommand _otherCommand;

        protected override void SetupDependencies()
        {
            _handler = new FirstTestCommandHandler();
            _secondHandler = new SecondTestCommandHandler();
            var messageRouter = new MessageRouter();
            messageRouter.Register<TestCommand>(x => _handler.Execute(x));
            messageRouter.Register<TestCommand>(x => _secondHandler.Execute(x));
            DoNotMock.Add(typeof(IRouteMessages), messageRouter);
        }

        protected override void Given()
        {
            _command = new TestCommand(Guid.NewGuid());
            _otherCommand = new TestCommand(Guid.NewGuid());
        }

        protected override void When()
        {
            SubjectUnderTest.Publish(new List<object>{ _command, _otherCommand });
            SubjectUnderTest.Commit();
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
            _secondHandler.Ids[0].WillBe(_command.Id);
        }

        [Then]
        public void Then_the_execute_method_on_the_second_returned_command_handler_is_invoked_with_the_second_provided_command()
        {
            _secondHandler.Ids[1].WillBe(_otherCommand.Id);
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

        public void Execute(TestCommand compensatingCommand)
        {
            Ids.Add(compensatingCommand.Id);
        }
    }

    public class SecondTestCommandHandler : ICommandHandler<TestCommand>
    {
        public List<Guid> Ids;

        public SecondTestCommandHandler()
        {
            Ids = new List<Guid>();
        }

        public void Execute(TestCommand compensatingCommand)
        {
            Ids.Add(compensatingCommand.Id);
        }
    }
}