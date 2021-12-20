using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Data {
    //여기에 저장할 데이터들 변수를 만들어 둔다.
    //이후 다른곳(SaveBtn)에서 변수에 값을 넣고 저장한다.
    //변수가 쓰이는 스크립트 Start에서 저장된 값을 불러와야 시작할때 저장된 값으로 시작된다
    //꼭 private으로 쓰고 싶은 변수는 저장용 public변수 하나 더 만들어서 값을 옮겨서 저장하는 방법도 있긴함
    //중요!! 꼭 Asset폴더 안에 Data라는 폴더가 있어야함!!! 거기 저장 파일 들어감

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
