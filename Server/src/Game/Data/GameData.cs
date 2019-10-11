using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.map;
using MazeServer.src.engine;
using MazeServer.src.Game.Players;
using MazeServer.src.engine.Events.Arguments;

namespace MazeServer.src.Data
{
    class GameData
    {
        public List<GameObject> Objects { get; set; }
        public List<Structure> Map { get; set; }
        public List<Player> Players { get; set; }

        public GameData()
        {
            this.Objects = new List<GameObject>();
            this.Map = new List<Structure>();
            this.Players = new List<Player>();

            GameObject.ObjectInstantiated += this.ObjectInstantiated;
        }

        public void ObjectInstantiated(object sender, GameObjectInitializedArguments arguments)
        {
            this.Objects.Add(arguments.Object);
        }
    }
}
