using System.Windows.Forms;
using System.Drawing;
namespace Ex_05
{
    internal class GuessCountForm : Form
    {
        private Button m_ButtonGuessCountRotate;
        private Button m_ButtonStart;
        private  readonly int r_LowerGuessBound, r_UpperGuessBound;
        private int m_CurrentGuessesCount;
        bool m_ClosedByStart = false;

        //### Constructors ###
        public GuessCountForm(int i_LowerGuessBound = 4, int i_UpperGuessBound = 10)
        {
            System.ComponentModel.ComponentResourceManager resources =
            new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            Icon = (Icon)resources.GetObject("$this.Icon");
            r_LowerGuessBound = i_LowerGuessBound;
            r_UpperGuessBound = i_UpperGuessBound;
            m_CurrentGuessesCount = r_LowerGuessBound;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            initializeButtonGuessCountRoatate();
            initializeButtonStart();

            Text = "Bulls & Cows";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            ClientSize = new Size(284, 107);
            Name = "GuessCountForm";
        }

        private void initializeButtonStart()
        {
            m_ButtonStart = new Button();
            m_ButtonStart.Location = new Point(197, 63);
            m_ButtonStart.Name = "m_ButtonStart";
            m_ButtonStart.Size = new Size(75, 23);
            m_ButtonStart.TabIndex = 1;
            m_ButtonStart.Text = "Start";
            m_ButtonStart.UseVisualStyleBackColor = true;
            m_ButtonStart.Click += new System.EventHandler(m_ButtonStart_Click);
            Controls.Add(m_ButtonStart);
        }

        private void initializeButtonGuessCountRoatate()
        {
            m_ButtonGuessCountRotate = new Button();
            m_ButtonGuessCountRotate.Location = new Point(63, 23);
            m_ButtonGuessCountRotate.Name = "m_ButtonGuessCountIncrease";
            m_ButtonGuessCountRotate.Size = new Size(150, 23);
            m_ButtonGuessCountRotate.TabIndex = 0;
            m_ButtonGuessCountRotate.Text = currentGuessCountAsText;
            m_ButtonGuessCountRotate.UseVisualStyleBackColor = true;
            m_ButtonGuessCountRotate.Click += new System.EventHandler(m_ButtonGuessCountIncrease_Click);
            Controls.Add(m_ButtonGuessCountRotate);
        }

        //### Accessors & Mutators ###
        public int LowerBoundGuessCount
        {
            get
            {
                return r_LowerGuessBound;
            }
        }

        public int UpperBoundGuessCount
        {
            get
            {
                return r_UpperGuessBound;
            }
        }

        public int GuessesCount
        {
            get 
            {
                return m_CurrentGuessesCount;
            }
            set
            {
                m_CurrentGuessesCount = value;
            }
        }

        public bool ClosedByStart
        {
            get
            {
                return m_ClosedByStart;
            }
        }

        public Button ButtonStart
        {
            get
            {
                return m_ButtonStart;
            }
            set
            {
                m_ButtonStart = value;
            }
        }

        //### Event Handling & Event Invoking ###
        private void m_ButtonStart_Click(object sender, System.EventArgs e)
        {
            Visible = false;
            m_ClosedByStart = true;
        }

        private void m_ButtonGuessCountIncrease_Click(object sender, System.EventArgs e)
        {
            increaseCurrentGuessCount();
            m_ButtonGuessCountRotate.Text = currentGuessCountAsText;
        }

        private void increaseCurrentGuessCount()
        {
            m_CurrentGuessesCount++;
            if (m_CurrentGuessesCount > r_UpperGuessBound)
            {
                m_CurrentGuessesCount = r_LowerGuessBound;
            }
        }

        private string currentGuessCountAsText
        {
            get
            {
                return string.Format("Number of chances : {0}", m_CurrentGuessesCount);
            }
        }
    }
}