using System.Collections.Generic;
using System.IO;

using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Maps.Grid;

namespace Pathfinding.Lib.Maps
{
    /// <summary>
    /// Provides an access to the loaded map in the application. 
    /// We don't want to load maps multiple times so we keep them 
    /// behind a singleton.
    /// </summary>
    public class MapSingleton
    {
        private static MapSingleton _instance;
        private Dictionary<string, IMap> _loadedMaps;

        /// <summary>
        /// Private constructor to follow pattern. Instantiate the Dictionary.
        /// </summary>
        private MapSingleton()
        {
            _loadedMaps = new Dictionary<string, IMap>();
        }

        /// <summary>
        /// Singleton Instance for the application. Use this.
        /// </summary>
        /// <value>MapSingleton Instance for the application.</value>
        public static MapSingleton Instance 
        { 
            get
            {  
                if (_instance == null)
                {
                    _instance  = new MapSingleton();
                }
                return _instance;
            } 
        }

        /// <summary>
        /// Gets or a loaded IMap or seamslessly loads it into memory. 
        /// </summary>
        /// <param name="mapFilePath">full path to datafile</param>
        /// <returns></returns>
        public IMap GetMap(string mapFilePath, MapTypes type)
        {
            if (!File.Exists(mapFilePath))
            {
                return new EmptyMapWithError("File does not exist!");
            }
            
            if (!_loadedMaps.ContainsKey(mapFilePath))
            {
                switch (type)   
                {
                    case MapTypes.Grid:
                        _loadedMaps.Add(mapFilePath, GridFactory.ReadGrid(mapFilePath));
                        break;
                    default:
                        return new EmptyMapWithError("Cannot find Map Type. Please enter a valid one.");
                }
            }
            return _loadedMaps[mapFilePath];
        }
    }
}