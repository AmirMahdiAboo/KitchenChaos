using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChenged;

    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 10f;
    private bool isGamepaused = false;

    private void Awake()
    {
        Instance = this;

        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountDownToStart;
                    OnStateChenged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChenged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChenged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;

        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountDownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countDownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    private void TogglePauseGame()
    {
        isGamepaused = !isGamepaused;
        if(isGamepaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
