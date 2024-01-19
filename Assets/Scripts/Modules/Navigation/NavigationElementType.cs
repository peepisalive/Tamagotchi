namespace Modules.Navigation
{
    public enum NavigationElementType : short
    {
        None = 0,
        MainScreen = 1,
        MenuScreen = 2,
        ActivitiesScreen = 3,
        
        HealthActivities = 100,
        SatietyActivities = 101,
        HappinessActivities = 102,

        TakeToVetActivity = 200,
    }
}