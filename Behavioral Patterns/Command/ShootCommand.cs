using Godot;

namespace Patterns.Behavioral_Patterns.Command
{
	public class ShootCommand : ICommand
	{
		public string Name { get; } = "Shoot";

		public void Execute(Label label)
		{
			label.Text = "Shoot";
		}
	}
}
