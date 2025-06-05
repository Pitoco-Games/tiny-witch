using System;
using Utils.Events;

namespace CoreGameplay.Quests
{
    public class QuestObjective
    {
        public string id;
        public int requiredProgress;

        public bool IsComplete => currentProgress == requiredProgress;
        public int CurrentProgress => currentProgress;

        private int currentProgress;
        private Action<int> onQuestProgressChanged;
        private Action onObjectiveCompleted;

        public QuestObjective(Action<int> onQuestProgressChangedCallback, Action onObjectiveCompletedCallback)
        {
            EventBus.Instance.AddListener<QuestObjectiveEvent>(OnQuestObjectiveRaised);
            onQuestProgressChanged = onQuestProgressChangedCallback;
            onObjectiveCompleted = onObjectiveCompletedCallback;
        }

        private void OnQuestObjectiveRaised(QuestObjectiveEvent e)
        {
            if (!e.objectiveId.Equals(id) || IsComplete)
            {
                return;
            }

            currentProgress = Math.Max(0, Math.Min(requiredProgress, currentProgress + e.amountToChange));
            onQuestProgressChanged.Invoke(currentProgress);

            if (currentProgress == requiredProgress)
            {
                onObjectiveCompleted.Invoke();
            }
        }
    }
}