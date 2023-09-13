using System.Windows.Forms;
using System.Drawing;
using System;
namespace Ex_05
{
    internal class TurnBox : Panel
    {
        private const int k_CellSize = 45;
        private int m_CommonTabIndex = 0;
        private EventHandler m_CheckGuessNotifier;
        private TurnBox m_NextTurnBox = null;
        private Button m_ButtomCheckGuess = new Button();
        private ButtonColorSelect[] m_Guess = new ButtonColorSelect[4];
        private PanelPointingBoard m_PointsBoard = new PanelPointingBoard();

        //### Constructors ###
        public TurnBox(Color[] i_Palette)
        {
            Size = StandardSize;
            Enabled = false;
            initializePointsBoard();
            initializeGuess(i_Palette);
            initializeGuessCheckButton();
        }

        private void initializePointsBoard()
        {
            m_PointsBoard.Location = new Point(Width - m_PointsBoard.Width - 20, getHeightCenter(m_PointsBoard));
            Controls.Add(m_PointsBoard);
        }

        private void initializeGuess(Color[] i_Palette)
        {
            for (int i = 0; i < m_Guess.Length; i++)
            {
                m_Guess[i] = addButtonColorSelect(i_Palette);
            }
        }

        private void initializeGuessCheckButton()
        {
            m_ButtomCheckGuess.Location = new Point(190, getHeightCenter(m_ButtomCheckGuess));
            m_ButtomCheckGuess.Name = "m_ButtomCheckGuess";
            m_ButtomCheckGuess.Size = new Size(40, 20);
            m_ButtomCheckGuess.TabIndex = 7;
            m_ButtomCheckGuess.Text = "->>";
            m_ButtomCheckGuess.UseVisualStyleBackColor = true;
            m_ButtomCheckGuess.Click += m_ButtomCheckGuess_Clicked;
            m_ButtomCheckGuess.Enabled = false;
            Controls.Add(m_ButtomCheckGuess);

        }

        //### Accessors & Mutators ###
        private ButtonColorSelect addButtonColorSelect(Color[] i_Palette)
        {
            ButtonColorSelect newButton = new ButtonColorSelect();
            newButton.Name = string.Format("m_ButtonGuessElement{0}", m_CommonTabIndex);
            newButton.Location = new Point(10 + k_CellSize * m_CommonTabIndex, getHeightCenter(newButton));
            newButton.TabIndex = m_CommonTabIndex++;
            newButton.UseVisualStyleBackColor = true;
            newButton.AddPalette(i_Palette);
            Controls.Add(newButton);
            newButton.Click += GuessElement_Clicked;
            return newButton;
        }

        public void CascadingInferPosition(TurnBox i_PreviousTurnBox = null)
        {
            if (i_PreviousTurnBox != null)
            {
                Location = new Point(0, i_PreviousTurnBox.Height + i_PreviousTurnBox.Location.Y);
            }
            m_NextTurnBox?.CascadingInferPosition(this);
        }

        public void CascadingAddToControls(Control i_ParentControl)
        {
            i_ParentControl.Controls.Add(this);
            if (m_NextTurnBox != null)
            {
                m_NextTurnBox.CascadingAddToControls(i_ParentControl);
            }
        }

        public static Size StandardSize
        {
            get
            {
                return new Size(300, 50);
            }
        }

        public ButtonColorSelect[] Guess
        {
            get
            {
                return m_Guess;
            }
            set
            {
                m_Guess = value;
            }
        }
        public TurnBox NextTurnBox
        {
            get
            {
                return m_NextTurnBox;
            }
            set
            {
                m_NextTurnBox = value;
            }
        }

        public bool IsLast
        {
            get
            {
                return m_NextTurnBox == null;
            }
        }
        private int getHeightCenter(Control i_VisualControl)
        {
            return (Height - i_VisualControl.Height) / 2;
        }

        public Button ButtomCheckGuess
        {
            get
            {
                return m_ButtomCheckGuess;
            }
            set
            {
                m_ButtomCheckGuess = value;
            }
        }

        public EventHandler CheckGuessNotifier
        {
            get 
            {
                return m_CheckGuessNotifier;
            }
            set
            {
                m_CheckGuessNotifier = value;
            }
        }

        public PanelPointingBoard PointingBoard
        {
            get
            {
                return m_PointsBoard;
            }
            set 
            {
                m_PointsBoard = value;
            }
        }

        //### Event Handlers & Event Invokers

        private void m_ButtomCheckGuess_Clicked(object i_Sender, EventArgs i_Event)
        {
            if(m_NextTurnBox != null)
            {
                m_NextTurnBox.Enabled = true;
            }
            m_CheckGuessNotifier.Invoke(this, i_Event);
        }

        private void GuessElement_Clicked(object i_Sender, EventArgs i_Event)
        {
            bool guessFilled = true;
            foreach (ButtonColorSelect colorButton in m_Guess)
            {
                guessFilled = guessFilled && colorButton.BackColor != Color.Transparent;
            }
            m_ButtomCheckGuess.Enabled = guessFilled;
        }
    }
}