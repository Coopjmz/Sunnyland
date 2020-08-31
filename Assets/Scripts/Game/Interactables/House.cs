namespace Sunnyland.Game.Interactables
{
	sealed class House : Interactable
	{
		internal override void Interact()
		{
			base.Interact();
			Game.Restart();
			SceneLoader.Load(Scene.Next);
		}
	}
}