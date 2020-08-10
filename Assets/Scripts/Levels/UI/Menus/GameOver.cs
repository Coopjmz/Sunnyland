namespace Levels
{
    class GameOver: Menu
    {
		//Methods
		void Awake()
		{
            //Hide stats when player dies
			UI.Display("Stats", false);
		}

		//Events
		public void PlayAgain()
        {
            //Restarts the game
            Game.Reset();
            SceneLoader.Load(Scene.FirstLevel);
        }
    }
}