namespace Ex03;

public abstract class Engine
{
    protected float? m_currentEnergyLevel =  null;
    public abstract float? CurrentEnergyLevel
    {
        set;
        get;
    }
}