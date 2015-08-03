package objects.asteroids;
import openfl.Assets;
import openfl.display.Bitmap;
import openfl.geom.ColorTransform;
import openfl.geom.Point;
import openfl.geom.Rectangle;

/**
 * ...
 * @author Evgeniy Morozov
 */
class SimpleAsteroid extends BaseAsteroid
{
	public static inline var MIN_MASS:Float = 0.5;
	public static inline var MAX_MASS:Float = 1.2;
	
	private static inline var MIN_VELOCITY = 70;
	private static inline var MAX_VELOCITY = 100;
	
	private static inline var MAX_HP = 100;
	
	public function new(position:Point, velocityDirection:Point) 
	{
		super();
		
		var asteroidsCount:Int = 0;
		while (Assets.exists("img/asteroids/simple/" + (asteroidsCount + 1) + ".png"))
		{
			asteroidsCount++;
		}
		// Выбираем случайную текстуру для астероида
		var asteroidTypeNumber = Utils.getRandomInt(1, asteroidsCount);
		var asteroidBitmap:Bitmap = new Bitmap(Assets.getBitmapData("img/asteroids/simple/" + asteroidTypeNumber + ".png"));
		// Попробовал примерить измененин цвета в текстуре
		asteroidBitmap.bitmapData.colorTransform(asteroidBitmap.bitmapData.rect, new ColorTransform(1, 1, 1, 1, Utils.getRandomInt(-100,100)));
		asteroidBitmap.x = -asteroidBitmap.width / 2;
		asteroidBitmap.y = -asteroidBitmap.height / 2;
		addChild(asteroidBitmap);
		// Задаём массу астероида
		mass = Utils.getRandomFloat(MIN_MASS, MAX_MASS);
		scaleX = scaleY = (Utils.gameScale * mass);
		
		this.velocityDirection = velocityDirection;
		velocity = Utils.getRandomFloat(70, 100) / mass;
		var reverseRotation = (Utils.getRandomFloat(0, 1) == 0) ? 1 : -1;
		rotationSpeed = velocity * 0.1 * reverseRotation;
		x = position.x;
		y = position.y;
		
		hp = MAX_HP / MAX_MASS * mass;
		
		trace("Asteroid created with parameters:\nMass: " + mass + "\nVelocity: " + velocity + "\nRotation speed: " + rotationSpeed + "\nHP: " + hp + "\n");
	}
	
	override public function update(deltaTime:Float) 
	{
		super.update(deltaTime);
	}
	
	override public function onHit(damage:Float) 
	{
		super.onHit(damage);
		hp -= damage;
	}
}