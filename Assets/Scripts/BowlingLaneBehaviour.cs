using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple bowling lane logic, is triggered externally by buttons that are routed
/// to the InitialiseRound, TalleyScore and ResetRack.
/// 
/// Future work;
///   Use the timer in update to limit how long a player has to bowl,
///   Detect that the player/ball is 'bowled' from behind the line
/// </summary>
public class BowlingLaneBehaviour : MonoBehaviour
{
    public GameObject pinPrefab;
    public GameObject bowlingBall;
    public Transform[] pinSpawnLocations;
    public Transform defaultBallLocation;

    GameObject newPin;
    
    //"pins" List created with GameObject data type to add every pin prefab into
    //Create an object from the BowlingCheckListItem class
    List<GameObject> pins = new List<GameObject>();
    BowlingCheckListItem bowlingCheckListItem;


    void Start()
    {
        //Use the Monobehaviour function "FindObjectOfType" to find the BowlingCheckListItem object in the project
        //Set the MaxScore variable in the BowlingCheckListItem class to the amount of pin spawn locations
        bowlingCheckListItem = FindObjectOfType<BowlingCheckListItem>();
        bowlingCheckListItem.MaxScore = pinSpawnLocations.Length;
    }

    [ContextMenu("InitialiseRound")]
    public void InitialiseRound()
    {
        //Uses the "foreach" loop to go over each instance from the pinSpawnLocations collection and instantiate a pin prefab onto it
        //The created pins are also added to the "pins" list
        foreach (var pinLoc in pinSpawnLocations)
        {
            newPin = Instantiate(pinPrefab, pinLoc.position, pinLoc.rotation);
            pins.Add(newPin);
        }
    }

    public void BallReachedEnd()
    {
        //Bowling Ball's position and rotation is reset to default 
        bowlingBall.transform.position = defaultBallLocation.transform.position;
        bowlingBall.transform.rotation = defaultBallLocation.transform.rotation;

        //Velocity and Angular Velocity values are reset to 0 to remove any inertia carried over
        Rigidbody bb = bowlingBall.GetComponent<Rigidbody>();
        bb.velocity = Vector3.zero;
        bb.angularVelocity = Vector3.zero;
    }


    [ContextMenu("TalleyScore")]
    public void TalleyScore()
    {
        //Integer variable "score" created to store the value for the amount of pins knocked over 
        //Score set to 0 at the start of function to prevent previous score from being carried over
        int score = 0;

        //For loop checks each pin's "up" angle and the game world's "up" angle using the Dot function
        //If the pin's angle is less than 0.9f (world up angle), then it is fallen over and a score is added
        //The score is printed in the console after the for loop
        for (int i = 0; i < pins.Count; i++)
        {
            float angle = Vector3.Dot(Vector3.up, pins[i].transform.up);
            if (angle < 0.9f) {
                score++;
            }
        }
        //Set the score to the BowlingCheckListItem's "Score" property
        //Call the OnBowlingScored function from the BowlingCheckListItem class
        bowlingCheckListItem.Score = score;
        bowlingCheckListItem.OnBowlingScored();
    }

    [ContextMenu("ResetRack")]
    public void ResetRack()
    {
        //For loop goes over each pin's position and rotation values and sets it to the default values, thus placing it upright
        for (int i = 0; i < pins.Count; i++)
        {
            pins[i].transform.position = pinSpawnLocations[i].transform.position;
            pins[i].transform.rotation = pinSpawnLocations[i].transform.rotation;
        }

        //Bowling Ball's position and rotation is reset to default 
        bowlingBall.transform.position = defaultBallLocation.transform.position;
        bowlingBall.transform.rotation = defaultBallLocation.transform.rotation;

        //Velocity and Angular Velocity values are reset to 0 to remove any inertia carried over
        Rigidbody bb = bowlingBall.GetComponent<Rigidbody>();
        bb.velocity = Vector3.zero;
        bb.angularVelocity = Vector3.zero;

        //Set the score to the BowlingCheckListItem's "Score" property
        //Call the OnBowlingScored function from the BowlingCheckListItem class
        bowlingCheckListItem.Score = 0;
        bowlingCheckListItem.OnBowlingScored();

    }

    protected void Update()
    {
        
    }
}
