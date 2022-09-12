using System.Collections.Generic;
using System.Linq;
using BitButterCORE.V2;
using static Constants;

/// <summary>
/// ����࣬������ǰ�����ڵ㣬����״̬��
/// </summary>
public class PlayerData : BaseObject
{
	public PlayerData(uint id)
		: base(id)
	{
		LootPosition = InventoryPosition.TopLeft;		// �����ʼλ��Ϊ�������Ͻ�
		lootIconIndex = 0;
	}

	/// <summary>
	/// ��ǰ�����ڵ�
	/// </summary>
	public ObjectReference CurrentNode { get; set; }

	/// <summary>
	/// �ڱ����������ƶ�����
	/// </summary>
	public void MoveLootRight()
	{
		LootPosition = LootPosition.MoveLoot(isHorizontal: true);
	}

	/// <summary>
	/// �ڱ����������ƶ�����
	/// </summary>
	public void MoveLootLeft()
	{
		LootPosition = LootPosition.MoveLoot(isHorizontal: true);
	}

	/// <summary>
	/// �ڱ����������ƶ�����
	/// </summary>
	public void MoveLootUp()
	{
		LootPosition = LootPosition.MoveLoot(isHorizontal: false);
	}

	/// <summary>
	/// �ڱ����������ƶ�����
	/// </summary>
	public void MoveLootDown()
	{
		LootPosition = LootPosition.MoveLoot(isHorizontal: false);
	}

	/// <summary>
	/// ��ǰ�����ڱ����е�λ��
	/// </summary>
	public InventoryPosition LootPosition { get; private set; }

	/// <summary>
	/// ������ͼ����������ǰ�ƶ�һ��
	/// </summary>
	public void AdvanceCurrentLootIcon()
	{
		lootIconIndex += 1;
	}

	/// <summary>
	/// ��ʼ��������Ϣ
	/// </summary>
	public void InitLootData(InventoryPosition pos, int index)
	{
		LootPosition = pos;      
		lootIconIndex = index;
	}

	/// <summary>
	/// ��ǰ����ͼ��
	/// </summary>
	public LootIcon CurrentLootIcon => LootIcons.ElementAt(lootIconIndex % LootIcons.Count());

	int lootIconIndex;

	IEnumerable<LootIcon> LootIcons => lootIcons ??
		(lootIcons = new[]
		{
			// �ڴ˳�ʼ������ͼ������
			LootIcon.Hat,
			LootIcon.Mask,
			LootIcon.Rabbit,
			LootIcon.Card,
			LootIcon.Coin
		});
	LootIcon[] lootIcons;
}
