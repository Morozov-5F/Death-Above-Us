package screens;
import openfl.display.Sprite;
import screens.Screen;
/**
 * ...
 * @author someone
 */
class ScreenManager extends Sprite
{
	private var currentScreen:Screen;
	
	public function new() 
	{
		super();
	}
	
	public function loadScreen(screen:Screen = null) 
	{
		// Отключение текущего экрана
		if (currentScreen != null)
		{
			if (currentScreen.parent != null)
			{
				removeChild(currentScreen);
			}
			if (!currentScreen.unload())
			{
				trace("Не удалось выгрузить экран");
			}
			currentScreen = null;
		}
		// Загрузка нового экрана
		if (screen != null)
		{
			if (screen.parent != null)
			{
				screen.parent.removeChild(screen);
			}
			addChild(screen);
			if (!screen.load())
			{
				trace("Не удалось загрузить экран");
			}
		}
	}
	
}