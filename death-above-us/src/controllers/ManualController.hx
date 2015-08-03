package controllers;
import objects.Turret;
import openfl.Lib;
import openfl.ui.Keyboard;
import screens.GameScreen;

/**
 * Реализует ручное управление турелью
 * @author Evgeniy Morozov
 */
class ManualController implements IController
{
	public var controllableTurret:Turret;
	public var leftToRight:Bool;
	
	private var prevPointerPosition:Float;
	
	/**
	 * Создает экземпляр класса ManualController с привязкой к турели
	 * @param	turretToControl турель, которую необходимо контроллировать
	 * @param	leftToRight сторона экрана, ответственная за стрельбу
	 *
	public function new(turretToControl:Turret, leftToRight:Bool = false) 
	{
		this.leftToRight = leftToRight;
		controllableTurret = turretToControl;
		
		prevPointerPosition = 0;
	}*/
	
	/**
	 * Создает экземпляр класса ManualController без привязки к турели
	 * @param	leftToRight сторона экрана, ответственная за стрельбу
	 */
	public function new (leftToRight:Bool)
	{
		this.leftToRight = leftToRight;
		
		prevPointerPosition = 0;
	}
	
	/* INTERFACE controllers.IController */
	
	public function update(deltaTime:Float):Void 
	{
		var stage = Lib.current.stage;
		var rotation = Utils.cameraPosX;
		
		#if mobile
			// TODO: реализовать управление касаниями
			trace("Mobile platforms not yet supported");
		#else	
			if (GameScreen.mouseDown)
			{
				rotation += (stage.mouseX - prevPointerPosition) * deltaTime * 20;
			}
			prevPointerPosition = stage.mouseX;
			
			if (controllableTurret == null)
			{
				trace("Turret is null");
				return;
			}
			
			if (GameScreen.keys[Keyboard.SPACE])
			{
				//trace("FIRE ZE MISSELS");
				controllableTurret.weapon.createShot();
			}
			
			rotation = controllableTurret.setRotation(rotation);
			Utils.cameraPosX = rotation;
		#end
	}
	
}