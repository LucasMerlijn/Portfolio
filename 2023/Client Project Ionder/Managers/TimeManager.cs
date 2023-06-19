using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float gameplayTimer;

    [SerializeField] private float idleTimer;

    [SerializeField] private float customiseTimer;

    [SerializeField] private float currentTimer;

    [SerializeField] private int cycleAmout = 0;


    //[SerializeField] private TMP_Text activeGameTimer, passiveGameTimer, customizeGameTimer, timerText;
    //private int minute, second = 59, lastSecond;

    [SerializeField] private bool DONTCOUNT;

    [SerializeField] private TimerVisual timerVisual;
    public TimerVisual TV
    {
        get { return timerVisual; }
    }
    // ### End of Variables ### //


    private void Start()
    {
        // Check server time
        currentTimer = gameplayTimer;
        //timerVisual.TimerValue = gameplayTimer;
        //timerVisual.MaxTime = gameplayTimer;

        //if (!DONTCOUNT)
        //    VisualTimer(currentTimer);
        //lastSecond = (int)currentTimer;

        //timerText = activeGameTimer;
    }

    public void SetupTimer()
    {

        switch (GameManager.Instance.gameState)
        {
            case GameState.ActiveGameplayState:
                timerVisual.MaxTime = gameplayTimer;
                break;

            case GameState.CustomizeGameplayState:
                timerVisual.MaxTime = customiseTimer;
                break;

            case GameState.PassiveGameplayState:
                timerVisual.MaxTime = idleTimer;
                break;

            case GameState.ReportState:
                timerVisual.MaxTime = idleTimer;
                break;
        }
        timerVisual.TimerValue = timerVisual.MaxTime;
    }

    private void Update()
    {
        if (!DONTCOUNT)
        {
            if (GameManager.Instance.GetState == GameState.ReportState || GameManager.Instance.GetState == GameState.ActiveGameplayState || GameManager.Instance.GetState == GameState.PassiveGameplayState || GameManager.Instance.GetState == GameState.CustomizeGameplayState)
            {
                Timer();
            }
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    cycleAmout = 4;
            //    currentTimer = 0;
            //}
        }
    }

    private void Timer()
    {
        currentTimer -= Time.deltaTime;

        if (currentTimer <= 0)
        {
            switch (GameManager.Instance.gameState)
            {
                case GameState.ActiveGameplayState:
                    currentTimer += idleTimer;
                    GameManager.Instance.SwitchState(GameState.ReportState);
                    //timerText = passiveGameTimer;

                    timerVisual.TimerValue = idleTimer;
                    timerVisual.MaxTime = idleTimer;
                    break;

                case GameState.PassiveGameplayState:
                    cycleAmout++;
                    CheckIdleState();
                    break;
                case GameState.CustomizeGameplayState:
                    currentTimer += idleTimer;
                    GameManager.Instance.SwitchState(GameState.PassiveGameplayState);
                    //timerText = passiveGameTimer;

                    timerVisual.TimerValue = gameplayTimer;
                    timerVisual.MaxTime = gameplayTimer;
                    break;

                case GameState.ReportState:
                    cycleAmout++;
                    CheckIdleState();
                    break;
            }
        }
        //VisualTimer(currentTimer);
    }

    private void CheckIdleState()
    {
        if (cycleAmout < 4)
        {
            currentTimer += gameplayTimer;
            GameManager.Instance.SwitchState(GameState.ActiveGameplayState);
            //timerText = activeGameTimer;

            timerVisual.TimerValue = gameplayTimer;
            timerVisual.MaxTime = gameplayTimer;
        }
        else
        {
            cycleAmout = 0;
            currentTimer += customiseTimer;
            GameManager.Instance.SwitchState(GameState.CustomizeGameplayState);
            //timerText = customizeGameTimer;

            timerVisual.TimerValue = customiseTimer;
            timerVisual.MaxTime = customiseTimer;
        }
    }

    //private void VisualTimer(float time)
    //{
    //    minute = Mathf.FloorToInt(time / 60);
    //    second = Mathf.FloorToInt(time % 60);

    //    //if (timerText == null)
    //    //    timerText = activeGameTimer;

    //    //timerText.text = string.Format("{0:00}:{1:00}", minute, second);

    //}
}
