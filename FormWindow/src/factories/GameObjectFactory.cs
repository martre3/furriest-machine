using Maze.src.engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Maze.src
{
    class GameObjectFactory
    {
        private readonly Dictionary<string, Bitmap> ImageMap = new Dictionary<string, Bitmap>();
        private readonly string ProjectDirectory = Environment.CurrentDirectory;

        public GameObject Create(Shared.Engine.GameObject gameObject)
        {
            return new GameObject() {
                X = gameObject.X,
                Y = gameObject.Y,
                Texture = this.FindImage(gameObject.TextureFile),
            };
        }

        private Bitmap FindImage(string file)
        {
            if (!this.ImageMap.ContainsKey(file)) {
                this.ImageMap.Add(file, new Bitmap(new Uri($"{this.ProjectDirectory}/assets/{file}", UriKind.Absolute).AbsolutePath));
            }

            return this.ImageMap[file];
        }
    }
}
