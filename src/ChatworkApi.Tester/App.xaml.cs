namespace ChatworkApi.Tester
{
    using System;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Windows;
    using AutoMapper;
    using Domain;
    using Domain.Interactors;
    using Domain.Repositories;
    using Domain.Services;
    using Domain.UseCases;
    using Prism.Ioc;
    using Prism.Mvvm;
    using Prism.Unity;
    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;
    using ViewModels.Bus;
    using Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication, IChatworkApiTokenRegister
    {
        private void Mapping()
        {
            var config = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>());
            config.AssertConfigurationIsValid();
            MapExtensions.SetMapper(new Mapper(config));
        }

        public App()
        {
            Mapping();
        }

        /// <summary>
        /// Used to register types with the container that will be used by your application.
        /// </summary>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var unityContainer = containerRegistry.GetContainer();

            // 特定の型をインスタンス化する際のコンストラクタへのパラメータ引き渡しを登録する。
            unityContainer.RegisterType<Encryption>(new InjectionConstructor(Tester.Properties.Resources.EncryptionKey, 256));
            unityContainer.RegisterType<Decryption>(new InjectionConstructor(Tester.Properties.Resources.EncryptionKey, 256));
            unityContainer.RegisterType<GetUserProfileInteractor>(new InjectionConstructor(new ResolvedParameter<IChatworkApiService>()));
            unityContainer.RegisterType<GetMyTasksInteractor>(new InjectionConstructor(new ResolvedParameter<IChatworkApiService>()));
            unityContainer.RegisterType<GetMyRoomsInteractor>(new InjectionConstructor(new ResolvedParameter<IChatworkApiService>()));
            unityContainer.RegisterType<GetRoomMessagesInteractor>(new InjectionConstructor(new ResolvedParameter<IChatworkApiService>()));
            unityContainer.RegisterType<GetContactsInteractor>(new InjectionConstructor(new ResolvedParameter<IChatworkApiService>()));
            unityContainer.RegisterType<WorkTaskStateChangeInteractor>(new InjectionConstructor(new ResolvedParameter<IChatworkApiService>()));
            unityContainer.RegisterType<GetRoomMembersInteractor>(new InjectionConstructor(new ResolvedParameter<IChatworkApiService>()));
            unityContainer.RegisterType<AddMessageInteractor>(new InjectionConstructor(new ResolvedParameter<IChatworkApiService>()));
            unityContainer.RegisterType<GetRoomDataInteractor>(new InjectionConstructor(new ResolvedParameter<IChatworkApiService>()));
            unityContainer.RegisterType<ChatRoomUseCaseBus>(new InjectionConstructor(new ResolvedParameter<IGetMyRoomsUseCase>()
                                                                                   , new ResolvedParameter<IGetRoomMessagesUseCase>()
                                                                                   , new ResolvedParameter<IGetRoomMembersUseCase>()
                                                                                   , new ResolvedParameter<IAddWorkTaskUseCase>()
                                                                                   , new ResolvedParameter<IAddMessageUseCase>()
                                                                                   , new ResolvedParameter<IGetRoomDataUseCase>()));
            unityContainer.RegisterType<EncryptionBus>(new InjectionConstructor(new ResolvedParameter<IEncryption>()
                                                                              , new ResolvedParameter<IDecryption>()));

            unityContainer.RegisterType<IEncryption, Encryption>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDecryption, Decryption>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IEncryptionBus, EncryptionBus>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IConfigurationRepository, ConfigurationRepository>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IChatworkApiService, ChatworkApiService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IGetUserProfileUseCase, GetUserProfileInteractor>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IGetMyTasksUseCase, GetMyTasksInteractor>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IGetMyRoomsUseCase, GetMyRoomsInteractor>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IGetRoomMessagesUseCase, GetRoomMessagesInteractor>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IGetContactsUseCase, GetContactsInteractor>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IWorkTaskStateChangeUseCase, WorkTaskStateChangeInteractor>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IGetRoomMembersUseCase, GetRoomMembersInteractor>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IAddWorkTaskUseCase, AddWorkTaskInteractor>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IChatRoomUseCaseBus, ChatRoomUseCaseBus>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IAddMessageUseCase, AddMessageInteractor>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IGetRoomDataUseCase, GetRoomDataInteractor>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterInstance(typeof(IChatworkApiTokenRegister), this);
        }

        /// <summary>Creates the shell or main window of the application.</summary>
        /// <returns>The shell of the application.</returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        /// <summary>Initializes the shell.</summary>
        protected override void InitializeShell(Window shell)
        {
            //base.InitializeShell(shell);

            var assembly = Assembly.GetEntryAssembly();
            var assemblyName = assembly.GetName();
            shell.Title = $"{assemblyName.Name} - {assemblyName.Version}";
            shell.Show();
        }

        /// <summary>
        /// Configures the <see cref="T:Prism.Mvvm.ViewModelLocator" /> used by Prism.
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType => Type.GetType($"ChatworkApi.Tester.ViewModels.{(viewType.Name.EndsWith("View") ? viewType.Name : $"{viewType.Name}View")}Model"));
        }

        public void Register(string apiToken)
        {
            Container.Resolve<IChatworkApiService>().Register(apiToken);
        }
    }
}