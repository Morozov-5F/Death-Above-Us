package particles;
import openfl.Assets;
import openfl.display.BitmapData;
import openfl.display.Sprite;
import openfl.Vector;

/**
 * Источник частиц
 * @author dcr30
 */
class ParticlesEmitter extends Sprite
{
	public var particlesContainer:Sprite;
	
	private var isActive:Bool = false;
	
	private var particlesType:Class<Particle>;
	private var particleSettings:Particle;
	
	private var particles:Array<Particle>;
	private var particleTexture:BitmapData;
	
	private var delay:Float = 0;

	public function new(particlesType:Class<Particle>, autoStart:Bool = true)
	{
		super();
		this.particlesType = particlesType;
		
		particlesContainer = new Sprite();
		
		particleSettings = Type.createInstance(particlesType, []);
		if (particleSettings.textureName == "none")
		{
			trace("No particle texture specified");
			return;
		}
		var texturePath:String = "img/particles/" + particleSettings.textureName + ".png";
		if (!Assets.exists(texturePath, AssetType.IMAGE))
		{
			trace("No particle texture found: " + texturePath);
			return;
		}
		particleTexture = Assets.getBitmapData(texturePath);
		
		if (autoStart)
		{
			start();
		}
		
		particles = new Array<Particle>();
	}
	
	/**
	 * Начать/возобновить создание частиц
	 */
	public function start()
	{
		isActive = true;
		if (particleSettings.emitOnce)
		{
			emit();
		}		
	}
	
	/**
	 * Остановить создание частиц
	 */
	public function stop()
	{
		isActive = false;
	}
	
	public function emit():Void
	{
		for (i in 0...particleSettings.emitCount)
		{
			createParticle();
		}
	}
	
	public function update(deltaTime:Float):Void
	{
		if (!isActive) 
		{
			return;
		}
		if (!particleSettings.emitOnce)
		{
			if (delay <= 0)
			{
				delay = particleSettings.emitDelay;
				emit();
			}
			else
			{
				delay -= deltaTime;
			}
		}
		
		for (i in 0...particles.length)
		{
			var particle = particles[i];
			if (particle != null)
			{
				particle.update(deltaTime);
				if (particle.isDead)
				{
					particlesContainer.removeChild(particle);
					particles[i] = null;
				}
			}
		}
	}
	
	private function createParticle():Void
	{
		if (particles.length >= particleSettings.maxCount)
		{
			return;
		}
		var insertIndex:Int = -1;
		for (i in 0...particles.length)
		{
			if (particles[i] == null)
			{
				insertIndex = i;
				break;
			}
		}
		
		var particle:Particle = Type.createInstance(particlesType, [particleTexture]);
		particle.x = x;
		particle.y = y;
		particlesContainer.addChild(particle);
		if (insertIndex == -1)
		{
			particles.push(particle);
		}
		else
		{
			particles[insertIndex] = particle;
		}
	}

}
