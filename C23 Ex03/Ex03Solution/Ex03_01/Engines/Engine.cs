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

    // protected void AddEnergy(float i_EnergyToAdd)
    // {
    //     if (m_CurrentEnergyLevel == null)
    //     {
    //         /*It is likely that in this case, the user
    //         Meant to actually set the energy level rather than
    //         to add more energy*/
    //         CurrentEnergyLevel = i_EnergyToAdd;
    //     }
    //     else
    //     {
    //         float maxEnergyToAdd = m_EnergyCapacity - (float)CurrentEnergyLevel;
    //         if (i_EnergyToAdd > maxEnergyToAdd || i_EnergyToAdd < 0 )
    //         {
    //             string erroMessage = string.Format("Additional energy cannot exceed {0} and cannot be negative",
    //                 maxEnergyToAdd);
    //             
    //             throw new ValueOutOfRangeException(new Exception(
    //                     erroMessage),
    //                 0, 
    //                 m_EnergyCapacity - (float)m_CurrentEnergyLevel);
    //         }
    //
    //         CurrentEnergyLevel += i_EnergyToAdd;
    //     }
    // }

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