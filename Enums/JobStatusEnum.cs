namespace GoWork.Enums
{
    public enum JobStatusEnum
    {
        Draft = 1,        // Job saved as draft, not published
        Published = 2,    // Actively accepting applications
        Paused = 3,       // Temporarily closed to applications
        Filled = 4,       // Position filled, no longer accepting
        Expired = 5       // Past application deadline
    }

}
