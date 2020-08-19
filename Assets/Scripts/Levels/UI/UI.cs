using System.Collections.Generic;

using UnityEngine;
using TMPro;

namespace Levels
{
    class UI: MonoBehaviour
    {
        //Singleton
        static UI ui;
        
        //Text fields
        [SerializeField] TextMeshProUGUI lifeCount = default;
        [SerializeField] TextMeshProUGUI cherryCount = default;
        [SerializeField] TextMeshProUGUI signText = default;
        static Dictionary<string, TextMeshProUGUI> textFields;
        
        //Objects
        [SerializeField] GameObject stats = default;
        [SerializeField] GameObject sign = default;
        [SerializeField] GameObject interact = default;

        //Tutorial
        [SerializeField] GameObject tutorial = default;

        //Menus
        [SerializeField] GameObject pauseMenu = default;
        [SerializeField] GameObject gameOver = default;
        static Dictionary<string, GameObject> objects;

        //Methods
        void Start()
        {
            //Singleton
            if(!ui)
            {
                ui = this;
                DontDestroyOnLoad(gameObject);

                //Initialize text fields
                textFields = new Dictionary<string, TextMeshProUGUI>
                {
                    {"Life", lifeCount},
                    {"Cherry", cherryCount},
                    {"Sign", signText}
                };

                //Initialize objects
                objects = new Dictionary<string, GameObject>
                {
                    {"Stats", stats},
                    {"Sign", sign},
                    {"Interact", interact},
                    {"Tutorial", tutorial},
                    {"Pause Menu", pauseMenu},
                    {"Game Over", gameOver}
                };
            }
            else
            {
                //Reset cherry count
                UpdateText("Cherry", 0);
                Destroy(gameObject);
            }
        }

        void Update()
        {
            //If the player presses ESCAPE, go to Pause Menu
            if(Input.GetButtonDown("Pause Menu") && !Game.IsGameOver)
                PauseMenu.TogglePauseMenu();
        }

        //Static methods
        internal static void UpdateText(string key, object value)
        {
            textFields[key].text = value.ToString();
        }

        internal static bool IsActive(string key)
        {
            return objects[key] && objects[key].activeSelf;
        }

        internal static void Display(string key, bool enable = true)
        {
            objects[key].SetActive(enable);
        }

        internal static void DisableTutorialText()
        {
            Destroy(ui.tutorial);
        }

        internal static void ResetUI()
        {
            Destroy(ui.gameObject);
        }
    }
}