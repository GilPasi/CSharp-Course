using System.Windows.Forms;
using System.Drawing;
using System;
namespace Ex_05
{
    internal class ColorsSelectForm : Form
    {
        private int m_CommonTabIndex = 0;
        private const int k_ButtonsNumberInLine = 4;
        private const int k_CellSize = 60;
        private int m_ColorsCount = 0;

        //### Constructors ###
        public ColorsSelectForm()
        {
            InitializeComponent();        
        }

        private void InitializeComponent()
        {
            updateSize();
            SuspendLayout();
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "ColorsSelectForm";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Pick A Color";
            ResumeLayout(false);
        }

        public Button AddColorButton(Color i_Color)
        {
            m_ColorsCount++;
            updateSize();
            return initalizeButton(i_Color);
        }

        private void updateSize()
        {
            ClientSize = new System.Drawing.Size(250, 10 + k_CellSize * LinesCount);
        }

        private Button initalizeButton(Color i_Color)
        {
            Button newButton = new Button();
            newButton.BackColor = i_Color;
            newButton.Name = string.Format("Button{0}", newButton.BackColor.ToString());
            newButton.Size = new System.Drawing.Size(50, 50);
            newButton.TabIndex = m_CommonTabIndex++;
            newButton.UseVisualStyleBackColor = false;
            newButton.Click += new System.EventHandler(ButtonColor_Click);
            Controls.Add(newButton);
            placeButton(ref newButton);

            return newButton;
        }

        private void placeButton(ref Button io_Button)
        {
            int line = io_Button.TabIndex / k_ButtonsNumberInLine;
            int coloumn = io_Button.TabIndex % k_ButtonsNumberInLine;
            io_Button.Location = new System.Drawing.Point(10 + k_CellSize * coloumn, 10 + k_CellSize * line);
        }
        //### Accessors & Mutaturs 
        public int ColorsCount
        {
            get 
            {
                return m_ColorsCount;
            }
        }

        private int LinesCount
        {
            get
            {
                int requiredLines = ColorsCount / k_ButtonsNumberInLine;
                requiredLines += ColorsCount % k_ButtonsNumberInLine == 0 ? 0 : 1;
                return requiredLines;
            }
        }

        //### Event Handlers & Event Invokers
        private void ButtonColor_Click(object i_Sender, EventArgs i_Event)
        {
            Visible = false;
        }
    }
}
