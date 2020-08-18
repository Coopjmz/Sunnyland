using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

//Scenes
enum Scene
{
    Active,
    Next,
    MainMenu,
    FirstLevel
}

class SceneLoader: MonoBehaviour
{
    //Static variables
    static string _nextScene;

    //Variables (initialized from Unity)
    [SerializeField] string nextScene = default;

    //Methods
    void Start()
	{
        //Initialize next scene
        _nextScene = nextScene;
	}

    //Static methods
    internal static void Load(Scene scene)
	{
        switch(scene)
		{
            case Scene.Active:
                LoadScene(GetActiveScene().name);
                break;
            case Scene.Next:
                LoadScene(_nextScene);
                break;
            case Scene.MainMenu:
                LoadScene("Main Menu");
                break;
            case Scene.FirstLevel:
                LoadScene("Level 1");
                break;
        }
	}
}