using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Article")]
public class NewsArticle : ScriptableObject
{
    public string title;
    [TextArea(15,20)]
    public string content;
}
