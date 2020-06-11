namespace Pathfinding.Lib.Scenarios.FromFile
{
    internal class FileScenario
    {
        internal FileScenario(params string[] vals)
        {
            
            StartY = int.Parse(vals[4]);
            StartX = int.Parse(vals[5]);
            EndY = int.Parse(vals[6]);
            EndX = int.Parse(vals[7]);
            ExpectedLength = decimal.Parse(vals[8]);
        }

        internal int StartX { get; set; }
        internal int StartY { get; set; }
        internal int EndX { get; set; }
        internal int EndY { get; set; }
        internal decimal ExpectedLength { get; set; }

    }
}
