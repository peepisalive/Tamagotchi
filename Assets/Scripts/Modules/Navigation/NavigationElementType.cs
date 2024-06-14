namespace Modules.Navigation
{
    public enum NavigationElementType : short
    {
        None = 0,
        MainScreen = 1,
        MenuScreen = 2,
        ActivitiesScreen = 3,
        JobScreen = 4,
        PetActionsScreen = 5,
        LeaderboardScreen = 6,
        
        HappinessActivities = 100,
        SatietyActivities = 101,
        HygieneActivities = 102,
        FatigueActivities = 103,
        HealthActivities = 104,

        TakeToVetActivity = 200,
        SpaTreatmentsActivity = 201,
        TrainingActivities = 202,
        WalkActivity = 203,
        PlayActivity = 205,
        WashActivity = 206,
        FeedActivity = 207,
        DrinkActivity = 208,
        CookActivity = 209,
        VisitCosmetologistActivity = 210,

        Accessories = 211,

        StretchingActivity = 300,
        YogaActivity = 301,
        ExerciseActivity = 302,
        CleanTheRoomActivity = 303,
        VentilateTheRoomActivity = 304,

        SoundProvider = 1000,
        HapticProvider = 1001,
        LocalizationProvider = 1002,
        ContactMe = 1003,
    }
}