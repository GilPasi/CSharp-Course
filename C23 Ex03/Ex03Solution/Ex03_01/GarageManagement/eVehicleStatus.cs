namespace Ex03
{
    public enum eVehicleStatus
    {
        CurrentlyTreated,
        Repaired,
        Redeemed,
    }
    
    static class eVehicleStatusExtention
    {
        public static string ToCustomString(this Enum i_Status)
        {
            return i_Status.ToString().Replace("_", " ");
        }
    }
}
