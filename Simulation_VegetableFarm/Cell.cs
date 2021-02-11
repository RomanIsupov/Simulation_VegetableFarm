using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_VegetableFarm
{
    public class Cell
    {
        private const Int32 GROWING_PROGRESS = 20;
        private const Int32 GREEN_PROGRESS = 60;
        private const Int32 YELLOW_PROGRESS = 80;
        private const Int32 RED_PROGRESS = 100;

        private Int32 progress { get; set; } = 0;
        public CellState state
        {
            get
            {
                if (progress == 0)
                {
                    return CellState.Empty;
                }
                else if (progress < GROWING_PROGRESS)
                {
                    return CellState.Growing;
                }
                else if (progress < GREEN_PROGRESS)
                {
                    return CellState.Green;
                }
                else if (progress < YELLOW_PROGRESS)
                {
                    return CellState.Yellow;
                }
                else if (progress < RED_PROGRESS)
                {
                    return CellState.Red;
                }
                else
                {
                    return CellState.Overgrow;
                }
            }
        }

        internal void StartGrow()
        {
            progress++;
        }

        internal void Cut()
        {
            progress = 0;
        }

        internal void Step()
        {
            if (state != CellState.Overgrow && state != CellState.Empty)
            {
                progress++;
            }
        }
    }
}
