using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CountDownScript : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CountDownScript, UxmlTraits> { }

    private readonly VisualTreeAsset _visualTree;
    private readonly VisualElement _root;
    private LevelState _levelstate;
    private int _totalStandbyCount;
    public CountDownScript()
    {
        _levelstate = GameObject.FindAnyObjectByType<LevelState>();
        _totalStandbyCount = _levelstate.totalStanbyCount;

        _visualTree = Resources.Load<VisualTreeAsset>("UI/CountDown");
        _root = _visualTree.CloneTree();
        Add(_root);

        _root.Q<Label>("CountDown").text = _totalStandbyCount.ToString();
        _levelstate.BindTimerCoroutine(TimerCoroutine());
    }

    public IEnumerator TimerCoroutine()
    {
        int count = _totalStandbyCount;
        while (count > 0)
        {
            _root.Q<Label>("CountDown").text = count.ToString();
            count--;
            yield return new WaitForSecondsRealtime(1);
        }

        _root.Q<Label>("CountDown").text = "0";

        _levelstate.GameStart();
        style.display = DisplayStyle.None;
    }


    // This function would stuck the game due to the while, it has been deserted and should be used coroutine
    /*public void TimerCoroutin()
    {
        // While might be faster than the speed of UI rendering
        // So we need to make sure UI just update in each delta time
        float whileGapTime = 10 * Time.unscaledDeltaTime;
        float whileTimer = 0f;
        DateTime timeInLastWhileLoop = DateTime.Now;
        DateTime timeInThisWhileLoop = DateTime.Now;
        // Condition of exit while loop
        _endTime = DateTime.Now.AddSeconds(_totalStandbyCount);

        while (DateTime.Now < _endTime)
        {
            timeInThisWhileLoop = DateTime.Now;
            if (whileTimer >= whileGapTime)
            {
                _root.Q<Label>("CountDown").text = ((int)(_endTime - DateTime.Now).TotalSeconds).ToString();
                whileTimer = 0f;
            }
            whileTimer += (float)(timeInThisWhileLoop - timeInLastWhileLoop).TotalSeconds;
            timeInLastWhileLoop = DateTime.Now;
        }

        _root.Q<Label>("CountDown").text = "0";

        _levelstate.GameStart();
        style.display = DisplayStyle.None;
    }*/
}
