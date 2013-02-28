using UnityEngine;
using System.Collections;

public class GUIStart : MonoBehaviour {


	public GUISkin guiSkin;
	public Texture2D black;
	public float splashScreenTimeout = 4f;
	public string gameSceneName = "test";
	public float nameSpace = 150f;
	public float titleSpace = 70f;

	public Texture2D logo;

	public Texture2D start;
	public Texture2D control;
	public Texture2D credits;
	public Texture2D exit;

	public Texture2D activeStart;
	public Texture2D activeControl;
	public Texture2D activeCredits;
	public Texture2D activeExit;

	private Texture2D startButton;
	private Texture2D controlButton;
	private Texture2D creditsButton;
	private Texture2D exitButton;

	private StartState state = StartState.SPLASH;
	private SelectedButton currentButton = SelectedButton.START;

	void OnGUI() {
		GUI.skin = guiSkin;
		setButtonImages();

		GUI.DrawTexture( new Rect( 0f, 0f, Screen.width + 10f, Screen.height + 10f ), black );

		switch ( state ) {

			case StartState.SPLASH:
				if ( Time.time > splashScreenTimeout ) {
					state = StartState.MAIN_MENU;
				}
				GUI.skin.label.fontSize = 40;
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				GUI.Label( new Rect( Screen.width * 0.1f, Screen.height / 2 - 150f, Screen.width * 0.8f, 200f ), "This game was developed as part of Indie Speed Run 2012\n(www.indiespeedrun.com)." );
				break;

			case StartState.MAIN_MENU:
				GUI.skin.label.fontSize = 60;
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				GUI.Label( new Rect( Screen.width * 0.5f - 150, Screen.height * 0.2f, 300f, 100f ), "SUNBLOCK" );
				GUI.DrawTexture( new Rect( Screen.width * 0.5f + 60f, Screen.height * 0.2f - 70f, 200f, 200f ), logo );

				if ( GUI.Button( new Rect( Screen.width * 0.5f - 100f, Screen.height * 0.2f + 150f, 200f, 62f ), startButton, GUIStyle.none ) ) {
					currentButton = SelectedButton.START;
					performAction();
				}
				if ( GUI.Button( new Rect( Screen.width * 0.5f - 100f, Screen.height * 0.2f + 240f, 200f, 62f ), controlButton, GUIStyle.none ) ) {
					currentButton = SelectedButton.CONTROLS;
					performAction();
				}
				if ( GUI.Button( new Rect( Screen.width * 0.5f - 100f, Screen.height * 0.2f + 330f, 200f, 62f ), creditsButton, GUIStyle.none ) ) {
					currentButton = SelectedButton.CREDITS;
					performAction();
				}
				if ( GUI.Button( new Rect( Screen.width * 0.5f - 100f, Screen.height * 0.2f + 420f, 200f, 62f ), exitButton, GUIStyle.none ) ) {
					currentButton = SelectedButton.EXIT;
					performAction();
				}


				break;
			case StartState.CONTROLS:
				GUI.skin.label.fontSize = 40;
				GUI.skin.label.alignment = TextAnchor.MiddleLeft;

				GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f +100f, Screen.width * 0.8f, 100f ), "Arrow keys/WASD\t\tMovement" );
				GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f + 160f, Screen.width * 0.8f, 100f ), "Space\t\t\t\t\t\t\t\t\t\t\t\t\tJump" );
				GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f + 220f, Screen.width * 0.8f, 100f ), "Z\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tAttack" );


				break;
			case StartState.CREDITS:
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;

				//GUI.skin.label.fontSize = 25;
				//GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f, Screen.width * 0.5f, 100f ), "Artist/Producer" );
				GUI.skin.label.fontSize = 40;
				GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f + titleSpace, Screen.width * 0.5f, 100f ), "Christopher Barakian" );

				//GUI.skin.label.fontSize = 25;
				//GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f + nameSpace, Screen.width * 0.5f, 100f ), "Lorem ipsum dolor sit amet" );
				GUI.skin.label.fontSize = 40;
				GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f + nameSpace + titleSpace, Screen.width * 0.5f, 100f ), "Bryan Edelman" );

				//GUI.skin.label.fontSize = 25;
				//GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f + nameSpace * 2f, Screen.width * 0.5f, 100f ), "Lead Programmer" );
				GUI.skin.label.fontSize = 40;
				GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f + nameSpace * 2f + titleSpace, Screen.width * 0.5f, 100f ), "Dennis McGrogan" );

				//GUI.skin.label.fontSize = 25;
				//GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f + nameSpace * 3f, Screen.width * 0.5f, 100f ), "Maecenas tempus" );
				GUI.skin.label.fontSize = 40;
				GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f + nameSpace * 3f + titleSpace, Screen.width * 0.5f, 100f ), "Ali Timnak" );

				GUI.skin.label.fontSize = 30;
				GUI.Label( new Rect( Screen.width * 0.25f, Screen.height * 0.1f + nameSpace * 4f + titleSpace + 30, Screen.width * 0.5f, 100f ), "Beach sounds and fire sizzle were recorded by Mike Koenig" );


				break;
		}

	}

	void Update() {

		if ( state == StartState.MAIN_MENU ) {
			if ( Input.GetKeyDown( KeyCode.UpArrow ) ) {
				previousButton();
			}
			if ( Input.GetKeyDown( KeyCode.DownArrow ) ) {
				nextButton();
			}
			if ( Input.GetKeyDown( KeyCode.KeypadEnter ) || Input.GetKeyDown( KeyCode.Return ) || Input.GetKeyDown( KeyCode.Space ) ) {
				performAction();
			}
		} else if ( state == StartState.CREDITS || state == StartState.CONTROLS ) {
			if ( Input.GetKeyDown( KeyCode.Escape ) ) {
				state = StartState.MAIN_MENU;
			}
		}
	}

	private void performAction() {
		switch ( currentButton ) {
			case SelectedButton.START:
				Application.LoadLevel( gameSceneName );
				break;
			case SelectedButton.CONTROLS:
				state = StartState.CONTROLS;
				break;
			case SelectedButton.CREDITS:
				state = StartState.CREDITS;
				break;
			case SelectedButton.EXIT:
				Application.Quit();
				print( "Exit" );
				break;
		}
	}

	private void previousButton() {
		switch ( currentButton ) {
			case SelectedButton.START:
				currentButton = SelectedButton.EXIT;
				break;
			case SelectedButton.CREDITS:
				currentButton = SelectedButton.CONTROLS;
				break;
			case SelectedButton.CONTROLS:
				currentButton = SelectedButton.START;
				break;
			case SelectedButton.EXIT:
				currentButton = SelectedButton.CREDITS;
				break;
		}
	}

	private void nextButton() {
		switch ( currentButton ) {
			case SelectedButton.START:
				currentButton = SelectedButton.CONTROLS;
				break;
			case SelectedButton.CONTROLS:
				currentButton = SelectedButton.CREDITS;
				break;
			case SelectedButton.CREDITS:
				currentButton = SelectedButton.EXIT;
				break;
			case SelectedButton.EXIT:
				currentButton = SelectedButton.START;
				break;
		}
	}

	private void setButtonImages() {
		startButton = start;
		controlButton = control;
		creditsButton = credits;
		exitButton = exit;

		switch ( currentButton ) {
			case SelectedButton.START:
				startButton = activeStart;
				break;
			case SelectedButton.CONTROLS:
				controlButton = activeControl;
				break;
			case SelectedButton.CREDITS:
				creditsButton = activeCredits;
				break;
			case SelectedButton.EXIT:
				exitButton = activeExit;
				break;
		}
	}


	private enum StartState { SPLASH, MAIN_MENU, CONTROLS, CREDITS }
	private enum SelectedButton { START, CONTROLS, CREDITS, EXIT }
}
