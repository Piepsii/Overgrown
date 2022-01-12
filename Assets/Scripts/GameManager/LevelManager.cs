using UnityEngine;
using Overgrown.GameEnums;

namespace Overgrown.GameManager
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject levelPrefab;
        public Level activeLevel;

        private void Start()
        {
            if (GameManager.Instance.LevelManager == null)
            {
                GameManager.Instance.LevelManager = this;
            }
            activeLevel = FindObjectOfType<Level>();
        }

        public void ToggleCellState(int id)
        {
            activeLevel.ToggleCellState(id);
        }

        public void CrossCell(int id)
        {
            activeLevel.CrossCell(id);
        }

        private bool CreateLevel()
        {
            if (levelPrefab)
            {
                Instantiate(levelPrefab);
                return true;
            }
            return false;
        }
    }
}

