using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rate;
    public BoxCollider meleearea;
    public int power1;

    private void Update()
    {
        power1 = GetComponentInParent<Player>().power;
    }
    public void Use()
    {
        StartCoroutine("Swing");
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleearea.enabled = true;
        yield return new WaitForSeconds(0.3f);
        meleearea.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        int dmg = Random.Range(0, 20);
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Hit(dmg+power1);
        }
    }

}
