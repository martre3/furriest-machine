﻿using System;
using Maze.Engine.Diagnostics;
using Maze.Engine.Events;
using Maze.Engine.Input;
using Maze.Engine.Renderer;
using Maze.Engine.Physics;
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

        // TODO: Why is this static?
        public static event EventHandler<UpdateEventArgs> PostUpdate;

        /// <summary>
        /// Raised when renderer prepares the current frame for drawing.
        /// </summary>
        public event EventHandler<FrameEventArgs> OnFrame;

        private IRenderer _renderer;

        private PerformanceMonitor _monitor { get; }

        private SimulationEngine _simulationEngine { get; }

        private double _unprocessedTime = 0;

        private int _updates = 0;

        public GameEngine(FormInput input, SimulationEngine simulationEngine)
        {
            _input = input;
            _monitor = new PerformanceMonitor();
            _simulationEngine = simulationEngine;
            PreUpdate += _monitor.CaptureFrame;
        }

        public GameEngine(IRenderer renderer, FormInput input, SimulationEngine simulationEngine): this(input, simulationEngine)
        {
            _renderer = renderer;

            // TODO: There's probably a better way to achieve event propogation.
            // Or perhaps event in renderer is unnecessary.
            renderer.OnFrame += (object sender, FrameEventArgs e) => {
                OnFrame(this, e);
            };
        }


        public void Loop()
        {
            _unprocessedTime = 0;
            _updates = 0;

            if (EnableRender) {
                _renderer.Initialize();
            }

            OnInit.Raise(this, null);

            // TODO: Need a way to break the loop from outside.
            while (IsApplicationIdle())
            {
                LimitFramerate();

                SimulateFrame();
            }
            // TODO: Probably clean up when loop is broken.
        }

        public void SimulateFrame()
        {
            // TODO: Not sure about creating hundreds of objects per second.
            // Doesn't seem to cause high memory usage, but further investigation is needed.
            PreUpdate.Raise(this, new UpdateEventArgs(_monitor.FrameRate, _monitor.LastFrameTime, UpdateTimeStep));

            _unprocessedTime += _monitor.LastFrameTime;
            _updates = 0;

            while (_unprocessedTime >= UpdateTimeStep && _updates < MaxSkippedFrames)
            {
                OnUpdate.Raise(this, new UpdateEventArgs(_monitor.FrameRate, _monitor.LastFrameTime, UpdateTimeStep));

                _simulationEngine.Simulate();

                _unprocessedTime -= UpdateTimeStep;
                _updates++;
            }

            PostUpdate.Raise(this, new UpdateEventArgs(_monitor.FrameRate, _monitor.LastFrameTime, UpdateTimeStep));

            if (EnableRender) {
                _renderer.Frame();
                _renderer.Render();
            }
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
