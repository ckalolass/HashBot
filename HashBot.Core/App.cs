using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore.IoC;
namespace HashBot.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<ViewModels.MainViewModel>();
            //RegisterAppStart<ViewModels.InfoViewModel>();
        }

    }
}
