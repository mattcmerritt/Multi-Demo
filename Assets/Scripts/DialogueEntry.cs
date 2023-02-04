using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entry")]
public class DialogueEntry : ScriptableObject
{
    public string speaker;
    [TextArea(15,20)]
    public string message;
    public Sprite img;
    public string callback;
    public string[] parameters;
}
