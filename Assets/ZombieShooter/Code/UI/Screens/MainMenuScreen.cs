using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieShooter
{
    public class MainMenuScreen : Screen<MainMenuScreen>
    {
        [SerializeField]
        private Button m_Button_StartPublicLobby;

        [SerializeField]
        private Button m_Button_StartPrivateLobby;

        [SerializeField]
        private Button m_Button_SearchPublicLobby;

        [SerializeField]
        private Button m_Button_Settings;

        [SerializeField]
        private Button m_Button_Exit;

        protected override void Initialize()
        {
            base.Initialize();

            m_Button_StartPublicLobby.onClick.AddListener(OnStartPublicLobby);
            m_Button_StartPrivateLobby.onClick.AddListener(OnStartPrivateLobby);
            m_Button_SearchPublicLobby.onClick.AddListener(OnSearchPublicLobby);
            m_Button_Settings.onClick.AddListener(OnSettings);
            m_Button_Exit.onClick.AddListener(OnExit);
        }

        private void OnStartPublicLobby()
        {
            Service<GameManager>.Get().LoadFirstMap();
        }

        private void OnStartPrivateLobby()
        {
        }

        private void OnSearchPublicLobby()
        {
        }

        private void OnSettings()
        {
        }

        private void OnExit()
        {
            Application.Quit();
        }
    }
}