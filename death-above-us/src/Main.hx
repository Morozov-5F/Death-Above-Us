package;

import controllers.ManualController;
import objects.Turret;
import openfl.display.DisplayObjectContainer;
import openfl.display.MovieClip;
import openfl.events.KeyboardEvent;
import openfl.events.MouseEvent;
import openfl.events.TouchEvent;
import openfl.geom.Point;
import openfl.ui.Keyboard;
import openfl.ui.Mouse;
import openfl.Vector;

import openfl.display.Sprite;
import openfl.display.Bitmap;
import openfl.events.Event;
import openfl.Lib;
import openfl.Assets;
	
/**
 * Класс приложения, откуда запускается игра
 * @author Evgeniy Morozov
 */
class Main extends Sprite 
{
	private var prevPointerPosition:Float;
	private var turret:Turret;
	
	public static var mouseDown: Bool;
	public static var keys     : Vector<Bool>;
	
	private var prevTick:Float;
	
	public function new() 
	{
		super();
		prevPointerPosition = 0;
		mouseDown = false;
		keys = new Vector<Bool>(101, true);
		prevTick = 0;
		
		this.addEventListener(Event.ADDED_TO_STAGE, initialize);
	}
	
	private function initialize(e:Event):Void 
	{
		removeEventListener(Event.ADDED_TO_STAGE, initialize);
		trace("Stage parameters: " + Lib.current.stage.stageWidth + "x" + Lib.current.stage.stageHeight);
	
		Utils.gameScale = stage.stageHeight / 900;
		Utils.cameraPosX = 0;
		var controller:ManualController = new ManualController(true);
		turret = new Turret(new Point(stage.stageWidth / 2, stage.stageHeight), controller);
		addChild(turret);
		#if mobile
		stage.addEventListener(TouchEvent.TOUCH_BEGIN, onTouchBegin);
		stage.addEventListener(TouchEvent.TOUCH_END, onTouchEnd);
		#else
		stage.addEventListener(MouseEvent.MOUSE_DOWN, onMouseDown);
		stage.addEventListener(MouseEvent.MOUSE_UP,  onMouseUp);
		stage.addEventListener(KeyboardEvent.KEY_DOWN, onKeyDown);
		stage.addEventListener(KeyboardEvent.KEY_UP, onKeyUp);
		#end
		addEventListener(Event.ENTER_FRAME, update);
	}
	

	#if mobile	
	private function onTouchBegin(e:TouchEvent):Void 
	{
		
	}	
	private function onTouchEnd(e:TouchEvent):Void 
	{
		
	}
	#else
	private function onKeyUp(e:KeyboardEvent):Void 
	{
		keys[e.keyCode] = false;
	}
	
	private function onKeyDown(e:KeyboardEvent):Void 
	{
		keys[e.keyCode] = true;
	}
	
	private function onMouseUp(e:MouseEvent):Void 
	{
		mouseDown = false;
	}
	
	private function onMouseDown(e:MouseEvent):Void 
	{
		mouseDown = true;
	}
	#end
	
	private function update(e:Event):Void 
	{
		var currentTick = Lib.getTimer();
		var deltaTime = (currentTick - prevTick) / 1000;
		prevTick = currentTick;
		
		turret.update(deltaTime);
	}
	
}
