package;

import openfl.display.DisplayObjectContainer;
import openfl.display.MovieClip;
import openfl.events.MouseEvent;
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
	private var testContainer:DisplayObjectContainer;
	private var bcgrd: GameBackground;
	
	private var mouseDown: Bool;
	
	public function new() 
	{
		super();
		
		testContainer = new DisplayObjectContainer();
		bcgrd = new GameBackground();
		
		prevPointerPosition = 0;
		
		this.addEventListener(Event.ADDED_TO_STAGE, initialize);
	}
	
	private function initialize(e:Event):Void 
	{
		removeEventListener(Event.ADDED_TO_STAGE, initialize);
		trace("Stage parameters: " + Lib.current.stage.stageWidth + "x" + Lib.current.stage.stageHeight);
		
		Utils.cameraPosX = 0;
		Utils.gameScale = Lib.current.stage.stageHeight / 900.0;	
		
		if (!bcgrd.load("1"))
		{
			trace("ERROR");
		}
		addChild(testContainer);
		testContainer.addChild(bcgrd);
		
		addEventListener(MouseEvent.MOUSE_DOWN, onMouseDown);
		addEventListener(MouseEvent.MOUSE_UP,  onMouseUp);
		addEventListener(Event.ENTER_FRAME, update);
	}
	
	private function update(e:Event):Void 
	{
		if (mouseDown)
		{
			Utils.cameraPosX += (Lib.current.stage.mouseX - prevPointerPosition );
			if (Math.abs(Utils.cameraPosX) >= 90)
			{
				Utils.cameraPosX = (Math.abs(Utils.cameraPosX) / Utils.cameraPosX) * 90;
			}
		}
		bcgrd.update(0);
		trace("Camera position: " + Utils.cameraPosX);
		prevPointerPosition = Lib.current.stage.mouseX;
	}
	
	private function onMouseUp(e:MouseEvent):Void 
	{
		mouseDown = false;
	}
	
	private function onMouseDown(e:MouseEvent):Void 
	{
		mouseDown = true;
	}
	
}
