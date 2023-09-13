using System.Windows.Forms;
using System.Drawing;
using System;
using Ex02;
using System.Collections.Generic;
using System.Linq;
namespace Ex_05
{
    internal class GameForm : Form
    {
        private const int k_HeaderSize = 60;
        private bool m_ValidGuessCountWasSet = false;
        private Color[] m_ColorsInGame = new Color[]
        {Color.Purple, Color.Blue, Color.Yellow, Color.Lime,
         Color.Maroon, Color.Red, Color.Cyan, Color.White};
        private Dictionary<Color, char> m_ColorsToLettersDictionary;
        private GameControl m_Controller;
        private Label m_EndOfGameMessage;
        private Button m_ButtonRetry;
        private Panel[] m_SecretSequence;
        private TurnsBoxesList m_AllTurnBoxes;
        private GuessCountForm m_GuessCountForm = new GuessCountForm();

        //### Constructors ### 

        public GameForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = 
                new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            Icon = (Icon)resources.GetObject("$this.Icon");

            Text = "Bulls & Cows";
            m_SecretSequence = new Panel[4];
            SuspendLayout();
            initializeColorsToLettersDictionary();
            m_GuessCountForm.ButtonStart.Click += ButtonStart_Clicked;
            initializeSecretSequence();
            initializeRetrySection();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GameForm";
            ResumeLayout(false);
        }

        private void initializeRetrySection()
        {
            m_ButtonRetry = new Button();
            m_ButtonRetry.Text = "Retry";
            int middleHeader = (k_HeaderSize - m_ButtonRetry.Height )/ 2 ;
            m_ButtonRetry.Location = new Point(Width - m_ButtonRetry.Width - 15, middleHeader + 10);
            m_ButtonRetry.Visible = false;
            m_ButtonRetry.Click += m_ButtonRetry_Clicked;
            Controls.Add(m_ButtonRetry);

            m_EndOfGameMessage = new Label();
            m_EndOfGameMessage.Font = new Font("Arial", 10, FontStyle.Bold);
            m_EndOfGameMessage.AutoSize = true;
            int almostEndOfForm = Width - m_EndOfGameMessage.Width ;
            m_EndOfGameMessage.Location = new Point(almostEndOfForm, middleHeader - 10);
            m_EndOfGameMessage.Visible = false;
            Controls.Add(m_EndOfGameMessage);
        }

        private void initializeSecretSequence()
        {
            for (int i = 0; i < m_SecretSequence.Length; i++)
            {
                m_SecretSequence[i] = initializeSecretSequence(i);
            }
        }

        private Panel initializeSecretSequence(int i_index)
        {
            Panel newSecretElement = new Panel();
            newSecretElement.BackColor = SystemColors.ActiveCaptionText;
            newSecretElement.Location = new Point(10 + 45 * i_index,10);
            newSecretElement.Name = string.Format("m_SecretSequnceElement{0}", i_index);
            newSecretElement.Size = new Size(40, 40);
            newSecretElement.TabIndex = i_index;
            Controls.Add(newSecretElement);
            return newSecretElement;
        }

        private void initializeTurnBoxes()
        {
            m_AllTurnBoxes.Head.Location = new Point(0, m_SecretSequence[0].Location.Y 
                + m_SecretSequence[0].Height + 10);
            m_AllTurnBoxes.InferAllBoxesPositions();
            m_AllTurnBoxes.AddAllBoxesToControls(this);
        }

        private void initializeColorsToLettersDictionary()
        {
            if (m_ColorsToLettersDictionary == null)
            {
                m_ColorsToLettersDictionary = new Dictionary<Color, char>();
                char correspondingLetter = 'A';
                foreach (Color color in m_ColorsInGame)
                {
                    m_ColorsToLettersDictionary.Add(color, correspondingLetter++);
                }
            }
        }

        //### Accessors & Mutators ###
        public Color[] PossibleColors
        {
            get 
            {
                return m_ColorsInGame;
            }
        }

        //### Operations ###

        public void ShowDialog() 
        {
            m_GuessCountForm.ShowDialog();

            if (m_ValidGuessCountWasSet)
            {
                base.ShowDialog();
            }
        }


        //### Event handlers & Event Invokers ###

