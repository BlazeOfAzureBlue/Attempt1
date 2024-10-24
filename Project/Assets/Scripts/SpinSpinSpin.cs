using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSpinSpin : MonoBehaviour
{
    private Animator AnimController;
    public GameObject player;
    public bool SpinActive;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        AnimController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SpinActive)
        {
            AnimController.Play("New Animation");
        }
        else
        {
            AnimController.Play("stop");
            transform.position = player.transform.position + new Vector3(0, 0.3f, 0);
        }
    }
}
