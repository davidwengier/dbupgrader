using System;
using DbUpgrader.Logging;

namespace DbUpgrader
{
    public class UpgraderBuilder
    {
        public ILogger Logger { get; set; }
        public ISourceManager SourceManager { get; set; }
        public IDestinationManager DestinationManager { get; set; }

        public Upgrader Build()
        {
            return new Upgrader()
            {
                Logger = this.Logger,
                SourceManager = this.SourceManager,
                DestinationManager = this.DestinationManager
            };
        }
    }
}