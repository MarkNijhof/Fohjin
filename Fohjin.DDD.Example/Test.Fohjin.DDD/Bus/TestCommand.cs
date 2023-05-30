﻿using Fohjin.DDD.Commands;
using System.Text.Json.Serialization;

namespace Test.Fohjin.DDD.Bus
{
    public class TestCommand : CommandBase
    {
        [JsonConstructor]
        public TestCommand() : base() { }
        public TestCommand(Guid id) : base(id)
        {
        }
    }
}