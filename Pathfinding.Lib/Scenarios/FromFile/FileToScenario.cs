using Pathfinding.Lib.Maps.Grid;
using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Scenarios.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Environment;

namespace Pathfinding.Lib.Scenarios.FromFile
{
    public class FileToScenario
    {
        private readonly string _scenFilepath;
        private readonly string _mapFilepath;

        public FileToScenario(string mapFilename)
        {
            _mapFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Maps", mapFilename);
            _scenFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Scenarios", mapFilename + ".scen");
        }

        public IEnumerable<IScenario> CreateActionsFromFile(int maxScenario = int.MaxValue)
        {
            using var streamReader = new StreamReader(new FileStream(_scenFilepath, FileMode.Open));
            string line = streamReader.ReadLine(); //skip first line of scenario file as useless.
            var scenarioParams = new ScenarioParams()
            {
                FilePath = _mapFilepath,
                MapType = MapTypes.Grid
            };
            var scenarioList = new List<Scenario>();

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
