using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using FlappyBuraders.GlobalStuff;

// Deriving the IAPManager class from IStoreListener enables it to receive messages from Unity Purchasing.
public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    
    public static string IAP_hero = "com.oyyystudio.gopro";  
    public static string IAP_BP = "buybp";
        
    // // Apple App Store-specific product identifier for the subscription product.
    // private static string kProductNameAppleSubscription =  "com.unity3d.subscription.new";
    
    // // Google Play Store-specific product identifier subscription product.
    // private static string kProductNameGooglePlaySubscription =  "com.unity3d.subscription.original"; 
    
    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }
    
    public void InitializePurchasing() 
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            return;
        }
        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        
        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        builder.AddProduct(IAP_hero, ProductType.Consumable);
        builder.AddProduct(IAP_BP, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }
    
    
    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }
    
    
    public void BuyHero()
    {
        BuyProductID(IAP_hero);
    }
    public void BuyBP() {
        BuyProductID(IAP_BP);
    }
    
    
    private void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);
            
            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }
    
    
    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
        
        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer || 
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");
            
            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }
    
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");
        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }
    
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
        {
            if (String.Equals(args.purchasedProduct.definition.id, IAP_hero, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
                LocalData.hasGonePRO = true;
                LocalData.lockedBuraders.Remove(Enums.BuraderName.GIO);
                LocalData.lockedBuraders.Remove(Enums.BuraderName.HORORO);
                LocalData.lockedBuraders.Remove(Enums.BuraderName.PLATIMOON);
                AudioManager.instance.PlaySFX(SFXS.SLASH);
                LocalData.BP += 29999;
                LocalData.toSave = true;
                SaveController.SaveGame();
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
            if (String.Equals(args.purchasedProduct.definition.id, IAP_BP, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
                LocalData.hasGonePRO = true;

                AudioManager.instance.PlaySFX(SFXS.SLASH);
                LocalData.BP += 99999;
                LocalData.toSave = true;
                SaveController.SaveGame();
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
            // Or ... an unknown product has been purchased by this user. Fill in additional products here....
            else 
            {
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            }

            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 
            return PurchaseProcessingResult.Complete;
        }
	
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        // this.PostNotification(PromptWindow.PromptError,
        //     new PromptErrorRequest() {
        //         ErrorMessage = PromptMessage.PURCHASE_FAILED,
        //         PrimaryButtonMessage = PromptMessage.OK,
        //         Callback = () => {}
        //     }
        // );
    }
}
