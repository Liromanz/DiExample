# Пример инверсии зависимостей с использованием DI-контейнера

### Описание: 
В приложении присутствует архитектура MVVM. В ее рамках, есть два окна - MainWindow и SecondWindow. Вся логика приложения - вывод сообщения. Первое окошко может показать сообщение и переключиться на второе окно, второе окно - просто показывает сообщение.

Логика сообщений - бизнес логика. Поэтому она вынесена в сервисы. Чтобы бизнес-логика была расширяемой, реализован интерфейс - IMessageService и его реализация - MessageService


### Реализация DI

Каждый конструктор принимает интерфейс, чтобы можно было пихнуть любую реализацию

```
 public MainWindow(IMessageService messageService)
 {
     _service = messageService;  

     InitializeComponent();
     DataContext = new MainVM(_service);
 }
```

Но передавать напрямую экземпляр - затея плохая, поэтому есть DI-контейнер в App.xaml.cs

1. В App.xaml нет Startup
2. App.xaml.cs состоит из IHost, который и является контейнером, билдера который хранит все иньекции, и два переписанных метода - OnStartup, который включает хост, и OnExit, который выключает хост
3. Все окна регистрируются внутри Host. Контейнер потом понимает что куда и почему идет
4. Последующие зависимости также регистрируются в Host, сопоставляя интерфейс и реализацию. DI-контейнер потом последовательно смотрит на каждую зависимость "Есть окно, ему надо эот. Это у нас есть? Да, внедрим. Внедрение требует это, это у нас есть? Да, внедрим" и так далее

```
public partial class App : Application
{
    public static IHost? AppHost { get; set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<SecondWindow>();
            services.AddTransient<IMessageService, MessageService>();
        }).Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startupWindow = AppHost.Services.GetRequiredService<MainWindow>();
        startupWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}
```