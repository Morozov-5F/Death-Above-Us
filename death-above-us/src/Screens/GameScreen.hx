package screens;
import openfl.Assets;
import openfl.display.Bitmap;
import openfl.display.DisplayObjectContainer;
import openfl.events.Event;

/**
 * 
 * @author Evgeniy Morozov
 */
class GameScreen extends Screen
{
	public function new()
	{
		super();
	}
	
	override public function load():Bool 
	{
		var test:Bitmap = new Bitmap(Assets.getBitmapData("imgx/menu/logo.png"));
		addChild(test);
		return true;
	}
	
	override public function unload():Bool 
	{
		return true;
	}
	
	override public function update(dt:Float):Void 
	{
		
	}
}