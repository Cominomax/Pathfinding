using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib.Maps.Grid
{
    /// <summary>
    /// Contains the logic to read a Grid from a file.
    /// </summary>
    public static class GridFactory
    {
        /// <summary>
        /// Reads Grid map file and returns a Grid object under an IMap interface.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        internal static async Task<IMap> ReadGrid(string filePath)
        {
            try
            {
                using var streamReader = new StreamReader(new FileStream(filePath, FileMode.Open));
            
                var input = await streamReader.ReadLineAsync();
                int height = 0, width = 0;
                while(input != "map")
                {
                    switch (input)
                    {
                        case string h when h.Contains("height"): 
                            height = int.Parse(h.Remove(0,7));
                            break;
                        case string w when w.Contains("width"): 
                            width = int.Parse(w.Remove(0,6));
                            break;
                        default:
                        break;
                    }
                    input = await streamReader.ReadLineAsync();
                }
                var newGrid = new Grid(filePath, height, width);
                for (int i = 0; i < height; i++)
                {
                    input = await streamReader.ReadLineAsync();
                    newGrid.GridMap[i] = input.ToArray();
                }

                return newGrid;
            }
            catch (Exception ex)
            {
                return new EmptyMapWithError(ex.Message);
            }
        }
    }
}