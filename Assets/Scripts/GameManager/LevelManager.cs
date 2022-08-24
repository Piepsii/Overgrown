using UnityEngine;
using Overgrown.GameEnums;

namespace Overgrown.GameManager
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject levelPrefab;
        public Level level;

        private void Start()
        {
            if (GameManager.Instance.LevelManager == null)
            {
                GameManager.Instance.LevelManager = this;
            }
            level = FindObjectOfType<Level>();
        }

        public void ToggleCellState(int id)
        {
            level.ToggleCellState(id);
        }

        public void CrossCell(int id)
        {
            level.CrossCell(id);
        }
    }
}

