using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPassthrough : MonoBehaviour
{

    public void Select()
    {
        Button button = transform.GetChild(0).GetComponent<Button>();
        button.onClick.Invoke();
    }
}