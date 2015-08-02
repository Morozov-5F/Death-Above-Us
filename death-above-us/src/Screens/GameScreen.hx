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
	private var gameObjectsContainer:DisplayObjectContainer;
	
	public function new()
	{
		super();
	}
	
	override public function load():Bool 
	{
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