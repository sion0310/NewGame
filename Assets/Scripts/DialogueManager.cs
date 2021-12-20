using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Delegate scripts")]
    public InteractionCtrl interCtrl = null;
    public DialogueUICtrl dialUI = null;
    public QuestPro questPro = null;

    [Header("scripts")]
    public DialogueData dialData = null;
    public NpcManager npcMg = null;

    string playerName = "�ÿ�";   //���߿� ��ǲ�ʵ�� �Է¹��� ���� �־��ش�

    
    DialogueData.Values values;

    public int questIndex;
    int dialIndex = 0;

    public bool questDone;

    private void Start()
    {
        questIndex = DataCtrl.Instance._gameData._questIndex;
        //�ݹ� �Լ���
        interCtrl.interact_pro = ShowDialogue;
        dialUI.exit_pro = ExitDial;
        dialUI.accept_pro = AcceptQuest;
        questPro.questAchieve = AchievedQuest;

        //���ۺ��� ����Ʈ �־��ֱ�
        SetQuestInfo();
    }
    void AchievedQuest(bool isAchieved)
    {
        if (isAchieved)
        {
            npcMg.AchievedQuest();
        }
    }

    //����Ʈ�� ���۵ɶ� ���� �ٲ��ִ°͵�
    void SetQuestInfo()
    {
        //����Ʈ ������ ���� �������� ����Ʈ ������
        values = dialData.GetQuestValues(questIndex);
        //����Ʈ ������ ���� �ٲ��ִ� npc����
        npcMg.SetNpcStateInfo(values._giveNpcNum, values._doneNpcNum);
    }

    //��ȭâ�� ������ ȣ��Ǵ� �Լ�
    void ShowDialogue(GameObject hitobj)
    {
        //�޾ƿ� ������Ʈ�� npc�ѹ��� ������ npcNum�� �ִ´�
        int npcNum = hitobj.GetComponent<NpcInfo>().GetNum();
        //����Ʈ �Ŵ����� ��ȭ�� npc�� �������ִ� �Լ��� �־��ش�
        npcMg.SetTalkNpc(npcNum, interCtrl.isTalking);

        //header�� npc���¿� ���� �ٸ��� �����´�
        string header = npcMg.GetHeader(values._giveQ, values._doneQ);
        //������ �������� �ְ� ��縦 �����´�
        string dialogue = GetDialogue(npcNum, header).Replace("����", playerName);
        //Ui�� ����ش�.
        dialUI.OpenDialBar(dialogue);

    }

    public string GetDialogue(int _npcNum, string _header)
    {
        //_npcNum��° ��ũ��Ʈ�� _header���� dialIndex��° ��縦 �����´�.
        string _dialogue = dialData.GetDialogue(_npcNum, dialIndex, _header);
        //���� ��簡 ��������
        if (_dialogue == "end")
        {
            //��� ������ ��� ��ȯ
            _dialogue = dialData.GetDialogue(_npcNum, dialIndex - 1, _header);
            //��ȭ�� ��ģ npc���°� havequest�̸� ����Ʈâ ����
            ShowQuest();

            return _dialogue;
        }
        dialIndex++;
        //������ ��� ��ȯ
        return _dialogue;
    }

    void ShowQuest()
    {
        if (npcMg.OpenQuestBar())
        {
            if (npcMg.NextQuest()) questDone = true;
            else questDone = false;
            dialUI.OpenQuestBar(values._questName, values._questExplan, questDone);
        }
    }


    void ExitDial()
    {
        interCtrl.isTalking = false;
        dialIndex = 0;
    }

    void AcceptQuest()
    {
        interCtrl.isTalking = false;
        dialIndex = 0;
        if (npcMg.NextQuest())
        {
            if (questIndex >= 4)
            {
                //�Ϸ�npc���� �ٲ��ְ�
                npcMg.DoneQuest();
                //���� ����Ʈ�� �Ѿ��
                questIndex++;
                questPro.isAchieved = false;
            }
            else
            {
                //�Ϸ�npc���� �ٲ��ְ�
                npcMg.DoneQuest();
                //���� ����Ʈ�� �Ѿ��
                questPro.condition = "";
                questPro.isAchieved = false;
                questIndex++;
                //����Ʈ ������ �ٽ� �ٲ��ش�.
                SetQuestInfo();
            }
        }
        else
        {
            npcMg.AcceptQuest();
            if (values._condition == "add")
            {
                questPro.AddItem();
            }
            if (values._condition == "enemy")
            {
                questPro.enemyCount = 0;
            }
            if (values._condition == "none")
            {
                questPro.isAchieved = true;
            }
            questPro.condition = values._condition.ToString();
        }
    }



}
