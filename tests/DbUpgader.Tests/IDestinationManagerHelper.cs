using System;
using Xunit.Abstractions;

namespace DbUpgrader.Tests
{
    public interface IDestinationManagerHelper : IXunitSerializable
    {
        UpgraderBuilder Init(UpgraderBuilder upgrade);

        void AssertTableExists(string v1, string v2);

        void AssertFieldExists(string v1, string v2, string v3);

        void AssertFieldSizeEquals(int v1, string v2, string v3, string v4);

        IDisposable TestRun();
    }
}