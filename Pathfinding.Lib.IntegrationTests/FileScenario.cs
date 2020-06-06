using System;
using System.Collections.Generic;
using System.Text;

namespace Pathfinding.Lib.IntegrationTests
{
    class FileScenario
    {
        public FileScenario(params string[] vals)
        {
            
            StartY = int.Parse(vals[4]);
            StartX = int.Parse(vals[5]);
            EndY = int.Parse(vals[6]);
            EndX = int.Parse(vals[7]);
            ExpectedLength = decimal.Parse(vals[8]);
        }

        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public decimal ExpectedLength { get; set; }

    }
}
