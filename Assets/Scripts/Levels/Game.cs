﻿using UnityEngine;

namespace Levels
{
    static class Game
    {
        //Constants
        internal const float DRAG = 5f;
        internal const float GRAVITY = 10f;
        
        //Static properties
        internal static bool IsInputEnabled { get; set; } = true;
        internal static bool IsTutorialEnabled { get; private set; } = true;
        internal static bool IsGameOver { get; private set; } = false;

        //Static methods
        internal static void DisableTutorial()
        {
            IsTutorialEnabled = false;

            //Disable tutorial text
            UI.DisableTutorialText();
        }

        internal static void GameOver()
        {
            IsGameOver = true;

            //Disable player
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            
            //Display Game Over screen
            UI.Display("Game Over");
        }

        internal static void Restart()
        {
            IsTutorialEnabled = true;
            IsGameOver = false;
            
            Player.ResetLives();
            UI.ResetUI();
        }
    }
}