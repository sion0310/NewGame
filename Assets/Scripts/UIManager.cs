using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Skill skill;
    public Player player;
    public Text dmg_s1;
    public Text cool_s1;
    public Text cool_s2;
    public Text powerUp;
    public Text level1;
    public Text level2;
    public Text skillPoint;
    public Text statusPoint;
    public Text hp;
    public Text mp;
    public Text str;

    int level_S1 = 1;
    int level_S2 = 1;
    int level_T1 = 1;
    int level_T2 = 1;
    int level_T3 = 1;
    public int skill_Point = 20;
    public int status_Point = 20;

    private void Update()
    {
        dmg_s1.text = skill.dmg.ToString();
        cool_s1.text = player.skil1.ToString();
        cool_s2.text = player.skil2.ToString();
        powerUp.text = player.power_s.ToString();
        level1.text = level_S1.ToString();
        level2.text = level_S2.ToString();
        skillPoint.text = skill_Point.ToString();
        statusPoint.text = status_Point.ToString();
        hp.text = player.hp.ToString();
        mp.text = player.mp.ToString();
        str.text = player.power.ToString();

        if(level_S1 == 10)
        {
            level1.color = Color.red;
        }
        if (level_S2 == 10)
        {
            level2.color = Color.red;
        }
    }
    public void Skill_UP1()
    {
        if (level_S1 < 10)
        {
            if (skill_Point > 0)
            {
                skill_Point -= 1;
                level_S1 += 1;
                skill.dmg += 5;
                player.skil1 -= 0.5f;
            }
        }
    }
    public void Skill_UP2()
    {
        if (level_S2 < 10)
        {
            if (skill_Point > 0)
            {
                skill_Point -= 1;
                level_S2 += 1;
                player.power_s += 3;
                player.skil2 -= 0.5f;
            }
        }
    }
    public void Status_UP_Hp()
    {
        if(status_Point > 0)
        {
            status_Point -= 1;
            player.hp += 10;
        }
    }
    public void Status_UP_Mp()
    {
        if (status_Point > 0)
        {
            status_Point -= 1;
            player.mp += 10;
        }
    }
    public void Status_UP_Str()
    {
        if (status_Point > 0)
        {
            status_Point -= 1;
            player.power += 2;
        }
    }
}
