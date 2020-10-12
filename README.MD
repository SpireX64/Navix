# Navix - Cross-platform navigation library for .NET

## Описание
Navix - кроссплатформенная библиотека навигации для .NET приложений.
Она позволяет вынести логику навигации с уровня платформы/фреймворка
в общую (переносимую) логику.

Основные функции уже реализованы, однако интерфейсы все еще могут быть изменены.

На данный момент готова поддержка только для платформы Android:
- Navix.Xamarin.AndroidX
- Navix.Xamarin.AndroidSupport
- Navix.Xamarin.AndroidLegacy

В дальнейшем планируется добавить поддержку:
- Xamarin iOS
- Xamarin Forms
- AvaloniaUI
- Blazor
- WPF



## Как использовать?

### 1. Экраны

В проекте с общей логикой создаются классы, идентифицирующие экраны приложения.
Эти классы должны наследоваться от класса `Screen`.

Например, имея экраны 'Catalog' и 'CatalogItem', можно создать следующие классы:
```csharp
// Экран каталога предметов
public class CatalogScreen: Screen {}

// Экран с подробностями о предмете
public class CatalogItemScreen: Screen 
{
    public CatalogItemScreen(long id) 
    {
        ItemID = id;
    }

    public long ItemID { get; } 
}
```

Во второй класс было добавлено свойство `ItemID`. Определение таких свойств
позволяет передавать типизированные данные при переходе между экранами.
В данном случае мы пробрасываем идентификатор предмета каталога.


### 2. Экраны на платформе/фреймворке

Сам Navix, из коробки, не знает о том как открывать и выполнять навигацию
на конкретной платформе (или фреймворке). Для этого в проекте, в котором пишется
платформ-зависимый код (например AndroidApp), добавляется специальная реализация
навигатора под текущую платформу (для Xamarin.Android: `Navix.Xamarin.AndroidX`).

В пакете `Navix.Xamarin.AndroidX` есть интерфейсы, позволяющие рассказать Navix,
о том как открыть экран в Android-приложении:
- `IActivityScreenResolver` - для экранов на основе `Activity`
- `IFragmentScreenResolver` - для экранов на основе `Fragment`

В данном примене, я буду использовать навигацию с помощью фрегментов:

```csharp
public class CatalogScreenResolver: IFragmentScreenResolver 
{
    public Fragment GetFragment(Screen screen) 
    {
        return new CatalogFragment();
    }
}

public class CatalogItemScreenResolver: IFragmentScreenResolver 
{
    public Fragment GetFragment(Screen screen) 
    {
        var itemScreen = (CatalogItemScreen) screen;
        var args = new Bundle();
        args.PutLong(CatalogItemFragment.ArgumentItemID, itemScreen.ItemId);

        var fragment = new CatalogItemFragment();
        fragment.SetArguments(args);
        return fragment;
    }
}
```

Refs: [Fragment](https://docs.microsoft.com/ru-ru/dotnet/api/android.app.fragment?view=xamarin-android-sdk-9), [Bundle](https://docs.microsoft.com/ru-ru/dotnet/api/android.os.bundle?view=xamarin-android-sdk-9)

### 3. Настройка Navix
Так как Navix не использует рефлекцию из коробки, теребуется предоставить ему класс,
в котором будет установлека связь между классом экрана (`Screen`) 
и его предоставителем (`IScreenResolver`). Это делается с помощью реализации
абстрактного класса `NavixConfig`:

```csharp
public class AppNavigationConfig: NavixConfig 
{
    public override void Configure(IScreenRegistry registry)
    {
        registry.Register(
            typeof(CatalogScreen), 
            new CatalogScreenResolver());

        registry.Register(
            typeof(CatalogItemScreen), 
            new CatalogItemScreenResolver());
    }
}
```

### 4. Инициализация Navix

Имея подготовленный класс для настройки Navix, можно его инициализировать.
Для этого создается класс `NavixSerice`:

```csharp
var config = new AppNavigationConfig();
var navix = new NavixService(config);
```

Из полученного экземпляра класса нам важно получить 2 объекта:
- IRouter - с помощью которого мы будет отдавать команды в Navix из общего кода.
- INavigatorHolder - для регистрации навигатора платформы.

```csharp
var router = navix.Router;
var navHolder = navix.NavigatorHolder;
```

### 5. Подготовка Activity (Android)

```csharp
[Activity(Theme="@style/AppTheme.NoActionBar")]
public class MainActivity: FragmentActivity {
    
    private AndroidNavigator _navigator;
    private INavigatorHolder _navigatorHolder;

    public override void OnCreate(Bundle savedInstanceState) {
        base.OnCreate(savedInstanceState);
        setContentView(Resource.Layout.activity_main);

        // Создаем экземпляр андроид навигатора для Navix
        // 'Resource.Id.container' - контейнер для фрагментов
        _navigator = new AndroidNavigator(this, Resource.Id.container);

        // INavigatorHolder мы получили из NavixService
        // как его передать в Activity - на усмотрение разработчика
        // Это можно реализовать с помощью DI (например Autofac)
        // я же просто проброшу свойство из Application
        _navigatorHolder = ((App) Application).NavHolder;
    }

    public override void OnResume() {
        base.OnResume();

        // Захватываем навигатор под контроль Navix
        _navigatorHolder.SetNavigator(_navigator);
    }

    public override void OnPause() {
        // Освобождаем навигатор
        _navigatorHolder.RemoveNavigator();
        base.OnPause();
    }
}
```

```xml
<!-- Layout/activity_main.xml -->
<?xml version="1.0" encoding="utf-8"?>
<FrameLayout xmlns:android="http://schemas.android.com/apk/res/android"
                android:id="@+id/container" 
                android:layout_width="match_parent" 
                android:layout_height="match_parent">
    <fragment android:name="HomeFragment"
                android:layout_width="match_parent"
                android:layout_height="match_parent"/>
</FragmeLayout>
```

### 6. Navix готов к работе!
Теперь используя полученный объект `IRouter` можно испытать работу Navix.
Например попросить перейти на экран каталога:

```csharp
    IRouter router = navix.Router;

    // Перейти на экран предмета с ID=4
    router.Forward(new CatalogItemScreen(4));

    // ...

    // Вернуться назад
    router.Back();
```