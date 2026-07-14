public enum ProjectileMoveType
{
    Straight,
    Orbit,
    Zigzag,
    Homing,
    Boomerang,
    Spiral
}

public static class ProjectileMoverFactory
{
    public static (IProjectileMover mover, IMoverState state) Create(ProjectileMoveType type)
    {
        return type switch
        {
            ProjectileMoveType.Straight => (new StraightProjectileMover(), null),
            ProjectileMoveType.Orbit => (new OrbitProjectileMover(), new OrbitMoverState()),
            ProjectileMoveType.Zigzag => (new ZigzagProjectileMover(), new ZigzagMoverState()),
            ProjectileMoveType.Boomerang => (new BoomerangProjectileMover(), new BoomerangMoverState()),
            ProjectileMoveType.Spiral => (new SpiralProjectileMover(), new SpiralMoverState()),
            _ => (new StraightProjectileMover(), null)
        };
    }
}