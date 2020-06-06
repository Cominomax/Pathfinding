using System.Collections.Generic;

using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps;
using System;

namespace Pathfinding.Lib
{
    /// <summary>
    /// Represents a running scenario of the algorithm.
    /// </summary>
    public class Scenario
    {
        private bool _hasBeenSet = false;

        public string FilePath { get; private set; }
        public MapTypes TypeOfMap { get; private set; }
        public IPathfindingAlgorithm ChosenAlgorithm { get; private set; }
        public INode Start { get; private set; }
        public INode End { get; private set; }
        public IMap Map { get; private set; }

        public IEnumerable<INode> Result { get; private set; }
        public decimal ResultPathLength { get; private set; }

        public (bool Success, string ErrorMessage) TrySetScenario(string filePath, MapTypes mapType 
            , IPathfindingAlgorithm chosenAlgorithm, INode start, INode end)
        {
            _hasBeenSet = false;
            FilePath = filePath;
            TypeOfMap = mapType;
            Map = MapSingleton.Instance.GetMap(filePath, mapType);
            if (Map is EmptyMapWithError)
            {
                return (false, Map.ToString());
            }
            ChosenAlgorithm = chosenAlgorithm;
            Start = start;
            if (!Map.IsValidInMap(Start))
            {
                return (false, "Invalid Start Point. Is either on a position which it cannot be or out of the map");
            }
            End = end;
            if (!Map.IsValidInMap(End))
            {
                return (false, "Invalid Stop Point. Is either on a position which it cannot be or out of the map");
            }
            _hasBeenSet = true;
            return (true, "Success");
        }

        public (bool Success, string ErrorMessage) RunScenario()
        {
            if (!_hasBeenSet)
            {
                return (false, "The scenario has not been set yet. Please call \'TrySetScenario()\' first.");
            }
            var resultNode = ChosenAlgorithm.Resolve(this);
            ResultPathLength = resultNode.DistanceFromOrigin;
            Result = resultNode.ToIEnumerable();
            return (true, "Success");
        }

        public bool IsCompleted(INode node)
        {
            if (node.Equals(End))
            {
                return true;
            }
            return false;
        }
    }
}