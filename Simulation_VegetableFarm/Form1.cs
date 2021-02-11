using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation_VegetableFarm
{
    public partial class Form1 : Form
    {
        private Int32 day = 0;

        private Dictionary<CheckBox, Cell> field = new Dictionary<CheckBox, Cell>();

        public Form1()
        {
            InitializeComponent();

            foreach (CheckBox cb in this.tableLayoutPanel1.Controls)
            {
                field.Add(cb, new Cell());
            }

            if (!timer1.Enabled)
            {
                timer1.Start();
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (sender as CheckBox);
            if (cb.Checked)
            {
                StartGrow(cb);
            }
            else
            {
                Cut(cb);
            }
        }

        private void Cut(CheckBox cb)
        {
            this.field[cb].Cut();
            UpdateBox(cb);
        }

        private void StartGrow(CheckBox cb)
        {
            this.field[cb].StartGrow();
            UpdateBox(cb);
        }

        private void UpdateBox(CheckBox cb)
        {
            Color color = Color.White;
            switch (field[cb].state)
            {
                case CellState.Growing:
                    color = Color.Black;
                    break;

                case CellState.Green:
                    color = Color.Green;
                    break;

                case CellState.Yellow:
                    color = Color.Yellow;
                    break;

                case CellState.Red:
                    color = Color.Red;
                    break;

                case CellState.Overgrow:
                    color = Color.Brown;
                    break;

                default:
                    break;
            }

            cb.BackColor = color;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (CheckBox cb in tableLayoutPanel1.Controls)
            {
                field[cb].Step();
                UpdateBox(cb);
            }
            day++;
            this.labDay.Text = $"Day: {day}";
        }
    }
}
