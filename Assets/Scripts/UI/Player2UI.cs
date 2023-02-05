using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2UI : MonoBehaviour
{
    [SerializeField] private GameObject ArticlePanel;
    [SerializeField] private GameObject FamilyTree;

    public void ShowArticlePanel()
    {
        ArticlePanel.SetActive(true);
        FamilyTree.SetActive(false);
    }

    public void ShowFamilyPanel()
    {
        ArticlePanel.SetActive(false);
        FamilyTree.SetActive(true);
    }
}
