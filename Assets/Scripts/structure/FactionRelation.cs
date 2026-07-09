public static class FactionRelation
{
    public static bool IsHostile(Faction a, Faction b)
    {
        if (a == b) return false;

        return (a, b) switch
        {
            (Faction.Player, Faction.Enemy) => true,
            (Faction.Enemy, Faction.Player) => true,
            (Faction.PlayerSummon, Faction.Enemy) => true,
            (Faction.Enemy, Faction.PlayerSummon) => true,
            _ => false
        };
    }
}