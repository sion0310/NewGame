using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        IDle, Follow, Attack, Die
    }
    public GameObject player;
    public Vector3 originPos; // ó�� ���� ��ġ ��
    Vector3 target; // ��ǥ ����

    public State state;
    Animator ani;
    NavMeshAgent nav;
    CapsuleCollider col;
    public GameObject item;

    public int moveRange = 5; //������ ����
    public int trackingRange = 5; //Ÿ�� ���� ����

    public float speed_NonCombat = 2f; //�ο��� ������ ���ǵ�
    public float speed_Combat = 4f;  //���� ���� �� ���ǵ�
    float originDis; // ���� ��ġ���� �Ÿ�
    float delayTime_IDle =4f; //���ð� IDle
    float delayTime_Atk = 2f;

    public bool isDead;

    public string name;
    public string type;
    public float hp;
    public float curhp;

    //�ÿ�!
    public QuestPro questPro;

    private void OnEnable()
    {
        originPos = transform.position;
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<CapsuleCollider>();
        gameObject.tag = "Enemy";
        StartCoroutine("EnemyAI");
        curhp = hp;
        col.enabled = true;
        state = State.IDle;
        isDead = false;
    }
    private void Update()
    {
        PosDwon();
        switch (state)
        {
            case State.IDle:
                IDle();
                break;
            case State.Follow:
                Follow();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Die:
                Die();
                break;
        }
    }
    void PosDwon()
    {
        Vector3 Pos = this.gameObject.transform.position;
        Pos.y -= 0.2f;
        this.gameObject.transform.position = Pos;
    }
    void IDle()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) <= trackingRange && player.tag != "Die")
        {
            state = State.Follow;
            ani.SetBool("IsWalk", false);
        }
        nav.speed = speed_NonCombat;
        delayTime_IDle += Time.deltaTime;
        if (delayTime_IDle >= 5f)
        {
            //��ǥ �缳��
            target = new Vector3(transform.position.x +
                Random.Range(-1 * moveRange, moveRange),
                100f,
                transform.position.z +
                Random.Range(-1 * moveRange, moveRange));

            RaycastHit HIT;
            if (Physics.Raycast(target, Vector3.down, out HIT))
            {
                if (HIT.collider.CompareTag("Land"))
                {
                    target.y = HIT.point.y;
                }
                else
                {
                    target = originPos;
                }
            }
            delayTime_IDle = 0;
            nav.SetDestination(target);
            ani.SetBool("IsWalk", true);
        }
        if(Vector3.Distance(this.transform.position, target) <= 0.5f)
        {
            ani.SetBool("IsWalk", false);
        }

        //�־������� �ٽ� ���ڸ��� ���� �ڵ� target ó�� ���ʹ� �ڸ�
        if (originDis >= moveRange)
        {
            target = originPos;
            nav.SetDestination(target);
            ani.SetBool("IsWalk", true);
        }
    }

    void Follow()
    {
        Vector3 moveVector = player.transform.position - transform.position;
        Vector3 playerPos = player.transform.position - originPos;
        nav.speed = speed_Combat;
        target = player.transform.position;
        nav.SetDestination(target);
        nav.stoppingDistance = 2.5f;
        ani.SetBool("IsRun", true);
        if (playerPos.magnitude >= 10f)
        {
            target = originPos;
            nav.SetDestination(target);
            ani.SetBool("IsRun", false);
            ani.SetBool("IsWalk", true);
            nav.stoppingDistance = 0.5f;
            state = State.IDle;
            curhp = hp; // �÷��̾ �����Ÿ��� ������ �ٽ� Ǯ��;
        }
        if(moveVector.magnitude <= 2.5f)
        {
            state = State.Attack;
            ani.SetBool("IsRun", false);
        }
        
    }
    void Attack()
    {
        delayTime_Atk += Time.deltaTime;
        Vector3 moveVector = player.transform.position - transform.position;
        target = player.transform.position;
        target.y = 0;
        if (delayTime_Atk >= 2.5f)
        {
            if(player.tag == "Die")
            {
                state = State.IDle;
                ani.Play("Idle");
            }
            transform.LookAt(target);
            ani.SetTrigger("IsAtk");
            delayTime_Atk = 0;
        }
        if (moveVector.magnitude > 2.5f)
        {
            state = State.Follow;
        }
    }
    public void Die()
    {
        if (player.GetComponent<Player>().target == transform)
        {
            player.GetComponent<Player>().target = null;
            player.GetComponent<Player>().target_Tool.SetActive(false);
        }
        isDead = true;
        ani.SetTrigger("Die");
        
        gameObject.tag = "Dead";
        col.enabled = false;
        StartCoroutine("PoolingEnemy");
    }

    IEnumerator EnemyAI()
    {
        while (true)
        {
            originDis = (originPos - transform.position).magnitude; //������ġ�� ������ġ ������ �Ÿ� ���

            yield return null;
        }
    }
    public void Hit(int dmg)
    {
        curhp -= dmg;
        if(curhp <= 0)
        {
            state = State.Die;
        }
    }
    public void Atk()
    {
        int dmg = Random.Range(0, 15);
        player.GetComponent<Player>().Hit(dmg);
    }
    public void Drop()
    {
        Vector3 pos = gameObject.transform.position;
        pos.y += 0.5f;
        Instantiate(item, pos, Quaternion.identity);

        player.GetComponent<Player>().curex += 30f;
        if (questPro.condition == "enemy")
        {
            questPro.enemyCount++;
        }
    }
    IEnumerator PoolingEnemy()
    {
        yield return new WaitForSeconds(5f);
        Pooling.getInstance.returnEnemy(this.gameObject);
    }

}
