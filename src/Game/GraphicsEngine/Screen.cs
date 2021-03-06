﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace BlazeraLib
{
    public enum ScreenType
    {
        LoginScreen,
        LoadingScreen,
        MainTitleScreen,
        GameScreen,
        BattleScreen
    }

    public class ScreenArgs
    {
        Dictionary<string, object> Args;

        public ScreenArgs(Dictionary<string, object> args)
        {
            Args = args;
        }

        public T Get<T>(string argName)
        {
            return (T)Args[argName];
        }
    }

    public abstract class Screen
    {
        const float MAP_VIEW_ZOOM_FACTOR = 1F;

        protected ScreenArgs Args;

        public ScreenArgs GetArgs()
        {
            return Args;
        }

        public Screen(RenderWindow window)
        {
            Window = window;
            GameView = new View(Window.GetView());
            GameView.Zoom(MAP_VIEW_ZOOM_FACTOR);
            GuiView = new View(Window.GetView());
            Gui = new GameBaseWidget(Window, GameView, GuiView);
            Gui.Dimension = GuiView.Size;
        }

        public virtual void FirstInit() { }

        public virtual void Init(ScreenArgs args = null) { }

        public virtual ScreenType Run(Time dt)
        {
            NextScreen = Type;

            Window.SetView(GuiView);

            Gui.Update(dt);
            Gui.Draw(Window);

            while (WindowEvents.EventHappened())
            {
                BlzEvent evt = new BlzEvent(WindowEvents.GetEvent());

                if (evt.Type == EventType.KeyPressed || evt.Type == EventType.KeyReleased)
                    Inputs.Instance.UpdateState(evt);

                GameScoring.EventCount++;

                if (HandleEvent(evt))
                {
                    GameScoring.EventHandled++;
                }
            }

            Window.SetView(GameView);

            return NextScreen;
        }

        public virtual Boolean HandleEvent(BlzEvent evt)
        {
            return Gui.HandleEvent(evt);
        }

        public ScreenType Type
        { 
            get;
            protected set;
        }

        public ScreenType NextScreen
        {
            get;
            set;
        }

        protected RenderWindow Window
        {
            get;
            set;
        }

        protected View GameView
        {
            get;
            set;
        }

        protected View GuiView
        {
            get;
            set;
        }

        public GameBaseWidget Gui
        {
            get;
            set;
        }
    }
}
