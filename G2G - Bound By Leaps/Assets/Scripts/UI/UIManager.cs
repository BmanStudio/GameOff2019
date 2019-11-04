using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI heightText;

    [SerializeField] GameObject leftShade;
    [SerializeField] GameObject rightShade;

    private void Start()
    {
        UpdateHeightText(0);
    }
    public void UpdateHeightText(float height)
    {
        heightText.text = "Height - " + string.Format("{0:0.00}", height);
    }

    public void SwapPlayer(float playerIndex)
    {
        if (playerIndex == 0)
        {
            //rightShade.SetActive(true);
            rightShade.GetComponent<CanvasGroup>().alpha = 1;
            //leftShade.SetActive(false);
            leftShade.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if (playerIndex == 1)
        {
            leftShade.GetComponent<CanvasGroup>().alpha = 1;
            rightShade.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}
