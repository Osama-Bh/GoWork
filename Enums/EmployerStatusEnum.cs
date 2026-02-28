namespace GoWork.Enums
{
    public enum EmployerStatusEnum
    {
        PendingApproval = 1,   // Registered but waiting for admin verification
        Active = 2,     // Approved; can post jobs and hire
        Suspended = 3,   // Temporarily blocked (policy / payment issues)
        Inactive = 4,    // Voluntarily deactivated or no longer using platform
        Rejected = 5,
        Blocked = 6,
    }

}
