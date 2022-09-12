using System.Linq;
using BitButterCORE.V2;
using static Constants;

/// <summary>
/// 侠盗类，表示节点处的赃物形变
/// </summary>
public class RogueData : BaseObject
{
	public RogueData(uint id, ObjectReference node, RogueAbility ability)
		: base(id)
	{
		Node = node;
		Ability = ability;
	}

	/// <summary>
	/// 侠盗所处节点
	/// </summary>
	public ObjectReference Node { get; }

	public RogueAbility Ability { get; }

	/// <summary>
	/// 使用侠盗能力，改变赃物形态
	/// </summary>
	public void ApplyAbility()
	{
		var playerReference = ObjectFactory.Instance.Query<PlayerData>().FirstOrDefault();
		if (playerReference.IsValid)
		{
			var player = playerReference.Object as PlayerData;
			switch (Ability)
			{
				case RogueAbility.MoveUp:
					player.MoveLootUp();
					break;
				case RogueAbility.MoveDown:
					player.MoveLootDown();
					break;
				case RogueAbility.MoveLeft:
					player.MoveLootLeft();
					break;
				case RogueAbility.MoveRight:
					player.MoveLootRight();
					break;
			}
		}
	}
}
