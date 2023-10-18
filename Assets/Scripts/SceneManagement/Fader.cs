using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentActiveFade = null;
        private void Start() 
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void FadeOutInmediate()
        {
            canvasGroup.alpha = 1;
        }
        public IEnumerator FadeOut(float time)
        {
            return Fade(1,time);
        }
        public IEnumerator Fade(float target, float time)
        {
            if(currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(target ,time));
            yield return currentActiveFade;
        }
        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha , target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
        public IEnumerator FadeIn(float time)
        {
            return Fade(0,time);
        }
        public void EndFade()
        {
            StartCoroutine(FadeRoutine(1, 2.5f));
        }
    }
}
