using System.Linq;
using BitButterCORE.V2;
using static Constants;

/// <summary>
/// �����࣬��ʾ�ڵ㴦�������α�
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
	/// ���������ڵ�
	/// </summary>
	public ObjectReference Node { get; }

	public RogueAbility Ability { get; }

	/// <summary>
	/// ʹ�������������ı�������̬
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
