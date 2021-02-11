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
        // Prices of putting on corresponding state
        private const Int32 START_GROW_PRICE = -2;
        private const Int32 GET_GROWING_PRICE = 0;
        private const Int32 GET_GREEN_PRICE = 0;
        private const Int32 GET_YELLOW_PRICE = 3;
        private const Int32 GET_RED_PRICE = 5;
        private const Int32 GET_OVERGROW_PRICE = -1;

        private Int32 day = 0;
        private Int32 money = 100;

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

        private Int32 GetPrice(CellState state)
        {
            switch (state)
            {
                case CellState.Empty:
                    return START_GROW_PRICE;

                case CellState.Growing:
                    return GET_GROWING_PRICE;

                case CellState.Green:
                    return GET_GREEN_PRICE;

                case CellState.Yellow:
                    return GET_YELLOW_PRICE;

                case CellState.Red:
                    return GET_RED_PRICE;

                case CellState.Overgrow:
                    return GET_OVERGROW_PRICE;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (sender as CheckBox);
            Int32 price = GetPrice(field[cb].state);
            if (money + price < 0)
            {
                return;
            }
            money += price;
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
            field[cb].Cut();
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
            this.labMoney.Text = $"Money: {money}";
        }
    }
}
