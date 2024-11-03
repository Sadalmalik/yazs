using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ZombieShooter
{
    public class LoadingScreen : Screen<LoadingScreen>
    {
        [SerializeField]
        private CanvasGroup m_Group;

        [SerializeField]
        private float m_FadeDuration;

        [SerializeField]
        private TMP_Text m_Label;

        public void SetText(string text)
        {
            m_Label.SetText(text);
        }
        
        public override void Show()
        {
            gameObject.SetActive(true);
            DOTween.To(
                () => m_Group.alpha,
                value => m_Group.alpha = value,
                1, m_FadeDuration
            );
        }

        public override void Hide()
        {
            DOTween.To(
                () => m_Group.alpha,
                value => m_Group.alpha = value,
                0, m_FadeDuration
            ).OnComplete(() => { gameObject.SetActive(false); });
        }

        public async Task ShowAsync()
        {
            gameObject.SetActive(true);
            var result = new TaskCompletionSource<bool>();
            DOTween.To(
                () => m_Group.alpha,
                value => m_Group.alpha = value,
                1, m_FadeDuration
            ).OnComplete(() => { result.SetResult(true); });
            await result.Task;
        }

        public async Task HideAsync()
        {
            var result = new TaskCompletionSource<bool>();
            DOTween.To(
                () => m_Group.alpha,
                value => m_Group.alpha = value,
                0, m_FadeDuration
            ).OnComplete(() => { result.SetResult(true); });
            await result.Task;
            gameObject.SetActive(false);
        }
    }
}