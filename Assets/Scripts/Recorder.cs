using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Action Type - For recording and replaying
public enum ActionType
{
    IDLE,
    LEFT,
    RIGHT,
    JUMP,
    // ...
}

public class Recorder : MonoBehaviour
{
    private ActionType currentActionHorizon;
    private ActionType currentActionVertical;
    private GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        DetectInput();
    }

    // Record current action
    public void SendActionHorizon(ActionType action, float time)
    {
        currentActionHorizon = action;
        Replayer.Records[GameManager.turn].Add(new Record(currentActionHorizon, currentActionVertical, time));
    }
    public void SendActionVertical(ActionType action, float time)
    {
        currentActionVertical = action;
        Replayer.Records[GameManager.turn].Add(new Record(currentActionHorizon, currentActionVertical, time));
    }

    // Recording helper (detect input)
    private void DetectInput()
    {
        // Left & Right
        float horizon = Input.GetAxisRaw("Horizontal");
        if (horizon == 1 && currentActionHorizon != ActionType.RIGHT)
        {
            SendActionHorizon(ActionType.RIGHT, Time.timeSinceLevelLoad);
        }
        else if (horizon == -1 && currentActionHorizon != ActionType.LEFT)
        {
            SendActionHorizon(ActionType.LEFT, Time.timeSinceLevelLoad);
        }
        else if (horizon == 0 && currentActionHorizon != ActionType.IDLE)
        {
            SendActionHorizon(ActionType.IDLE, Time.timeSinceLevelLoad);
        }

        // Jump 

        if (Input.GetButtonDown("Jump") && currentActionVertical != ActionType.JUMP)
        {
            if (player.GetComponent<PlayerController>().isGround)
                SendActionVertical(ActionType.JUMP, Time.timeSinceLevelLoad);
        }
        if (Input.GetButtonUp("Jump") && currentActionVertical != ActionType.IDLE)
        {
            SendActionVertical(ActionType.IDLE, Time.timeSinceLevelLoad);
        }
        
    }

    public void InitSingleRecord()
    {
        currentActionHorizon = ActionType.IDLE;
        currentActionVertical = ActionType.IDLE;
    }
}
