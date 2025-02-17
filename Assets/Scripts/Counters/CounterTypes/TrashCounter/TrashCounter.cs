using System;

public class TrashCounter : BaseCounter {
    public static event EventHandler OnAnyObjectTrash;

    public override void Interact(Player player) {
        if (player.HasKitchenObject())
            player.GetKitchenObject().DestorySelf();

        OnAnyObjectTrash?.Invoke(this, EventArgs.Empty);
    }
}
