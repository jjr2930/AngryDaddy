using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rootobject
{
    public List<Levels> Levels;
}

public class Levels
{
    public int id;
    public string subject;
    public int diffcult;
    public List<Chats> chats;
    public string background;
    public List<Answer> answer;
}

public class Chats
{
    public string speaker;
    public string word;
}

public class Answer
{
    public string word;
    public string attacker;
    public int power;
    public int linkid;

}
