using DbUpgrader.XunitHelpers;
using Xunit.Abstractions;

namespace DbUpgrader
{
    public static class XUnitExtensions
    {
        public static UpgraderBuilder LogToXunit(this UpgraderBuilder builder, ITestOutputHelper output)
        {
            builder.Logger = new XunitLogger(output);
            return builder;
        }
    }
}
