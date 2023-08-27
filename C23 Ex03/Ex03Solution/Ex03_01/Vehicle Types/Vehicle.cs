using System.Text;

namespace Ex03
{
    public abstract class Vehicle
    {
        protected LettersAndDigitsParser m_LicenseNumberParser = new LettersAndDigitsParser();
        protected string m_LicenseNumber = String.Empty;
        protected eVehicleStatus m_Status = eVehicleStatus.CurrentlyTreated;
        protected List<Tire> m_Tires;
        protected Engine m_Engine;
        protected List<PseudoAttribute> m_UniqueDataMembers = null;

        public Vehicle()
        {
            //Force any new vehicle type to specify its needs
            intiateUniqueDataMembers();
        }
        
        public Vehicle(string i_LicenseNumber):this()
        {
            m_LicenseNumber = i_LicenseNumber;
        }

        public float? CurrentEnergyLevel
        {
            get
            {
                return m_Engine.CurrentEnergyLevel;
            }
            set
            {
                m_Engine.CurrentEnergyLevel = value;
            }
        }

        public float GetMaxEnergy()
        {
            return m_Engine.EnergyCapacity;
        }
        
        public float GetTiresMaxPressure()
        {
            return m_Tires[0].MaxAirPressure;
        }
        
        public int GetTiresQuantity()
        {
            return m_Tires.Count;
        }
        
        public eVehicleStatus Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }
        }
        
        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
            set
            {
                if (! m_LicenseNumberParser.TryParse(value, out object parsedLicenseNumber))
                {
                    throw new FormatException("License number must contain only letters or numbers");
                }
                else
                {
                    m_LicenseNumber = value;
                }
            }
        }
        
        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
            set
            {
                if (m_Engine == null)
                {
                    m_Engine = value;
                }
            }
        }
        
        public List<Tire> Tires
        {
            get
            {
                return m_Tires;
            }
            set
            {
                if (m_Tires == null)
                {
                    m_Tires = value;
                }
            }
        }

        public void SetUniqueDataMembersValues(object[] i_NewValues)
        {
            if (checkIfCanRecieveNewValues(i_NewValues))
            {
                int i = 0;
                
                foreach (object newValue in i_NewValues)
                {
                    m_UniqueDataMembers[i++].Value = newValue;
                }
            }
            else
            {
                throw new ArgumentException("The new values must match as the target values");
            }
        }
        private bool checkIfCanRecieveNewValues(object[] i_NewValues)
        {
            int i = 0;
            bool result = i_NewValues.Length == m_UniqueDataMembers.Count;
            
            if (result)
            {
                foreach (object newValue in i_NewValues)
                {
                    Type currentValueType = m_UniqueDataMembers[i++].ValueType;
                    result = newValue.GetType() == currentValueType;
                }
            }

            return result;
        }

        public List<PseudoAttribute> UniqueDataMembers
        {
            get
            {
                return m_UniqueDataMembers;
            }

            set
            {
                if (!compareDataMembersLists(value, m_UniqueDataMembers))
                {
                    throw new ArgumentException("The attributes' array does not match the current structure");
                }
                else
                {
                    m_UniqueDataMembers = value;
                }
            }
        }

        private static bool compareDataMembersLists(List<PseudoAttribute> i_List1, List<PseudoAttribute> i_List2)
        {
            bool result = i_List1.Count == i_List2.Count;
            if (result)
            {
                for (int i = 0; i < i_List1.Count; i++)
                {
                    result = result && i_List1[i].ValueType == i_List2[i].ValueType;
                }
            }

            return result;
        }

        protected abstract void intiateUniqueDataMembers();

        public bool CheckIfAllTiresAreSet()
        {
            bool result = true;
            
            foreach (Tire tire in Tires)
            {
                result = result && tire.CurrentAirPressure != null;
            }

            return result;
        }

        protected void InitiateTires(int i_TiresCount, float i_MaxAirPressure)
        {
            m_Tires = new List<Tire>(i_TiresCount);
            
            for (int i = 0; i < i_TiresCount; i++)
            {
                m_Tires.Add(new Tire(i_MaxAirPressure));
            }
        }


        
        protected void InitiateEnumDataMember<E>()
        {
            string enumName = ConvertEnumNameToHumanReadable(typeof(E).Name);
            List<E> allPossibleValues = new List<E>((E[])Enum.GetValues(typeof(E)));
            int valuesCount = Enum.GetNames(typeof(E)).Length;
            BoundedIntParser enumParser = new BoundedIntParser(new int[]{0,valuesCount});
            
            m_UniqueDataMembers.Add(new PseudoAttribute<E>(enumName, enumParser,allPossibleValues));
        }

        protected static string ConvertEnumNameToHumanReadable(string i_EnumName)
        {
            StringBuilder humanReadable = new StringBuilder();
            
            if (i_EnumName[0] == 'e')
            {
                i_EnumName = i_EnumName.Substring(1); // Skip the 'e' prefix
            }
            
            for (int i = 0; i < i_EnumName.Length; i++)
            {
                if (char.IsUpper(i_EnumName[i]))
                {
                    if (i != 0)
                    {
                        humanReadable.Append(' ');
                    }

                    humanReadable.Append(i_EnumName[i]);
                }
                else
                {
                    humanReadable.Append(i_EnumName[i]);
                }
            }

            return humanReadable.ToString();
        }

        protected void InitiateUFloatDataMember(string i_DataMemberName)
        {
            PseudoAttribute<float> newAttribute = new PseudoAttribute<float>(
                i_DataMemberName, new UFloatParser());
            m_UniqueDataMembers.Add(newAttribute);
        }

        public virtual string UniqueDataMembersToString()
        {
            /*This method is virtual so it could be altered in the generations to come.
             This way a new vehicle type can specify the string for new attributes.
             For example showing the engine temperature in two formats: celsius and fahrenheit 32C|89.6F*/
            const string k_Spacer = "\t\t\t\t\t";
            const int k_ApproximateNeededChararctersForDataMember = 30;
            StringBuilder uniqueDataMembersAsString = new StringBuilder(
                k_ApproximateNeededChararctersForDataMember * UniqueDataMembers.Count);

            foreach (PseudoAttribute uniqueDataMember in UniqueDataMembers)
            {
                string dataMemberAsString = string.Format("{0}:{1}{2}{3}", uniqueDataMember.Name,
                    k_Spacer, uniqueDataMember.Value, Environment.NewLine);
                uniqueDataMembersAsString.Append(dataMemberAsString);
            }
            
            return uniqueDataMembersAsString.ToString();
        }
    }
}