using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuNavigator : MonoBehaviour
{
    [SerializeField] Button[] elements;
    [SerializeField] int index;
    [SerializeField] NoteType selectButton = NoteType.green;
    [SerializeField] NoteType positiveButton = NoteType.right;
    [SerializeField] NoteType negativeButton = NoteType.left;


    MyInputButton _buttonSelect;
    InputState _stateSelect;
    MyInputButton _buttonPositive;
    InputState _statePositive;
    MyInputButton _buttonNegative;
    InputState _stateNegative;


    private void Start()
    {
        _buttonSelect = new MyInputButton(selectButton);
        _stateSelect = InputState.idle;

        _buttonPositive = new MyInputButton(positiveButton);
        _statePositive = InputState.idle;

        _buttonNegative = new MyInputButton(negativeButton);
        _stateNegative = InputState.idle;

        elements[0].GetComponent<ISelectable>().OnSelect();
    }

    private void Update()
    {
        if(_buttonNegative.CheckInputState(ref _stateNegative) == InputState.down
            || _buttonPositive.CheckInputState(ref _statePositive) == InputState.down)
        {
            index += _buttonPositive.CheckInput() - _buttonNegative.CheckInput();
            index = Mathf.Clamp(index, 0, elements.Length -1);
            elements[index].GetComponent<ISelectable>().OnSelect();
        }

        elements[index].Select();
        if (_buttonSelect.CheckInputState(ref _stateSelect) == InputState.down)
        {
            print(elements[index].name + " is selected" );
            elements[index].GetComponent<ISelectable>().OnPress();
        }
    }
}
