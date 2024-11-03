using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ZombieShooter
{
    public abstract class Screen : MonoBehaviour
    {
        private void Awake()
        {
            Initialize();
        }
        
        protected virtual void Initialize()
        {
            Hide();
            Screens.AddScreen(this.GetType(), this);
        }

        public virtual void Show()
        {
            if (Screens.Active != null)
            {
                Screens.Active.Hide();
            }

            Screens.Active = this;
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            if (Screens.Active == this)
            {
                Screens.Active = null;
            }
        }
    }

    public class Screen<T> : Screen where T : Screen<T>
    {
        public static T Instance { get; private set; }

        protected override void Initialize()
        {
            if (Instance != null)
            {
                throw new Exception($"Screen {typeof(T).Name} is already have instance!");
            }
            Instance = (T) this;
            base.Initialize();
        }
    }
}