namespace Ex03
{
    public class LimitedChoiceQuestion<T> : Question<T>
    {
        private readonly BoundedIntParser m_Parser ;
        private readonly List<T> m_Options;
        private readonly List<string> m_OptionsAsText;
        
        public LimitedChoiceQuestion(List<T> i_Options, List<string> i_OptionsAsText, string i_Wording, string i_ErrorText)
        :this(i_Options,i_Wording,i_ErrorText)
        {
            if (i_OptionsAsText.Count != i_Options.Count)
            {
                throw new ArgumentException("The options and the options texts must have the same length");
            }
            
            m_OptionsAsText = i_OptionsAsText;
        }
        
        public LimitedChoiceQuestion(List<T> i_Options, string i_Wording, string i_ErrorText)
        :base(i_Wording,i_ErrorText)
        {
            m_Parser = new BoundedIntParser(new int[]{1,i_Options.Count});
            m_Options = i_Options;
            m_OptionsAsText = new List<string>(i_Options.Count) ;
        }

        public LimitedChoiceQuestion(LimitedChoiceQuestion<T>? i_OtherQuestion)
        :base(i_OtherQuestion){
            m_Parser = i_OtherQuestion.m_Parser;
            m_Options = new List<T>(i_OtherQuestion.m_Options);
            m_OptionsAsText = new List<string>(i_OtherQuestion.m_OptionsAsText);
        }

        public override void PresentQuestion()
        {
            int optionCounter = 1;

            Console.WriteLine(m_Wording);
            foreach (string option in m_OptionsAsText)
            {
                if (option != string.Empty)
                {
                    Console.WriteLine("{0}. {1}",optionCounter++, option);
                }
            }
        }
        
        public override T Ask()
        {
            T parsedAnswer;
            int parsedInput;
            PresentQuestion();
            parsedInput = IOManipulator.ParseInput<int>("Your input must be a number of choice", m_Parser);
            if (!m_Parser.IsItemInRange<int>(parsedInput, 1, m_Options.Count))
            {
                throw new ArgumentException("Your choice was not one of the options");
            }
            parsedAnswer = m_Options[parsedInput - 1];
            
            return parsedAnswer;
        }
        
        public override T  AskAndForceInput()
        {
            PresentQuestion();
             int parsedInput = IOManipulator.ForceParseableInput<int>("Your input must be a number of choice"
                ,m_Parser);
             
            return m_Options[parsedInput - 1];
        }

        public void InferOptions()
        {
            int i = 0;
            
            if (typeof(T) == typeof(bool))
            {
                m_OptionsAsText.Add("Yes");
                m_OptionsAsText.Add( "No");

            }

            //This will apply when using an array of classes
            //For example [ElectricityDrivenCar, FuelDrivenCar,Truck ...]
            else if (typeof(T) == typeof(Type))
            {
                foreach (T option in m_Options)
                {
                    string name = (option as Type).Name;
                    string formattedName = IOManipulator.PascalCaseToHumanReadable(name);
                    m_OptionsAsText.Add(formattedName);
                }
            }

            else
            {
                foreach (T option in m_Options)
                {
                    string formattedName = IOManipulator.PascalCaseToHumanReadable(option.ToString());
                    m_OptionsAsText.Add(formattedName);
                }   
            }
        }

        public void RemoveOption(int i_OptionPosition)
        {
            m_Options.RemoveAt(i_OptionPosition);
            m_OptionsAsText.RemoveAt(i_OptionPosition);
        }

        public void AddOption(T i_OptionToAdd, string i_OptionAsString)
        {
            m_Options.Add(i_OptionToAdd);
            m_OptionsAsText.Add(i_OptionAsString);
            m_Parser.IncreaseUpperBound();
        }
        
        public void EditOption(int i_OptionPosition, T i_Option, string i_OptionAsString = null)
        {
            m_Options[i_OptionPosition] = i_Option;
            EditOption(i_OptionPosition, i_OptionAsString);
        }
        
        public void EditOption(int i_OptionPosition, string i_OptionAsString = null)
        {
            if (i_OptionAsString != null)
            {
                m_OptionsAsText[i_OptionPosition] = i_OptionAsString;
            }
        }
    }
}