        private void ButtonStart_Clicked(object i_Sender, EventArgs i_Event)
        {
            m_ValidGuessCountWasSet = true;
            uint guessesCount = (uint)m_GuessCountForm.GuessesCount;
            m_Controller = new GameControl(guessesCount);
            GameForm selfRefernce = this;
            m_AllTurnBoxes = new TurnsBoxesList(ref selfRefernce, m_GuessCountForm.GuessesCount);
            initializeTurnBoxes();
            ClientSize = new Size(300, TurnBox.StandardSize.Height * m_AllTurnBoxes.Count + k_HeaderSize);
            CenterToScreen();
            m_AllTurnBoxes.Head.Enabled = true;
        }

        public void TurnBox_GuessChecked(object i_Sender, EventArgs i_Event)
        {
            if (!(i_Sender is TurnBox))
            {
                string errorMessage = string.Format(
                    "Invalid event invoker, expeced TurnBox, instead got {0}",
                    i_Sender.GetType());
                throw new ArgumentException(errorMessage);
            }
            TurnBox turnBoxSender = (TurnBox)i_Sender;

            char[] guess = turnBoxToGuess(turnBoxSender);

            if (!GameControl.CheckIfPragmaticallyValidSequence(guess))
            {
                MessageBox.Show("A guess must not contain any two identical colors",
                    "Invalid Guess", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                m_Controller.AddTurn(guess);
                Turn lastTurn = m_Controller.TurnsHistory.Last();
                turnBoxSender.PointingBoard.GrantPoints(lastTurn.Bulls, lastTurn.Cows);
                Color semiTransparentGray = Color.FromArgb(25, 25, 25, 25);
                turnBoxSender.BackColor = semiTransparentGray;
                turnBoxSender.Enabled = false;
                eGameStatus gameStatus = m_Controller.EvaluateGameStatus();
                referGameEnd(gameStatus);
            }
        }

        private void m_ButtonRetry_Clicked(object i_Sender, EventArgs i_Event)
        {
            Controls.Clear();
            Visible = false;
            m_GuessCountForm.ShowDialog();
            InitializeComponent();
            Visible = true;
        }

        //### Utilities ###

        private char[] turnBoxToGuess(TurnBox i_SenderBox)
        {
            List<char> resultedGuess = new List<char>();

            foreach (Control colorButton in i_SenderBox.Controls)
            {
                if (colorButton is ButtonColorSelect)
                {
                    Color colorOfTheButton = colorButton.BackColor;
                    resultedGuess.Add(m_ColorsToLettersDictionary[colorOfTheButton]);
                }
            }

            return resultedGuess.ToArray();
        }


        private Color[] lettersToColors(char[] i_Letters)
        {
            Color[] guessAsColors = new Color[i_Letters.Length];
            int colorPosition = 0;

            foreach (char letter in i_Letters)
            {
                foreach (KeyValuePair<Color, char> pair in m_ColorsToLettersDictionary)
                {
                    if (letter == pair.Value)
                    {
                        guessAsColors[colorPosition++] = pair.Key;
                    }
                }
            }
            return guessAsColors;
        }
        private void referGameEnd(eGameStatus gameStatus)
        {
            if (gameStatus != eGameStatus.Ongoing)
            {
                endOfGameProcedure();

                if (gameStatus == eGameStatus.Victory)
                {
                    m_EndOfGameMessage.Text = "You Won :-)";
                    m_EndOfGameMessage.ForeColor = Color.Green;
                }

                if (gameStatus == eGameStatus.Defeat)
                {
                    m_EndOfGameMessage.Text = "You Lost :-(";
                    m_EndOfGameMessage.ForeColor = Color.Red;
                }
            }
        }

        private void endOfGameProcedure()
        {
            char[] correctSequence = m_Controller.CorrectSequence;
            Color[] correctSequenceAsColors = lettersToColors(correctSequence);
            for (int i = 0; i < m_SecretSequence.Length; i++)
            {
                m_SecretSequence[i].BackColor = correctSequenceAsColors[i];
            }
            m_AllTurnBoxes.Enabled = false;
            m_EndOfGameMessage.Visible = true;
            m_ButtonRetry.Visible = true;
        }
    }
}