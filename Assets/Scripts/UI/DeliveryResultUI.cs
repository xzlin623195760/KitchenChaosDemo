using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour {
    private const string POPU = "Popu";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successIcon;
    [SerializeField] private Sprite failedIcon;

    private Animator animator;

    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        gameObject.SetActive(true);
        animator.SetTrigger(POPU);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedIcon;
        messageText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        gameObject.SetActive(true);
        animator.SetTrigger(POPU);
        backgroundImage.color = successColor;
        iconImage.sprite = successIcon;
        messageText.text = "DELIVERY\nSUCCESS";
    }
}