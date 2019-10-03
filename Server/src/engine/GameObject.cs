using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.src.engine.Events.Arguments;

namespace MazeServer.src.engine
{
    abstract class GameObject: Shared.Engine.GameObject
    {
        public static event EventHandler<GameObjectInitializedArguments> ObjectInstantiated;
        public static event EventHandler<GameObjectInitializedArguments> ObjectDestroyed;

        // public BitmapImage Texture { get; set; }

        public GameObject Instantiate()
        {
            GameObject.ObjectInstantiated(this, new GameObjectInitializedArguments(this));

            return this;
        }
    }
}
