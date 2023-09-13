using System.Windows.Forms;
using System.Drawing;
using Ex02;
using System;
namespace Ex_05
{
    internal class PanelPointingBoard : Panel
    {
        private Panel[] m_PanelPoints = new Panel[4];
        private const int k_DefaultEdgeSize = 30;
        private const int k_PointEdgeSize = 15;
        private const int k_PointsGap = 15;
        private const int k_LineSize = 2;
        private readonly Color r_BullsColor = Color.GreenYellow, r_CowsColor = Color.Orange;

        //### Constructros ###
        public PanelPointingBoard()
        {
            Size = new Size(k_DefaultEdgeSize, k_DefaultEdgeSize);
            for (int i = 0; i < m_PanelPoints.Length; i++)
            {
                m_PanelPoints[i] = createPanelPoint(i);
            }
        }

        public Panel createPanelPoint(int i_Index)
        {
            Panel newPanelPoint = new Panel();

            newPanelPoint.BackColor = Color.WhiteSmoke;
            newPanelPoint.BorderStyle = BorderStyle.FixedSingle;
            newPanelPoint.Location = new Point(
                (i_Index / k_LineSize) * k_PointsGap, (i_Index % k_LineSize) * k_PointsGap);
            newPanelPoint.Name = string.Format("m_PanelPoint{0}", i_Index);
            newPanelPoint.Size = new Size(k_PointEdgeSize, k_PointEdgeSize);
            newPanelPoint.TabIndex = i_Index;
            Controls.Add(newPanelPoint);

            return newPanelPoint;
        }

        //### Operations ###

        public void GrantPoints(eHitOptions i_Bulls, eHitOptions i_Cows)
        {
            int totalPoints = (int)i_Bulls + (int)i_Cows;
            if (totalPoints > m_PanelPoints.Length)
            {
                string errorMesssage = string.Format("The combined value of bulls and cows connot " +
                    "exceed the guess size. Got {0}/{1}", totalPoints, m_PanelPoints.Length);
                throw new ArgumentException(errorMesssage);
            }

            int pointPosition = 0;
            for (int i = 0; i < (int)i_Bulls; i++)
            {
                m_PanelPoints[pointPosition++].BackColor = r_BullsColor;
            }
            for (int i = 0; i < (int)i_Cows; i++)
            {
                m_PanelPoints[pointPosition++].BackColor = r_CowsColor;
            }

        }
    }
}
