using System.Drawing;
using Maze.Engine.Physics;
using Xunit;

namespace Maze.Tests.Engine
{
    public class MeshTest
    {
        private Mesh _mesh;

        public MeshTest()
        {
            _mesh = new Mesh();
        }

        [Fact]
        public void TestUpdatesDirectionVector()
        {
            Assert.StrictEqual(_mesh.GetDirection(), new Point(0, 0));

            _mesh.Translate(1, -1);

            Assert.StrictEqual(_mesh.GetDirection(), new Point(1, -1));

            _mesh.Translate(-1, 0);

            Assert.StrictEqual(_mesh.GetDirection(), new Point(0, -1));
        }

        [Fact]
        public void TestResetsDirectionVector()
        {
            Assert.StrictEqual(_mesh.GetDirection(), new Point(0, 0));

            _mesh.Translate(5, -5);
            _mesh.ResetDirection();

            Assert.StrictEqual(_mesh.GetDirection(), new Point(0, 0));
        }

        [Fact]
        public void TestCollidesWithOtherMeshes()
        {
            var other = new Mesh() {
                Size = new Size(64, 64),
                Position = new Point(128, 128),
                IsCollider = true,
            };

            _mesh.Size = new Size(32, 32);
            _mesh.IsCollider = true;

            _mesh.Position = new Point(128, 128 - 32 - 1);

            Assert.False(_mesh.IsColliding(other));

            _mesh.Translate(0, 1);

            Assert.True(_mesh.IsColliding(other));
        }
    }
}
