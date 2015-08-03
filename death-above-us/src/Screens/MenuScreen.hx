package screens;
import flash.events.Event;
import openfl.Assets;
import openfl.display.Bitmap;
import openfl.media.Sound;
import openfl.media.SoundChannel;
import openfl.media.SoundTransform;
import ui.MenuButton;

/**
 * ...
 * @author someone
 */
class MenuScreen extends Screen
{
	private var background:MenuBackground;
	var startButton:MenuButton;
	
	public function new() 
	{
		super();
	}
	
	override public function load():Bool 
	{
		background = new MenuBackground();
		addChild(background);
		
		var logo:Bitmap = new Bitmap(Assets.getBitmapData("img/menu/logo.png"));
		logo.scaleX = logo.scaleY = stage.stageWidth / logo.width;
		addChild(logo);
		
		startButton = new MenuButton("Start game");
		startButton.x = stage.stageWidth / 2 - startButton.width / 2;
		startButton.y = logo.height;
		startButton.addEventListener(MenuButton.DOWN, buttonDown);
		addChild(startButton);
		
		return true;
	}
	
	private function buttonDown(e:Event):Void 
	{
		if (e.target == startButton)
		{
			Main.screenManager.loadScreen(new GameScreen());
		}
	}
	
	override public function unload():Bool 
	{
		return true;
	}
	
	override public function update(deltaTime:Float):Void 
	{
		background.update(deltaTime);
	}
}