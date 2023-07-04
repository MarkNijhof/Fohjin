using Fohjin.DDD.Bus.Direct;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Bus
{
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
            Services.AddConfigurationServices()
                .AddTransient<ICommandHandler>(_ => _handler)
                .AddTransient<ICommandHandler>(_ => _secondHandler)
                ;

            var messageRouter = new MessageRouter(this.Provider, this.Logger<MessageRouter>());
            DoNotMock?.Add(typeof(IRouteMessages), messageRouter);
        }

        protected override void Given()
        {
            _command = new TestCommand(Guid.NewGuid());
            _otherCommand = new TestCommand(Guid.NewGuid());
        }

        protected override async Task WhenAsync()
        {
            if (SubjectUnderTest == null || _command == null || _otherCommand == null)
                return;

            SubjectUnderTest.Publish(new List<object> { _command, _otherCommand });
            await SubjectUnderTest.CommitAsync();
        }

        [TestMethod]
        public void Then_the_execute_method_on_the_first_returned_command_handler_is_invoked_with_the_first_provided_command()
        {
            _handler?.Ids[0].WillBe(_command?.Id);
        }

        [TestMethod]
        public void Then_the_execute_method_on_the_first_returned_command_handler_is_invoked_with_the_second_provided_command()
        {
            _handler?.Ids[1].WillBe(_otherCommand?.Id);
        }

        [TestMethod]
        public void Then_the_execute_method_on_the_second_returned_command_handler_is_invoked_with_the_first_provided_command()
        {
            _secondHandler?.Ids[0].WillBe(_command?.Id);
        }

        [TestMethod]
        public void Then_the_execute_method_on_the_second_returned_command_handler_is_invoked_with_the_second_provided_command()
        {
            _secondHandler?.Ids[1].WillBe(_otherCommand?.Id);
        }
    }
}