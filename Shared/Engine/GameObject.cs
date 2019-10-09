using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Shared.Engine
{
    [Serializable]
    public class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string TextureFile { get; set; }
    }
}
