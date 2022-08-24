using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject clueUIObject;
    private ClueUI clueUI;

    public void Create()
    {
        if (clueUI)
        {
            Destroy(clueUI.gameObject);
        }
        var instance = Instantiate(clueUIObject, transform);
        instance.name = "ClueUI";
        clueUI = instance.AddComponent<ClueUI>();
    }
}
