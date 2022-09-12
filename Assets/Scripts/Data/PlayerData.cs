using System.Collections.Generic;
using System.Linq;
using BitButterCORE.V2;
using static Constants;

/// <summary>
/// 玩家类，包括当前所处节点，背包状态等
/// </summary>
public class PlayerData : BaseObject
{
	public PlayerData(uint id)
		: base(id)
	{
		LootPosition = InventoryPosition.TopLeft;		// 赃物初始位置为背包左上角
		lootIconIndex = 0;
	}

	/// <summary>
	/// 当前所处节点
	/// </summary>
	public ObjectReference CurrentNode { get; set; }

	/// <summary>
	/// 在背包中向右移动赃物
	/// </summary>
	public void MoveLootRight()
	{
		LootPosition = LootPosition.MoveLoot(isHorizontal: true);
	}

	/// <summary>
	/// 在背包中向左移动赃物
	/// </summary>
	public void MoveLootLeft()
	{
		LootPosition = LootPosition.MoveLoot(isHorizontal: true);
	}

	/// <summary>
	/// 在背包中向上移动赃物
	/// </summary>
	public void MoveLootUp()
	{
		LootPosition = LootPosition.MoveLoot(isHorizontal: false);
	}

	/// <summary>
	/// 在背包中向下移动赃物
	/// </summary>
	public void MoveLootDown()
	{
		LootPosition = LootPosition.MoveLoot(isHorizontal: false);
	}

	/// <summary>
	/// 当前赃物在背包中的位置
	/// </summary>
	public InventoryPosition LootPosition { get; private set; }

	/// <summary>
	/// 在赃物图案序列中向前移动一步
	/// </summary>
	public void AdvanceCurrentLootIcon()
	{
		lootIconIndex += 1;
	}

	/// <summary>
	/// 初始化赃物信息
	/// </summary>
	public void InitLootData(InventoryPosition pos, int index)
	{
		LootPosition = pos;      
		lootIconIndex = index;
	}

	/// <summary>
	/// 当前赃物图案
	/// </summary>
	public LootIcon CurrentLootIcon => LootIcons.ElementAt(lootIconIndex % LootIcons.Count());

	int lootIconIndex;

	IEnumerable<LootIcon> LootIcons => lootIcons ??
		(lootIcons = new[]
		{
			// 在此初始化赃物图案序列
			LootIcon.Hat,
			LootIcon.Mask,
			LootIcon.Rabbit,
			LootIcon.Card,
			LootIcon.Coin
		});
	LootIcon[] lootIcons;
}
