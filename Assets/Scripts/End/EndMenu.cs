namespace End
{
    class EndMenu: Menu
    {
        //Events
        public void PlayAgain()
        {
            SceneLoader.Load(Scene.FirstLevel);
        }

        public new void ToMainMenu()
        {
            SceneLoader.Load(Scene.MainMenu);
        }
    }
}