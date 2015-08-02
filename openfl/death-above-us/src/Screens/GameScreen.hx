package Screens;
import openfl.display.DisplayObjectContainer;

/**
 * 
 * @author Evgeniy Morozov
 */
class GameScreen extends Screen
{
	private var gameObjectsContainer:DisplayObjectContainer;
	
	private var bullets:Array;
	private var asteroid:Array;
	
	public function new()
	{
		
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