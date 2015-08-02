package objects;

import openfl.Assets;
import openfl.display.Bitmap;
import openfl.display.Sprite;
import openfl.geom.Point;

import weapons.BaseWeapon;
import controllers.IController;

/**
 * Класс турели
 * @author Evgeniy Morozov
 */
class Turret extends Sprite
{
	public static var MAX_ROTATION_ANGLE:Int = 75;
	
	public var weapon:BaseWeapon;
	
	private var gunSprite :Sprite;
	private var controller:IController;
	
	public function new(position:Point, controller:IController = null) 
	{
		super();
		// TODO: Переделать как-нибудь, чтобы было более универсально
		var baseTexture = new Bitmap(Assets.getBitmapData("img/turrets/1/1.png"));
		var gunTexture  = new Bitmap(Assets.getBitmapData("img/turrets/1/2.png"));
		
		baseTexture.x = -52;
		baseTexture.y = -baseTexture.height;
		addChild(baseTexture);
		
		gunSprite = new Sprite();
		gunTexture.x = -30;
		gunTexture.y = -70;
		gunSprite.y = baseTexture.y;
		gunSprite.addChild(gunTexture);
		addChild(gunSprite);
		
		var globalCoordinates = localToGlobal(position);
		x = globalCoordinates.x;
		y = globalCoordinates.y;
		
		scaleX = scaleY = Utils.gameScale;
		
		this.controller = controller;
		if (this.controller.controllableTurret == null)
		{
			this.controller.controllableTurret = this;
		}
	}
	
	/**
	 * Устанавливает вращение пушки турели
	 * @param	newRotation
	 * @return  Возвращается результат присваивания
	 */
	public function setRotation(newRotation:Float):Float
	{
		if (gunSprite == null)
		{
			return Math.NaN;
		}
		gunSprite.rotation = newRotation;
		if (Math.abs(gunSprite.rotation) > Turret.MAX_ROTATION_ANGLE)
		{
			gunSprite.rotation = (gunSprite.rotation > 0) ? Turret.MAX_ROTATION_ANGLE : -Turret.MAX_ROTATION_ANGLE;
		}
		return gunSprite.rotation;
	}
	
	public function update(deltaTime:Float):Void
	{
		controller.update(deltaTime);
	}
}