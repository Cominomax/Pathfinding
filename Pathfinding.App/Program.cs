using System;
using System.IO;
using static System.Environment;

using Pathfinding.Lib;
using Pathfinding.Lib.Maps.Grid;
using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Extensions;

namespace PathFinding.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //maps found on https://movingai.com/benchmarks/grids.html
            var filePath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Maps", "AR0014SR.map");
            
            var scen = new Scenario();
            var (Success, ErrorMessage) = scen.TrySetScenario(filePath, MapTypes.Grid, new AStar(), new GridNode(56,40), new GridNode(53,41));
            if(!Success)
            {
                Console.WriteLine(ErrorMessage);
                return;
            }

            (bool Success, string ErrorMessage) responsesTuple = scen.RunScenario();
            if(!responsesTuple.Success)
            {
                Console.WriteLine(ErrorMessage);
                return;
            }

            Console.WriteLine(scen.Result.ToCollectionString());
        }
    }
}
