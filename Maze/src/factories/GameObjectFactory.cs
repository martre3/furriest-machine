using Maze.src.engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;

namespace Maze.src
{
    class GameObjectFactory
    {
        private readonly Dictionary<int, string> TextureMap = new Dictionary<int, string>();
        private readonly string ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        //private readonly Structure WallPrototype = new Wall();
        //private readonly Structure FloorPrototype = new Floor();

        public GameObjectFactory()
        {
            this.TextureMap.Add(1, "Wall.tif");
            this.TextureMap.Add(2, "Floors.tif");
            this.TextureMap.Add(3, "slime.png");
        }

        public GameObject Create(Shared.Engine.GameObject gameObject)
        {
            return new GameObject() {
                X = gameObject.X,
                Y = gameObject.Y,
                Texture = this.FindImage(gameObject.TextureID),
            };
        }

        private BitmapImage FindImage(int id)
        {
            return new BitmapImage(new Uri($"{this.ProjectDirectory}/assets/{this.TextureMap[id]}", UriKind.Absolute));
        }
    }
}
