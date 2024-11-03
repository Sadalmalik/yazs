using System;
using System.Collections.Generic;

namespace ZombieShooter
{
    public static class Screens
    {
        public static List<Screen> All { get; private set; }
        public static Dictionary<Type, Screen> ByType { get; private set; }
        public static Screen Active;

        static Screens()
        {
            All = new List<Screen>();
            ByType = new Dictionary<Type, Screen>();
        }

        public static void AddScreen(Type type, Screen screen)
        {
            ByType.Add(type, screen);
        }

        public static void AddScreen<T>(T screen) where T :   Screen
        {
            ByType.Add(typeof(T), screen);
        }
        
        public static void HideAll()
        {
            foreach (var screen in All)
            {
                screen.Hide();
            }
        }
    }
}