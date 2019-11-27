using System;
using System.Collections.Generic;
using System.Drawing;
using Maze.Engine.Physics;
using Maze.Game.Objects;
using Moq;
using Xunit;

namespace Maze.Tests.Engine
{
    internal class PhysicsTestGameObject : GameObject
    {
        private Action<Collision> _collisionCallback;

        public PhysicsTestGameObject(Size size, Point position, Action<Collision> collisionCallback)
        {
            _mesh.Size = size;
            _mesh.Position = position;
            _mesh.IsCollider = true;
            _collisionCallback = collisionCallback;
        }

        public void TranslateMesh(int x, int y)
        {
            _mesh.Translate(x, y);
        }

        public override void Draw(Graphics surface)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(Collision collision)
        {
            _collisionCallback(collision);
        }
    }

    public class PhysicsEngineTest
    {
        private PhysicsEngine _physics;
        private Mock<ICollidableContainer> _container;

        public PhysicsEngineTest()
        {
            _container = new Mock<ICollidableContainer>();
            _physics = new PhysicsEngine(_container.Object);
        }

        [Fact]
        public void Test()
        {
            bool o1Collided = false;
            bool o2Collided = false;

            Action<Collision> o1CollisionHandler = (Collision collision) => {
                o1Collided = true;
            };

            Action<Collision> o2CollisionHandler = (Collision collision) => {
                o2Collided = true;
            };

            var colliders = new List<ICollidable>();
            var o1 = new PhysicsTestGameObject(new Size(64, 64), new Point(128, 128), o1CollisionHandler);
            var o2 = new PhysicsTestGameObject(new Size(32, 32), new Point(128, 128 - 32 - 1), o2CollisionHandler);


            colliders.Add(o1);
            colliders.Add(o2);

            _container.Setup(c => c.GetDynamicObjects()).Returns(colliders);
            _container.Setup(c => c.GetAllColliders()).Returns(colliders);

            _physics.Simulate();

            Assert.False(o1Collided);
            Assert.False(o2Collided);

            o2.TranslateMesh(0, 1);

            _physics.Simulate();

            Assert.True(o1Collided);
            Assert.True(o2Collided);
        }
    }
}
