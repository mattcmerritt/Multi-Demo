using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class Player2UI : MonoBehaviour
{
    [SerializeField] private GameObject ArticlePanel;
    [SerializeField] private GameObject FamilyTree;

    [SerializeField] private List<NewsArticle> Articles;
    [SerializeField] private TMP_Dropdown ArticleSelect;
    [SerializeField] private TMP_Text Title, Content;

    private void Awake()
    {
        List<string> titles = new List<string>();
        foreach (NewsArticle a in Articles)
        {
            titles.Add(a.title);
        }

        DisplayArticle(0);
        ArticleSelect.ClearOptions();
        ArticleSelect.AddOptions(titles);
        ArticleSelect.onValueChanged.AddListener((int i) =>
        {
            DisplayArticle(i);
        });
    }

    public void DisplayArticle(int i)
    {
        Title.text = Articles[i].title;
        Content.text = Articles[i].content;
    }

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
