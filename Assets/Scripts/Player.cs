using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    float fireDelay = 0; // 공격 딜레이
    public float hp;
    public float curhp;
    public float mp;
    public float curmp;
    public float ex;
    public float curex;
    private float range = 15f;
    public int level = 1; 
    private float remainder;
    public float skil1 = 5f;
    private float skil1_Cool;
    public float skil2 = 10f;
    private float skil2_Cool;
    private float strong_S2 = 5f;
    private float strong_Cool;
    public int power = 15;
    public int power_s = 10;

    private NavMeshAgent nav;
    public Camera cam;
    public Transform target;
    public GameObject target_Tool;
    public GameObject Die_Tool;
    public GameObject resurrection_Point;
    public TextMeshProUGUI name_Target;
    public GameObject hpBar_Target;
    public GameObject skill;
    public Slider hpBar;
    public Slider mpBar;
    public Slider exBar;
    public Image skil_1;
    public Image skil_2;
    public Image skil_2_P;
    public Image skil_2_PP;
    public Text lv;
    TrailRenderer tr;
    public UIManager uiManager;

    bool isWalk;
    bool fDown;
    bool isFireReady; // 공격 준비 
    bool skil1_B = false;
    bool skillOn = false;
    bool skil2_B = false;
    bool strong_B = false;
    public bool target_tool;

    Vector3 HitPos;
    RaycastHit hit;
    RaycastHit hit1;
    Animator ani;
    CapsuleCollider col;
    Weapon weapon;

    private void Awake()
    {
        col = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
        tr = GetComponentInChildren<TrailRenderer>();
        skil1_Cool = skil1;
        skil2_Cool = skil2;

        float x = DataCtrl.Instance._gameData.player_x;
        float y = DataCtrl.Instance._gameData.player_y;
        float z = DataCtrl.Instance._gameData.player_z;
        this.transform.position = new Vector3(x, y, z);

        hp = DataCtrl.Instance._gameData.hp;
        curhp = DataCtrl.Instance._gameData.curhp;
        mp = DataCtrl.Instance._gameData.mp;
        curmp = DataCtrl.Instance._gameData.curmp;
        ex = DataCtrl.Instance._gameData.ex;
        curex = DataCtrl.Instance._gameData.curex;
        level = DataCtrl.Instance._gameData.level;

        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        Move();
        Input_B();
        Attack();
        Targetkip(); // 적 피받아오는 함수
        OnTarget();
        Charactor();
        SkilOn();
    }

    private void Move()
    {
        Vector3 Pos = this.gameObject.transform.position;
        Pos.y -= 0.12f;
        this.gameObject.transform.position = Pos;
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Physics.Raycast() : 광선을 발사해서 부딪히는 오브젝트를 검출
            // 광선에 부딪히는 오브젝트가 있으면  true로 반환

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                MoveTo(hit.point);
                ani.SetBool("IsWalk",true);
                HitPos = hit.point;
                isWalk = true;
            }
            if (hit.transform.gameObject.tag == "Enemy")
                Targeting();
        }
        // 도착 위치에 오면 에니메이터 끄기
        if (Vector3.Distance(this.transform.position, HitPos) <= 0.5f)
        {
            ani.SetBool("IsWalk", false);
            isWalk = false;
        }
        //카메라 따라오게 하기.
        cam.transform.position = new Vector3(
            this.transform.position.x-6.36f,
            this.transform.position.y+6.36f,
            this.transform.position.z-6.36f
            );
    }
    void Input_B()
    {
        fDown = Input.GetButtonDown("Fire1"); //마우스 왼쪽버튼
    }
    public void MoveTo(Vector3 goalPosition)
    {
        //이동 속도 설정
        nav.speed = moveSpeed;
        //목표지점 설정 (목표까지의 경로 계산 후 알아서 움직임)
        nav.SetDestination(goalPosition);
    }
    void Attack()
    {
        fireDelay += Time.deltaTime;
        isFireReady = weapon.rate < fireDelay;

        if (fDown && isFireReady)
        {
            Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray1, out hit1, Mathf.Infinity);
            transform.LookAt(hit1.point);
            ani.SetBool("IsWalk", false);
            nav.SetDestination(gameObject.transform.position);
            weapon.Use();
            ani.SetTrigger("Atk");
            fireDelay = 0;
            
        }
    }
    public void isStopped()
    {
        ani.SetTrigger("IDle");
    }
    public void Hit(int dmg)
    {
        curhp -= dmg;
        //Debug.Log(curhp);
        if (curhp <= 0)
        {
            Debug.Log("죽음");
            ani.Play("Die");
            col.enabled = false;
            gameObject.tag = "Die";
            Die_Tool.SetActive(true);
            
        }
    }
    void Targetkip()
    {
        if (target != null && target.gameObject.tag == "Enemy")
        {
            Enemy enemy = target.GetComponent<Enemy>();
            hpBar_Target.transform.GetChild(0).GetComponent<Image>().fillAmount = enemy.curhp / enemy.hp;
        }
    }
    public void Targeting()
    {
        target = hit.transform;
        name_Target.text = target.GetComponent<Enemy>().name;
        hpBar_Target.SetActive(target.GetComponent<Enemy>().type == "Enemy");
        target_Tool.SetActive(true);
        target_tool = true;
        skillOn = true;
    }
    void OnTarget()
    {
        if (target != null)
        {
            float targetDis = (target.position - gameObject.transform.position).magnitude;

            if (targetDis > range)
            {
                target = null;
                target_Tool.SetActive(false);
                target_tool = false;
            }
        }
    }
    private void Charactor()
    {
        lv.text = "Level "+level.ToString();
        hpBar.value = curhp / hp;
        mpBar.value = curmp / mp;
        exBar.value = curex / ex ;
        skil_1.fillAmount = skil1_Cool / skil1;
        skil_2.fillAmount = skil2_Cool / skil2;
        skil_2_P.fillAmount = strong_Cool / strong_S2;
        if ( curex >= ex) //레벨 업
        {
            remainder = curex - ex;
            ex += 10f;
            curex = 0;
            curex += remainder;
            level += 1;
            power += 5;
            hp += 10f;
            curhp = hp;
            mp += 5f;
            curmp = mp;
            uiManager.GetComponent<UIManager>().skill_Point += 3;
            uiManager.GetComponent<UIManager>().status_Point += 3;
        }
    }
    public void TrOn() { tr.enabled = true; }
    public void TrOff() { tr.enabled = false; }

    private void SkilOn() 
    {
        if (Input.GetKeyDown(KeyCode.E) && !skil1_B && skillOn)
        {
            skil1_Cool = 0;
            skil1_B = true;
            curmp -= 30;
            transform.LookAt(target);
            skill.gameObject.transform.position = this.gameObject.transform.position;
            skill.SetActive(true);
            skillOn = false;
            ani.SetTrigger("Atk2");
        }
        if (Input.GetKeyDown(KeyCode.R) && !skil2_B)
        {
            skil2_Cool = 0;
            power += power_s;
            curmp -= 20;
            skil2_B = true;
            skil_2_PP.gameObject.SetActive(true);
            strong_Cool = strong_S2;
            strong_B = true;
        }

        if (skil1_B)
        {
            skil1_Cool += Time.deltaTime;
            if(skil1_Cool > skil1)
            {
                skil1_B = false;
            }
        }
        if (skil2_B)
        {
            skil2_Cool += Time.deltaTime;
            if(skil2_Cool > skil2)
            {
                skil2_B = false;
            }
        }
        if (strong_B)
        {
            strong_Cool -= Time.deltaTime;
            if(strong_Cool <= 0)
            {
                power -= power_s;
                strong_B = false;
                skil_2_PP.gameObject.SetActive(false);
            }
        }
    }
    public void Resurrection_1()
    {
        curhp = hp / 4;
        col.enabled = true;
        gameObject.tag = "Player";
        ani.SetTrigger("IDle");
        Die_Tool.SetActive(false);
    }
    public void Resurrection_2()
    {
        nav.enabled = false;
        curhp = hp / 4;
        col.enabled = true;
        gameObject.tag = "Player";
        ani.SetTrigger("IDle");
        gameObject.transform.position = resurrection_Point.transform.position;
        nav.enabled = true;
        nav.SetDestination(resurrection_Point.transform.position);
        Die_Tool.SetActive(false);
    }

    //-----------------------------------------------------------------------
    private static Player instance;
    public static Player getInstance
    {
        get
        {
            return instance;
        }
    }

    public void HpFunc(int _val)
    {
        curhp += _val;
        Debug.Log(hp);
    }

    public void MpFunc(int _val)
    {
        curmp += _val;
        Debug.Log(mp);

    }



    //-----------------------------------------------------------------------

}
