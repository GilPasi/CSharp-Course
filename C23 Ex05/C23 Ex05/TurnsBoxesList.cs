using System.Windows.Forms;
using System.Drawing;
using System;
namespace Ex_05
{
    internal class TurnsBoxesList : Panel
    {
        private TurnBox m_HeadBox = null;
        private int m_Count = 0;
        private Color[] m_Palette;

        //### Constructors ###

        public TurnsBoxesList(ref GameForm io_GameForm, int i_ListSize = 0)
        {
            if (i_ListSize < 0)
            {
                throw new ArgumentException("Linked list size must be a non-negative integer");
            }

            m_Palette = io_GameForm.PossibleColors;
            for (int i = 0; i < i_ListSize; i++)
            {
                InsertNewTurnBox(ref io_GameForm);
            }
        }

        // ### Accessors & Mutators

        public void InsertNewTurnBox(ref GameForm io_GameForm)
        {
            TurnBox newHead = new TurnBox(m_Palette);
            newHead.CheckGuessNotifier += io_GameForm.TurnBox_GuessChecked;
            newHead.NextTurnBox = m_HeadBox;
            m_HeadBox = newHead;
            m_Count++;
        }

        public void InferAllBoxesPositions()
        {
            m_HeadBox.CascadingInferPosition();
        }

        public void AddAllBoxesToControls(Control i_ParentControl)
        {
            m_HeadBox.CascadingAddToControls(i_ParentControl);
        }

        public TurnBox Head
        {
            get
            {
                return m_HeadBox;
            }
            set
            {
                m_HeadBox = value;
            }
        }

        public int Count
        {
            get 
            {
                return m_Count;
            }   
        }
    }
}
