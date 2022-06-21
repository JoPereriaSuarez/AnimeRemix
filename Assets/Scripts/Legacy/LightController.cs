using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LightIntensity
{
    disable = -20,
    terrible = -9,
    bad     = -8,
    great = -7,
    perfect = -6,
    maxPerfect = -5,
}

public class LightController : MonoBehaviour
{
    LightIntensity lightIntensity = LightIntensity.disable;
    [SerializeField] Transform _light;
    Vector3 lightPos;
    float intensityValue;
    [HideInInspector] public bool isInCollision = false;
    [SerializeField] NoteType button;
    MyInputButton my_button;
    InputState state;

    public void ChangeLightIntensity(LightIntensity intensity)
    {
        lightIntensity = intensity;
        intensityValue = (float)lightIntensity;
    }

    private void Start()
    {
        lightPos = _light.transform.localPosition;
        intensityValue = (float)lightIntensity;
        my_button = new MyInputButton(button);
        state = InputState.idle;
    }
    private void Update()
    {
        if(my_button.CheckInputState(ref state) == InputState.hold || my_button.CheckInputState(ref state) == InputState.down)
        {

            if(!isInCollision)
            {
                if (lightIntensity != LightIntensity.terrible)
                {
                    ChangeLightIntensity(LightIntensity.terrible);
                }
            }
            /*
            if(isInCollision)
            {
                if(lightIntensity != LightIntensity.full)
                {
                    ChangeLightIntensity(LightIntensity.full);
                }
            }
            else
            {
                if (lightIntensity != LightIntensity.normal)
                {
                    ChangeLightIntensity(LightIntensity.normal);
                }
            }
            */
        }
        else
        {
            if (lightIntensity != LightIntensity.disable)
            {
                ChangeLightIntensity(LightIntensity.disable);
            }
        }

        if(lightPos.y != intensityValue)
        {
            lightPos.y = intensityValue;
            _light.transform.localPosition = lightPos;
        }
    }
}
