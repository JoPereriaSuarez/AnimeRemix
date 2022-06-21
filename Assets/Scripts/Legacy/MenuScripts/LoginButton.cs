using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class LoginButton : MonoBehaviour, ISelectable
{
    public Button thisButton { get; private set; }
    Text the_text;
    public LoginButton nextButton;
    private ISelectable nextSelectable;
    public bool isSelected = false;
    [SerializeField] bool isFirst = false;
    [SerializeField] bool isLast = false;

    public LoginButton[] logins = new LoginButton[3];

    [SerializeField] NoteType selectButton = NoteType.green;
    [SerializeField] NoteType positiveButton = NoteType.down;
    [SerializeField] NoteType negativeButton = NoteType.up;

    MyInputButton _buttonSelect;
    InputState _stateSelect;
    MyInputButton _buttonPositive;
    InputState _statePositive;
    MyInputButton _buttonNegative;
    InputState _stateNegative;

    int index = 0;
    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private void Start()
    {
        the_text = transform.GetChild(0).GetComponent<Text>();

        _buttonSelect = new MyInputButton(selectButton);
        _stateSelect = InputState.idle;

        _buttonPositive = new MyInputButton(positiveButton);
        _statePositive = InputState.idle;

        _buttonNegative = new MyInputButton(negativeButton);
        _stateNegative = InputState.idle;

        thisButton = GetComponent<Button>();
        if(!isLast)
        {
            nextSelectable = nextButton.GetComponent<ISelectable>();
        }
        if (this.isFirst)
        {
            OnSelect();
        }
    }

    public void OnPress()
    {
        isSelected = false;
        if(isLast)
        {
            if(GameController.instance != null)
            {
                char[] characters = new char[3];
                for (int i = 0; i < logins.Length; i++)
                {
                    characters[i] = logins[i].transform.GetChild(0).GetComponent<Text>().text[0];
                }

                GameController.instance.LogPlayer(characters);
            }
        }
        else
        {
            nextButton.OnSelect();
        }
        
    }

    public void OnSelect()
    {
        thisButton.Select();
        //the_text.color = Color.grey;
        Invoke("SelectThis", 1F);
    }

    void SelectThis()
    {
        isSelected = true;
    }

    private void Update()
    {
        if(!isSelected)
        { return; }


        if (_buttonNegative.CheckInputState(ref _stateNegative) == InputState.down
        || _buttonPositive.CheckInputState(ref _statePositive) == InputState.down)
        {
            index += _buttonPositive.CheckInput() - _buttonNegative.CheckInput();
            index = Mathf.Clamp(index, 0, alphabet.Length - 1);
            the_text.text = alphabet[index].ToString();
        }

        else if (_buttonSelect.CheckInputState(ref _stateSelect) == InputState.down)
        {
            the_text.color = Color.white;
            OnPress();
        }        

    }
}
