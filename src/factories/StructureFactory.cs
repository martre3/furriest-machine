using Maze.src.map;
using Maze.src.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;

namespace Maze.src
{
    class StructureFactory
    {
        private readonly string ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        private readonly Structure WallPrototype = new Wall();
        private readonly Structure FloorPrototype = new Floor();
        private readonly Structure PlayerPrototype = new Player();

        public StructureFactory()
        {
            this.WallPrototype.Texture = this.FindImage(this.WallPrototype);
            this.FloorPrototype.Texture = this.FindImage(this.FloorPrototype);
            this.PlayerPrototype.Texture = this.FindImage(this.PlayerPrototype);
        }

        public Structure Create(Structures type)
        {
            switch (type)
            {
                case Structures.Wall:
                    return this.WallPrototype.Clone();
                case Structures.Floor:
                    return this.FloorPrototype.Clone();
                case Structures.Player:
                    return this.PlayerPrototype.Clone();
                default:
                    throw new NotImplementedException($"{type.ToString()} structure type is not supported");
            }
        }

        private BitmapImage FindImage(Structure structure)
        {
            return new BitmapImage(new Uri($"{ProjectDirectory}/assets/{structure.TextureFile}", UriKind.Absolute));
        }
    }
}
