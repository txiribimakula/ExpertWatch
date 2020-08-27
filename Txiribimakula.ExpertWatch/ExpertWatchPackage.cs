using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace Txiribimakula.ExpertWatch
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(ExpertWatchPackage.PackageGuidString)]
    [ProvideToolWindow(typeof(ExpertWatch))]
    public sealed class ExpertWatchPackage : AsyncPackage
    {
        public const string PackageGuidString = "2f2f2923-9433-4dcb-b3b6-373c61e85461";

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress) {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            ExpertWatchCommand.Initialize(this);
        }

        public new object GetService(Type serviceType) {
            return base.GetService(serviceType);
        }

        public new DialogPage GetDialogPage(Type dialogPageType) {
            return base.GetDialogPage(dialogPageType);
        }
    }
}
