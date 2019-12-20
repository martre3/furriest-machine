using System;
using System.Net;
using Maze.Engine.Input;
using Maze.Game.Objects;
using Maze.Server.Game.Mediators;

namespace Maze.Server.Game
{
    public class ExplosionHandler
    {   
        private BombMediator _mediator;

        public ExplosionHandler(BombMediator mediator)
        {
            this._mediator = mediator;
        }

        public void Explode(Bomb bomb)
        {
            this._mediator.Register(new Explosion(bomb.Position));

        }
    }
}
