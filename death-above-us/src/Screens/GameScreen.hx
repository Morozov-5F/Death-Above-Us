package screens;

import controllers.ManualController;
import objects.asteroids.BaseAsteroid;
import objects.asteroids.SimpleAsteroid;
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
	private static inline var MIN_ASTEROIDS = 10;
	private static inline var MAX_ASTEROIDS = 20;
	
	private var prevPointerPosition:Float;
	private var turret:Turret;
	
	public static var mouseDown: Bool;
	public static var keys     : Vector<Bool>;
	
	private var bullets:Array<BaseBullet>;
	private var asteroids:Array<BaseAsteroid>;
	
	public function new()
	{
		super();
		
		prevPointerPosition = 0;
		mouseDown = false;
		keys = new Vector<Bool>(101, true);
		
		bullets = new Array<BaseBullet>();
		asteroids = new Array<BaseAsteroid>();
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
		spawnAsteroids(SimpleAsteroid);
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
	
	private function spawnAsteroids(asteroidType:Class<BaseAsteroid>)
	{
		var asteroidsToSpawn = Utils.getRandomInt(MIN_ASTEROIDS, MAX_ASTEROIDS);
		for (i in 0 ... asteroidsToSpawn)
		{
			// TODO: подогнать оптимальные координаты для спавна
			var leftCoordinate = -stage.stageWidth / 4;
			var rightCoordinate = stage.stageWidth + stage.stageWidth / 4;
			var upperCoordinate = -stage.stageHeight / 4;
			var lowerCoordinate = -stage.stageHeight / 8;
			
			var position:Point = new Point(Utils.getRandomFloat(leftCoordinate, rightCoordinate), Utils.getRandomFloat(upperCoordinate, lowerCoordinate));
			var directionLeftCorner  = turret.x - stage.stageWidth / 10;
			var directionRightCorner = turret.x + stage.stageWidth / 10;
			
			var velocityDirection:Point = new Point(Utils.getRandomFloat(directionLeftCorner, directionRightCorner) - position.x, turret.y);
			velocityDirection.normalize(1);
			var currentAsteroid = Type.createInstance(asteroidType, [position, velocityDirection]);
			
			if (currentAsteroid == null)
			{
				trace("Unable to create asteroid");
				continue;
			}
			
			addChild(currentAsteroid);
			asteroids.push(currentAsteroid);
		}
	}
	
	override public function update(deltaTime:Float):Void 
	{	
		turret.update(deltaTime);		
		for (cA in asteroids)
		{
			if (cA == null)
			{
				continue;
			}
			cA.update(deltaTime);
			var index = asteroids.indexOf(cA);
			if (cA.hp <= 0)
			{
				removeChild(cA);
				asteroids.splice(index, 1);
			}
			if (cA.x > stage.stageWidth + cA.width|| 
				cA.x < -cA.width || cA.y > stage.stageHeight + cA.width)
			{
				removeChild(cA);
				asteroids.splice(index, 1);
			}
		}
		for (cB in bullets)
		{
			if (cB == null)
			{
				continue;
			}
			cB.update(deltaTime);
			var index = bullets.indexOf(cB);
			// Проверка на то, что пуля ушла куда-то за экран.
			// Пока только верх экрана и боковые стороны. Вряд ли будет 
			// возможность стрелять вниз
			if (cB.x > stage.stageWidth + cB.width ||
				cB.y < -cB.height || cB.x < -cB.width)
			{
				removeChild(cB);
				bullets.splice(index, 1);
			}
			
			for (cA in asteroids)
			{
				if (cA == null)
				{
					continue;
				}
				if (cB.hitTestObject(cA))
				{
					cA.onHit(cB.damage);
					removeChild(cB);
					bullets.splice(index, 1);
				}
			}
		}	
		

	}
}