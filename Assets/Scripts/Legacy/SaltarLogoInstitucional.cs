using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaltarLogoInstitucional : MonoBehaviour
{
    public float showTime = 3F;

    private void Start()
    {

#if ( !UNITY_EDITOR )
Cursor.visible = false;
#endif
        Invoke("JumpScene", showTime);
    }

    void JumpScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
