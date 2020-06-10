using Pathfinding.Lib.Maps;
using Pathfinding.Lib.Maps.Utils;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static System.Environment;

namespace Pathfinding.Lib.UnitTests.Maps
{
    public class MapSingletonTests
    {
        [Fact]
        public void MapSingleton_ShouldBeThreadSafe()
        {
            //Arrange
            var mapNames = new string[] {"AR0011SR.map",
                    "AR0013SR.map", "AR0069SR.map", "AR0315SR.map",
                    "AR0400SR.map", "AR0408SR.map", "AR0411SR.map",
                    "AR0701SR.map", "AR0600SR.map", "AR0700SR.map"};
            var mapFilepaths = mapNames.Select(mn => Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Maps", mn)).ToArray();
            var tasks = new Task[mapFilepaths.Count()*3];

            //Act
            int i = 0;
            foreach (var item in mapFilepaths)
            {
                tasks[i++] = Task.Factory.StartNew(() => MapSingleton.Instance.GetMap(item, MapTypes.Grid));
                tasks[i++] = Task.Factory.StartNew(() => MapSingleton.Instance.GetMap(item, MapTypes.Grid));
                tasks[i++] = Task.Factory.StartNew(() => MapSingleton.Instance.GetMap(item, MapTypes.Grid));
            }
            Task.WaitAll(tasks);

            //Assert
            Assert.Equal(mapNames.Count(), MapSingleton.Instance.LoadedMap.Count);
            foreach (var key in mapFilepaths)
            {
                Assert.True(MapSingleton.Instance.LoadedMap.ContainsKey(key));
            }
        }
    }
}
