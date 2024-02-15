namespace Modules.Navigation
{
    public enum NavigationElementType : short
    {
        None = 0,
        MainScreen = 1,
        MenuScreen = 2,
        ActivitiesScreen = 3,
        
        HappinessActivities = 100,
        SatietyActivities = 101,
        HygieneActivities = 102,
        FatigueActivities = 103,
        HealthActivities = 104,

        TakeToVetActivity = 200,
        SpaTreatmentsActivity = 201,
        TrainingActivity = 202,
        WalkActivity = 203,
        YogaActivity = 204,
        BallGameActivity = 205,
        WashActivity = 206,
        FeedActivity = 207,
        DrinkActivity = 208,
    }
}