using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public GameObject black;
    public Queue<GameObject> obj_pool = new Queue<GameObject>();
    static Pooling instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(instance.gameObject);
        }
    }
    void Start()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    GameObject obj = Instantiate(black);
        //    obj_pool.Enqueue(obj); //큐에 입력
        //    obj.SetActive(false);
        //    //사용전 엑티브 꺼줌.
        //    obj.transform.parent = this.transform;
        //    //새로만든 오브젝트의 부모 = 풀링 객체
        //}
    }
    public static Pooling getInstance
    {
        get
        {
            return instance;
        }
    }
    public GameObject getEnemy()
    {
        if (obj_pool.Count > 0)
        {
            GameObject target = obj_pool.Dequeue();
            //큐에서 사용대기중인 오브젝트를 꺼내고
            target.SetActive(true);
            //오브젝트를 활성화
            return target;
            //반환
        }
        else
        {
            GameObject target = Instantiate(black);
            //추가로 객체 만듬
            return target;
            //반환
        }
    }
    public void returnEnemy(GameObject obj) //사용이 끝난 오브젝트를 반환받는 함수
    {
        obj_pool.Enqueue(obj);
        //큐에 다시 너어라~
        obj.SetActive(false);
        //비활성화
        obj.transform.parent = this.transform;
        //부모를 다시 풀로 변경
    }
}
