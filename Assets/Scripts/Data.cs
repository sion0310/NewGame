using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Data {
    //���⿡ ������ �����͵� ������ ����� �д�.
    //���� �ٸ���(SaveBtn)���� ������ ���� �ְ� �����Ѵ�.
    //������ ���̴� ��ũ��Ʈ Start���� ����� ���� �ҷ��;� �����Ҷ� ����� ������ ���۵ȴ�
    //�� private���� ���� ���� ������ ����� public���� �ϳ� �� ���� ���� �Űܼ� �����ϴ� ����� �ֱ���
    //�߿�!! �� Asset���� �ȿ� Data��� ������ �־����!!! �ű� ���� ���� ��

    public int _questIndex = 0;
    
    public float player_x;
    public float player_y;
    public float player_z;

    public float hp = 100;
    public float curhp = 100;
    public float mp = 100;
    public float curmp = 100;
    public float ex = 100;
    public float curex = 0;
    public int level = 1;


}
