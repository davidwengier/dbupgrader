using System;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.Integration.InMemory
{
    internal class InMemoryHelper : IDestinationManagerHelper
    {
        private InMemoryDatabase _database = new InMemoryDatabase();

        public InMemoryHelper()
        {
        }

        public void AssertFieldExists(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }

        public void AssertFieldSizeEquals(int v1, string v2, string v3, string v4)
        {
            throw new NotImplementedException();
        }

        public void AssertTableExists(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
        }

        public UpgraderBuilder Init(UpgraderBuilder upgrade)
        {
            throw new NotImplementedException();
        }

        public void Serialize(IXunitSerializationInfo info)
        {
        }

        public IDisposable TestRun()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "In Memory Database";
        }
    }
}