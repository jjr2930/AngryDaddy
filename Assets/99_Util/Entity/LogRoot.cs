using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogRoot {
    public List<Log> _log;
}

public class Log
{
    //년월일시분초
    public string _id;
    //0-여자 / 1-남자
    public int _sex;
    //생년 4자리
    public int _year;
    //답변로그
    public List<answerSheet> _answerSheet;
    //최대콤보수
    public int _maxCombo;
    //타임아웃카운트
    public int _timeOutCnt;
    //플레이타임로그
    public List<playTime> _playTimeSec;
    //재시도횟수
    public int _retryCnt;
}

public class answerSheet
{
    //회차번호
    public int _id;
    //문제번호
    public int _qnum;
    //크리티컬 - 3, Hit - 2, 오답 - 1, 오답크리티컬 - 0
    public int _score;
}

public class playTime
{
    //회차번호
    public int _id;
    //플레이시간(초)
    public int _sec;
}