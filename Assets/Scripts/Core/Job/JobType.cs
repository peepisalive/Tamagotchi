namespace Core.Job
{
    public enum JobType : sbyte
    {
        FullTime = 0,
        PartTime = 1
    }

    
    public enum PartTimeJobType : sbyte
    {
        VideoGameTester = 0,
        PoolCleaner = 1,
        Secretary = 2,
    }


    public enum FullTimeJobType : sbyte
    {
        Loader = 0,
        ITSupport = 1,
        Electrician = 2,
    }
}