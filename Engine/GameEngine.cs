using System;
using Maze.Engine.Diagnostics;
using Maze.Engine.Events;
using Maze.Engine.Input;
using Maze.Engine.Renderer;
using Win32;

namespace Maze.Engine
{
    public class GameEngine
    {
        /// <value>
        /// Whether rendering procedures should be invoked.
        /// </value>
        public bool EnableRender = true;

        /// <value>
        /// Maximum number of subsequent updates, before a render is called.
        /// </value>
        public byte MaxSkippedFrames { get; set; } = 10;

        /// <value>
        /// Fixed rate in (ms) to update at.
        /// </value>
        public float UpdateTimeStep { get; set; } = 33f;

        /// <value>
        /// The minimum time in (ms) to spend processing a frame.
        /// Effectively limits the framerate to no more than <c>1000 / MinFrameTime</c>.
        /// Set to 0 to disable.
        /// </value>
        public float MinFrameTime { get; set; } = 16.66f;

        /// <value>
        /// Enables querying keyboard input for current frame.
        /// </value>
        public IQueryableFormInput Input
        {
            get
            {
                return (IQueryableFormInput)_input;
            }
        }

        private FormInput _input;

        /// <summary>
        /// Raised just before entering main loop.
        /// </summary>
        public event EventHandler<InitEventArgs> OnInit;

        /// <summary>
        /// Raised before state update for the frame is requested.
        /// </summary>
        public event EventHandler<UpdateEventArgs> PreUpdate;

        /// <summary>
        /// Raised when game state needs to be updated.
        /// May be raised multiple times per frame, before drawing.
        /// </summary>
        public event EventHandler<UpdateEventArgs> OnUpdate;

        /// <summary>
        /// Raised when renderer prepares the current frame for drawing.
        /// </summary>
        public event EventHandler<FrameEventArgs> OnFrame;

        private IRenderer _renderer;

        private PerformanceMonitor _monitor;

        public GameEngine(IRenderer renderer, FormInput input)
        {
            _renderer = renderer;
            _input = input;

            _monitor = new PerformanceMonitor();
            PreUpdate += _monitor.CaptureFrame;

            // TODO: There's probably a better way to achieve event propogation.
            // Or perhaps event in renderer is unnecessary.
            renderer.OnFrame += (object sender, FrameEventArgs e) => {
                OnFrame(this, e);
            };
        }


        public void Loop()
        {
            double unprocessedTime = 0;
            int updates = 0;

            _renderer.Initialize();
            _input.Initialize();

            OnInit.Raise(this, null);

            // TODO: Need a way to break the loop from outside.
            while (IsApplicationIdle())
            {
                LimitFramerate();

                // TODO: Not sure about creating hundreds of objects per second.
                // Doesn't seem to cause high memory usage, but further investigation is needed.
                PreUpdate.Raise(this, new UpdateEventArgs(_monitor.FrameRate, _monitor.LastFrameTime, UpdateTimeStep));

                unprocessedTime += _monitor.LastFrameTime;
                updates = 0;

                while (unprocessedTime >= UpdateTimeStep && updates < MaxSkippedFrames)
                {
                    OnUpdate.Raise(this, new UpdateEventArgs(_monitor.FrameRate, _monitor.LastFrameTime, UpdateTimeStep));

                    unprocessedTime -= UpdateTimeStep;
                    updates++;
                }

                if (EnableRender) {
                    _renderer.Frame();
                    _renderer.Render();
                }
            }
            // TODO: Probably clean up when loop is broken.
        }

        private void LimitFramerate()
        {
            // Have to busy wait while polling, to properly clamp the frame rate.
            while (_monitor.CurrentFrameTime < MinFrameTime)
            {
                System.Threading.Thread.Sleep(1);
            }
        }

        private bool IsApplicationIdle()
        {
            return !Win32Native.PeekMessage(out _, IntPtr.Zero, (uint) 0, (uint) 0, (uint) 0);
        }
    }
}
