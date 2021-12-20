using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool SpawnEnemy2 = true;
    
    private void Update()
    {

        //for(int i=0;i <)
        if (Pooling.getInstance.obj_pool.Count >=1)
        {
            
            if (SpawnEnemy2)
            {
                StartCoroutine("SpawnEnemy");
            }
        }
    }
    IEnumerator SpawnEnemy()
    {
        SpawnEnemy2 = false;
        yield return new WaitForSeconds(3f);
        GameObject obj = Pooling.getInstance.getEnemy();
        //obj.transform.position = enemy.originPos;
        obj.transform.parent = this.transform;
        SpawnEnemy2 = true;
    }
}
