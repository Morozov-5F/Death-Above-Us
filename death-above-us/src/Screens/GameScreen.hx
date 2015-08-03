package screens;

import controllers.ManualController;
import objects.Turret;
import openfl.display.DisplayObjectContainer;
import openfl.display.MovieClip;
import openfl.events.KeyboardEvent;
import openfl.events.MouseEvent;
import openfl.events.TouchEvent;
import openfl.geom.Point;
import openfl.Lib;
import openfl.ui.Keyboard;
import openfl.ui.Mouse;
import openfl.Vector;
import weapons.BaseWeapon.WeaponShotEvent;
import weapons.bullets.BaseBullet;
import weapons.DoubleBarreledWeapon;
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
	private var prevPointerPosition:Float;
	private var turret:Turret;
	
	public static var mouseDown: Bool;
	public static var keys     : Vector<Bool>;
	private var bullets:Array<BaseBullet>;
	
	public function new()
	{
		super();
		prevPointerPosition = 0;
		mouseDown = false;
		keys = new Vector<Bool>(101, true);
		bullets = new Array<BaseBullet>();
	}
	
	override public function load():Bool 
	{
		trace("Stage parameters: " + Lib.current.stage.stageWidth + "x" + Lib.current.stage.stageHeight);

		Utils.gameScale = stage.stageHeight / 900;
		Utils.cameraPosX = 0;
		var controller:ManualController = new ManualController(true);
		turret = new Turret(new Point(stage.stageWidth / 2, stage.stageHeight), DoubleBarreledWeapon ,controller);
		addChild(turret);
		turret.weapon.addEventListener(WeaponShotEvent.WEAPON_SHOT, onShot);
		
		#if mobile
		stage.addEventListener(TouchEvent.TOUCH_BEGIN, onTouchBegin);
		stage.addEventListener(TouchEvent.TOUCH_END, onTouchEnd);
		#else
		stage.addEventListener(MouseEvent.MOUSE_DOWN, onMouseDown);
		stage.addEventListener(MouseEvent.MOUSE_UP,  onMouseUp);
		stage.addEventListener(KeyboardEvent.KEY_DOWN, onKeyDown);
		stage.addEventListener(KeyboardEvent.KEY_UP, onKeyUp);
		#end
		
		return true;
	}
	
	private function onShot(e:WeaponShotEvent):Void 
	{
		for (i in 0 ... e.bullets.length)
		{
			bullets.push(e.bullets[i]);
			addChild(e.bullets[i]);
		}
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
	
	override public function unload():Bool 
	{
		return true;
	}
	
	override public function update(deltaTime:Float):Void 
	{	
		turret.update(deltaTime);
		trace(bullets.length);
		for (cB in bullets)
		{
			if (cB == null)
			{
				continue;
			}
			cB.update(deltaTime);
			// Проверка на то, что пуля ушла куда-то за экран.
			// Пока только верх экрана и боковые стороны. Вряд ли будет 
			// возможность стрелять вниз
			if (cB.x > stage.stageWidth + cB.width ||
				cB.y < -cB.height || cB.x < -cB.width)
			{
				bullets.splice(bullets.indexOf(cB), 1);
			}
		}	
	}
}