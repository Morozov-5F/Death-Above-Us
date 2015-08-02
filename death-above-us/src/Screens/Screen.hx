package Screens;
import openfl.display.DisplayObjectContainer;

/**
 * ...
 * @author Evgeniy Morozov
 */
abstract class Screen extends Sprite
{
	private var guiContainer:DisplayObjectContainer;
	private var backgroundContainer:DisplayObjectContainer;
	
	abstract public function load() : Bool;
	abstract public function unload() : Bool;

	abstract public function update(dt:Float) : Void;
}