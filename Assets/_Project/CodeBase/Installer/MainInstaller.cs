using _Project.CodeBase.Data;
using _Project.CodeBase.QuizMechanics;
using _Project.CodeBase.UI;
using _Project.CodeBase.Utils;
using _Project.CodeBase.VFX;
using CodeBase.Card;
using CodeBase.Grid;
using CodeBase.Level;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Installer
{
    public class MainInstaller : MonoInstaller
    {
        [Header("Services")] [SerializeField] private DataService _dataService;

        [Header("Prefabs")] [SerializeField] private Card _cardPrefab;
        [SerializeField] private ParticleSystem _starParticle;

        [Header("UI")] [SerializeField] private TaskView _taskView;
        [SerializeField] private CustomGridLayout _gridLayout;
        [SerializeField] private EndGameView _endGameView;
        [SerializeField] private LoadingView _loadingView;


        public override void InstallBindings()
        {
            BindUI();
            BindGridLayout();
            BindCardCreator();
            BindAnswerSystem();
            BindCoroutines();
            BindDataService();
            BindVFX();

            BindGameLoop();
        }

        private void BindVFX()
        {
            Container.Bind<Particle>()
                .AsSingle();

            Container.Bind<ParticleSystem>()
                .FromComponentOn(_starParticle.gameObject)
                .AsSingle();
        }

        private void BindDataService()
        {
            Container.Bind<IDataService>()
                .To<DataService>()
                .FromComponentOn(_dataService.gameObject)
                .AsSingle();
        }

        private void BindGameLoop()
        {
            Container.BindInterfacesAndSelfTo<GameLoop>()
                .AsSingle();
        }

        private void BindCoroutines()
        {
            Container.Bind<Coroutines>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName("[Coroutines]")
                .AsSingle();
        }

        private void BindAnswerSystem()
        {
            Container.BindInterfacesAndSelfTo<AnswerListener>().AsSingle();
            Container.Bind<UniqueAnswerService>().AsSingle();
        }

        private void BindCardCreator()
        {
            Container.Bind<CardPackFactory>()
                .AsSingle()
                .WithArguments(_cardPrefab);

            Container.Bind<CardCreator>().AsSingle();
        }

        private void BindGridLayout()
        {
            Container.Bind<CustomGridLayout>()
                .FromComponentOn(_gridLayout.gameObject)
                .AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<EndGameView>()
                .FromComponentOn(_endGameView.gameObject)
                .AsSingle();

            Container.Bind<TaskView>()
                .FromComponentOn(_taskView.gameObject)
                .AsSingle();

            Container.Bind<LoadingView>()
                .FromComponentOn(_loadingView.gameObject)
                .AsSingle();
        }
    }
}