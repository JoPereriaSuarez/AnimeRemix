using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    public MyInputButton greenButton;
    public MyInputButton yellowButton;
    public MyInputButton redButton;
    public MyInputButton blueButton;
    public MyInputButton rightButton;
    public MyInputButton leftButton;
    public MyInputButton upButton;
    public MyInputButton downButton;

    private void Start()
    {
        greenButton = new MyInputButton(NoteType.green);
        yellowButton = new MyInputButton(NoteType.yellow);
        redButton = new MyInputButton(NoteType.red);
        blueButton = new MyInputButton(NoteType.blue);
        leftButton = new MyInputButton(NoteType.left);
        rightButton = new MyInputButton(NoteType.right);
        upButton = new MyInputButton(NoteType.up);
        downButton = new MyInputButton(NoteType.down);
    }

    private void Update()
    {
        #region ALL D-PAD INPUT CHECK
        
        if (greenButton.CheckInput() != 0)
        { print("GREEN " + greenButton.CheckInput()); }

        if (yellowButton.CheckInput() != 0)
            print("Yellow " + yellowButton.CheckInput());
        if (redButton.CheckInput() != 0)
            print("RED " + redButton.CheckInput());
        if(blueButton.CheckInput() != 0)
            print("BLUE " + blueButton.CheckInput());
        if (rightButton.CheckInput() != 0)
            print("RIGHT " + rightButton.CheckInput());
        if (leftButton.CheckInput() != 0)
            print("LEFT " + leftButton.CheckInput());
        if (upButton.CheckInput() != 0)
            print("UP " + upButton.CheckInput());
        if (downButton.CheckInput() != 0)
            print("DOWN " + downButton.CheckInput());
        
        #endregion
    }
}
