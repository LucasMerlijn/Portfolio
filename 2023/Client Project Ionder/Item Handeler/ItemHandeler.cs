using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandeler : MonoBehaviour
{
    // Dictionary for the bought items
    public Dictionary<RoomItemSpawnable, int> boughtItems = new Dictionary<RoomItemSpawnable, int>();

    // Lists and amount of sets for the room item sets
    [SerializeField] private int SetAmount = 5;
    private List<RoomItemSpawnable> ItemsSet1 = new List<RoomItemSpawnable>();
    private List<RoomItemSpawnable> ItemsSet2 = new List<RoomItemSpawnable>();
    private List<RoomItemSpawnable> ItemsSet3 = new List<RoomItemSpawnable>();
    private List<RoomItemSpawnable> ItemsSet4 = new List<RoomItemSpawnable>();
    private List<RoomItemSpawnable> ItemsSet5 = new List<RoomItemSpawnable>();
    private List<RoomItemSpawnable> temp = new List<RoomItemSpawnable>();

    // placeholder sprite when there is no item loaded
    public Sprite placeholderImage;

    private int currentShopSet = 1;
    private int currentPage;
    private float pagesSet1, pagesSet2, pagesSet3, pagesSet4, pagesSet5;
    [SerializeField] private Item UIelement;
    [SerializeField] private List<Item> UIelementList = new List<Item>();
    [SerializeField] private Transform parent;


    // Buttons for next pages.
    [SerializeField] private Button _nextStorePage, _nextInventoryPage;

    // Variables for Inventory
    private int currentInventoryID = 0;
    private int currentInventorySet = 0, currentInventoryPage;
    private float Set1InventoryPages, Set2InventoryPages, Set3InventoryPages, Set4InventoryPages, Set5InventoryPages;
    private List<RoomItemSpawnable> tempList = new List<RoomItemSpawnable>();
    [SerializeField] private Transform PlacementParent, InventoryUiElementParent;

    [SerializeField] private ParticleSystem placementParticleSystem;

    // END VARIABLES

    private void Start()
    {
        GetItemSets();
        CalculatePages();
    }

    /// <summary>
    /// Calculates all pages for the store
    /// </summary>
    private void CalculatePages()
    {
        pagesSet1 = Mathf.Ceil(ItemsSet1.Count / 8f);
        pagesSet2 = Mathf.Ceil(ItemsSet2.Count / 8f);
        pagesSet3 = Mathf.Ceil(ItemsSet3.Count / 8f);
        pagesSet4 = Mathf.Ceil(ItemsSet4.Count / 8f);
        pagesSet5 = Mathf.Ceil(ItemsSet5.Count / 8f);
    }

    /// <summary>
    /// Load all prefabs(item sets) into their respective lists
    /// </summary>
    private void GetItemSets()
    {
        for (int i = 0; i < SetAmount; i++)
        {
            string path = "RoomItems/Set" + (i + 1).ToString();

            UnityEngine.Object[] ItemSet = Resources.LoadAll(path, typeof(RoomItemSpawnable));

            foreach (UnityEngine.Object TheItem in ItemSet)
            {
                temp.Add((RoomItemSpawnable)TheItem);
            }

            for (int x = 0; x < temp.Count; x++)
            {
                temp[x].Bought = false;
            }

            switch (i)
            {
                case 0:
                    ItemsSet1 = new List<RoomItemSpawnable>((List<RoomItemSpawnable>)temp);
                    break;
                case 1:
                    ItemsSet2 = new List<RoomItemSpawnable>((List<RoomItemSpawnable>)temp);
                    break;
                case 2:
                    ItemsSet3 = new List<RoomItemSpawnable>((List<RoomItemSpawnable>)temp);
                    break;
                case 3:
                    ItemsSet4 = new List<RoomItemSpawnable>((List<RoomItemSpawnable>)temp);
                    break;
                case 4:
                    ItemsSet5 = new List<RoomItemSpawnable>((List<RoomItemSpawnable>)temp);
                    break;
            }
            temp.Clear();
        }
    }

    #region Shop

    public void OpenShop(int id)
    {
        if (id <= SetAmount)
        {
            ClearUIelements();
            ShowSet(id);
        }
    }

    private void ShowSet(int id)
    {
        switch (id)
        {
            case 1:
                currentShopSet = 1;
                if (pagesSet1 > 1)
                    _nextStorePage.gameObject.SetActive(true);
                else
                    _nextStorePage.gameObject.SetActive(false);
                GenerateShopUIelements(ItemsSet1);
                break;

            case 2:
                currentShopSet = 2;
                if (pagesSet2 > 1)
                    _nextStorePage.gameObject.SetActive(true);
                else
                    _nextStorePage.gameObject.SetActive(false);
                GenerateShopUIelements(ItemsSet2);
                break;

            case 3:
                currentShopSet = 3;
                if (pagesSet3 > 1)
                    _nextStorePage.gameObject.SetActive(true);
                else
                    _nextStorePage.gameObject.SetActive(false);
                GenerateShopUIelements(ItemsSet3);
                break;

            case 4:
                currentShopSet = 4;
                if (pagesSet4 > 1)
                    _nextStorePage.gameObject.SetActive(true);
                else
                    _nextStorePage.gameObject.SetActive(false);
                GenerateShopUIelements(ItemsSet4);
                break;

            case 5:
                currentShopSet = 5;
                if (pagesSet5 > 1)
                    _nextStorePage.gameObject.SetActive(true);
                else
                    _nextStorePage.gameObject.SetActive(false);
                GenerateShopUIelements(ItemsSet5);
                break;

            default:
                Debug.Log("Something went wrong. went to default case");
                break;
        }
        currentPage = 1;
    }

    /// <summary>
    /// Generate the ui elements of the shop.
    /// </summary>
    /// <param name="itemList"></param>
    private void GenerateShopUIelements(List<RoomItemSpawnable> itemList)
    {
        for (int i = 0; i < 8; i++) // Generate the UI placeholders
        {
            Item item = Instantiate(UIelement, parent);
            UIelementList.Add(item);
        }

        for (int i = 0; i < itemList.Count; i++) // Set the ui values
        {
            if (i == 8)
                break;

            if (itemList[i].Bought)
            {
                UIelementList[i].price = "You own this.";
                UIelementList[i].buttonText = "Bought!";
            }
            else
            {
                UIelementList[i].price = "Price: " + itemList[i].price.ToString();
                UIelementList[i].buttonText = "Buy";
            }

            UIelementList[i].RIS = itemList[i];
            UIelementList[i].IH = this;
            UIelementList[i].img = itemList[i].UIimage;
            UIelementList[i].SetUI();
        }
    }
    #region Next Page
    public void NextStorePage()
    {
        ClearUIelements();
        currentPage++;
        switch (currentShopSet)
        {
            case 1:
                if (currentPage > pagesSet1)
                    currentPage = 1;
                LoadNext(ItemsSet1, currentPage - 1, (int)pagesSet1);

                break;

            case 2:
                if (currentPage > pagesSet2)
                    currentPage = 1;
                LoadNext(ItemsSet2, currentPage - 1, (int)pagesSet2);
                break;

            case 3:
                if (currentPage > pagesSet3)
                    currentPage = 1;
                LoadNext(ItemsSet3, currentPage - 1, (int)pagesSet3);
                break;

            case 4:
                if (currentPage > pagesSet4)
                    currentPage = 1;
                LoadNext(ItemsSet4, currentPage - 1, (int)pagesSet4);
                break;

            case 5:
                if (currentPage > pagesSet5)
                    currentPage = 1;
                LoadNext(ItemsSet5, currentPage - 1, (int)pagesSet5);
                break;

            default:
                Debug.Log("Next page, something went wrong.");
                break;
        }
    }

    private void LoadNext(List<RoomItemSpawnable> itemList, int indexOffset, int maxPages)
    {
        for (int i = 0; i < 8; i++) // Generate the UI placeholders
        {
            Item item = Instantiate(UIelement, parent);
            UIelementList.Add(item);
        }

        indexOffset = indexOffset * 8;

        for (int i = 0; i < itemList.Count; i++)
        {
            if (i == 8 || currentPage == maxPages && i == itemList.Count - 8)
                break;

            if (itemList[indexOffset + i].Bought)
            {
                UIelementList[i].price = "You own this.";
                UIelementList[i].buttonText = "Bought!";
            }
            else
            {
                UIelementList[i].price = "Price: " + itemList[indexOffset + i].price.ToString();
                UIelementList[i].buttonText = "Buy";
            }

            UIelementList[i].RIS = itemList[indexOffset + i];
            UIelementList[i].IH = this;
            UIelementList[i].img = itemList[indexOffset + i].UIimage;
            UIelementList[i].SetUI();
        }
    }
    #endregion

    #endregion





    private void ClearUIelements()
    {
        for (int i = 0; i < UIelementList.Count; i++)
        {
            UIelementList[i].price = "";
            UIelementList[i].buttonText = "Buy";
            UIelementList[i].img = placeholderImage;
            Destroy(UIelementList[i].gameObject);
        }
        UIelementList.Clear();
    }

    #region Inventory

    public void boughtItem(Item item)
    {
        boughtItems.Add(item.RIS, item.RIS.getID);
        //Debug.Log(boughtItems.Count);
    }

    public void OpenInventory(int id)
    {
        currentInventoryID = id;
        ClearTempList();
        ClearUIelements();
        GenerateInventoryUIelements(boughtItems, id);
    }


    private void GenerateInventoryUIelements(Dictionary<RoomItemSpawnable, int> boughtList, int ID)
    {
        for (int i = 0; i < 8; i++) // Generate the UI placeholders
        {
            Item item = Instantiate(UIelement, InventoryUiElementParent);
            UIelementList.Add(item);
        }

        foreach (var boughtItem in boughtList)
        {
            if (boughtItem.Value == ID)
            {
                tempList.Add(boughtItem.Key);
            }
        }

        for (int i = 0; i < tempList.Count; i++)
        {
            if (i == 8)
                break;

            UIelementList[i].img = tempList[i].UIimage;
            UIelementList[i].RIS = tempList[i];
            UIelementList[i].PlaceButton(PlacementParent, placementParticleSystem);
            UIelementList[i].InventoryIni();
        }
        currentInventorySet = ID;
    }

    public void NextInventoryPage()
    {
        ClearUIelements();
        currentInventoryPage++;
        switch (currentInventorySet)
        {
            case 1:
                CalculateInventoryPages();
                if (currentInventoryPage > Set1InventoryPages)
                    currentInventoryPage = 1;
                InventoryLoadPage(tempList, currentInventoryPage - 1, (int)Set1InventoryPages);
                break;

            case 2:
                CalculateInventoryPages();
                if (currentInventoryPage > Set2InventoryPages)
                    currentInventoryPage = 1;
                InventoryLoadPage(tempList, currentInventoryPage - 1, (int)Set2InventoryPages);
                break;

            case 3:
                CalculateInventoryPages();
                if (currentInventoryPage > Set3InventoryPages)
                    currentInventoryPage = 1;
                InventoryLoadPage(tempList, currentInventoryPage - 1, (int)Set3InventoryPages);
                break;

            case 4:
                CalculateInventoryPages();
                if (currentInventoryPage > Set4InventoryPages)
                    currentInventoryPage = 1;
                InventoryLoadPage(tempList, currentInventoryPage - 1, (int)Set4InventoryPages);
                break;

            case 5:
                CalculateInventoryPages();
                if (currentInventoryPage > Set5InventoryPages)
                    currentInventoryPage = 1;
                InventoryLoadPage(tempList, currentInventoryPage - 1, (int)Set5InventoryPages);
                break;

            default:
                Debug.Log("Next page, something went wrong.");
                break;
        }


    }

    private void CalculateInventoryPages()
    {
        Set1InventoryPages = Mathf.Ceil(ItemsSet1.Count / 8f);
        Set2InventoryPages = Mathf.Ceil(ItemsSet2.Count / 8f);
        Set3InventoryPages = Mathf.Ceil(ItemsSet3.Count / 8f);
        Set4InventoryPages = Mathf.Ceil(ItemsSet4.Count / 8f);
        Set5InventoryPages = Mathf.Ceil(ItemsSet5.Count / 8f);
    }

    private void InventoryLoadPage(List<RoomItemSpawnable> itemList, int indexOffset, int maxPages)
    {
        for (int i = 0; i < 8; i++) // Generate the UI placeholders
        {
            Item item = Instantiate(UIelement, InventoryUiElementParent);
            UIelementList.Add(item);
        }

        indexOffset = indexOffset * 8;

        for (int i = 0; i < itemList.Count; i++)
        {
            if (i == 8 || currentInventoryPage == maxPages && i == itemList.Count - 8)
                break;

            UIelementList[i].RIS = itemList[indexOffset + i];
            UIelementList[i].img = itemList[indexOffset + i].UIimage;
            UIelementList[i].InventoryIni();
            UIelementList[i].PlaceButton(PlacementParent, placementParticleSystem);
        }
    }

    private void ClearTempList()
    {
        tempList.Clear();
        tempList = new List<RoomItemSpawnable>();
    }


    #endregion
}