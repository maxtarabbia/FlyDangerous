﻿using System.Collections.Generic;
using Core;
using Gameplay.Game_Modes.Components.Interfaces;
using GameUI.GameModes;
using Misc;
using UnityEngine;

namespace Gameplay.Game_Modes {
    public class TimeTrialSprint : IGameMode, IGameModeWithScore, IGameModeWithCountdown, IGameModeWithTimer, IGameModeWithCheckpoints {
        // These are set by the game mode handler by virtue of implementing the interface
        public GameModeUIHandler GameModeUIHandler { get; set; }
        public IGameModeScore GameModeScore { get; set; }
        public IGameModeLifecycle GameModeLifecycle { get; set; }
        public IGameModeCheckpoints GameModeCheckpoints { get; set; }
        public IGameModeTimer GameModeTimer { get; set; }

        // Exposed game mode metadata
        public bool IsHotJoinable => false;
        public bool CanWarpToHost => false;
        public bool HasFixedStartLocation => true;
        public bool SupportsReplays => true;
        public bool RequireBoostHeldToStart => true;
        public GameModeScoreType GameModeScoreType => GameModeScoreType.Time;
        public bool AllowEarlyBoost => true;
        public float StartingCountdownTime => 2.5f;


        private readonly List<float> _splits = new();
        protected float _lastCheckpointHitTimeSeconds;
        private Coroutine _splitFadeOutCoroutine;

        public virtual void OnInitialise() {
            _splits.Clear();
            _lastCheckpointHitTimeSeconds = 0;
        }

        public virtual void OnBegin() {
            /* nothing to do */
        }

        public virtual void OnFixedUpdate() {
            var time = GameModeTimer.CurrentSessionTimeSeconds;
            var headerColor = time < -1 ? Color.red : time < 0 ? Color.yellow : time < 1.5f ? Color.green : Color.white;
            GameModeUIHandler.GameModeUIText.TopHeader.color = headerColor;
            GameModeUIHandler.GameModeUIText.TopHeader.text =
                TimeExtensions.TimeSecondsToString(Mathf.Abs(GameModeTimer.CurrentSessionTimeSeconds));
        }

        public virtual void OnRestart() {
            if (_splitFadeOutCoroutine != null) Game.Instance.StopCoroutine(_splitFadeOutCoroutine);
            GameModeUIHandler.GameModeUIText.CentralCanvasGroup.alpha = 0;
            GameModeCheckpoints.Reset();
            _splits.Clear();
            _lastCheckpointHitTimeSeconds = 0;
        }

        public virtual void OnComplete() {
            GameModeUIHandler.GameModeUIText.HideGameUIText(false);
            GameModeScore.NewScore(_lastCheckpointHitTimeSeconds, _splits);
        }

        public virtual void OnQuit() {
            if (_splitFadeOutCoroutine != null) Game.Instance.StopCoroutine(_splitFadeOutCoroutine);
        }

        public virtual void CountdownStarted() {
            /* nothing to do */
        }

        public virtual void CountdownComplete() {
            /* nothing to do */
        }

        public virtual void OnGameSettingsApplied() {
            /* nothing to do */
        }

        public virtual void OnCheckpointHit(Checkpoint checkpoint, float hitTimeSeconds) {
            _lastCheckpointHitTimeSeconds = hitTimeSeconds;

            // store split 
            _splits.Add(_lastCheckpointHitTimeSeconds);
            float previousSplitTimeSeconds = 0;
            if (GameModeScore.CurrentBestSplits.Count >= _splits.Count) previousSplitTimeSeconds = GameModeScore.CurrentBestSplits[_splits.Count - 1];
            SetSplitTimer(_lastCheckpointHitTimeSeconds, previousSplitTimeSeconds);

            // enable last checkpoint and complete
            if (GameModeCheckpoints.AllCheckpointsHit) {
                // enable the end checkpoint
                var endCheckpoint = GameModeCheckpoints.Checkpoints.Find(c => c.Type == CheckpointType.End);
                if (endCheckpoint) endCheckpoint.ToggleValidEndMaterial(true);
            }
        }

        public virtual void OnLastCheckpointHit(float hitTimeSeconds) {
            if (GameModeCheckpoints.AllCheckpointsHit)
                GameModeLifecycle.Complete();
        }

        public void SetCurrentPersonalBest(float score, List<float> splits) {
            var targetScore = GameModeScore.NextTargetScore();
            GameModeUIHandler.GameModeUIText.TopRightHeader.color = targetScore.MedalColor;
            GameModeUIHandler.GameModeUIText.TopRightHeader.text = $"TARGET {targetScore.Medal}".ToUpper();
            GameModeUIHandler.GameModeUIText.TopRightContent.text = TimeExtensions.TimeSecondsToString(targetScore.Score);
        }


        protected void SetSplitTimer(float splitTimeSeconds, float previousSplitTimeSeconds = 0) {
            GameModeUIHandler.GameModeUIText.CentralHeader.color = Color.white;
            GameModeUIHandler.GameModeUIText.CentralHeader.text = TimeExtensions.TimeSecondsToString(splitTimeSeconds);
            if (previousSplitTimeSeconds > 0) {
                var deltaSplit = splitTimeSeconds - previousSplitTimeSeconds;
                var deltaSplitText = TimeExtensions.TimeSecondsToString(deltaSplit);
                GameModeUIHandler.GameModeUIText.CentralContent.color = deltaSplit > 0 ? Color.red : Color.green;
                GameModeUIHandler.GameModeUIText.CentralContent.text = deltaSplit > 0 ? $"+{deltaSplitText}" : deltaSplitText;
            }

            ShowAndFadeOutSplits();
        }

        protected void ShowAndFadeOutSplits() {
            if (_splitFadeOutCoroutine != null) Game.Instance.StopCoroutine(_splitFadeOutCoroutine);
            GameModeUIHandler.GameModeUIText.CentralCanvasGroup.alpha = 1;
            _splitFadeOutCoroutine = Game.Instance.StartCoroutine(
                YieldExtensions.SimpleAnimationTween(
                    val => GameModeUIHandler.GameModeUIText.CentralCanvasGroup.alpha = 1 - val,
                    5f
                )
            );
        }
    }
}