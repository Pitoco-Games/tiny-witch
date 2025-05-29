using System.Collections;
using System.Threading.Tasks;
using CoreGameplay.Items.Inventory;
using NUnit.Framework;
using UnityEngine.TestTools;
using Utils;
using Utils.Save;

public class InventoryTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void InventoryTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator InventoryTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [Test]
    public async Task AddItemToInventory()
    {
        PlayerInventoryService playerInventoryService = await SetupPlayerInventoryService();

        await playerInventoryService.AddItem("Sample1", 1);

        Assert.IsTrue(playerInventoryService.TakeItem("Sample1") != null);
    }

    private static async Task<PlayerInventoryService> SetupPlayerInventoryService()
    {
        var saveService = new SaveService();
        ServicesLocator.Register<SaveService>(saveService);

        var playerInventoryService = await PlayerInventoryService.Create(saveService);
        ServicesLocator.Register<PlayerInventoryService>(playerInventoryService);
        return playerInventoryService;
    }
}
