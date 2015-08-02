package;

import openfl.display.Sprite;
import openfl.events.Event;
import screens.GameScreen;
import screens.Screen;
import screens.ScreenManager;
	
/**
 * Класс приложения, откуда запускается игра
 * @author Evgeniy Morozov
 */
class Main extends Sprite 
{
	public static var screenManager:ScreenManager;
	public function new() 
	{
		super();
		this.addEventListener(Event.ADDED_TO_STAGE, initialize);
	}
	
	private function initialize(e:Event):Void 
	{
		removeEventListener(Event.ADDED_TO_STAGE, initialize);
		// ScreenManager
		screenManager = new ScreenManager();
		addChild(screenManager);
		var startupScreen:Screen = new GameScreen(); // new MenuScreen();
		screenManager.loadScreen(startupScreen);
	}
	
}
