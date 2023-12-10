using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace HUD
{
    public class HUDManager : MonoBehaviour
    {
        public static HUDManager Singleton;
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private TextMeshProUGUI _toastText;
        [SerializeField] private GameObject _interactPrompt;
        [SerializeField] private GameObject _toastPanel;

        private void Awake()
        {
            Singleton = this;
        }

        public void UpdateCoinHUD(int currentCoins)
        {
            _coinText.text = currentCoins.ToString();
        }

        public void ToggleInteractPrompt(bool isActive)
        {
            _interactPrompt.SetActive(isActive);
        }

        public void ShowToast(string text, bool isNegative = false)
        {
            StartCoroutine(ShowToastRoutine());

            IEnumerator ShowToastRoutine()
            {
                _toastPanel.SetActive(false);
                _toastText.text = text;
                yield return new WaitForEndOfFrame();
                _toastPanel.SetActive(true);

                _toastText.color = isNegative ? Color.red : Color.black;
            }
        }
    }
}