using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingCheckListItem : CheckListItem
{
    //Set the IsComplete property to return true when the Score variable is equal to the MaxScore variable
    public override bool IsComplete { get { return Score == MaxScore; } }

    //Create Score and MaxScore properties to be used in BowlingLaneBehaviour script
    public int Score { get;  set; }
    public int MaxScore { get; set; }

    //This function returns a float that is calculated by dividing the float values of Score and MaxScore
    public override float GetProgress()
    {
        return (float)Score / (float)MaxScore;
    }

    //This function displays the current Score and MaxScore values as strings 
    public override string GetStatusReadout()
    {
        return Score.ToString() + " / " + MaxScore.ToString();
    }

    //This function displays a string
    public override string GetTaskReadout()
    {
        return "Total bowling tally";
    }

    //This function is uses the value from the GetProgress function (a float value between 0 and -1) and uses  
    //that value to determine the color of thescoreboard text
    //The data is also stored in the InvokeCheckListItemChanged game event
    public void OnBowlingScored()
    {
            var ourData = new GameEvents.CheckListItemChangedData();
            ourData.item = this;
            ourData.previousItemProgress = GetProgress();

            GameEvents.InvokeCheckListItemChanged(ourData);
    }
}
