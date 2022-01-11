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
        }

        public void ToggleCellState(Vector3 position)
        {

        }

        public void CrossCell(Vector3 position)
        {

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

