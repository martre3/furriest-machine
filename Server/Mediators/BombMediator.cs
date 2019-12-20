using System;
using System.Net;
using Maze.Engine.Input;
using Maze.Game.Items.Bombs;
using System.Collections.Generic;
using Maze.Server.Game.Data;
using Maze.Game.Objects;
using Maze.Game.Mediators;

namespace Maze.Server.Game.Mediators
{
    public class BombMediator: IBombMediator
    {    
        private GameData _data;
        private ExplosionHandler _explosionHandler;

        public BombMediator(GameData data) 
        {
            this._data = data;
            this._explosionHandler = new ExplosionHandler(this);
        }

        public void Register(GameObject o)
        {
            this._data.AddObject(o);
        }

        public void Explode(Bomb bomb)
        {
            this._explosionHandler.Explode(bomb);
            this._data.Players.ForEach((player) => player.OnBombExplosion(bomb));
        }
    }
}
