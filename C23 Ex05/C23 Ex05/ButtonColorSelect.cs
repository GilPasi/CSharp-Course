using System.Windows.Forms;
using System.Drawing;
using System;
namespace Ex_05
{
    internal class ButtonColorSelect : Button
    {
        private const int k_DefalutEdgeSize = 40;
        private ColorsSelectForm m_ColorSelectForm = new ColorsSelectForm();
        
        //### Constructors ###

        public ButtonColorSelect()
        {
            Click += this_Click;
            Size = new Size(k_DefalutEdgeSize, k_DefalutEdgeSize);
            BackColor = Color.Transparent;
        }

        //### Accessors & Mutators ###

        public void AddColor(Color i_Color)
        {
            Button colorButton = m_ColorSelectForm.AddColorButton(i_Color);
            colorButton.Click += ButtonColor_Click;
        }

        public void AddPalette(Color[] i_Palette)
        {
            foreach (Color color in i_Palette)
            {
                AddColor(color);
            }
        }

        //### Event Handlers & Event Invokers ###

        private void ButtonColor_Click(object i_Sender, EventArgs i_Event)
        {
            if (!(i_Sender is Control))
            {
                string errorMessage = string.Format("Invalid event handler allocation for {0}", i_Sender);
                throw new ArgumentException(errorMessage);    
            }

            BackColor = (i_Sender as Control).BackColor; 
        }

        private void this_Click(object i_Sender, EventArgs i_Event)
        {
            if (Enabled)
            {
                m_ColorSelectForm.ShowDialog();
            }
        }
    }
}