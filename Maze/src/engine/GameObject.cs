using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Maze.src.engine.Events.Arguments;

namespace Maze.src.engine
{
    class GameObject
    {
        public static event EventHandler<GameObjectInitializedArguments> ObjectInstantiated;
        public static event EventHandler<GameObjectInitializedArguments> ObjectDestroyed;

        public int X { get; set; }
        public int Y { get; set; }
        public BitmapImage Texture { get; set; }

        public GameObject Insantiate()
        {
            GameObject.ObjectInstantiated(this, new GameObjectInitializedArguments(this));

            return this;
        }
    }
}
