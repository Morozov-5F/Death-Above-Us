package;

import openfl.display.Sprite;
import openfl.events.Event;
import openfl.Lib;
import screens.GameScreen;
import screens.MenuScreen;
import screens.Screen;
import screens.ScreenManager;
	
/**
 * Класс приложения, откуда запускается игра
 * @author Evgeniy Morozov
 */
class Main extends Sprite 
{
	public static var screenManager:ScreenManager;
	private var previousTime:Float;
	
	public function new() 
	{
		super();
		this.addEventListener(Event.ADDED_TO_STAGE, initialize);
	}
	
	private function initialize(e:Event):Void 
	{
		removeEventListener(Event.ADDED_TO_STAGE, initialize);
		// DeltaTime
		previousTime = Lib.getTimer();
		
		// Создание ScreenManager
		screenManager = new ScreenManager();
		addChild(screenManager);
		var startupScreen:Screen = new MenuScreen();
		screenManager.loadScreen(startupScreen);
		addEventListener(Event.ENTER_FRAME, update);
	}
	
	private function update(e:Event):Void 
	{
		var currentTime:Float = Lib.getTimer();
		var deltaTime:Float = (currentTime - previousTime) / 1000;
		previousTime = currentTime;
		
		screenManager.update(deltaTime);
	}
	
}
