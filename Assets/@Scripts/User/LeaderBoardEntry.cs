using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardEntry
{
    public string uid;
    public int score = 0;

    public LeaderBoardEntry() { }
    public LeaderBoardEntry(string uid, int score)
    {
        this.uid = uid;
        this.score = score;
    }
    public Dictionary<string, Object> ToDictionary() 
    {
        Dictionary<string,Object> result = new Dictionary<string, Object> ();
        //result["uid"] = uid;
        //result["score"] = score;

        return result;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
