using System;
using Maze.Engine;
using Maze.Engine.Events;
using Maze.Engine.Input;
using Maze.Engine.Physics;
using Maze.Engine.Renderer;
using Maze.Game;
using Moq;
using Xunit;

namespace Maze.Tests.Engine
{
    public class GameEngineTest
    {
        private GameEngine _engine;
        private Mock<FormInput> _input;
        private Mock<IRenderer> _renderer;
        private Mock<PhysicsEngine> _physics;

        public GameEngineTest()
        {
            var collideContainer = new Mock<GameState>();
            _renderer = new Mock<IRenderer>();
            _input = new Mock<FormInput>();
            _physics = new Mock<PhysicsEngine>(collideContainer.Object);
            _engine = new GameEngine(_renderer.Object, _input.Object, _physics.Object);
        }

        [Fact]
        public void TestShouldRaiseUpdateEvent()
        {
            bool updateRaised = false;

            EventHandler<UpdateEventArgs> updateHandler = (object sender, UpdateEventArgs args) => {
                updateRaised = true;
            };

            // Smaller update step to ensure update isn't skipped for the frame
            _engine.UpdateTimeStep = 0.1f;
            _engine.MaxSkippedFrames = 4;

            _engine.OnUpdate += updateHandler;
            _engine.SimulateFrame();
            _engine.OnUpdate -= updateHandler;

            Assert.True(updateRaised);
        }

        [Fact]
        public void TestRaisesPreUpdateEventBeforeUpdate()
        {
            bool preUpdateRaisedBeforeUpdate = false;
            bool updateRaised = false;

            EventHandler<UpdateEventArgs> preUpdateHandler = (object sender, UpdateEventArgs args) => {
                if (!updateRaised) {
                    preUpdateRaisedBeforeUpdate = true;
                }
            };

            EventHandler<UpdateEventArgs> updateHandler = (object sender, UpdateEventArgs args) => {
                updateRaised = true;
            };

            _engine.UpdateTimeStep = 0.001f;
            _engine.MaxSkippedFrames = 4;

            _engine.PreUpdate += preUpdateHandler;
            _engine.OnUpdate += updateHandler;
            _engine.SimulateFrame();
            _engine.PreUpdate -= preUpdateHandler;
            _engine.OnUpdate -= updateHandler;

            Assert.True(preUpdateRaisedBeforeUpdate);
        }

        [Fact]
        public void TestShouldNotUpdateMoreThanMaxSkippedFramesInSuccession()
        {
            int updates = 0;

            EventHandler<UpdateEventArgs> updateHandler = (object sender, UpdateEventArgs args) => {
                updates++;
            };

            // Make it practically impossible to catch up on frames
            _engine.UpdateTimeStep = 0.001f;
            _engine.MaxSkippedFrames = 4;

            _engine.OnUpdate += updateHandler;
            _engine.SimulateFrame();
            _engine.OnUpdate -= updateHandler;

            Assert.Equal(updates, _engine.MaxSkippedFrames);
        }

        [Fact]
        public void TestRaisesPostUpdateLast()
        {
            bool postUpdateRaiseAfterUpdate = false;
            bool preUpdateRaised = false;
            bool updateRaised = false;

            EventHandler<UpdateEventArgs> preUpdateHandler = (object sender, UpdateEventArgs args) => {
                preUpdateRaised = true;
            };

            EventHandler<UpdateEventArgs> updateHandler = (object sender, UpdateEventArgs args) => {
                updateRaised = true;
            };

            EventHandler<UpdateEventArgs> postUpdateHandler = (object sender, UpdateEventArgs args) => {
                if (preUpdateRaised && updateRaised) {
                    postUpdateRaiseAfterUpdate = true;
                }
            };

            _engine.UpdateTimeStep = 0.001f;
            _engine.MaxSkippedFrames = 4;

            _engine.PreUpdate += preUpdateHandler;
            _engine.OnUpdate += updateHandler;
            GameEngine.PostUpdate += postUpdateHandler;
            _engine.SimulateFrame();
            _engine.PreUpdate -= preUpdateHandler;
            _engine.OnUpdate -= updateHandler;
            GameEngine.PostUpdate += postUpdateHandler;

            Assert.True(postUpdateRaiseAfterUpdate);
        }

        [Fact]
        public void TestDoesNotInvokeRendererMethodsIfRenderIsDisabled()
        {
            _engine.UpdateTimeStep = 0.001f;
            _engine.MaxSkippedFrames = 4;
            _engine.EnableRender = false;

            _engine.SimulateFrame();

            _renderer.Verify(r => r.Frame(), Times.Never);
            _renderer.Verify(r => r.Render(), Times.Never);
        }

        [Fact]
        public void TestDoesInvokesRendererMethodsIfRenderIsEnabled()
        {
            _engine.UpdateTimeStep = 0.001f;
            _engine.MaxSkippedFrames = 4;
            _engine.EnableRender = true;

            _engine.SimulateFrame();

            _renderer.Verify(r => r.Frame(), Times.Once);
            _renderer.Verify(r => r.Render(), Times.Once);
        }
    }
}
