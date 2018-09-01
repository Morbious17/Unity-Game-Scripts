/******************************************************************************
  File Name: OptionsManager.cs
  Author(s): Kyle Powell (kyle.powell)
    Project: Dead Letter - Prototype 2.0

Description: This file contains functions that manage the main buttons in the
             options tab.
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController = null;
      //the main buttons in the option tab
    [SerializeField] private Button audioSettingsButton = null;
    [SerializeField] private Button videoSettingsButton = null;
    [SerializeField] private Button controlSettingsButton = null;
    [SerializeField] private Button gameplaySettingsButton = null;
    [SerializeField] private Button quitGameButton = null;
      //the panels attached to each main button
    [SerializeField] private GameObject audioSettingsPanel = null;
    [SerializeField] private GameObject videoSettingsPanel = null;
    [SerializeField] private GameObject controlSettingsPanel = null;
    [SerializeField] private GameObject gameplaySettingsPanel = null;
    [SerializeField] private GameObject quitGamePanel = null;
      //this plays background music on the current scene
    [SerializeField] private AudioSource musicAudioSource = null;
      //sliders that control the volume of music and sound effects
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Slider soundVolumeSlider = null;
      //this button resets music and sound sliders to default values
    [SerializeField] private Button audioDefaultSettingsButton = null;
      //this button resets the brighting slider to its default value
    [SerializeField] private Button videoDefaultSettingsButton = null;
      //this button resets the mouse sensitivity to its default value
    [SerializeField] private Button controlDefaultSettingsButton = null;
      //this button resets crouch setting to tis default value
    [SerializeField] private Button gameplayDefaultSettingsButton = null;
      //plays player sound effects, such as footsteps
    [SerializeField] private AudioSource playerAudioSource = null;
      //plays firing and reloading sound effects of the guns
    [SerializeField] private AudioSource pistolAudiosource = null;
    [SerializeField] private AudioSource shotgunAudioSource = null;
    [SerializeField] private AudioSource rifleAudioSource = null;
      //toggle that switches between fullscreen and window mode
    [SerializeField] private Toggle fullScreenToggle = null;
      //allows player to change resolution
    [SerializeField] private Dropdown resolutionDropdown = null;
      //raises and lowers the game's brightness
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private Image brightnessHUDOverlay = null;
      //this slider controls the mouse sensitivity
    [SerializeField] private Slider mouseSensitivitySlider = null;
      //when enabled, inverts the y axis
    [SerializeField] private Toggle invertYAxisToggle = null;
      //this enables the keyboard control buttons panel
    [SerializeField] private Button keyboardControlButton = null;
      //displays default keyboard controls
    [SerializeField] private GameObject keyboardControlPanel = null;
      //this is used to either require holding the button down or pressing it
      //again to crouch or uncrouch
    [SerializeField] private Toggle crouchHoldToggle = null;

      //array that contains all sound effect audio sources
    private AudioSource[] soundEffectAudioSources = new AudioSource[4];

      //creates color that will be altered with brightness slider
    private Color brightnessHUDOverlayColor = new Color(0f, 0f, 0f, 0f);

    private const float defaultMusicVolume = 0.05f;
    private const float defaultSoundVolume = 0.5f;
    private const float defaultBrightness = 0.75f;
    private const float defaultMouseXSensitivity = 2f;
    private const float defaultMouseYSensitivity = 2f;

    private bool crouchHold; //if true, player must hold crouch button
      //the player cannot hotkey out of inventory or press other buttons until
      //the quit yes or quit no button has been pressed
    private bool quitPanelOpen;

    void Start ()
    {
        SetSoundEffectAudioSourceArray();

          //sets audio, brightness, and mouse settings to their default values
        musicVolumeSlider.value = defaultMusicVolume;
        soundVolumeSlider.value = defaultSoundVolume;
        brightnessSlider.value = defaultBrightness;
        mouseSensitivitySlider.value = defaultMouseXSensitivity;

        if (Screen.fullScreen)
        {
            fullScreenToggle.isOn = true;
        }

        UpdateBrightness();
        UpdateMusicVolume();
        UpdateSoundVolume();
        UpdateMouseSensitivity();

        crouchHoldToggle.isOn = true; //player must hold crouch button by default
        crouchHold = true;
    }

    /**************************************************************************
   Function: PressAudioSettingsButton

Description: This function deselects all other sub tab buttons and hides their
             panels while displaying the audio setting's panel and its
             buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressAudioSettingsButton()
    {
        audioSettingsButton.interactable = false;
        audioSettingsPanel.SetActive(true);

          //deselecting all buttons so it doesn't matter what order the player
          //clicks a button
        DeselectVideoSettingsButton();
        DeselectControlSettingsButton();
        DeselectKeyboardControlButton();
        DeselectGameplaySettingsButton();
        DeselectQuitGameButton();
    }

    /**************************************************************************
   Function: PressVideoSettingsButton

Description: This function deselects all other sub tab buttons and hides their
             panels while displaying the video setting's panel and its
             buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressVideoSettingsButton()
    {
        videoSettingsButton.interactable = false;
        videoSettingsPanel.SetActive(true);

        DeselectAudioSettingsButton();
        DeselectControlSettingsButton();
        DeselectKeyboardControlButton();
        DeselectGameplaySettingsButton();
        DeselectQuitGameButton();
    }

    /**************************************************************************
   Function: PressControlSettingsButton

Description: This function deselects all other sub tab buttons and hides their
             panels while displaying the control setting's panel and its
             buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressControlSettingsButton()
    {
        controlSettingsButton.interactable = false;
        controlSettingsPanel.SetActive(true);

        DeselectAudioSettingsButton();
        DeselectVideoSettingsButton();
        DeselectGameplaySettingsButton();
        DeselectQuitGameButton();
    }

    /**************************************************************************
   Function: PressGameplaySettingsButton

Description: This function deselects all other sub tab buttons and hides their
             panels while displaying the gameplay setting's panel and its
             buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressGameplaySettingsButton()
    {
        gameplaySettingsButton.interactable = false;
        gameplaySettingsPanel.SetActive(true);

        DeselectAudioSettingsButton();
        DeselectVideoSettingsButton();
        DeselectKeyboardControlButton();
        DeselectControlSettingsButton();
        DeselectQuitGameButton();
    }

    /**************************************************************************
   Function: PressQuitGameButton

Description: This function deselects all other sub tab buttons and hides their
             panels while displaying the quit game's panel and its buttons.

      Input: none

     Output: none
    **************************************************************************/
    public void PressQuitButton()
    {
        quitGameButton.interactable = false;
        EnableQuitGamePanel(true);

        DeselectAudioSettingsButton();
        DeselectVideoSettingsButton();
        DeselectControlSettingsButton();
        DeselectKeyboardControlButton();
        DeselectGameplaySettingsButton();
    }

    /**************************************************************************
   Function: DeselectAllButtons

Description: This function deselects all the main buttons and the panels
             attached to them.

      Input: none

     Output: none
    **************************************************************************/
    public void DeselectAllButtons()
    {
        DeselectAudioSettingsButton();
        DeselectVideoSettingsButton();
        DeselectControlSettingsButton();
        DeselectGameplaySettingsButton();
        DeselectKeyboardControlButton();
        DeselectQuitGameButton();
    }

    /**************************************************************************
   Function: PressDefaultAudioSettingsButton

Description: This function sets the music and sound volume sliders to their
             default values.

      Input: none

     Output: none
    **************************************************************************/
    public void PressDefaultAudioSettingsButton()
    {
        musicVolumeSlider.value = defaultMusicVolume;
        soundVolumeSlider.value = defaultSoundVolume;

        audioDefaultSettingsButton.interactable = true;
        audioDefaultSettingsButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: PressDefaultVideoSettingsButton

Description: This function sets the brightness slider to its default value.

      Input: none

     Output: none
    **************************************************************************/
    public void PressDefaultVideoSettingsButton()
    {
        brightnessSlider.value = defaultBrightness;

        videoDefaultSettingsButton.interactable = true;
        videoDefaultSettingsButton.OnDeselect(null);

        //NOTE: This doesn't change resolution at this time
    }

    /**************************************************************************
   Function: PressDefaultControlSettingsButton

Description: This function sets the mouse sensitivity slider to its default
             value and turns off invert Y axis toggle.

      Input: none

     Output: none
    **************************************************************************/
    public void PressDefaultControlSettingsButton()
    {
        mouseSensitivitySlider.value = defaultMouseXSensitivity;
        invertYAxisToggle.isOn = false;
        ToggleInvertYAxis();

        controlDefaultSettingsButton.interactable = true;
        controlDefaultSettingsButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: PressDefaultGameplaySettingsButton

Description: This function sets the crouch hold toggle to true and the crouch
             hold bool to true as they are currently true by default.

      Input: none

     Output: none
    **************************************************************************/
    public void PressDefaultGameplaySettingsButton()
    {
        crouchHoldToggle.isOn = true;
        crouchHold = true;

        gameplayDefaultSettingsButton.interactable = true;
        gameplayDefaultSettingsButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: ToggleFullScreen

Description: This function checks if the fullScreenToggle is on. If it is, the
             resolution is set to fullScreen. If not, the default window
             resolution is set and the screen resolution is changed to that.
             Then the mouse cursor is set to confined.

      Input: none

     Output: none
    **************************************************************************/
    public void ToggleFullScreen()
    {
        if (fullScreenToggle.isOn)
        {
            Screen.SetResolution(Screen.width, Screen.height, true);
        }
        else
        {
            resolutionDropdown.value = 0; //sets value to default option
            ChangeResolution();
        }

        Cursor.lockState = CursorLockMode.Confined;
    }

    /**************************************************************************
   Function: ChangeResolution

Description: This function checks the dropdown menu's current value and sets
             the game's resolution to the resolution that corresponds to that
             value. This function also uses the fullScreenToggle to determine
             whether or not the game is fullScreen.

      Input: none

     Output: none
    **************************************************************************/
    public void ChangeResolution()
    {
        switch(resolutionDropdown.value)
        {
            case 0: //first option in list
                Screen.SetResolution(1136, 640, fullScreenToggle.isOn);
                break;
            case 1: //second option in list
                Screen.SetResolution(1024, 768, fullScreenToggle.isOn);
                break;
            case 2: //third option in list
                Screen.SetResolution(1280, 1024, fullScreenToggle.isOn);
                break;
            case 3:  //fourth option in list
                Screen.SetResolution(1366, 768, fullScreenToggle.isOn);
                break;
            case 4: //fifth option in list
                Screen.SetResolution(1440, 900, fullScreenToggle.isOn);
                break;
            default:
                //Debug.Log("This shouldn't happen!");
                break;
        }
    }

    /**************************************************************************
   Function: ToggleInvertYAxis

Description: This function sets the mouse script's invert Y Axis bool to the
             bool that represents if the invertYAxisToggle is on or not.

      Input: none

     Output: none
    **************************************************************************/
    public void ToggleInvertYAxis()
    {
        firstPersonController.GetMouseLook().SetInvertYAxis(invertYAxisToggle.isOn);
    }

    /**************************************************************************
   Function: UpdateMusicVolume

Description: This function sets the volume of the audio source that plays
             music/background soundtracks to the value of the music volume
             slider.

      Input: none

     Output: none
    **************************************************************************/
    public void UpdateMusicVolume()
    {
          //adjusts volume based on the volume slider
        musicAudioSource.volume = musicVolumeSlider.value;
    }

    /**************************************************************************
   Function: UpdateSoundVolume

Description: This function sets the volume of all audio sources in the sound
             effect array to the volume of the sound volume slider.

      Input: none

     Output: none
    **************************************************************************/
    public void UpdateSoundVolume()
    {
          //sets all audio sources in array to volume of sound slider
        for (int i = 0; i < soundEffectAudioSources.Length; i++)
        {
            soundEffectAudioSources[i].volume = soundVolumeSlider.value;
        }
    }

    /**************************************************************************
   Function: UpdateBrightness

Description: This function sets the hud overlay color's alpha to the value 
             calculated by subtracting the brightness slider's current value to 
             its max value. This makes the alpha go down as the slider goes up. 
             Then the updated color is assigned to the hud overlay.

      Input: none

     Output: none
    **************************************************************************/
    public void UpdateBrightness()
    {
          //updates alpha of image to make it appear lighter or darker
        brightnessHUDOverlayColor.a = brightnessSlider.maxValue - brightnessSlider.value;

          //assigns updated color to overlay image
        brightnessHUDOverlay.color = brightnessHUDOverlayColor;
    }

    /**************************************************************************
   Function: UpdateMouseSensitivity

Description: This function sets the x and y sensitivity of the mouse in game
             to the value of the mouse sensitivity slider.

      Input: none

     Output: none
    **************************************************************************/
    public void UpdateMouseSensitivity()
    {
        firstPersonController.GetMouseLook().XSensitivity = mouseSensitivitySlider.value;
        firstPersonController.GetMouseLook().YSensitivity = mouseSensitivitySlider.value;
    }

    /**************************************************************************
   Function: PressKeyboardControlButton

Description: This function enables the keyboard control panel which displays
             the game's controls.

      Input: none

     Output: none
    **************************************************************************/
    public void PressKeyboardControlButton()
    {
        keyboardControlButton.interactable = false;
        keyboardControlPanel.SetActive(true);
    }

    /**************************************************************************
   Function: PressKeyboardControlCloseButton

Description: This function deselects the keyboard control button which closes
             the control panel that shows keyboard controls.

      Input: none

     Output: none
    **************************************************************************/
    public void PressKeyboardControlCloseButton()
    {
        DeselectKeyboardControlButton();
    }

    /**************************************************************************
   Function: PressCrouchHoldToggle

Description: This function sets the crouch hold bool to the same status of the
             crouch hold toggle.

      Input: none

     Output: none
    **************************************************************************/
    public void PressCrouchHoldToggle()
    {
        crouchHold = crouchHoldToggle.isOn;
    }

    /**************************************************************************
   Function: GetCrouchHoldStatus

Description: This function returns the bool used to control whether or not the
             player has to hold the crouch button or press it to toggle crouch.

      Input: none

     Output: Returns true if crouch hold is the current setting, otherwise, 
             returns false.
    **************************************************************************/
    public bool GetCrouchHoldStatus()
    {
        return crouchHold;
    }

    /**************************************************************************
   Function: GetQuitPanelStatus

Description: This function returns the bool that represents whether or not the
             quit panel is open.

      Input: none

     Output: Returns true if quit panel is open, otherwise, returns false.
    **************************************************************************/
    public bool GetQuitPanelStatus()
    {
        return quitPanelOpen;
    }

    /**************************************************************************
   Function: PressQuitYesButton

Description: This function currently closes the game.

      Input: none

     Output: none
    **************************************************************************/
    public void PressQuitYesButton()
    {
        Application.Quit(); //TODO: later, have this return to main menu
    }

    /**************************************************************************
   Function: PressQuitNoButton

Description: This function calls the deselect quit game button function which
             closes the quit game panel and allows the quit game button to be
             clickable again.

      Input: none

     Output: none
    **************************************************************************/
    public void PressQuitNoButton()
    {
        DeselectQuitGameButton();
    }

    /**************************************************************************
   Function: EnableQuitPanel

Description: Given a bool, this function either hides or displays the panel
             that contains the quit message and the yes/no buttons

      Input: enable - bool used to hide or display the quit game panel

     Output: none
    **************************************************************************/
    private void EnableQuitGamePanel(bool enable)
    {
        quitPanelOpen = enable;

        quitGamePanel.SetActive(enable);
    }

    /**************************************************************************
   Function: SetSoundEffectAudioSourceArray

Description: This function stores each audio source that plays sound effects
             into an array for easy access by any other function.

      Input: none

     Output: none
    **************************************************************************/
    private void SetSoundEffectAudioSourceArray()
    {
        soundEffectAudioSources[0] = playerAudioSource;
        soundEffectAudioSources[1] = pistolAudiosource;
        soundEffectAudioSources[2] = shotgunAudioSource;
        soundEffectAudioSources[3] = rifleAudioSource;
    }

    /**************************************************************************
   Function: DeselectKeyboardControlButton

Description: This function deselects the keyboard control button and disables
             the panel that shows keyboard controls.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectKeyboardControlButton()
    {
        keyboardControlButton.interactable = true;
        keyboardControlPanel.SetActive(false);
        keyboardControlButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectVideoSettingsButton

Description: This function deselects the video settings button and hides its
             panel.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectVideoSettingsButton()
    {
        videoSettingsButton.interactable = true;
        videoSettingsPanel.SetActive(false);
        videoSettingsButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectAudioSettingsButton

Description: This function deselects the audio settings button and hides its
             panel.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectAudioSettingsButton()
    {
        audioSettingsButton.interactable = true;
        audioSettingsPanel.SetActive(false);
        audioSettingsButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectControlsSettingsButton

Description: This function deselects the controls settings button and hides its
             panel.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectControlSettingsButton()
    {
        controlSettingsButton.interactable = true;
        controlSettingsPanel.SetActive(false);
        controlSettingsButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectGameplaySettingsButton

Description: This function deselects the gameplay settings button and hides its
             panel.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectGameplaySettingsButton()
    {
        gameplaySettingsButton.interactable = true;
        gameplaySettingsPanel.SetActive(false);
        gameplaySettingsButton.OnDeselect(null);
    }

    /**************************************************************************
   Function: DeselectQuitGameButton

Description: This function deselects the quit game button and hides its panel.

      Input: none

     Output: none
    **************************************************************************/
    private void DeselectQuitGameButton()
    {
        quitGameButton.interactable = true;
        quitGameButton.OnDeselect(null);

        EnableQuitGamePanel(false);
    }
}
