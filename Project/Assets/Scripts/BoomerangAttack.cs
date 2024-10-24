using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoomerangAttack : MonoBehaviour
{

    public GameObject Boomerang;
    public GameObject Player;
    public PlayerController PlayerControllerScript;
    private bool BoomerangActive = false;
    public Animator AnimController;
    public SpinSpinSpin spinSpin;
    // Start is called before the first frame update
    void Start()
    {
        Boomerang.GetComponent<SpriteRenderer>().enabled = false;  
        AnimController = GetComponent<Animator>();
    }

    // Update is called once per framea
    void Update()
    {
        if(BoomerangActive)
        {
            Boomerang.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.F) && BoomerangActive == false && PlayerControllerScript.BoomerangAcquired == true)
        {
            BoomerangActive = true;
            StartCoroutine(BoomerangThrow());
        }
    }

    IEnumerator BoomerangThrow()
    {
        AnimController.Play("ThrowAnim");
        spinSpin.SpinActive = true;
        if (Player.transform.localScale.x == 1)
        {
            Boomerang.transform.position = Player.transform.position;
            Vector3 BoomerangEndPos = new Vector2(Boomerang.transform.position.x + 5, Boomerang.transform.position.y);
            Boomerang.SetActive(true);
            while (Vector2.Distance(Boomerang.transform.position, BoomerangEndPos) > 0.1f)
            {
                Boomerang.transform.position = Vector2.MoveTowards(Boomerang.transform.position, new Vector2(Boomerang.transform.position.x + 0.05f, Boomerang.transform.position.y), 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            while (Vector2.Distance(Player.transform.position, Boomerang.transform.position) > 0.1f)
            {
                Boomerang.transform.position = Vector2.MoveTowards(Boomerang.transform.position, new Vector2(Boomerang.transform.position.x - 0.05f, Player.transform.position.y), 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            BoomerangActive = false;
            StopCoroutine(BoomerangThrow());
            spinSpin.SpinActive = false;
        }
        else if(Player.transform.localScale.x == -1)
        {
            Boomerang.transform.position = Player.transform.position;
            Vector3 BoomerangEndPos = new Vector2(Boomerang.transform.position.x - 5, Boomerang.transform.position.y);
            Boomerang.SetActive(true);
            while (Vector2.Distance(Boomerang.transform.position, BoomerangEndPos) > 0.1f)
            {
                Boomerang.transform.position = Vector2.MoveTowards(Boomerang.transform.position, new Vector2(Boomerang.transform.position.x - 0.05f, Boomerang.transform.position.y), 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            while (Vector2.Distance(Player.transform.position, Boomerang.transform.position) > 0.1f)
            {
                Boomerang.transform.position = Vector2.MoveTowards(Boomerang.transform.position, new Vector2(Boomerang.transform.position.x + 0.05f, Player.transform.position.y), 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
            BoomerangActive = false;
            StopCoroutine(BoomerangThrow());
            spinSpin.SpinActive = false;
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Breakable"))
            {
                Destroy(collision.gameObject);
            }
        }
}
