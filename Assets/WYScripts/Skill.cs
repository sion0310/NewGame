using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Transform target;
    public float speed;
    public GameObject hitEffect;
    public Player player;
    public int dmg = 20;


    private void OnEnable()
    {
        target = player.GetComponent<Player>().target;
        StartCoroutine("Arrow");
        
    }
    IEnumerator Arrow()
    {
        while (target != null)
        {
            Vector3 dir = target.position + new Vector3(0, 2, 0);
            transform.LookAt(dir);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            yield return null;
        }
    }
    void OnHitEffect(Vector3 hitPoint)
    {
        hitEffect.transform.position = hitPoint + new Vector3(0, 1, 0);
        hitEffect.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            StopCoroutine("Arrow");
            gameObject.SetActive(false);
            Enemy enemy = other.GetComponent<Enemy>();
            int ran = Random.Range(0,10);
            enemy.Hit(dmg+ran);

            Vector3 hitPoint = (other.transform.position + transform.position) * 0.5f;
            OnHitEffect(hitPoint);
        }
    }
}
