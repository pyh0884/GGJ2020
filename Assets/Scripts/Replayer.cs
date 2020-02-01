using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Record
{
    public ActionType currentActionHorizon;
    public ActionType currentActionVertical;
    public float time;

    public Record(ActionType currentActionHorizon, ActionType currentActionVertical, float time) : this()
    {
        this.currentActionHorizon = currentActionHorizon;
        this.currentActionVertical = currentActionVertical;
        this.time = time;
    }
}


public class Replayer : MonoBehaviour
{
    
    public static List<List<Record>> Records = new List<List<Record>>() {new List<Record>()};
    //public static List<bool> underControll;
    public GameObject Character;
    public Vector3 StartPoint;

    private List<GameObject> DeadBodies;
    private List<int> indexes;
    public static List<bool> ended;
    

    void Start()
    {

    }

    void Update()
    {
        DoRecord();
    }

    // init dead bodies at start point
    public void InitCharacters()
    {
        DeadBodies = new List<GameObject>();
        //underControll = new List<bool>();
        ended = new List<bool>();
        indexes = new List<int>();
        for (int i = 0; i < GameManager.turn; i++)
        {
            GameObject tmpG = Instantiate(Character, StartPoint, Quaternion.Euler(0, 0, 0));
            tmpG.GetComponent<DeadBody>().number = i;
            DeadBodies.Add(tmpG);
            indexes.Add(0);
            //underControll.Add(true);
            ended.Add(false);
        }
        
    }

    // follow the record, to do actions they should do
    private void DoRecord()
    {
        if (Time.timeScale != 0)
        {
            for(int i = 0; i < GameManager.turn; i++)
            {
                TryDoAction(i);
            }
        }
    }

    // if reached the recorded time, let the dead body do certain action
    private void TryDoAction(int index)
    {
        if (Records[index] == null || ended[index])
            return;
        else if (Records[index].Count <= indexes[index] && !ended[index])
        {
            ended[index] = true;
            DeadBodies[index].AddComponent<CannonBullet>();
            DeadBodies[index].layer = 11;
            return;
        }
      
 

        int i = indexes[index];
        Record tmpr = Records[index][i];
        if (Time.timeSinceLevelLoad >= tmpr.time)
        {
            indexes[index]++;
            var ctrl = DeadBodies[index].GetComponent<DeadBody>();
            // Horizon
            if (tmpr.currentActionHorizon == ActionType.IDLE)
                ctrl.ChangeDirection(0);
            else if (tmpr.currentActionHorizon == ActionType.LEFT)
                ctrl.ChangeDirection(-1);
            else if (tmpr.currentActionHorizon == ActionType.RIGHT)
                ctrl.ChangeDirection(1);
            else
                Debug.LogWarning("Something goes wrong in Replayer.TryDoAction()");

            // Vertical
            if (tmpr.currentActionVertical == ActionType.IDLE)
            { }// pass
            else if (tmpr.currentActionVertical == ActionType.JUMP)
                ctrl.Jump();
            else
                Debug.LogWarning("Something goes wrong in Replayer.TryDoAction()");
        }
    }

    /* ABANDON
    // end a certain body
    public void EndLife(GameObject obj)
    {
        for(int i = 0; i < GameManager.turn; i++)
        {
            if (DeadBodies[i].Equals(obj)&& ended[i] == false)
            {
                ended[i] = true;
                obj.GetComponent<DeadBody>().ChangeDirection(0);
                //DeadBodies[i].AddComponent<CannonBullet>();
                //DeadBodies[i].layer = 11;

            }
        }
    }
    */
    
}
