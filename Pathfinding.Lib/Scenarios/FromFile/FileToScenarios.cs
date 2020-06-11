using Pathfinding.Lib.Maps.Grid;
using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Scenarios.Base;
using System.Collections.Generic;
using System.IO;

namespace Pathfinding.Lib.Scenarios.FromFile
{
    internal class FileToScenarios
    {
        internal IEnumerable<IScenario> Convert(string mapFilepath, string scenFilepath, int maxScenario = int.MaxValue)
        {
            var scenarioList = new List<Scenario>();
            if (!File.Exists(mapFilepath) || !File.Exists(scenFilepath))
            {
                return scenarioList;
            }

            using var streamReader = new StreamReader(new FileStream(scenFilepath, FileMode.Open));
            string line = streamReader.ReadLine(); //skip first line of scenario file as useless.
            var scenarioParams = new ScenarioParams()
            {
                FilePath = mapFilepath,
                MapType = MapTypes.Grid
            };

            for (int i = 0; i < maxScenario && !streamReader.EndOfStream; i++)
            {
                var fileScenario = new FileScenario(streamReader.ReadLine().Split('\t'));
                scenarioParams.ScenarioName = i.ToString();
                scenarioParams.Start = new GridNode(fileScenario.StartX, fileScenario.StartY);
                scenarioParams.End = new GridNode(fileScenario.EndX, fileScenario.EndY);
                scenarioParams.ExpectedLength = fileScenario.ExpectedLength;
                var scen = new Scenario();
                scen.TrySetScenario(scenarioParams);
                scenarioList.Add(scen);
            }

            return scenarioList;
        }
    }
}
