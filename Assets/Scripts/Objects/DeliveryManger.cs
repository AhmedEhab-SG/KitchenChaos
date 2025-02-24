using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManger : MonoBehaviour {

    public event EventHandler OnRecipeSpwaned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSuccess;



    public static DeliveryManger Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spwanRecipeTimer;
    private float maxSpwanRecipeTimer = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipesAmount = 0;



    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spwanRecipeTimer -= Time.deltaTime;
        if (spwanRecipeTimer <= 0f) {
            spwanRecipeTimer = maxSpwanRecipeTimer;

            if (waitingRecipeSOList.Count < waitingRecipesMax) {

                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpwaned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {

        foreach (RecipeSO recipeSO in waitingRecipeSOList) {
            RecipeSO waitingRecipeSO = recipeSO;

            // Check if the recipe is the same as the plate
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {

                bool plateContentsMatchRecipe = true;

                // cyicle in all the ingriedants the recipelist
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {

                    bool ingriedantFound = false;
                    // cycle in all the ingriedants the plate
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {

                        // if the ingriedant is not the same
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            ingriedantFound = true;
                            break;
                        }
                    }
                    // if the ingriedant is not found on plate
                    if (!ingriedantFound)
                        plateContentsMatchRecipe = false;
                }

                // if the plate contents match the recipe
                if (plateContentsMatchRecipe) {
                    successfulRecipesAmount++;

                    waitingRecipeSOList.Remove(recipeSO);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // No match found
        // Player didnt deliver a correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount() {
        return successfulRecipesAmount;
    }

}
