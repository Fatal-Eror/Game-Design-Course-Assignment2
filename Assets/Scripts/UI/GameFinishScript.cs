using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameFinishScript : VisualElement
{
     public new class UxmlFactory : UxmlFactory<GameFinishScript, UxmlTraits> { }
    private readonly VisualTreeAsset _visualTree;
    private readonly VisualElement _root;
    private LevelState _levelState;

    public GameFinishScript()
    {
        _visualTree = Resources.Load<VisualTreeAsset>("UI/GameFinish");
        _root = _visualTree.CloneTree();
        Add(_root);
        style.display = DisplayStyle.None;
        
        _levelState = GameObject.FindAnyObjectByType<LevelState>();
        _levelState.OnLevelEnd += PopGameFinishWindow;

        _root.Q<Button>("RestartBtn").RegisterCallback<ClickEvent>(RestartGame);
    }

    private void RestartGame(ClickEvent evt)
    {
        SceneManager.LoadScene(0);
    }

    private void PopGameFinishWindow()
    {
        _root.Q<Label>("PresentScore").text = $"Score: {Time.timeSinceLevelLoad}";
        _root.Q<Label>("BestScore").text = $"Best: {PlayerPrefs.GetFloat("Best")}";
        style.display = DisplayStyle.Flex;
    }
}
