using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemManager : MonoBehaviour
{
    //This script manages all the items in the game. It increments items when found or purchased,
    //decrements them when sold, used, or discarded, and adds/removes them from item panels.

    //TODO: Make sure the curitemcount and other variables are used properly so the functions work.

    //These are the sprites that will be assigned to item panels
    [SerializeField] private Sprite SmallFirstAidKitSprite;
    [SerializeField] private Sprite LargeFirstAidKitSprite;
    [SerializeField] private Sprite PistolAmmoSprite;
    [SerializeField] private Sprite ShotgunAmmoSprite;
    [SerializeField] private Sprite RifleAmmoSprite;
    [SerializeField] private Sprite FuelSprite;

    //These are the ammo scripts that are attached to each instance of ammo that will be accessed from this script.
    //Need to add the other ammo scripts when they are written.
    private PistolAmmoFromEnemy pistolAmmoFromEnemy;

    private int NumberOfPanels = 18;

    [SerializeField] private ItemImages itemImages;

    [SerializeField] private Image ItemImage1;
    [SerializeField] private Image ItemImage2;
    [SerializeField] private Image ItemImage3;
    [SerializeField] private Image ItemImage4;
    [SerializeField] private Image ItemImage5;
    [SerializeField] private Image ItemImage6;
    [SerializeField] private Image ItemImage7;
    [SerializeField] private Image ItemImage8;
    [SerializeField] private Image ItemImage9;
    [SerializeField] private Image ItemImage10;
    [SerializeField] private Image ItemImage11;
    [SerializeField] private Image ItemImage12;
    [SerializeField] private Image ItemImage13;
    [SerializeField] private Image ItemImage14;
    [SerializeField] private Image ItemImage15;
    [SerializeField] private Image ItemImage16;
    [SerializeField] private Image ItemImage17;
    [SerializeField] private Image ItemImage18;

    [SerializeField] private Text ItemNameText1;
    [SerializeField] private Text ItemNameText2;
    [SerializeField] private Text ItemNameText3;
    [SerializeField] private Text ItemNameText4;
    [SerializeField] private Text ItemNameText5;
    [SerializeField] private Text ItemNameText6;
    [SerializeField] private Text ItemNameText7;
    [SerializeField] private Text ItemNameText8;
    [SerializeField] private Text ItemNameText9;
    [SerializeField] private Text ItemNameText10;
    [SerializeField] private Text ItemNameText11;
    [SerializeField] private Text ItemNameText12;
    [SerializeField] private Text ItemNameText13;
    [SerializeField] private Text ItemNameText14;
    [SerializeField] private Text ItemNameText15;
    [SerializeField] private Text ItemNameText16;
    [SerializeField] private Text ItemNameText17;
    [SerializeField] private Text ItemNameText18;

    [SerializeField] private Text ItemCountText1;
    [SerializeField] private Text ItemCountText2;
    [SerializeField] private Text ItemCountText3;
    [SerializeField] private Text ItemCountText4;
    [SerializeField] private Text ItemCountText5;
    [SerializeField] private Text ItemCountText6;
    [SerializeField] private Text ItemCountText7;
    [SerializeField] private Text ItemCountText8;
    [SerializeField] private Text ItemCountText9;
    [SerializeField] private Text ItemCountText10;
    [SerializeField] private Text ItemCountText11;
    [SerializeField] private Text ItemCountText12;
    [SerializeField] private Text ItemCountText13;
    [SerializeField] private Text ItemCountText14;
    [SerializeField] private Text ItemCountText15;
    [SerializeField] private Text ItemCountText16;
    [SerializeField] private Text ItemCountText17;
    [SerializeField] private Text ItemCountText18;

    //Number of items the player can hold in a single item panel
    const int FirstAidPanelMaxCount = 3;
    const int PistolPanelMaxAmmoCount = 40;
    const int ShotgunPanelMaxAmmoCount = 24;
    const int RiflePanelMaxAmmoCount = 15;
    const int FuelPanelMaxCount = 2;
    const int AssistAidPanelMaxCount = 2;
    const int AssistPistolPanelMaxAmmoCount = 14;
    const int AssistShotgunPanelMaxAmmoCount = 6;
    const int AssistRiflePanelMaxAmmoCount = 4;

    //The total number of each item that can be held in the inventory. It's panel max count times number of empty panels.
    public int SmallFirstAidKitTotalCount;
    public int LargeFirstAidKitTotalCount;
    public int PistolAmmoTotalCount;
    public int ShotgunAmmoTotalCount;
    public int RifleAmmoTotalCount;
    public int FuelTotalCount;
    public int AssistSmallAidTotalCount;
    public int AssistLargeAidTotalCount;
    public int AssistPistolAmmoTotalCount;
    public int AssistShotgunAmmoTotalCount;
    public int AssistRifleAmmoTotalCount;

    //These will be accessed by the ammo scripts so this script can add them to the panels.
    public int tempPistolAmmo;
    public int tempShotgunAmmo;
    public int tempRifleAmmo;

    public int excessPistolAmmo;

    //The current count of each item
    public static int SmallFirstAidKitCount;
    public static int LargeFirstAidKitCount;
    public static int PistolAmmoCount;
    public static int ShotgunAmmoCount;
    public static int RifleAmmoCount;
    public static int FuelCount;
    public static int AssistSmallAidCount;
    public static int AssistLargeAidCount;
    public static int AssistPistolAmmoCount;
    public static int AssistShotgunAmmoCount;
    public static int AssistRifleAmmoCount;

    public int EarliestEmptyPanel;
    public bool NoEmptyPanels;

    //These will be manipulated via a function so it can be used in a switch to know which panel to add or remove item count from.
    public string ItemToPickUp;
    public string OccupiedPanel;
    public int OccupiedPanelNumber;

    //private string itemName;
    //private string itemType;
    //private int curItemCount;
    //private int maxItemCount;
    //private int emptyPanelCount;

    void Start ()
    {
        SmallFirstAidKitCount = 0;
        LargeFirstAidKitCount = 0;
        PistolAmmoCount = 0;
        ShotgunAmmoCount = 0;
        RifleAmmoCount = 0;
        FuelCount = 0;
        AssistSmallAidCount = 0;
        AssistLargeAidCount = 0;
        AssistPistolAmmoCount = 0;
        AssistShotgunAmmoCount = 0;
        AssistRifleAmmoCount = 0;

        ItemNameText1.text = "";
        ItemNameText2.text = "";
        ItemNameText3.text = "";
        ItemNameText4.text = "";
        ItemNameText5.text = "";
        ItemNameText6.text = "";
        ItemNameText7.text = "";
        ItemNameText8.text = "";
        ItemNameText9.text = "";
        ItemNameText10.text = "";
        ItemNameText11.text = "";
        ItemNameText12.text = "";
        ItemNameText13.text = "";
        ItemNameText14.text = "";
        ItemNameText15.text = "";
        ItemNameText16.text = "";
        ItemNameText17.text = "";
        ItemNameText18.text = "";
    }

	void Update ()
    {
        SmallFirstAidKitTotalCount = FirstAidPanelMaxCount * UIController.NumOfEmptyItemPanels;
        LargeFirstAidKitTotalCount = FirstAidPanelMaxCount * UIController.NumOfEmptyItemPanels;
        PistolAmmoTotalCount = PistolPanelMaxAmmoCount * UIController.NumOfEmptyItemPanels;
        ShotgunAmmoTotalCount = ShotgunPanelMaxAmmoCount * UIController.NumOfEmptyItemPanels;
        RifleAmmoTotalCount = RiflePanelMaxAmmoCount * UIController.NumOfEmptyItemPanels;
        FuelTotalCount = FuelPanelMaxCount * UIController.NumOfEmptyItemPanels;
        AssistSmallAidTotalCount = AssistAidPanelMaxCount * UIController.NumOfEmptyItemPanels;
        AssistLargeAidTotalCount = AssistAidPanelMaxCount * UIController.NumOfEmptyItemPanels;
        AssistPistolAmmoTotalCount = AssistPistolPanelMaxAmmoCount * UIController.NumOfEmptyItemPanels;
        AssistShotgunAmmoTotalCount = AssistShotgunPanelMaxAmmoCount * UIController.NumOfEmptyItemPanels;
        AssistRifleAmmoTotalCount = AssistRiflePanelMaxAmmoCount * UIController.NumOfEmptyItemPanels;

        Debug.Log("Item count text 1: " + ItemCountText1.text);
        Debug.Log("Pistol Ammo count: " + PistolAmmoCount);
        Debug.Log("Earliest empty panel: " + EarliestEmptyPanel);
        Debug.Log("Tag: " + FirstPersonController.tag);
        Debug.Log("Item Image 1: " + ItemImage1.sprite);

        //Debug.Log("Small Aid Total: " + SmallFirstAidKitTotalCount);
        //Debug.Log("Large Aid Total: " + LargeFirstAidKitTotalCount);
        //Debug.Log("Pistol Ammo Total: " + PistolAmmoTotalCount);
        //Debug.Log("Shotgun Ammo Total: " + ShotgunAmmoTotalCount);
        //Debug.Log("Rifle Ammo Total: " + RifleAmmoTotalCount);
        //Debug.Log("Fuel Total: " + FuelTotalCount);
        //Debug.Log("Assist Small Aid Total: " + AssistSmallAidTotalCount);
        //Debug.Log("Assist Large Aid Total: " + AssistLargeAidTotalCount);
        //Debug.Log("Assist Pistol Total: " + AssistPistolAmmoTotalCount);
        //Debug.Log("Assist Shotgun Total: " + AssistShotgunAmmoTotalCount);
        //Debug.Log("Assist Rifle Total: " + AssistRifleAmmoTotalCount);
    }

    //Iterates through each panel's image to see if any are null. If so, return an integer
    //of the first available empty panel. If not, set a bool so the item can't be picked up
    public void FindEmptyPanel()
    {
            if (ItemImage1.sprite == null)
            {
                EarliestEmptyPanel = 1;
                NoEmptyPanels = false;
            }
            else if (ItemImage2.sprite == null)
            {
                EarliestEmptyPanel = 2;
                NoEmptyPanels = false;
            }
            else if (ItemImage3.sprite == null)
            {
                EarliestEmptyPanel = 3;
                NoEmptyPanels = false;
            }
            else if (ItemImage4.sprite == null)
            {
                EarliestEmptyPanel = 4;
                NoEmptyPanels = false;
            }
            else if (ItemImage5.sprite == null)
            {
                EarliestEmptyPanel = 5;
                NoEmptyPanels = false;
            }
            else if (ItemImage6.sprite == null)
            {
                EarliestEmptyPanel = 6;
                NoEmptyPanels = false;
            }
            else if (ItemImage7.sprite == null)
            {
                EarliestEmptyPanel = 7;
                NoEmptyPanels = false;
            }
            else if (ItemImage8.sprite == null)
            {
                EarliestEmptyPanel = 8;
                NoEmptyPanels = false;
            }
            else if (ItemImage9.sprite == null)
            {
                EarliestEmptyPanel = 9;
                NoEmptyPanels = false;
            }
            else if (ItemImage10.sprite == null)
            {
                EarliestEmptyPanel = 10;
                NoEmptyPanels = false;
            }
            else if (ItemImage11.sprite == null)
            {
                EarliestEmptyPanel = 11;
                NoEmptyPanels = false;
            }
            else if (ItemImage12.sprite == null)
            {
                EarliestEmptyPanel = 12;
                NoEmptyPanels = false;
            }
            else if (ItemImage13.sprite == null)
            {
                EarliestEmptyPanel = 13;
                NoEmptyPanels = false;
            }
            else if (ItemImage14.sprite == null)
            {
                EarliestEmptyPanel = 14;
                NoEmptyPanels = false;
            }
            else if (ItemImage15.sprite == null)
            {
                EarliestEmptyPanel = 15;
                NoEmptyPanels = false;
            }
            else if (ItemImage16.sprite == null)
            {
                EarliestEmptyPanel = 16;
                NoEmptyPanels = false;
            }
            else if (ItemImage17.sprite == null)
            {
                EarliestEmptyPanel = 17;
                NoEmptyPanels = false;
            }
            else if (ItemImage18.sprite == null)
            {
                EarliestEmptyPanel = 18;
                NoEmptyPanels = false;
            }
            else
            {
                EarliestEmptyPanel = 0;
                NoEmptyPanels = true;
            }
    }

    //TODO: This function might not be needed any longer.
    //All items in inventory will initially be hidden. When they are incremented from 0 to 1,
    //reveal the item in the inventory. When the item count is 0, hide the item again.
    public void RevealItem()
    {

    }
    //TODO: Repeat the small first aid code to the other item cases.


    //Increments the item count in the inventory. 
    //If item count is max, don't allow item to be picked up and incremented.
    public void AddItem()
    {
        //TODO: remove this and add it to each case individual since there's a possibility of it being needed more than
        //once in a single add item function call.
        FindEmptyPanel();

        //Keep an eye on these and see if these need to be removed.
        tempPistolAmmo = 0;
        tempShotgunAmmo = 0;
        tempRifleAmmo = 0;

        //This gets the pistol ammo from enemy script from the pistol ammo game object when this function is called from the item pickup script.
        pistolAmmoFromEnemy = FirstPersonController.targetedObject.GetComponent<PistolAmmoFromEnemy>();

        switch(FirstPersonController.tag)
        {
            case "Small First Aid Kit":
                ItemToPickUp = "Small First Aid Kit";

                //Finds the earliest available empty panel and assigns the small first aid kit to it.
                if (SmallFirstAidKitCount == 0)
                {
                    switch(EarliestEmptyPanel)
                    {
                        case 1:
                            ItemImage1.color = new Color(1, 1, 1, 1);
                            ItemImage1.sprite = SmallFirstAidKitSprite;                           
                            ItemNameText1.text = "Small First Aid Kit";
                            ItemCountText1.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 2:
                            ItemImage2.color = new Color(1, 1, 1, 1);
                            ItemImage2.sprite = SmallFirstAidKitSprite;
                            ItemNameText2.text = "Small First Aid Kit";
                            ItemCountText2.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 3:
                            ItemImage3.color = new Color(1, 1, 1, 1);
                            ItemImage3.sprite = SmallFirstAidKitSprite;
                            ItemNameText3.text = "Small First Aid Kit";
                            ItemCountText3.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 4:
                            ItemImage4.color = new Color(1, 1, 1, 1);
                            ItemImage4.sprite = SmallFirstAidKitSprite;
                            ItemNameText4.text = "Small First Aid Kit";
                            ItemCountText4.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 5:
                            ItemImage5.color = new Color(1, 1, 1, 1);
                            ItemImage5.sprite = SmallFirstAidKitSprite;
                            ItemNameText5.text = "Small First Aid Kit";
                            ItemCountText5.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 6:
                            ItemImage6.color = new Color(1, 1, 1, 1);
                            ItemImage6.sprite = SmallFirstAidKitSprite;
                            ItemNameText6.text = "Small First Aid Kit";
                            ItemCountText6.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 7:
                            ItemImage7.color = new Color(1, 1, 1, 1);
                            ItemImage7.sprite = SmallFirstAidKitSprite;
                            ItemNameText7.text = "Small First Aid Kit";
                            ItemCountText7.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 8:
                            ItemImage8.color = new Color(1, 1, 1, 1);
                            ItemImage8.sprite = SmallFirstAidKitSprite;
                            ItemNameText8.text = "Small First Aid Kit";
                            ItemCountText8.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 9:
                            ItemImage9.color = new Color(1, 1, 1, 1);
                            ItemImage9.sprite = SmallFirstAidKitSprite;
                            ItemNameText9.text = "Small First Aid Kit";
                            ItemCountText9.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 10:
                            ItemImage10.color = new Color(1, 1, 1, 1);
                            ItemImage10.sprite = SmallFirstAidKitSprite;
                            ItemNameText10.text = "Small First Aid Kit";
                            ItemCountText10.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 11:
                            ItemImage11.color = new Color(1, 1, 1, 1);
                            ItemImage11.sprite = SmallFirstAidKitSprite;
                            ItemNameText11.text = "Small First Aid Kit";
                            ItemCountText11.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 12:
                            ItemImage12.color = new Color(1, 1, 1, 1);
                            ItemImage12.sprite = SmallFirstAidKitSprite;
                            ItemNameText12.text = "Small First Aid Kit";
                            ItemCountText12.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 13:
                            ItemImage13.color = new Color(1, 1, 1, 1);
                            ItemImage13.sprite = SmallFirstAidKitSprite;
                            ItemNameText13.text = "Small First Aid Kit";
                            ItemCountText13.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 14:
                            ItemImage14.color = new Color(1, 1, 1, 1);
                            ItemImage14.sprite = SmallFirstAidKitSprite;
                            ItemNameText14.text = "Small First Aid Kit";
                            ItemCountText14.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 15:
                            ItemImage15.color = new Color(1, 1, 1, 1);
                            ItemImage15.sprite = SmallFirstAidKitSprite;
                            ItemNameText15.text = "Small First Aid Kit";
                            ItemCountText15.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 16:
                            ItemImage16.color = new Color(1, 1, 1, 1);
                            ItemImage16.sprite = SmallFirstAidKitSprite;
                            ItemNameText16.text = "Small First Aid Kit";
                            ItemCountText16.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 17:
                            ItemImage17.color = new Color(1, 1, 1, 1);
                            ItemImage17.sprite = SmallFirstAidKitSprite;
                            ItemNameText17.text = "Small First Aid Kit";
                            ItemCountText17.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        case 18:
                            ItemImage18.color = new Color(1, 1, 1, 1);
                            ItemImage18.sprite = SmallFirstAidKitSprite;
                            ItemNameText18.text = "Small First Aid Kit";
                            ItemCountText18.text = "1";
                            SmallFirstAidKitCount++;
                            break;
                        default:
                            //InteractPrompts needs to check for this bool and if it's true, display a text message informing
                            //the player that there is no room to pick up the small first aid kit.
                            NoEmptyPanels = true;
                            break;
                    }
                }
                //If current small aid count is less than the total possible count, this goes 
                //through each panel to find one with small first aid kit then checks the count 
                //to see if it's less than the max panel count. If it is, it increments it.
                else if (SmallFirstAidKitCount < SmallFirstAidKitTotalCount)
                {
                    if (ItemNameText1.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText1.text);
                        if(tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText1.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText2.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText2.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText2.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText3.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText3.text);
                        if(tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText3.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText4.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText4.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText4.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText5.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText5.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText5.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText6.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText6.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText6.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText7.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText7.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText7.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText8.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText8.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText8.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText9.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText9.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText9.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText10.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText10.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText10.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText11.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText11.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText11.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText12.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText12.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText12.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText13.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText13.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText13.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText14.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText14.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText14.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText15.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText15.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText15.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText16.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText16.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText16.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText17.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText17.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText17.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText18.text == "Small First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText18.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            SmallFirstAidKitCount++;
                            ItemCountText18.text = tempItemCount.ToString();
                        }
                    }
                }
                break;
            case "Large First Aid Kit":
                ItemToPickUp = "Large First Aid Kit";

                if (LargeFirstAidKitCount == 0)
                {
                    //TODO: Come back to this later. Currently, this assume only 1 aid kit is being added at a time.
                    switch (EarliestEmptyPanel)
                    {
                        case 1:
                            ItemImage1.color = new Color(1, 1, 1, 1);
                            ItemImage1.sprite = LargeFirstAidKitSprite;
                            ItemNameText1.text = "Large First Aid Kit";
                            ItemCountText1.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 2:
                            ItemImage2.color = new Color(1, 1, 1, 1);
                            ItemImage2.sprite = LargeFirstAidKitSprite;
                            ItemNameText2.text = "Large First Aid Kit";
                            ItemCountText2.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 3:
                            ItemImage3.color = new Color(1, 1, 1, 1);
                            ItemImage3.sprite = LargeFirstAidKitSprite;
                            ItemNameText3.text = "Large First Aid Kit";
                            ItemCountText3.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 4:
                            ItemImage4.color = new Color(1, 1, 1, 1);
                            ItemImage4.sprite = LargeFirstAidKitSprite;
                            ItemNameText4.text = "Large First Aid Kit";
                            ItemCountText4.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 5:
                            ItemImage5.color = new Color(1, 1, 1, 1);
                            ItemImage5.sprite = LargeFirstAidKitSprite;
                            ItemNameText5.text = "Large First Aid Kit";
                            ItemCountText5.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 6:
                            ItemImage6.color = new Color(1, 1, 1, 1);
                            ItemImage6.sprite = LargeFirstAidKitSprite;
                            ItemNameText6.text = "Large First Aid Kit";
                            ItemCountText6.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 7:
                            ItemImage7.color = new Color(1, 1, 1, 1);
                            ItemImage7.sprite = LargeFirstAidKitSprite;
                            ItemNameText7.text = "Large First Aid Kit";
                            ItemCountText7.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 8:
                            ItemImage8.color = new Color(1, 1, 1, 1);
                            ItemImage8.sprite = LargeFirstAidKitSprite;
                            ItemNameText8.text = "Large First Aid Kit";
                            ItemCountText8.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 9:
                            ItemImage9.color = new Color(1, 1, 1, 1);
                            ItemImage9.sprite = LargeFirstAidKitSprite;
                            ItemNameText9.text = "Large First Aid Kit";
                            ItemCountText9.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 10:
                            ItemImage10.color = new Color(1, 1, 1, 1);
                            ItemImage10.sprite = LargeFirstAidKitSprite;
                            ItemNameText10.text = "Large First Aid Kit";
                            ItemCountText10.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 11:
                            ItemImage11.color = new Color(1, 1, 1, 1);
                            ItemImage11.sprite = LargeFirstAidKitSprite;
                            ItemNameText11.text = "Large First Aid Kit";
                            ItemCountText11.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 12:
                            ItemImage12.color = new Color(1, 1, 1, 1);
                            ItemImage12.sprite = LargeFirstAidKitSprite;
                            ItemNameText12.text = "Large First Aid Kit";
                            ItemCountText12.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 13:
                            ItemImage13.color = new Color(1, 1, 1, 1);
                            ItemImage13.sprite = LargeFirstAidKitSprite;
                            ItemNameText13.text = "Large First Aid Kit";
                            ItemCountText13.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 14:
                            ItemImage14.color = new Color(1, 1, 1, 1);
                            ItemImage14.sprite = LargeFirstAidKitSprite;
                            ItemNameText14.text = "Large First Aid Kit";
                            ItemCountText14.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 15:
                            ItemImage15.color = new Color(1, 1, 1, 1);
                            ItemImage15.sprite = LargeFirstAidKitSprite;
                            ItemNameText15.text = "Large First Aid Kit";
                            ItemCountText15.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 16:
                            ItemImage16.color = new Color(1, 1, 1, 1);
                            ItemImage16.sprite = LargeFirstAidKitSprite;
                            ItemNameText16.text = "Large First Aid Kit";
                            ItemCountText16.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 17:
                            ItemImage17.color = new Color(1, 1, 1, 1);
                            ItemImage17.sprite = LargeFirstAidKitSprite;
                            ItemNameText17.text = "Large First Aid Kit";
                            ItemCountText17.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        case 18:
                            ItemImage18.color = new Color(1, 1, 1, 1);
                            ItemImage18.sprite = LargeFirstAidKitSprite;
                            ItemNameText18.text = "Large First Aid Kit";
                            ItemCountText18.text = "1";
                            LargeFirstAidKitCount++;
                            break;
                        default:
                            //InteractPrompts needs to check for this bool and if it's true, display a text message informing
                            //the player that there is no room to pick up the small first aid kit.
                            NoEmptyPanels = true;
                            break;
                    }
                }
                else if (LargeFirstAidKitCount < LargeFirstAidKitTotalCount)
                {
                    if (ItemNameText1.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText1.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText1.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText2.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText2.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText2.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText3.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText3.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText3.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText4.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText4.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText4.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText5.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText5.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText5.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText6.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText6.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText6.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText7.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText7.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText7.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText8.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText8.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText8.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText9.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText9.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText9.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText10.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText10.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText10.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText11.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText11.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText11.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText12.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText12.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText12.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText13.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText13.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText13.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText14.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText14.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText14.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText15.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText15.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText15.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText16.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText16.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText16.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText17.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText17.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText17.text = tempItemCount.ToString();
                        }
                    }
                    else if (ItemNameText18.text == "Large First Aid Kit")
                    {
                        int tempItemCount = int.Parse(ItemCountText18.text);
                        if (tempItemCount < FirstAidPanelMaxCount)
                        {
                            tempItemCount++;
                            LargeFirstAidKitCount++;
                            ItemCountText18.text = tempItemCount.ToString();
                        }
                    }
                }
                break;
                //TODO: Look at pistol ammo script. if I want the player to be able to pick up just a part of that ammo,
                //the variables should be stored in that prefab and accessed by this script.
                //TODO: Currently, temp pistol ammo variable is being put in the item count text. Check to see if pistol ammo count
                //variable should be put in that text object instead.
            case "Pistol Ammo":
                ItemToPickUp = "Pistol Ammo";

                if (PistolAmmoCount == 0)
                {
                    FindEmptyPanel();
                    AssignPistolPickUpToEmptyPanel();
                }

                //This means pistol ammo count is more than 0 and less than the total possible count.
                else if (PistolAmmoCount < PistolAmmoTotalCount)
                {

                    //This while loop will continue to execute until all ammo from an ammo pickup is placed in item panels.
                    //This while loop currently executes infinitely. Fix it. It's changed to if for debugging purposes.
                    while (pistolAmmoFromEnemy.allAmmoTaken == false)
                    {
                        //TODO: This might be the problem. Currently can't add ammo to 2nd panel.
                        FindOccupiedPanels();

                        switch (OccupiedPanelNumber)
                        {
                            case 1:
                                tempPistolAmmo = int.Parse(ItemCountText1.text);

                                //TODO: For some reason, excess is reset to 0 so the loop executes infinitely.
                                //If there is still some excess ammo after this loop ran at least once, add that to temp.
                                //If there is none, that means the loop is running for the first time, so add the ammo drop to temp.
                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText1.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {                                    
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    //Temp item count is reduced by the amount stored in excess so it can be added to this panel.
                                    //TODO: Check to see if this results in the panel being assigned the max panel amount.
                                    tempPistolAmmo -= excessPistolAmmo;
                                    //This should be equal to pistol panel max ammo count.
                                    ItemCountText1.text = tempPistolAmmo.ToString();
                                }

                                //This might now the problem.
                                //TODO: Maybe add a bool associated with each panel. if it's one setting, excess can be added to it, otherwise find
                                //a new panel.
                                if (excessPistolAmmo > 0)
                                {
                                    Debug.Log("Line 1045");
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                Debug.Log("Line 1048");
                                break;
                            case 2:
                                tempPistolAmmo = int.Parse(ItemCountText2.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText2.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                    Debug.Log("Line 1058");
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText2.text = tempPistolAmmo.ToString();
                                    Debug.Log("Line 1066");
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                    Debug.Log("Line 1072");
                                }
                                break;
                            case 3:
                                tempPistolAmmo = int.Parse(ItemCountText3.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;
                                Debug.Log("Line 1078");
                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    Debug.Log("Line 1081");
                                    ItemCountText3.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    Debug.Log("Line 1087");
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText3.text = tempPistolAmmo.ToString();
                                    Debug.Log("Line 1093");
                                }
                                Debug.Log("Line 1095");
                                if (excessPistolAmmo > 0)
                                {
                                    Debug.Log("Line 1098");
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                Debug.Log("Line 1102");
                                break;
                            case 4:
                                tempPistolAmmo = int.Parse(ItemCountText4.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText4.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText4.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 5:
                                tempPistolAmmo = int.Parse(ItemCountText5.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText5.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;                                  
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText5.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 6:
                                tempPistolAmmo = int.Parse(ItemCountText6.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText6.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText6.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 7:
                                tempPistolAmmo = int.Parse(ItemCountText7.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText7.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText7.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 8:
                                tempPistolAmmo = int.Parse(ItemCountText8.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText8.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText8.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 9:
                                tempPistolAmmo = int.Parse(ItemCountText9.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText9.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText9.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 10:
                                tempPistolAmmo = int.Parse(ItemCountText10.text);
                                
                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;
  
                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText10.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText10.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 11:
                                tempPistolAmmo = int.Parse(ItemCountText11.text);
                                
                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText11.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText11.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 12:
                                tempPistolAmmo = int.Parse(ItemCountText12.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText12.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText12.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 13:
                                tempPistolAmmo = int.Parse(ItemCountText13.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText13.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText13.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 14:
                                tempPistolAmmo = int.Parse(ItemCountText14.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText14.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText14.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 15:
                                tempPistolAmmo = int.Parse(ItemCountText15.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText15.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText15.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 16:
                                tempPistolAmmo = int.Parse(ItemCountText16.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText16.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText16.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 17:
                                tempPistolAmmo = int.Parse(ItemCountText17.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText17.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText17.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                            case 18:
                                tempPistolAmmo = int.Parse(ItemCountText18.text);

                                tempPistolAmmo += pistolAmmoFromEnemy.PistolAmmoAmount;

                                if (tempPistolAmmo <= PistolPanelMaxAmmoCount)
                                {
                                    ItemCountText18.text = tempPistolAmmo.ToString();
                                    pistolAmmoFromEnemy.allAmmoTaken = true;
                                }
                                else if (tempPistolAmmo > PistolPanelMaxAmmoCount)
                                {
                                    excessPistolAmmo = tempPistolAmmo - PistolPanelMaxAmmoCount;
                                    tempPistolAmmo -= excessPistolAmmo;
                                    ItemCountText18.text = tempPistolAmmo.ToString();
                                }

                                if (excessPistolAmmo > 0)
                                {
                                    FindEmptyPanel();
                                    AssignPistolAmmoToEmptyPanel();
                                }
                                break;
                        }
                    }
                }
           
                //This means pistol ammo count is = to the total possible count and a message should pop up on the HUD
                //telling the player the ammo can't be picked up because there is no room and the ammo is NOT collected or destroyed.
                else
                {

                }
                break;

            case "Shotgun Ammo":
                ItemToPickUp = "Shotgun Ammo";

                if (ShotgunAmmoCount == 0)
                {

                }
                break;
            case "Rifle Ammo":
                ItemToPickUp = "Rifle Ammo";

                if (RifleAmmoCount == 0)
                {

                }
                break;
            case "Fuel":
                ItemToPickUp = "Fuel";

                if (FuelCount == 0)
                {

                }
                break;
        }
    }

    //Decrements the item count by 1. If item count is 0, don't allow it to be decremented.
    public void RemoveItem()
    {
        //imageComponent.color = new Color(0, 0, 0, 1);
        //imageComponent.sprite = null;
    }

    //Uses usable items in the inventory. If item is used, call RemoveItem() function. 
    //If item count is zero, don't allow use to be an option.
    public void UseItem()
    {

    }

    //If the discard option is chosen, call the RemoveItem() function.
    //If item is a key item or weapon, don't allow it to be discarded.
    public void DiscardItem()
    {

    }

    //When the examine option is selected on any revealed item, open the examine panel and display the description
    //of the item. Possibly include an image of the item in a window above the description.
    public void ExamineItem()
    {

    }

    //Pressing certain keys or clicking the close button should disable the examine panel.
    public void CloseExaminePanel()
    {

    }

    //If sell item option is chosen in vendor panel, call RemoveItem() function and AddMoney() function.
    //If item is a key item or weapon, don't allow it to be sold.
    public void SellItem()
    {

    }

    //If money item is picked up or an item is sold to vendor, add the specified amount of money.
    //If item is picked up, destroy instance of item being picked up.
    public void AddCurrency()
    {

    }

    //If item or upgrade is purchased, reduce money by the specified amount. If amount to be reduced is
    //greater than amount player has, don't allow purchase to be made or money to be reduced.
    public void RemoveCurrency()
    {

    }

    //During other functions where the count of each item is needed to compare to total counts, this function can be
    //called to parse all text and determine the current total of each item. This will also apply to assist items.
    //TODO: test this function to see if all switches run. May need to switch to a series of if statements.
    public void DetermineItemCount()
    {
        //Sets all counts to 0 before parsing all 18 panel texts to add them together.
        SmallFirstAidKitCount = 0;
        LargeFirstAidKitCount = 0;
        PistolAmmoCount = 0;
        ShotgunAmmoCount = 0;
        RifleAmmoCount = 0;
        FuelCount = 0;
        AssistSmallAidCount = 0;
        AssistLargeAidCount = 0;
        AssistPistolAmmoCount = 0;
        AssistShotgunAmmoCount = 0;
        AssistRifleAmmoCount = 0;

        if (ItemCountText1.text == "")
        {
            ItemCountText1.text = "0";
        }

        if (ItemCountText2.text == "")
        {
            ItemCountText2.text = "0";
        }

        if (ItemCountText3.text == "")
        {
            ItemCountText3.text = "0";
        }

        if (ItemCountText4.text == "")
        {
            ItemCountText4.text = "0";
        }

        if (ItemCountText5.text == "")
        {
            ItemCountText5.text = "0";
        }

        if (ItemCountText6.text == "")
        {
            ItemCountText6.text = "0";
        }

        if (ItemCountText7.text == "")
        {
            ItemCountText7.text = "0";
        }

        if (ItemCountText8.text == "")
        {
            ItemCountText8.text = "0";
        }

        if (ItemCountText9.text == "")
        {
            ItemCountText9.text = "0";
        }

        if (ItemCountText10.text == "")
        {
            ItemCountText10.text = "0";
        }

        if (ItemCountText11.text == "")
        {
            ItemCountText11.text = "0";
        }

        if (ItemCountText12.text == "")
        {
            ItemCountText12.text = "0";
        }

        if (ItemCountText13.text == "")
        {
            ItemCountText13.text = "0";
        }

        if (ItemCountText14.text == "")
        {
            ItemCountText14.text = "0";
        }

        if (ItemCountText15.text == "")
        {
            ItemCountText15.text = "0";
        }

        if (ItemCountText16.text == "")
        {
            ItemCountText16.text = "0";
        }

        if (ItemCountText17.text == "")
        {
            ItemCountText17.text = "0";
        }

        if (ItemCountText18.text == "")
        {
            ItemCountText18.text = "0";
        }

        switch (ItemNameText1.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText1.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText1.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText1.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText1.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText1.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText1.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText1.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText1.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText1.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText1.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText1.text);
                break;
        }

        switch (ItemNameText2.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText2.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText2.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText2.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText2.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText2.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText2.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText2.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText2.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText2.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText2.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText2.text);
                break;
        }

        switch (ItemNameText3.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText3.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText3.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText3.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText3.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText3.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText3.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText3.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText3.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText3.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText3.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText3.text);
                break;
        }

        switch (ItemNameText4.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText4.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText4.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText4.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText4.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText4.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText4.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText4.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText4.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText4.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText4.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText4.text);
                break;
        }

        switch (ItemNameText5.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText5.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText5.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText5.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText5.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText5.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText5.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText5.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText5.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText5.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText5.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText5.text);
                break;
        }

        switch (ItemNameText6.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText6.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText6.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText6.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText6.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText6.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText6.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText6.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText6.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText6.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText6.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText6.text);
                break;
        }

        switch (ItemNameText7.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText7.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText7.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText7.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText7.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText7.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText7.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText7.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText7.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText7.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText7.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText7.text);
                break;
        }
        
        switch (ItemNameText8.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText8.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText8.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText8.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText8.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText8.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText8.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText8.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText8.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText8.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText8.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText8.text);
                break;
        }

        switch (ItemNameText9.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText9.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText9.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText9.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText9.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText9.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText9.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText9.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText9.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText9.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText9.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText9.text);
                break;
        }

        switch (ItemNameText10.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText10.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText10.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText10.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText10.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText10.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText10.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText10.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText10.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText10.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText10.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText10.text);
                break;
        }

        switch (ItemNameText11.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText11.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText11.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText11.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText11.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText11.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText11.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText11.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText11.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText11.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText11.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText11.text);
                break;
        }

        switch (ItemNameText12.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText12.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText12.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText12.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText12.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText12.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText12.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText12.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText12.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText12.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText12.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText12.text);
                break;
        }

        switch (ItemNameText13.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText13.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText13.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText13.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText13.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText13.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText13.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText13.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText13.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText13.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText13.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText13.text);
                break;
        }

        switch (ItemNameText14.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText14.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText14.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText14.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText14.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText14.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText14.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText14.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText14.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText14.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText14.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText14.text);
                break;
        }

        switch (ItemNameText15.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText15.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText15.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText15.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText15.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText15.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText15.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText15.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText15.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText15.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText15.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText15.text);
                break;
        }

        switch (ItemNameText16.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText16.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText16.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText16.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText16.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText16.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText16.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText16.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText16.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText16.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText16.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText16.text);
                break;
        }

        switch (ItemNameText17.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText17.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText17.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText17.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText17.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText17.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText17.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText17.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText17.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText17.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText17.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText17.text);
                break;
        }

        switch (ItemNameText18.text)
        {
            case "Small First Aid Kit":
                SmallFirstAidKitCount += int.Parse(ItemCountText18.text);
                break;
            case "Large First Aid Kit":
                LargeFirstAidKitCount += int.Parse(ItemCountText18.text);
                break;
            case "Pistol Ammo":
                PistolAmmoCount += int.Parse(ItemCountText18.text);
                break;
            case "Shotgun Ammo":
                ShotgunAmmoCount += int.Parse(ItemCountText18.text);
                break;
            case "Rifle Ammo":
                RifleAmmoCount += int.Parse(ItemCountText18.text);
                break;
            case "Fuel":
                FuelCount += int.Parse(ItemCountText18.text);
                break;
            case "Assist Small First Aid Kit":
                AssistSmallAidCount += int.Parse(ItemCountText18.text);
                break;
            case "Assist Large First Aid Kit":
                AssistLargeAidCount += int.Parse(ItemCountText18.text);
                break;
            case "Assist Pistol Ammo":
                AssistPistolAmmoCount += int.Parse(ItemCountText18.text);
                break;
            case "Assist Shotgun Ammo":
                AssistShotgunAmmoCount += int.Parse(ItemCountText18.text);
                break;
            case "Assist Rifle Ammo":
                AssistRifleAmmoCount += int.Parse(ItemCountText18.text);
                break;
        }

        if (ItemCountText1.text == "0")
        {
            ItemCountText1.text = "";
        }

        if (ItemCountText2.text == "0")
        {
            ItemCountText2.text = "";
        }

        if (ItemCountText3.text == "0")
        {
            ItemCountText3.text = "";
        }

        if (ItemCountText4.text == "0")
        {
            ItemCountText4.text = "";
        }

        if (ItemCountText5.text == "0")
        {
            ItemCountText5.text = "";
        }

        if (ItemCountText6.text == "0")
        {
            ItemCountText6.text = "";
        }

        if (ItemCountText7.text == "0")
        {
            ItemCountText7.text = "";
        }

        if (ItemCountText8.text == "0")
        {
            ItemCountText8.text = "";
        }

        if (ItemCountText9.text == "0")
        {
            ItemCountText9.text = "";
        }

        if (ItemCountText10.text == "0")
        {
            ItemCountText10.text = "";
        }

        if (ItemCountText11.text == "0")
        {
            ItemCountText11.text = "";
        }

        if (ItemCountText12.text == "0")
        {
            ItemCountText12.text = "";
        }

        if (ItemCountText13.text == "0")
        {
            ItemCountText13.text = "";
        }

        if (ItemCountText14.text == "0")
        {
            ItemCountText14.text = "";
        }

        if (ItemCountText15.text == "0")
        {
            ItemCountText15.text = "";
        }

        if (ItemCountText16.text == "0")
        {
            ItemCountText16.text = "";
        }

        if (ItemCountText17.text == "0")
        {
            ItemCountText17.text = "";
        }

        if (ItemCountText18.text == "0")
        {
            ItemCountText18.text = "";
        }
    }

    //This function will look through the panels to see what is in them. Then it'll change a variable that'll be used
    //as a variable on switch statements so the add item function knows where to put what items.
    public void FindOccupiedPanels()
    {
        //The occupied variables are cleared of values from the last time the function ran before being changed this time.
        OccupiedPanel = "";
        OccupiedPanelNumber = 0;

        //When add item is called and it has been determined which item is going to be picked up, this switch statement cycles through each panel
        //searching for the specified item. If it's found and its count is less than the max panel count, it'll return variables that
        //tell the function that panel can be added to.
        //TODO: Check to see if this is getting the item counts from the right panels. Ex: if it gets an item count from panel 2, make
        //sure the item name is the correct name.
        //TODO: If adding name text evaluation works, return and add it to the rest.
        //TODO: The inner if statements might be redundant. Return at another time and see if removing them doesn't change anything.
        switch (ItemToPickUp)
        {
            case "Small First Aid Kit":
                if (ItemNameText1.text == "Small First Aid Kit" && int.Parse(ItemCountText1.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText1.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 1;
                    }
                }
                else if (ItemNameText2.text == "Small First Aid Kit" && int.Parse(ItemCountText2.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText2.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 2;
                    }
                }
                else if (ItemNameText3.text == "Small First Aid Kit" && int.Parse(ItemCountText3.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText3.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 3;
                    }
                }
                else if (ItemNameText4.text == "Small First Aid Kit" && int.Parse(ItemCountText4.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText4.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 4;
                    }
                }
                else if (ItemNameText5.text == "Small First Aid Kit" && int.Parse(ItemCountText5.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText5.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 5;
                    }
                }
                else if (ItemNameText6.text == "Small First Aid Kit" && int.Parse(ItemCountText6.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText6.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 6;
                    }
                }
                else if (ItemNameText7.text == "Small First Aid Kit" && int.Parse(ItemCountText7.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText7.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 7;
                    }
                }
                else if (ItemNameText8.text == "Small First Aid Kit" && int.Parse(ItemCountText8.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText8.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 8;
                    }
                }
                else if (ItemNameText9.text == "Small First Aid Kit" && int.Parse(ItemCountText9.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText9.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 9;
                    }
                }
                else if (ItemNameText10.text == "Small First Aid Kit" && int.Parse(ItemCountText10.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText10.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 10;
                    }
                }
                else if (ItemNameText11.text == "Small First Aid Kit" && int.Parse(ItemCountText11.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText11.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 11;
                    }
                }
                else if (ItemNameText12.text == "Small First Aid Kit" && int.Parse(ItemCountText12.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText12.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 12;
                    }
                }
                else if (ItemNameText13.text == "Small First Aid Kit" && int.Parse(ItemCountText13.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText13.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 13;
                    }
                }
                else if (ItemNameText14.text == "Small First Aid Kit" && int.Parse(ItemCountText14.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText14.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 14;
                    }
                }
                else if (ItemNameText15.text == "Small First Aid Kit" && int.Parse(ItemCountText15.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText15.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 15;
                    }
                }
                else if (ItemNameText16.text == "Small First Aid Kit" && int.Parse(ItemCountText16.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText16.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 16;
                    }
                }
                else if (ItemNameText17.text == "Small First Aid Kit" && int.Parse(ItemCountText17.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText17.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 17;
                    }
                }
                else if (ItemNameText18.text == "Small First Aid Kit" && int.Parse(ItemCountText18.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText18.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Small First Aid Kit";
                        OccupiedPanelNumber = 18;
                    }
                }     
                break;
            case "Large First Aid Kit":
                if (ItemNameText1.text == "Large First Aid Kit" && int.Parse(ItemCountText1.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText1.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 1;
                    }
                }
                else if (ItemNameText2.text == "Large First Aid Kit" && int.Parse(ItemCountText2.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText2.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 2;
                    }
                }
                else if (ItemNameText3.text == "Large First Aid Kit" && int.Parse(ItemCountText3.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText3.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 3;
                    }
                }
                else if (ItemNameText4.text == "Large First Aid Kit" && int.Parse(ItemCountText4.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText4.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 4;
                    }
                }
                else if (ItemNameText5.text == "Large First Aid Kit" && int.Parse(ItemCountText5.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText5.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 5;
                    }
                }
                else if (ItemNameText6.text == "Large First Aid Kit" && int.Parse(ItemCountText6.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText6.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 6;
                    }
                }
                else if (ItemNameText7.text == "Large First Aid Kit" && int.Parse(ItemCountText7.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText7.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 7;
                    }
                }
                else if (ItemNameText8.text == "Large First Aid Kit" && int.Parse(ItemCountText8.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText8.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 8;
                    }
                }
                else if (ItemNameText9.text == "Large First Aid Kit" && int.Parse(ItemCountText9.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText9.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 9;
                    }
                }
                else if (ItemNameText10.text == "Large First Aid Kit" && int.Parse(ItemCountText10.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText10.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 10;
                    }
                }
                else if (ItemNameText11.text == "Large First Aid Kit" && int.Parse(ItemCountText11.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText11.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 11;
                    }
                }
                else if (ItemNameText12.text == "Large First Aid Kit" && int.Parse(ItemCountText12.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText12.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 12;
                    }
                }
                else if (ItemNameText13.text == "Large First Aid Kit" && int.Parse(ItemCountText13.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText13.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 13;
                    }
                }
                else if (ItemNameText14.text == "Large First Aid Kit" && int.Parse(ItemCountText14.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText14.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 14;
                    }
                }
                else if (ItemNameText15.text == "Large First Aid Kit" && int.Parse(ItemCountText15.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText15.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 15;
                    }
                }
                else if (ItemNameText16.text == "Large First Aid Kit" && int.Parse(ItemCountText16.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText16.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 16;
                    }
                }
                else if (ItemNameText17.text == "Large First Aid Kit" && int.Parse(ItemCountText17.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText17.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 17;
                    }
                }
                else if (ItemNameText18.text == "Large First Aid Kit" && int.Parse(ItemCountText18.text) != FirstAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText18.text) < FirstAidPanelMaxCount)
                    {
                        OccupiedPanel = "Large First Aid Kit";
                        OccupiedPanelNumber = 18;
                    }
                }
                break;
            case "Pistol Ammo":
                //This is the issue. Even if first panel is full, these conditions are always being met.
                //TODO: change itemcount to not equal 40 and see if that works.
                if (ItemNameText1.text == "Pistol Ammo" && int.Parse(ItemCountText1.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText1.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 1;
                    }
                }
                else if (ItemNameText2.text == "Pistol Ammo" && int.Parse(ItemCountText2.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText2.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 2;
                    }
                }
                else if (ItemNameText3.text == "Pistol Ammo" && int.Parse(ItemCountText3.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText3.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 3;
                    }
                }
                else if (ItemNameText4.text == "Pistol Ammo" && int.Parse(ItemCountText4.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText4.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 4;
                    }
                }
                else if (ItemNameText5.text == "Pistol Ammo" && int.Parse(ItemCountText5.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText5.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 5;
                    }
                }
                else if (ItemNameText6.text == "Pistol Ammo" && int.Parse(ItemCountText6.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText6.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 6;
                    }
                }
                else if (ItemNameText7.text == "Pistol Ammo" && int.Parse(ItemCountText7.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText7.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 7;
                    }
                }
                else if (ItemNameText8.text == "Pistol Ammo" && int.Parse(ItemCountText8.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText8.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 8;
                    }
                }
                else if (ItemNameText9.text == "Pistol Ammo" && int.Parse(ItemCountText9.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText9.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 9;
                    }
                }
                else if (ItemNameText10.text == "Pistol Ammo" && int.Parse(ItemCountText10.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText10.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 10;
                    }
                }
                else if (ItemNameText11.text == "Pistol Ammo" && int.Parse(ItemCountText11.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText11.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 11;
                    }
                }
                else if (ItemNameText12.text == "Pistol Ammo" && int.Parse(ItemCountText12.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText12.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 12;
                    }
                }
                else if (ItemNameText13.text == "Pistol Ammo" && int.Parse(ItemCountText13.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText13.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 13;
                    }
                }
                else if (ItemNameText14.text == "Pistol Ammo" && int.Parse(ItemCountText14.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText14.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 14;
                    }
                }
                else if (ItemNameText15.text == "Pistol Ammo" && int.Parse(ItemCountText15.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText15.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 15;
                    }
                }
                else if (ItemNameText16.text == "Pistol Ammo" && int.Parse(ItemCountText16.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText16.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 16;
                    }
                }
                else if (ItemNameText17.text == "Pistol Ammo" && int.Parse(ItemCountText17.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText17.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 17;
                    }
                }
                else if (ItemNameText18.text == "Pistol Ammo" && int.Parse(ItemCountText18.text) != PistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText18.text) < PistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Pistol Ammo";
                        OccupiedPanelNumber = 18;
                    }
                }
                break;
            case "Shotgun Ammo":
                if (ItemNameText1.text == "Shotgun Ammo" && int.Parse(ItemCountText1.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText1.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 1;
                    }
                }
                else if (ItemNameText2.text == "Shotgun Ammo" && int.Parse(ItemCountText2.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText2.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 2;
                    }
                }
                else if (ItemNameText3.text == "Shotgun Ammo" && int.Parse(ItemCountText3.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText3.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 3;
                    }
                }
                else if (ItemNameText4.text == "Shotgun Ammo" && int.Parse(ItemCountText4.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText4.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 4;
                    }
                }
                else if (ItemNameText5.text == "Shotgun Ammo" && int.Parse(ItemCountText5.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText5.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 5;
                    }
                }
                else if (ItemNameText6.text == "Shotgun Ammo" && int.Parse(ItemCountText6.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText6.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 6;
                    }
                }
                else if (ItemNameText7.text == "Shotgun Ammo" && int.Parse(ItemCountText7.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText7.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 7;
                    }
                }
                else if (ItemNameText8.text == "Shotgun Ammo" && int.Parse(ItemCountText8.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText8.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 8;
                    }
                }
                else if (ItemNameText9.text == "Shotgun Ammo" && int.Parse(ItemCountText9.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText9.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 9;
                    }
                }
                else if (ItemNameText10.text == "Shotgun Ammo" && int.Parse(ItemCountText10.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText10.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 10;
                    }
                }
                else if (ItemNameText11.text == "Shotgun Ammo" && int.Parse(ItemCountText11.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText11.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 11;
                    }
                }
                else if (ItemNameText12.text == "Shotgun Ammo" && int.Parse(ItemCountText12.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText12.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 12;
                    }
                }
                else if (ItemNameText13.text == "Shotgun Ammo" && int.Parse(ItemCountText13.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText13.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 13;
                    }
                }
                else if (ItemNameText14.text == "Shotgun Ammo" && int.Parse(ItemCountText14.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText14.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 14;
                    }
                }
                else if (ItemNameText15.text == "Shotgun Ammo" && int.Parse(ItemCountText15.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText15.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 15;
                    }
                }
                else if (ItemNameText16.text == "Shotgun Ammo" && int.Parse(ItemCountText16.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText16.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 16;
                    }
                }
                else if (ItemNameText17.text == "Shotgun Ammo" && int.Parse(ItemCountText17.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText17.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 17;
                    }
                }
                else if (ItemNameText18.text == "Shotgun Ammo" && int.Parse(ItemCountText18.text) != ShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText18.text) < ShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Shotgun Ammo";
                        OccupiedPanelNumber = 18;
                    }
                }
                break;
            case "Rifle Ammo":
                if (ItemNameText1.text == "Rifle Ammo" && int.Parse(ItemCountText1.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText1.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 1;
                    }
                }
                else if (ItemNameText2.text == "Rifle Ammo" && int.Parse(ItemCountText2.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText2.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 2;
                    }
                }
                else if (ItemNameText3.text == "Rifle Ammo" && int.Parse(ItemCountText3.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText3.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 3;
                    }
                }
                else if (ItemNameText4.text == "Rifle Ammo" && int.Parse(ItemCountText4.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText4.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 4;
                    }
                }
                else if (ItemNameText5.text == "Rifle Ammo" && int.Parse(ItemCountText5.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText5.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 5;
                    }
                }
                else if (ItemNameText6.text == "Rifle Ammo" && int.Parse(ItemCountText6.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText6.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 6;
                    }
                }
                else if (ItemNameText7.text == "Rifle Ammo" && int.Parse(ItemCountText7.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText7.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 7;
                    }
                }
                else if (ItemNameText8.text == "Rifle Ammo" && int.Parse(ItemCountText8.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText8.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 8;
                    }
                }
                else if (ItemNameText9.text == "Rifle Ammo" && int.Parse(ItemCountText9.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText9.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 9;
                    }
                }
                else if (ItemNameText10.text == "Rifle Ammo" && int.Parse(ItemCountText10.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText10.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 10;
                    }
                }
                else if (ItemNameText11.text == "Rifle Ammo" && int.Parse(ItemCountText11.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText11.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 11;
                    }
                }
                else if (ItemNameText12.text == "Rifle Ammo" && int.Parse(ItemCountText12.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText12.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 12;
                    }
                }
                else if (ItemNameText13.text == "Rifle Ammo" && int.Parse(ItemCountText13.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText13.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 13;
                    }
                }
                else if (ItemNameText14.text == "Rifle Ammo" && int.Parse(ItemCountText14.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText14.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 14;
                    }
                }
                else if (ItemNameText15.text == "Rifle Ammo" && int.Parse(ItemCountText15.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText15.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 15;
                    }
                }
                else if (ItemNameText16.text == "Rifle Ammo" && int.Parse(ItemCountText16.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText16.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 16;
                    }
                }
                else if (ItemNameText17.text == "Rifle Ammo" && int.Parse(ItemCountText17.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText17.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 17;
                    }
                }
                else if (ItemNameText18.text == "Rifle Ammo" && int.Parse(ItemCountText18.text) != RiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText18.text) < RiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Rifle Ammo";
                        OccupiedPanelNumber = 18;
                    }
                }
                break;
            case "Fuel":
                if (ItemNameText1.text == "Fuel" && int.Parse(ItemCountText1.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText1.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 1;
                    }
                }
                else if (ItemNameText2.text == "Fuel" && int.Parse(ItemCountText2.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText2.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 2;
                    }
                }
                else if (ItemNameText3.text == "Fuel" && int.Parse(ItemCountText3.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText3.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 3;
                    }
                }
                else if (ItemNameText4.text == "Fuel" && int.Parse(ItemCountText4.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText4.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 4;
                    }
                }
                else if (ItemNameText5.text == "Fuel" && int.Parse(ItemCountText5.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText5.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 5;
                    }
                }
                else if (ItemNameText6.text == "Fuel" && int.Parse(ItemCountText6.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText6.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 6;
                    }
                }
                else if (ItemNameText7.text == "Fuel" && int.Parse(ItemCountText7.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText7.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 7;
                    }
                }
                else if (ItemNameText8.text == "Fuel" && int.Parse(ItemCountText8.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText8.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 8;
                    }
                }
                else if (ItemNameText9.text == "Fuel" && int.Parse(ItemCountText9.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText9.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 9;
                    }
                }
                else if (ItemNameText10.text == "Fuel" && int.Parse(ItemCountText10.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText10.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 10;
                    }
                }
                else if (ItemNameText11.text == "Fuel" && int.Parse(ItemCountText11.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText11.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 11;
                    }
                }
                else if (ItemNameText12.text == "Fuel" && int.Parse(ItemCountText12.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText12.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 12;
                    }
                }
                else if (ItemNameText13.text == "Fuel" && int.Parse(ItemCountText13.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText13.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 13;
                    }
                }
                else if (ItemNameText14.text == "Fuel" && int.Parse(ItemCountText14.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText14.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 14;
                    }
                }
                else if (ItemNameText15.text == "Fuel" && int.Parse(ItemCountText15.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText15.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 15;
                    }
                }
                else if (ItemNameText16.text == "Fuel" && int.Parse(ItemCountText16.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText16.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 16;
                    }
                }
                else if (ItemNameText17.text == "Fuel" && int.Parse(ItemCountText17.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText17.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 17;
                    }
                }
                else if (ItemNameText18.text == "Fuel" && int.Parse(ItemCountText18.text) != FuelPanelMaxCount)
                {
                    if (int.Parse(ItemCountText18.text) < FuelPanelMaxCount)
                    {
                        OccupiedPanel = "Fuel";
                        OccupiedPanelNumber = 18;
                    }
                }
                break;
            case "Assist Small First Aid Kit":
                if (ItemNameText1.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText1.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText1.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 1;
                    }
                }
                else if (ItemNameText2.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText2.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText2.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 2;
                    }
                }
                else if (ItemNameText3.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText3.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText3.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 3;
                    }
                }
                else if (ItemNameText4.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText4.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText4.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 4;
                    }
                }
                else if (ItemNameText5.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText5.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText5.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 5;
                    }
                }
                else if (ItemNameText6.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText6.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText6.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 6;
                    }
                }
                else if (ItemNameText7.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText7.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText7.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 7;
                    }
                }
                else if (ItemNameText8.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText8.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText8.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 8;
                    }
                }
                else if (ItemNameText9.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText9.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText9.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 9;
                    }
                }
                else if (ItemNameText10.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText10.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText10.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 10;
                    }
                }
                else if (ItemNameText11.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText11.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText11.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 11;
                    }
                }
                else if (ItemNameText12.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText12.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText12.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 12;
                    }
                }
                else if (ItemNameText13.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText13.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText13.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 13;
                    }
                }
                else if (ItemNameText14.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText14.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText14.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 14;
                    }
                }
                else if (ItemNameText15.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText15.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText15.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 15;
                    }
                }
                else if (ItemNameText16.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText16.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText16.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 16;
                    }
                }
                else if (ItemNameText17.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText17.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText17.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 17;
                    }
                }
                else if (ItemNameText18.text == "Assist Small FIrst Aid Kit" && int.Parse(ItemCountText18.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText18.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Small First Aid Kit";
                        OccupiedPanelNumber = 18;
                    }
                }
                break;
            case "Assist Large First Aid Kit":
                if (ItemNameText1.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText1.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText1.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 1;
                    }
                }
                else if (ItemNameText2.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText2.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText2.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 2;
                    }
                }
                else if (ItemNameText3.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText3.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText3.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 3;
                    }
                }
                else if (ItemNameText4.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText4.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText4.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 4;
                    }
                }
                else if (ItemNameText5.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText5.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText5.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 5;
                    }
                }
                else if (ItemNameText6.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText6.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText6.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 6;
                    }
                }
                else if (ItemNameText7.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText7.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText7.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 7;
                    }
                }
                else if (ItemNameText8.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText8.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText8.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 8;
                    }
                }
                else if (ItemNameText9.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText9.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText9.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 9;
                    }
                }
                else if (ItemNameText10.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText10.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText10.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 10;
                    }
                }
                else if (ItemNameText11.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText11.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText11.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 11;
                    }
                }
                else if (ItemNameText12.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText12.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText12.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 12;
                    }
                }
                else if (ItemNameText13.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText13.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText13.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 13;
                    }
                }
                else if (ItemNameText14.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText14.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText14.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 14;
                    }
                }
                else if (ItemNameText15.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText15.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText15.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 15;
                    }
                }
                else if (ItemNameText16.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText16.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText16.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 16;
                    }
                }
                else if (ItemNameText17.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText17.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText17.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 17;
                    }
                }
                else if (ItemNameText18.text == "Assist Large First Aid Kit" && int.Parse(ItemCountText18.text) != AssistAidPanelMaxCount)
                {
                    if (int.Parse(ItemCountText18.text) < AssistAidPanelMaxCount)
                    {
                        OccupiedPanel = "Assist Large First Aid Kit";
                        OccupiedPanelNumber = 18;
                    }
                }
                break;
            case "Assist Pistol Ammo":
                if (ItemNameText1.text == "Assist Pistol Ammo" && int.Parse(ItemCountText1.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText1.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 1;
                    }
                }
                else if (ItemNameText2.text == "Assist Pistol Ammo" && int.Parse(ItemCountText2.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText2.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 2;
                    }
                }
                else if (ItemNameText3.text == "Assist Pistol Ammo" && int.Parse(ItemCountText3.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText3.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 3;
                    }
                }
                else if (ItemNameText4.text == "Assist Pistol Ammo" && int.Parse(ItemCountText4.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText4.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 4;
                    }
                }
                else if (ItemNameText5.text == "Assist Pistol Ammo" && int.Parse(ItemCountText5.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText5.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 5;
                    }
                }
                else if (ItemNameText6.text == "Assist Pistol Ammo" && int.Parse(ItemCountText6.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText6.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 6;
                    }
                }
                else if (ItemNameText7.text == "Assist Pistol Ammo" && int.Parse(ItemCountText7.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText7.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 7;
                    }
                }
                else if (ItemNameText8.text == "Assist Pistol Ammo" && int.Parse(ItemCountText8.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText8.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 8;
                    }
                }
                else if (ItemNameText9.text == "Assist Pistol Ammo" && int.Parse(ItemCountText9.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText9.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 9;
                    }
                }
                else if (ItemNameText10.text == "Assist Pistol Ammo" && int.Parse(ItemCountText10.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText10.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 10;
                    }
                }
                else if (ItemNameText11.text == "Assist Pistol Ammo" && int.Parse(ItemCountText11.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText11.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 11;
                    }
                }
                else if (ItemNameText12.text == "Assist Pistol Ammo" && int.Parse(ItemCountText12.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText12.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 12;
                    }
                }
                else if (ItemNameText13.text == "Assist Pistol Ammo" && int.Parse(ItemCountText13.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText13.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 13;
                    }
                }
                else if (ItemNameText14.text == "Assist Pistol Ammo" && int.Parse(ItemCountText14.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText14.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 14;
                    }
                }
                else if (ItemNameText15.text == "Assist Pistol Ammo" && int.Parse(ItemCountText15.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText15.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 15;
                    }
                }
                else if (ItemNameText16.text == "Assist Pistol Ammo" && int.Parse(ItemCountText16.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText16.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 16;
                    }
                }
                else if (ItemNameText17.text == "Assist Pistol Ammo" && int.Parse(ItemCountText17.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText17.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 17;
                    }
                }
                else if (ItemNameText18.text == "Assist Pistol Ammo" && int.Parse(ItemCountText18.text) != AssistPistolPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText18.text) < AssistPistolPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Pistol Ammo";
                        OccupiedPanelNumber = 18;
                    }
                }
                break;
            case "Assist Shotgun Ammo":
                if (ItemNameText1.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText1.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText1.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 1;
                    }
                }
                else if (ItemNameText2.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText2.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText2.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 2;
                    }
                }
                else if (ItemNameText3.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText3.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText3.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 3;
                    }
                }
                else if (ItemNameText4.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText4.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText4.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 4;
                    }
                }
                else if (ItemNameText5.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText5.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText5.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 5;
                    }
                }
                else if (ItemNameText6.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText6.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText6.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 6;
                    }
                }
                else if (ItemNameText7.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText7.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText7.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 7;
                    }
                }
                else if (ItemNameText8.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText8.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText8.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 8;
                    }
                }
                else if (ItemNameText9.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText9.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText9.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 9;
                    }
                }
                else if (ItemNameText10.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText10.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText10.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 10;
                    }
                }
                else if (ItemNameText11.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText11.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText11.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 11;
                    }
                }
                else if (ItemNameText12.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText12.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText12.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 12;
                    }
                }
                else if (ItemNameText13.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText13.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText13.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 13;
                    }
                }
                else if (ItemNameText14.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText14.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText14.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 14;
                    }
                }
                else if (ItemNameText15.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText15.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText15.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 15;
                    }
                }
                else if (ItemNameText16.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText16.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText16.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 16;
                    }
                }
                else if (ItemNameText17.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText17.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText17.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 17;
                    }
                }
                else if (ItemNameText18.text == "Assist Shotgun Ammo" && int.Parse(ItemCountText18.text) != AssistShotgunPanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText18.text) < AssistShotgunPanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Shotgun Ammo";
                        OccupiedPanelNumber = 18;
                    }
                }
                break;
            case "Assist Rifle Ammo":
                if (ItemNameText1.text == "Assist Rifle Ammo" && int.Parse(ItemCountText1.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText1.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 1;
                    }
                }
                else if (ItemNameText2.text == "Assist Rifle Ammo" && int.Parse(ItemCountText2.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText2.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 2;
                    }
                }
                else if (ItemNameText3.text == "Assist Rifle Ammo" && int.Parse(ItemCountText3.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText3.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 3;
                    }
                }
                else if (ItemNameText4.text == "Assist Rifle Ammo" && int.Parse(ItemCountText4.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText4.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 4;
                    }
                }
                else if (ItemNameText5.text == "Assist Rifle Ammo" && int.Parse(ItemCountText5.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText5.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 5;
                    }
                }
                else if (ItemNameText6.text == "Assist Rifle Ammo" && int.Parse(ItemCountText6.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText6.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 6;
                    }
                }
                else if (ItemNameText7.text == "Assist Rifle Ammo" && int.Parse(ItemCountText7.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText7.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 7;
                    }
                }
                else if (ItemNameText8.text == "Assist Rifle Ammo" && int.Parse(ItemCountText8.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText8.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 8;
                    }
                }
                else if (ItemNameText9.text == "Assist Rifle Ammo" && int.Parse(ItemCountText9.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText9.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 9;
                    }
                }
                else if (ItemNameText10.text == "Assist Rifle Ammo" && int.Parse(ItemCountText10.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText10.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 10;
                    }
                }
                else if (ItemNameText11.text == "Assist Rifle Ammo" && int.Parse(ItemCountText11.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText11.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 11;
                    }
                }
                else if (ItemNameText12.text == "Assist Rifle Ammo" && int.Parse(ItemCountText12.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText12.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 12;
                    }
                }
                else if (ItemNameText13.text == "Assist Rifle Ammo" && int.Parse(ItemCountText13.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText13.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 13;
                    }
                }
                else if (ItemNameText14.text == "Assist Rifle Ammo" && int.Parse(ItemCountText14.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText14.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 14;
                    }
                }
                else if (ItemNameText15.text == "Assist Rifle Ammo" && int.Parse(ItemCountText15.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText15.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 15;
                    }
                }
                else if (ItemNameText16.text == "Assist Rifle Ammo" && int.Parse(ItemCountText16.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText16.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 16;
                    }
                }
                else if (ItemNameText17.text == "Assist Rifle Ammo" && int.Parse(ItemCountText17.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText17.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 17;
                    }
                }
                else if (ItemNameText18.text == "Assist Rifle Ammo" && int.Parse(ItemCountText18.text) != AssistRiflePanelMaxAmmoCount)
                {
                    if (int.Parse(ItemCountText18.text) < AssistRiflePanelMaxAmmoCount)
                    {
                        OccupiedPanel = "Assist Rifle Ammo";
                        OccupiedPanelNumber = 18;
                    }
                }
                break;
        }
    }

    //The add item function will call this function to assign any pistol ammo to an empty panel. This will be used when the ammo
    //count is 0 and is first being added to inventory and when a panel count is full and a new panel is needed.
    public void AssignPistolPickUpToEmptyPanel()
    {
        switch (EarliestEmptyPanel)
        {
            case 1:
                ItemImage1.color = new Color(1, 1, 1, 1);
                ItemImage1.sprite = PistolAmmoSprite;
                ItemNameText1.text = "Pistol Ammo";
                //This should add the pistol ammo game object's ammount to temp before adding it to count.
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText1.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                //TODO: Test this with multiple pistol ammo objects spawned at the same time to see if they are
                //all destroyed when a single one is 'collected'.
                //This should set the bool in the game object's script to true so it'll destroy itself.
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 2:
                ItemImage2.color = new Color(1, 1, 1, 1);
                ItemImage2.sprite = PistolAmmoSprite;
                ItemNameText2.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText2.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 3:
                ItemImage3.color = new Color(1, 1, 1, 1);
                ItemImage3.sprite = PistolAmmoSprite;
                ItemNameText3.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText3.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 4:
                ItemImage4.color = new Color(1, 1, 1, 1);
                ItemImage4.sprite = PistolAmmoSprite;
                ItemNameText4.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText4.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 5:
                ItemImage5.color = new Color(1, 1, 1, 1);
                ItemImage5.sprite = PistolAmmoSprite;
                ItemNameText5.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText5.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 6:
                ItemImage6.color = new Color(1, 1, 1, 1);
                ItemImage6.sprite = PistolAmmoSprite;
                ItemNameText6.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText6.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 7:
                ItemImage7.color = new Color(1, 1, 1, 1);
                ItemImage7.sprite = PistolAmmoSprite;
                ItemNameText7.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText7.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 8:
                ItemImage8.color = new Color(1, 1, 1, 1);
                ItemImage8.sprite = PistolAmmoSprite;
                ItemNameText8.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText8.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 9:
                ItemImage9.color = new Color(1, 1, 1, 1);
                ItemImage9.sprite = PistolAmmoSprite;
                ItemNameText9.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText9.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 10:
                ItemImage10.color = new Color(1, 1, 1, 1);
                ItemImage10.sprite = PistolAmmoSprite;
                ItemNameText10.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText10.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 11:
                ItemImage11.color = new Color(1, 1, 1, 1);
                ItemImage11.sprite = PistolAmmoSprite;
                ItemNameText11.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText11.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 12:
                ItemImage12.color = new Color(1, 1, 1, 1);
                ItemImage12.sprite = PistolAmmoSprite;
                ItemNameText12.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText12.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 13:
                ItemImage13.color = new Color(1, 1, 1, 1);
                ItemImage13.sprite = PistolAmmoSprite;
                ItemNameText13.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText13.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 14:
                ItemImage14.color = new Color(1, 1, 1, 1);
                ItemImage14.sprite = PistolAmmoSprite;
                ItemNameText14.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText14.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 15:
                ItemImage15.color = new Color(1, 1, 1, 1);
                ItemImage15.sprite = PistolAmmoSprite;
                ItemNameText15.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText15.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 16:
                ItemImage16.color = new Color(1, 1, 1, 1);
                ItemImage16.sprite = PistolAmmoSprite;
                ItemNameText16.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText16.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 17:
                ItemImage17.color = new Color(1, 1, 1, 1);
                ItemImage17.sprite = PistolAmmoSprite;
                ItemNameText17.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText17.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 18:
                ItemImage18.color = new Color(1, 1, 1, 1);
                ItemImage18.sprite = PistolAmmoSprite;
                ItemNameText18.text = "Pistol Ammo";
                tempPistolAmmo = pistolAmmoFromEnemy.PistolAmmoAmount;
                ItemCountText18.text = tempPistolAmmo.ToString();
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            default:
                //InteractPrompts needs to check for this bool and if it's true, display a text message informing
                //the player that there is no room to pick up the small first aid kit.
                NoEmptyPanels = true;
                break;
        }
    }

    //This function is nearly identical to the one above, except it won't try to pick up ammo and instead be used to put excess
    //pistol ammo in an empty slot.
    public void AssignPistolAmmoToEmptyPanel()
    {
        switch (EarliestEmptyPanel)
        {
            case 1:
                ItemImage1.color = new Color(1, 1, 1, 1);
                ItemImage1.sprite = PistolAmmoSprite;
                ItemNameText1.text = "Pistol Ammo";
                //Since this is called when excess is > 0, temp should have that value and it should be assigned to the text property.
                ItemCountText1.text = excessPistolAmmo.ToString();
                //This resets excess so it won't be used again until it's assigned a new value.
                excessPistolAmmo = 0;
                DetermineItemCount();
                //TODO: Test this with multiple pistol ammo objects spawned at the same time to see if they are
                //all destroyed when a single one is 'collected'.
                //This should set the bool in the game object's script to true so it'll destroy itself.
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 2:
                ItemImage2.color = new Color(1, 1, 1, 1);
                ItemImage2.sprite = PistolAmmoSprite;
                ItemNameText2.text = "Pistol Ammo";
                ItemCountText2.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 3:
                ItemImage3.color = new Color(1, 1, 1, 1);
                ItemImage3.sprite = PistolAmmoSprite;
                ItemNameText3.text = "Pistol Ammo";
                ItemCountText3.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 4:
                ItemImage4.color = new Color(1, 1, 1, 1);
                ItemImage4.sprite = PistolAmmoSprite;
                ItemNameText4.text = "Pistol Ammo";
                ItemCountText4.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 5:
                ItemImage5.color = new Color(1, 1, 1, 1);
                ItemImage5.sprite = PistolAmmoSprite;
                ItemNameText5.text = "Pistol Ammo";
                ItemCountText5.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 6:
                ItemImage6.color = new Color(1, 1, 1, 1);
                ItemImage6.sprite = PistolAmmoSprite;
                ItemNameText6.text = "Pistol Ammo";
                ItemCountText6.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 7:
                ItemImage7.color = new Color(1, 1, 1, 1);
                ItemImage7.sprite = PistolAmmoSprite;
                ItemNameText7.text = "Pistol Ammo";
                ItemCountText7.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 8:
                ItemImage8.color = new Color(1, 1, 1, 1);
                ItemImage8.sprite = PistolAmmoSprite;
                ItemNameText8.text = "Pistol Ammo";
                ItemCountText8.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 9:
                ItemImage9.color = new Color(1, 1, 1, 1);
                ItemImage9.sprite = PistolAmmoSprite;
                ItemNameText9.text = "Pistol Ammo";
                ItemCountText9.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 10:
                ItemImage10.color = new Color(1, 1, 1, 1);
                ItemImage10.sprite = PistolAmmoSprite;
                ItemNameText10.text = "Pistol Ammo";
                ItemCountText10.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 11:
                ItemImage11.color = new Color(1, 1, 1, 1);
                ItemImage11.sprite = PistolAmmoSprite;
                ItemNameText11.text = "Pistol Ammo";
                ItemCountText11.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 12:
                ItemImage12.color = new Color(1, 1, 1, 1);
                ItemImage12.sprite = PistolAmmoSprite;
                ItemNameText12.text = "Pistol Ammo";     
                ItemCountText12.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 13:
                ItemImage13.color = new Color(1, 1, 1, 1);
                ItemImage13.sprite = PistolAmmoSprite;
                ItemNameText13.text = "Pistol Ammo";      
                ItemCountText13.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 14:
                ItemImage14.color = new Color(1, 1, 1, 1);
                ItemImage14.sprite = PistolAmmoSprite;
                ItemNameText14.text = "Pistol Ammo";            
                ItemCountText14.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 15:
                ItemImage15.color = new Color(1, 1, 1, 1);
                ItemImage15.sprite = PistolAmmoSprite;
                ItemNameText15.text = "Pistol Ammo";
                ItemCountText15.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 16:
                ItemImage16.color = new Color(1, 1, 1, 1);
                ItemImage16.sprite = PistolAmmoSprite;
                ItemNameText16.text = "Pistol Ammo";
                ItemCountText16.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 17:
                ItemImage17.color = new Color(1, 1, 1, 1);
                ItemImage17.sprite = PistolAmmoSprite;
                ItemNameText17.text = "Pistol Ammo";  
                ItemCountText17.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
            case 18:
                ItemImage18.color = new Color(1, 1, 1, 1);
                ItemImage18.sprite = PistolAmmoSprite;
                ItemNameText18.text = "Pistol Ammo";      
                ItemCountText18.text = excessPistolAmmo.ToString();
                excessPistolAmmo = 0;
                DetermineItemCount();
                pistolAmmoFromEnemy.allAmmoTaken = true;
                break;
        }
    }
}
