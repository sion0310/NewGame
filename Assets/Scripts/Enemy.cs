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
    public Vector3 originPos; // 처음 시작 위치 값
    Vector3 target; // 목표 지점

    public State state;
    Animator ani;
    NavMeshAgent nav;
    CapsuleCollider col;
    public GameObject item;

    public int moveRange = 5; //움직임 범위
    public int trackingRange = 5; //타겟 추적 범위

    public float speed_NonCombat = 2f; //싸우지 않을때 스피드
    public float speed_Combat = 4f;  //전투 돌입 시 스피드
    float originDis; // 시작 위치와의 거리
    float delayTime_IDle =4f; //대기시간 IDle
    float delayTime_Atk = 2f;

    public bool isDead;

    public string name;
    public string type;
    public float hp;
    public float curhp;

    //시온!
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
            //목표 재설정
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

        //멀어졌을때 다시 제자리로 가는 코드 target 처음 에너미 자리
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
            curhp = hp; // 플레이어가 사정거리를 나가면 다시 풀피;
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
            originDis = (originPos - transform.position).magnitude; //시작위치와 현재위치 사이의 거리 계산

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
