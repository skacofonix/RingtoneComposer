using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using RingtoneComposer.Core.Services;

namespace RingtoneComposer.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<ViewModels.RingtonesViewModel>();

            //Mvx.RegisterSingleton<IRingtoneImporterService>(new RttlImporterService());
        }
    }
}