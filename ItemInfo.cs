using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    //TODO: Need to return to this script when all keys have been created so array sizes
    //can be adjusted and the keys can be added to this script.
    //TODO: Return to this script to reorganize the arrays. There should probably be a series of variables
    //and the array index is assigned to the variable.
    //TODO: Add conditional statements either in this script or a different one that checks to see how many
    //bullets are being sold. Maybe 6-10 pistol bullets would be worth 2 gems and 1-5 pistol bullets would be worth 1 gem.
    //Selling a single bullet could be exploited possibly.

    //ID number of the item
    private int[] ID = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
    //Bool that determines if the item can be sold
    private bool KeyItem;
    //Name of the item
    private string[] Name = {"Round Cut Gem S", "Round Cut Gem M", "Round Cut Gem L", "Rose Cut Gem S", "Rose Cut Gem M",
    "Rose Cut Gem L", "Pear Cut Gem S", "Pear Cut Gem M", "Pear Cut Gem L", "Small First Aid Kit", "Large First Aid Kit",
    "Pistol Ammo", "Shotgun Ammo", "Rifle Ammo", "Fuel", "Assist Small First Aid Kit", "Assist Large First Aid Kit",
    "Assist Pistol Ammo", "Assist Shotgun Ammo", "Assist Rifle Ammo", "Old Flashlight", "LED Flashlight"};
    //Description of the item
    private string[] Description = {
        "A crystal that is left behind when a monster is killed. It is warm to the touch and causes your hands to tingle.",
        "A first aid kit for treating minor wounds. Restores a small amount of health.",
        "A first aid kit for treating severe wounds. Fully restores health.",
        "Ammunition used by pistols.",
        "Ammunition used by shotguns.",
        "Ammunition used by rifles.",
        "Fuel that can be used to power generators.",
        "An old flashlight. The light is dim, but the batteries appear to have been replaced recently, so it should last for a while.",
        "A newer, brighter flashlight."};
    //Type of the item
    private string[] Type = { "Currency", "Healing", "Ammo", "Fuel", "Key Item", "Upgrade" };
    //How much the item costs to purchase
    private int[] Cost = { 3, 6, 4, 6, 8, 10, 15 };
    //How much the item is worth when sold
    private int[] ItemValue = { 1, 3, 2, 3, 4, 5, 0 };
    //How much each gem is worth
    private int[] GemValue = { 1, 2, 5, 7, 10, 15, 20, 25, 30 };


    //The image of each item that will be displayed in an item panel.
    private Image[] ItemImages;

    private int RoundGemSID = 1;
    private string RoundGemSName = "Round Cut Gem S";
    private string RoundGemSDesc = "A crystal that is left behind when a monster is killed. It is warm to the touch and causes your hands to tingle.";
    private string RoundGemSType = "Currency";
    private int RoundGemSValue = 1;
    private Image RoundGemSImage;

    private int RoundGemMID = 2;
    private string RoundGemMName = "Round Cut Gem M";
    private string RoundGemMDesc = "A crystal that is left behind when a monster is killed. It is warm to the touch and causes your hands to tingle.";
    private string RoundGemMType = "Currency";
    private int RoundGemMValue = 2;
    private Image RoundGemMImage;

    private int RoundGemLID = 3;
    private string RoundGemLName = "Round Cut Gem L";
    private string RoundGemLDesc = "A crystal that is left behind when a monster is killed. It is warm to the touch and causes your hands to tingle.";
    private string RoundGemLType = "Currency";
    private int RoundGemLValue = 5;
    private Image RoundGemLImage;

    private int RoseGemSID = 4;
    private string RoseGemSName = "Rose Cut Gem S";
    private string RoseGemSDesc = "A crystal that is left behind when a monster is killed. It is warm to the touch and causes your hands to tingle.";
    private string RoseGemSType = "Currency";
    private int RoseGemSValue = 7;
    private Image RoseGemSImage;

    private int RoseGemMID = 5;
    private string RoseGemMName = "Rose Cut Gem M";
    private string RoseGemMDesc = "A crystal that is left behind when a monster is killed. It is warm to the touch and causes your hands to tingle.";
    private string RoseGemMType = "Currency";
    private int RoseGemMValue = 10;
    private Image RoseGemMImage;

    private int RoseGemLID = 6;
    private string RoseGemLName = "Rose Cut Gem L";
    private string RoseGemLDesc = "A crystal that is left behind when a monster is killed. It is warm to the touch and causes your hands to tingle.";
    private string RoseGemLType = "Currency";
    private int RoseGemLValue = 15;
    private Image RoseGemLImage;

    private int PearGemSID = 7;
    private string PearGemSName = "Pear Cut Gem S";
    private string PearGemSDesc = "A crystal that is left behind when a monster is killed. It is warm to the touch and causes your hands to tingle.";
    private string PearGemSType = "Currency";
    private int PearGemSValue = 20;
    private Image PearGemSImage;

    private int PearGemMID = 8;
    private string PearGemMName = "Pear Cut Gem M";
    private string PearGemMDesc = "A crystal that is left behind when a monster is killed. It is warm to the touch and causes your hands to tingle.";
    private string PearGemMType = "Currency";
    private int PearGemMValue = 25;
    private Image PearGemMImage;

    private int PearGemLID = 9;
    private string PearGemLName = "Pear Cut Gem L";
    private string PearGemLDesc = "A crystal that is left behind when a monster is killed. It is warm to the touch and causes your hands to tingle.";
    private string PearGemLType = "Currency";
    private int PearGemLValue = 30;
    private Image PearGemLImage;

    private int SmallAidKitID = 10;
    private string SmallAidKitName = "Small First Aid Kit";
    private string SmallAidKitDesc = "A first aid kit for treating minor wounds. Restores a small amount of health.";
    private string SmallAidKitType = "Healing";
    private int SmallAidKitCost = 3;
    private int SmallAidKitValue = 1;
    private Image SmallAidKitImage;

    private int LargeAidKitID = 11;
    private string LargeAidKitName = "Large First Aid Kit";
    private string LargeAidKitDesc = "A first aid kit for treating severe wounds. Fully restores health.";
    private string LargeAidKitType = "Healing";
    private int LargeAidKitCost = 6;
    private int LargeAidKitValue = 3;
    private Image LargeAidKitImage;

    private int PistolAmmoID = 12;
    private string PistolAmmoName = "Pistol Ammo";
    private string PistolAmmoDesc = "Ammunition used by pistols.";
    private string PistolAmmoType = "Ammo";
    private int PistolAmmoCost = 4;
    private int PistolAmmoValue = 2;
    private Image PistolAmmoImage;

    private int ShotgunAmmoID = 13;
    private string ShotgunAmmoName = "Shotgun Ammo";
    private string ShotgunAmmoDesc = "Ammunition used by shotguns.";
    private string ShotgunAmmoType = "Ammo";
    private int ShotgunAmmoCost = 6;
    private int ShotgunAmmoValue = 3;
    private Image ShotgunAmmoImage;

    private int RifleAmmoID = 14;
    private string RifleAmmoName = "Rifle Ammo";
    private string RifleAmmoDesc = "Ammunition used by rifles.";
    private string RifleAmmoType = "Ammo";
    private int RifleAmmoCost = 8;
    private int RifleAmmoValue = 4;
    private Image RifleAmmoImage;

    private int FuelID = 15;
    private string FuelName = "Fuel";
    private string FuelDesc = "Fuel that can be used to power generators.";
    private string FuelType = "Fuel";
    private int FuelCost = 10;
    private int FuelValue = 5;
    private Image FuelImage;

    private int AssistSmallAidKitID = 16;
    private string AssistSmallAidKitName = "Assist Small First Aid Kit";
    private string AssistSmallAidKitDesc = "A first aid kit left by someone. Restores a small amount of health.";
    private string AssistSmallAidKitType = "Healing";
    private int AssistSmallAidKitValue = 0;
    private Image AssistSmallAidKitImage;

    private int AssistLargeAidKitID = 17;
    private string AssistLargeAidKitName = "Assist Large First Aid Kit";
    private string AssistLargeAidKitDesc = "A first aid kit left by someone. Fully restores health.";
    private string AssistLargeAidKitType = "Healing";
    private int AssistLargeAidKitValue = 0;
    private Image AssistLargeAidKitImage;

    private int AssistPistolAmmoID = 18;
    private string AssistPistolAmmoName = "Assist Pistol Ammo";
    private string AssistPistolAmmoDesc = "Ammunition left by someone. Used by pistols.";
    private string AssistPistolAmmoType = "Ammo";
    private int AssistPistolAmmoValue = 0;
    private Image AssistPistolAmmoImage;

    private int AssistShotgunAmmoID = 19;
    private string AssistShotgunAmmoName = "Assist Shotgun Ammo";
    private string AssistShotgunAmmoDesc = "Ammunition left by someone. Used by shotguns.";
    private string AssistShotgunAmmoType = "Ammo";
    private int AssistShotgunAmmoValue = 0;
    private Image AssistShotgunAmmoImage;

    private int AssistRifleAmmoID = 20;
    private string AssistRifleAmmoName = "Assist Rifle Ammo";
    private string AssistRifleAmmoDesc = "Ammunition left by someone. Used by rifles.";
    private string AssistRifleAmmoType = "Ammo";
    private int AssistRifleAmmoValue = 0;
    private Image AssistRifleAmmoImage;

    private int OldFlashlightID = 21;
    private string OldFlashlightName = "Old Flashlight";
    private string OldFlashlightDesc = "An old flashlight. The light is dim, but the batteries appear to have been replaced recently, so it should last for a while.";
    private string OldFlashlightType = "Key Item";
    private Image OldFlashlightImage;

    private int LEDFlashlightID = 22;
    private string LEDFlashlightName = "LED Flashlight";
    private string LEDFlashlightDesc = "A newer, brighter flashlight.";
    private string LEDFlashlightType = "Upgrade";
    private int LEDFlashlightCost = 15;
    private Image LEDFlashlightImage;

    void Start ()
    {
        //RoundGemSImage = 
        //RoundGemMImage = 
        //RoundGemLImage = 
        //RoseGemSImage = 
        //RoseGemMImage = 
        //RoseGemLImage = 
        //PearGemSImage = 
        //PearGemMImage = 
        //PearGemLImage =
        //SmallAidKitImage =
        //LargeAidKitImage =
        //PistolAmmoImage = 
        //ShotgunAmmoImage = 
        //RifleAmmoImage = 
        //FuelImage = 
        //AssistSmallAidKitImage = 
        //AssistLargeAidKitImage = 
        //AssistPistolAmmoImage = 
        //AssistShotgunAmmoImage = 
        //AssistRifleAmmoImage = 
        //OldFlashlightImage = 
        //LEDFlashlightImage = 

    }

	void Update ()
    {
		
	}
}
