package particles;
import openfl.display.Bitmap;
import openfl.display.BitmapData;
import openfl.display.Sprite;

/**
 * Базовый класс для частиц
 * @author dcr30
 */
class Particle extends Sprite
{
	// Свойства, индивидуальные для каждой частицы
	private var bitmap:Bitmap;
	// Скорость
	private var sx:Float = 0;
	private var sy:Float = 0;
	private var lifeTime:Float = 0;
	public 	var isDead:Bool = false;
		
	// Свойства, общие для всех частиц определенного типа
	public var textureName:String = "none";
	public var originX:Float 	= 0.5;
	public var originY:Float 	= 0.5;
	// Физика
	public var friction:Float 	= 1;
	public var forceX:Float 	= 0;
	public var forceY:Float 	= 0;
	// Спавн
	public var emitOnce:Bool 	= true;	// При true частицы спавнятся только один раз
	public var emitDelay:Float 	= 0;	// Если spawnOnce == false, частицы спавнятся через фиксированный промежуток времени
	public var emitCount:Int 	= 1;	// Количество частиц при спавне
	// Ограничения
	public var maxLifeTime:Float = 60;
	public var maxCount:Int 	= 1000;
	
	/**
	 * Базовый конструктор для частиц
	 * @param	texture Текстура частицы. Должна создаваться в ParticleEmitter'e
	 */
	public function new(texture:BitmapData)
	{
		super();
		bitmap = new Bitmap(texture);
		addChild(bitmap);
		bitmap.x = -bitmap.width * originX;
		bitmap.y = -bitmap.height * originY;
		
		lifeTime = 0;
		isDead = false;
	}
	
	public function update(deltaTime:Float):Void
	{
		if (isDead)
		{
			return;
		}
		x += sx * deltaTime;
		y += sy * deltaTime;
		sx += forceX * deltaTime;
		sy += forceY * deltaTime;
		sx *= friction;
		sy *= friction;
		lifeTime += deltaTime;
		if (lifeTime >= maxLifeTime)
		{
			isDead = true;
		}
	}

}
