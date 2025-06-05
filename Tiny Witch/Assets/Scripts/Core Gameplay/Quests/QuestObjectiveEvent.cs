using Utils.Events;

namespace CoreGameplay.Quests
{
    public class QuestObjectiveEvent : BaseEvent
    {
        public string objectiveId;
        public int amountToChange;

        public QuestObjectiveEvent(string objectiveId, int amountToIncrement)
        {
            this.objectiveId = objectiveId;
            this.amountToChange = amountToIncrement;
        }
    }
}