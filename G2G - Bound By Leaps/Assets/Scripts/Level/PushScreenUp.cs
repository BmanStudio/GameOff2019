using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScreenUp : MonoBehaviour
{
    float maxPlayerTopPosition = 8f;
    bool needToMove = false;

    float height = 0;

    UIManager uIManager;
    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (needToMove)
        {
            float oldYPos = transform.position.y;
            transform.position = new Vector2(transform.position.x,maxPlayerTopPosition);
            float newYPos = transform.position.y;
            Vector3 cameraPos = Camera.main.transform.position;
            height += (newYPos - oldYPos);
            uIManager.UpdateHeightText(height);
            Camera.main.transform.position = new Vector3(cameraPos.x, cameraPos.y + (newYPos - oldYPos), cameraPos.z);
            if (oldYPos == newYPos)
            {
                needToMove = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.root.CompareTag("Player"))
        {
            float playerTopPosition = collision.transform.position.y + (collision.transform.localScale.y / 2);
            maxPlayerTopPosition = Mathf.Max(maxPlayerTopPosition, playerTopPosition);
            needToMove = true;
        }
    }
}
