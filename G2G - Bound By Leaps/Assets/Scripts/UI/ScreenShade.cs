using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShade : MonoBehaviour
{
    [SerializeField] GameObject midWall;
    [SerializeField] GameObject closeBorderWall;
    [SerializeField] GameObject farBorderWall;

    private float midWallDistance;
    private float borderWallsDistance;

    // Start is called before the first frame update
    void Start()
    {
        borderWallsDistance = Vector3.Distance(closeBorderWall.transform.position, farBorderWall.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        midWallDistance = Vector3.Distance(midWall.transform.position,closeBorderWall.transform.position)/borderWallsDistance;
        transform.localScale = new Vector2(midWallDistance, 1);
    }
}
