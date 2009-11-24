using Fohjin.EventStore;

namespace Test.Fohjin.EventStore
{
    public class When_requesting_a_new_object_from_the_repository_that_has_no_private_apply_method_declared : BaseTestFixture
    {
        protected override void When()
        {
            new DomainRepository(new AggregateRootFactory()).CreateNew<TestObjectWithoutPrivateApplyMethod>();
        }

        [Then]
        public void The_an_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<PublicVirtualApplyMethodException>();
        }

        [Then]
        public void The_exception_message_will_be()
        {
            CaughtException.Message.WillBe(string.Format("Object '{0}' does not have a private 'void Apply(object @event);' method", typeof(TestObjectWithoutPrivateApplyMethod).FullName));
        }
    }

    public class TestObjectWithoutPrivateApplyMethod
    {
        public void Apply(object @event)
        {
        }
    }

    public class When_requesting_a_new_object_from_the_repository_that_has_no_private_void_apply_method_declared : BaseTestFixture
    {
        protected override void When()
        {
            new DomainRepository(new AggregateRootFactory()).CreateNew<TestObjectWithoutPrivateVoidApplyMethod>();
        }

        [Then]
        public void The_an_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<PublicVirtualApplyMethodException>();
        }

        [Then]
        public void The_exception_message_will_be()
        {
            CaughtException.Message.WillBe(string.Format("The Apply method in Object '{0}' does not return 'void'", typeof(TestObjectWithoutPrivateVoidApplyMethod).FullName));
        }
    }

    public class TestObjectWithoutPrivateVoidApplyMethod
    {
        private string Apply(object @event)
        {
            return string.Empty;
        }
    }

    public class When_requesting_a_new_object_from_the_repository_that_has_a_private_void_apply_method_with_the_wrong_signature_declared : BaseTestFixture
    {
        protected override void When()
        {
            new DomainRepository(new AggregateRootFactory()).CreateNew<TestObjectWithWrongSignatureApplyMethod>();
        }

        [Then]
        public void The_an_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<PublicVirtualApplyMethodException>();
        }

        [Then]
        public void The_exception_message_will_be()
        {
            CaughtException.Message.WillBe(string.Format("The Apply method in Object '{0}' does not have the correct signature 'Apply(object @event)'", typeof(TestObjectWithWrongSignatureApplyMethod).FullName));
        }
    }

    public class TestObjectWithWrongSignatureApplyMethod
    {
        private void Apply(string @event)
        {
        }
    }
}