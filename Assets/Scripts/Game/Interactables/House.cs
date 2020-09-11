namespace Sunnyland.Game.Interactables
{
	sealed class House : Interactable
	{
		public override void Interact()
		{
			base.Interact();
			Game.Restart();
			SceneLoader.Load(Scene.Next);
		}
	}
}