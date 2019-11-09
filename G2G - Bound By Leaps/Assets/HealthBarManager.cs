using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] GameObject leftHealthBarPos;
    [SerializeField] RectTransform leftHealthBar;

    [SerializeField] GameObject rightHealthBarPos;
    [SerializeField] RectTransform rightHealthBar;

    [SerializeField] Health leftPlayerHealth;
    [SerializeField] Health rightPlayerHealth;


    private void Update()
    {
        Vector2 leftHealthPosition = leftHealthBarPos.transform.position;
        leftHealthBar.transform.position = leftHealthPosition;
        leftHealthBar.Find("HealthFill").GetComponent<Image>().fillAmount = leftPlayerHealth.GetHealthFraction();


        Vector2 rightHealthPosition = rightHealthBarPos.transform.position;
        rightHealthBar.transform.position = rightHealthPosition;
        rightHealthBar.Find("HealthFill").GetComponent<Image>().fillAmount = rightPlayerHealth.GetHealthFraction();

    }
}
