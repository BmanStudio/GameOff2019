using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace Game.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] PostProcessVolume playerPostProcessVol;

        [SerializeField] float slowMoMultiplier = 0.5f;
        [SerializeField] float slowMoMinScale = 0.1f;

        [SerializeField] UnityEvent leftPlayerEnabled;
        [SerializeField] UnityEvent rightPlayerEnabled;
        [SerializeField] UnityEvent gameStop;

        private bool isSlowMo = false;
        private float currSlowMoDelta = 1;
        private void Start()
        {
            leftPlayerEnabled.Invoke();
        }
        private void Update()
        {

            if (Input.GetKey(KeyCode.A))
            {
                leftPlayerEnabled.Invoke();
                ToggleSlowMo(false);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                rightPlayerEnabled.Invoke();
                ToggleSlowMo(false);
            }

            if (isSlowMo)
            {
                currSlowMoDelta = Mathf.Clamp(currSlowMoDelta - (Time.deltaTime * slowMoMultiplier), slowMoMinScale, 1f);
            }
            else
            {
                currSlowMoDelta = Mathf.Clamp(currSlowMoDelta + (Time.deltaTime * slowMoMultiplier * 2), slowMoMinScale, 1f);
                playerPostProcessVol.weight = 1 - (currSlowMoDelta - slowMoMinScale);
            }
            Time.timeScale = currSlowMoDelta;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            //print(Time.timeScale);
        }

        public void ActiveVignetteFXSlowMotion(GameObject player)
        {
            Vignette vignette;
            playerPostProcessVol.profile.TryGetSettings(out vignette);
            vignette.center.value = Camera.main.WorldToViewportPoint(player.transform.position);
            playerPostProcessVol.weight = 1 - (currSlowMoDelta - slowMoMinScale);
        }

        public void ToggleSlowMo(bool toggle)
        {
            //Debug.Break();
            isSlowMo = toggle;
        }

        public bool GetIsSLowMo()
        {
            return isSlowMo;
        }

        public void StopGame()
        {
            gameStop.Invoke();
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 1000);
        }

        public void RestartGame()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}
