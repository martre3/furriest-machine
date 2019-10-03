using System;
using System.Collections.Generic;
using Maze.Game.Objects;
using Maze.Game.Assets;

namespace Maze.Game
{
    public class ClientGameState: GameState
    {
        private AssetsLoader _assetsLoader = new AssetsLoader();

        public override void Register(GameObject gameObject)
        {
            base.Register(gameObject);

            gameObject.InitializeAssets(_assetsLoader);
        }
    }
}
