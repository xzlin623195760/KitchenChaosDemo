using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnRecipeSpawned;

    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnRecipeSuccess;

    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private float spawnRecipeTimerMax = 4f;
    [SerializeField] private int waitingRecipesMax = 4;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private int successfulRecipesAmount;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
        spawnRecipeTimer = 0f;
        successfulRecipesAmount = 0;
    }

    private void Update() {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO waitRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log($"RecipeName:{waitRecipeSO.recipeName}");
                waitingRecipeSOList.Add(waitRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // 已知有相同数量的食材 接下来检查是否同类食材
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    // 遍历该食谱中所需的食材是否在盘子里面
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        // 没有在盘子里找到食谱所需食材 则遍历下一份食谱
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe) {
                    //Debug.Log($"玩家交付正确的食谱:{waitingRecipeSO.recipeName}");
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    successfulRecipesAmount++;
                    return;
                }
            }
        }

        // 没有匹配符合的食谱
        //Debug.Log("玩家未能交付正确食谱");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount() {
        return successfulRecipesAmount;
    }
}