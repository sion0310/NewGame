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
        //    obj_pool.Enqueue(obj); //ť�� �Է�
        //    obj.SetActive(false);
        //    //����� ��Ƽ�� ����.
        //    obj.transform.parent = this.transform;
        //    //���θ��� ������Ʈ�� �θ� = Ǯ�� ��ü
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
            //ť���� ��������� ������Ʈ�� ������
            target.SetActive(true);
            //������Ʈ�� Ȱ��ȭ
            return target;
            //��ȯ
        }
        else
        {
            GameObject target = Instantiate(black);
            //�߰��� ��ü ����
            return target;
            //��ȯ
        }
    }
    public void returnEnemy(GameObject obj) //����� ���� ������Ʈ�� ��ȯ�޴� �Լ�
    {
        obj_pool.Enqueue(obj);
        //ť�� �ٽ� �ʾ��~
        obj.SetActive(false);
        //��Ȱ��ȭ
        obj.transform.parent = this.transform;
        //�θ� �ٽ� Ǯ�� ����
    }
}
