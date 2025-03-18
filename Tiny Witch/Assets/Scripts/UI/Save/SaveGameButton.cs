using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.Save;

namespace UI.Save
{
    public class SaveGameButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(SaveAllData);
        }

        private void SaveAllData()
        {
            ServicesLocator.Get<SaveService>().SaveAll();
        }
    }
}