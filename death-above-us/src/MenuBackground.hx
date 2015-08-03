package;

import openfl.Assets;
import openfl.display.Bitmap;
import openfl.display.BitmapData;
import openfl.display.Sprite;
import openfl.events.Event;
import openfl.filters.BitmapFilter;
import openfl.Vector;

/**
 * ...
 * @author someone
 */
class MenuBackground extends Sprite
{
	// Небо
	private var STARS_MOVEMENT_SPEED:Float = 20;
	private var stars:Vector<Bitmap>;
	private var starsContainer:Sprite;
	// Фон
	private var BACKGROUND_SCROLLING_SPEED:Float = 15;
	private var backgroundLayers:Vector<Sprite>;
	private var bgDepths = [ 0.4, 0.6, 0.7, 1 ];
	// Пули
	private var MAX_PROJECTILE_DELAY = 2;
	private var MAX_PROJECTILE_SPEED = 500;
	private var projectiles:Array<Bitmap>;
	private var projectileDelay:Float;
	private var projectileBitmapData:BitmapData;
	private var projectilesContainer:Sprite;

	public function new() 
	{
		super();
		addEventListener(Event.ADDED_TO_STAGE, initialize);
	}
	
	private function initialize(e:Event):Void 
	{
		removeEventListener(Event.ADDED_TO_STAGE, initialize);
		
		// Небо
		var sky:Bitmap = new Bitmap(Assets.getBitmapData("img/menu/sky2.png"));
		sky.scaleX = sky.scaleY = stage.stageHeight / sky.height;
		sky.x = stage.stageWidth / 2 - sky.width / 2;
		addChild(sky);		
		
		// Звёзды
		starsContainer = new Sprite();
		addChild(starsContainer);
		
		stars = new Vector<Bitmap>(2, true);
		var starsBitmapData:BitmapData = Assets.getBitmapData("img/menu/sky1.png"); 
		for (i in 0...2)
		{
			stars[i] = new Bitmap(starsBitmapData);
			stars[i].scaleX = stars[i].scaleY = sky.scaleX;
			stars[i].x = sky.x;
			starsContainer.addChild(stars[i]);
		}
		stars[1].y = stars[0].y + stars[0].height;
		
		// Выстрелы
		projectilesContainer = new Sprite();
		addChild(projectilesContainer);
		projectiles = new Array();
		projectileBitmapData = Assets.getBitmapData("img/menu/projectile.png");
		projectileDelay = 0;
		
		// Фон
		backgroundLayers = new Vector<Sprite>(4, true);
		for (i in 0...4)
		{
			var path:String = "img/menu/bg" + (i + 1) + ".png";
			var layerBitmapData = Assets.getBitmapData(path);
			backgroundLayers[i] = new Sprite();
			backgroundLayers[i].scaleX = backgroundLayers[i].scaleY = sky.scaleX;
			var left:Bitmap = new Bitmap(layerBitmapData);
			var right:Bitmap = new Bitmap(layerBitmapData);
			right.x = left.width;
			backgroundLayers[i].addChild(left);
			backgroundLayers[i].addChild(right);
			addChild(backgroundLayers[i]);
		}
	}
	
	public function update(deltaTime:Float):Void
	{
		for (star in stars)
		{
			starsContainer.y -= STARS_MOVEMENT_SPEED * deltaTime;
			if (starsContainer.y <= -star.height)
			{
				starsContainer.y += star.height;
			}
		}
		
		for (i in 0...backgroundLayers.length)
		{
			var layer:Sprite = backgroundLayers[i];
			layer.x -= BACKGROUND_SCROLLING_SPEED * bgDepths[i] * deltaTime;
			if (layer.x <= -layer.width / 2)
			{
				layer.x += layer.width / 2;
			}
		}
		
		var i:Int = 0;
		while (i < projectiles.length)
		{
			var projectile:Bitmap = projectiles[i];
			var radians:Float = projectile.rotation / 180 * Math.PI;
			projectile.x += Math.cos(radians) * deltaTime * MAX_PROJECTILE_SPEED * projectile.scaleX;
			projectile.y += Math.sin(radians) * deltaTime * MAX_PROJECTILE_SPEED * projectile.scaleY;
		
			if (projectile.y < -projectile.width)
			{
				projectiles.splice(i, 1);
			}
			i++;
		}
		
		projectileDelay -= deltaTime;
		if (projectileDelay <= 0)
		{
			projectileDelay = MAX_PROJECTILE_DELAY * Math.random() + 0.01;
			
			var projectile:Bitmap = new Bitmap(projectileBitmapData);
			projectile.x = Math.random() * stage.stageWidth * 0.7 + 0.15;
			projectile.y = stage.stageHeight + projectile.width * 2;
			
			projectile.rotation = -90 + (Math.random() * 80) - 40;
			projectile.scaleX = projectile.scaleY = Math.random() * 0.5 + 0.8;
			projectile.cacheAsBitmap = true;
			projectiles.push(projectile);
			projectilesContainer.addChild(projectile);
		}
	}
	
}