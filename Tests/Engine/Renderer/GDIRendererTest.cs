using System;
using Maze.Engine.Events;
using Maze.Engine.Renderer;
using Maze.Windows;
using Moq;
using Xunit;

namespace Maze.Tests.Engine
{
    public class GDIRendererTest
    {
        private GDIRenderer _renderer;
        private Mock<IGraphicsForm> _form;

        public GDIRendererTest()
        {
            _form = new Mock<IGraphicsForm>();
            _renderer = new GDIRenderer(_form.Object);
        }

        [Fact(Skip = "BufferedGraphics can't be mocked.")]
        public void TestInitializesGraphicsBufferOnInitialize()
        {
            _renderer.Initialize();

            _form.Verify(f => f.InitGraphicsBuffers(), Times.Once);
        }

        [Fact(Skip = "BufferedGraphics can't be mocked.")]
        public void TestRaisesOnFrameEvent()
        {
            bool onFrameCalled = false;

            EventHandler<FrameEventArgs> frameHandler = (object sender, FrameEventArgs args) => {
                onFrameCalled = true;
            };

            _renderer.Frame();

            Assert.True(onFrameCalled);
        }
    }
}
