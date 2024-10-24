using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject StartPosition;
    public GameObject EndPosition;
    public GameObject platform;

    private bool EndMovePosition = false;
    private bool Moving = false;

    // Update is called once per frame
    void Update()
    {
        if (Moving == false)
        {
            StartCoroutine(PlatformMove());
        }
    }

    IEnumerator PlatformMove()
    {
        if (EndMovePosition == false)
        {
            Moving = true;
            while (Vector2.Distance(platform.transform.position, EndPosition.transform.position) > 1f)
            {
                platform.transform.position = Vector2.MoveTowards(platform.transform.position, new Vector2(platform.transform.position.x + 0.02f, platform.transform.position.y), 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            EndMovePosition = true;
        }
        else
        {
            Moving = true;
            while (Vector2.Distance(platform.transform.position, StartPosition.transform.position) > 1f)
            {
                platform.transform.position = Vector2.MoveTowards(platform.transform.position, new Vector2(platform.transform.position.x - 0.02f, platform.transform.position.y), 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            EndMovePosition = false;
        }
        yield return new WaitForSeconds(1f);
        Moving = false;
        StopCoroutine(PlatformMove());
    }
}
