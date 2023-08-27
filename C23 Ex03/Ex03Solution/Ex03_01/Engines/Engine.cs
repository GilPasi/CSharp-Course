namespace Ex03;

public class Engine
{
    protected readonly float m_EnergyCapacity;
    protected float? m_CurrentEnergyLevel =  null;

    public Engine(float i_EnergyCapacity)
    {
        if (i_EnergyCapacity > 0)
        {
            m_EnergyCapacity = i_EnergyCapacity;
        }
        else
        {
            throw new ArgumentException("Energy capacity cannot be non-positive number");
        }
    }

    public float? CurrentEnergyLevel
    {
        get
        {
            return m_CurrentEnergyLevel;
        }
        set
        {
            if (value > m_EnergyCapacity || value < 0 )
            {
                throw new ValueOutOfRangeException(new Exception(
                    "An error occured while trying to set charge level"),
                    0, m_EnergyCapacity);
            }

            m_CurrentEnergyLevel = value;
        }
    }

    public float EnergyCapacity
    {
        get
        {
            return m_EnergyCapacity;
        }
    }
}