using UnityEngine;
using UnityEngine.Events;

namespace Game.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] float slowMoMultiplier = 0.5f;
        [SerializeField] float slowMoMinScale = 0.2f;

        [SerializeField] UnityEvent leftPlayerEnabled;
        [SerializeField] UnityEvent rightPlayerEnabled;

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
            }
            else if(Input.GetKey(KeyCode.D))
            {
                rightPlayerEnabled.Invoke();
            }

            if (isSlowMo)
            {
                currSlowMoDelta = Mathf.Clamp(currSlowMoDelta - (Time.deltaTime * slowMoMultiplier), slowMoMinScale, 1f);
            }
            else
            {
                currSlowMoDelta = Mathf.Clamp(currSlowMoDelta + (Time.deltaTime * slowMoMultiplier * 2), slowMoMinScale, 1f);
            }
            Time.timeScale = currSlowMoDelta;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            //print(Time.timeScale);
        }

        public void ToggleSlowMo(bool toggle)
        {
            //print("pee" + toggle) ;
            isSlowMo = toggle;
        }

        public bool GetIsSLowMo()
        {
            return isSlowMo;
        }
    }
}
