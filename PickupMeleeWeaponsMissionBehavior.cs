using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace PickupMeleeWeapons
{
	public class PickupMeleeWeaponsMissionBehavior : MissionBehavior
	{
		private readonly PickupMeleeWeaponsManager _manager;

		public override MissionBehaviorType BehaviorType => MissionBehaviorType.Other;

		public PickupMeleeWeaponsMissionBehavior() => _manager = PickupMeleeWeaponsManager.Current;

		public override void AfterStart()
		{
			_manager.ItemPickupQueueEven.Clear();
			_manager.ItemPickupQueueOdd.Clear();
		}

		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			_manager.ItemPickupQueueEven.Remove(affectedAgent);
			_manager.ItemPickupQueueOdd.Remove(affectedAgent);
		}

		public override void OnMissionTick(float dt)
		{
			foreach (Agent agent in Mission.Agents.FindAll(a => a.IsHuman))
			{
				if (!agent.HasMount && PickupMeleeWeaponsHelper.HasLostMeleeWeapon(agent))
				{
					// If an agent has lost a melee weapon, add them to the respective queue based on their index.
					if (agent.Index % 2 == 0 && _manager.ItemPickupQueueEven.Count < 50)
					{
						_manager.ItemPickupQueueEven.Add(agent);
					}
					else if (agent.Index % 2 == 1 && _manager.ItemPickupQueueOdd.Count < 50)
					{
						_manager.ItemPickupQueueOdd.Add(agent);
					}
				}
				else
				{
					// Remove the agent from the respective queue.
					_manager.ItemPickupQueueEven.Remove(agent);
					_manager.ItemPickupQueueOdd.Remove(agent);
				}
			}
		}
	}
}
