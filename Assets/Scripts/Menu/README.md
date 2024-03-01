# VR Menu

This VR menu system enables interaction with menu items in a VR environment using Unity's XR Interaction Toolkit. The system allows for seamless scene transitions and menu item activation through VR controllers.

## Features

- **Menu Item Interaction**: Interact with menu items using VR controllers to trigger scene loads or other actions.
- **Scene Transitions**: Asynchronously load scenes when menu items are activated.
- **Error Handling**: Robust error handling and null checks to ensure stability.

## Setup

1. **Add Menu Items**: Attach `MenuItemInteractable` to your menu item GameObjects.
2. **Implement IMenuItem**: Create new scripts for different menu actions by implementing the `IMenuItem` interface.
3. **Scene Loading**: Use `ShootingRangeMenuItem` or `RaceMenuItem` as examples to load scenes asynchronously.
4. **Menu Management**: Use `MenuManager` to show or hide the menu.

## How to Use

- **Creating a Menu Item**:
  - Implement the `IMenuItem` interface in your custom script.
  - Attach your script and `MenuItemInteractable` to the GameObject you want to use as a menu item.
- **Configuring Scene Transitions**:
  - Specify scene names in your custom `IMenuItem` implementations to load different scenes.
- **Menu Visibility**:
  - Use `MenuManager.ToggleMenu(GameObject menuObject, bool show)` to control the visibility of your menu.

## Components

### MenuItemInteractable

Handles user selection on menu items through VR interaction. Must be attached to each menu item GameObject.

### IMenuItem Interface

Defines the `Activate` method that is called when a menu item is selected. Implement this interface for custom menu item actions.

### ShootingRangeMenuItem & RaceMenuItem

Examples of `IMenuItem` implementations that load specific scenes.

### MenuManager

Manages overall menu visibility and can be used to show or hide the entire menu.

## Getting Started

1. Import the XR Interaction Toolkit from Unity's Package Manager.
2. Set up your XR Rig and ensure your project settings are correctly configured for VR.
3. Follow the "Setup" section to integrate the VR Menu System into your project.

Enjoy creating immersive VR menus for your games and experiences!

# VR Menu

Эта система меню для VR позволяет взаимодействовать с элементами меню в виртуальной реальности с использованием XR Interaction Toolkit от Unity. Система обеспечивает бесшовные переходы между сценами и активацию элементов меню через контроллеры VR.

## Особенности

- **Взаимодействие с элементами меню**: Взаимодействуйте с элементами меню с помощью контроллеров VR для запуска загрузки сцен или других действий.
- **Переходы между сценами**: Асинхронная загрузка сцен при активации элементов меню.
- **Обработка ошибок**: Надежная обработка ошибок и проверки на `null` для обеспечения стабильности.

## Настройка

1. **Добавление элементов меню**: Прикрепите `MenuItemInteractable` к GameObject'ам ваших элементов меню.
2. **Реализация IMenuItem**: Создайте новые скрипты для различных действий элементов меню, реализуя интерфейс `IMenuItem`.
3. **Загрузка сцен**: Используйте `ShootingRangeMenuItem` или `RaceMenuItem` в качестве примеров для асинхронной загрузки сцен.
4. **Управление меню**: Используйте `MenuManager` для отображения или скрытия меню.

## Использование

- **Создание элемента меню**:
  - Реализуйте интерфейс `IMenuItem` в вашем пользовательском скрипте.
  - Прикрепите ваш скрипт и `MenuItemInteractable` к GameObject, который вы хотите использовать в качестве элемента меню.
- **Настройка переходов между сценами**:
  - Укажите имена сцен в ваших реализациях `IMenuItem` для загрузки различных сцен.
- **Видимость меню**:
  - Используйте `MenuManager.ToggleMenu(GameObject menuObject, bool show)` для управления видимостью вашего меню.

## Компоненты

### MenuItemInteractable

Обрабатывает выбор пользователя на элементах меню через взаимодействие в VR. Должен быть прикреплен к каждому GameObject элемента меню.

### Интерфейс IMenuItem

Определяет метод `Activate`, который вызывается при выборе элемента меню. Реализуйте этот интерфейс для пользовательских действий элементов меню.

### ShootingRangeMenuItem & RaceMenuItem

Примеры реализаций `IMenuItem`, которые загружают определенные сцены.

### MenuManager

Управляет общей видимостью меню и может использоваться для отображения или скрытия всего меню.

## Начало работы

1. Импортируйте XR Interaction Toolkit из Package Manager Unity.
2. Настройте свой XR Rig и убедитесь, что настройки проекта корректно сконфигурированы для VR.
3. Следуйте разделу "Настройка", чтобы интегрировать систему меню VR в ваш проект.

Приятного создания погружающих меню VR для ваших игр и опытов!