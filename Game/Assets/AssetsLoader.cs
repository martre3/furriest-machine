using System;
using System.Collections.Generic;
using Maze.Game.Objects;
using System.Drawing;

namespace Maze.Game.Assets
{
    public class AssetsLoader
    {
        private int _amount = 0;
        private Dictionary<string, Brush> _brushCache = new Dictionary<string, Brush>();

        public Brush LoadBrush(string fileName)
        {
            if (!_brushCache.ContainsKey(fileName)) {
                var brush = new TextureBrush(new Bitmap(new Uri($"{Environment.CurrentDirectory}/assets/{fileName}", UriKind.Absolute).AbsolutePath));
                _brushCache.Add(fileName, brush);
            }
            
            return _brushCache[fileName];
        }
    }
}
