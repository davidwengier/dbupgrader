namespace DbUpgrader
{
    public static class DbUpgrader
    {
        public static UpgraderBuilder Upgrade
        {
            get { return new UpgraderBuilder(); }
        }
    }
}
