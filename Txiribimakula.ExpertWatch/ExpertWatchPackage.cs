using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Txiribimakula.ExpertWatch.Views;
using Task = System.Threading.Tasks.Task;

namespace Txiribimakula.ExpertWatch
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(ExpertWatchPackage.PackageGuidString)]
    [ProvideToolWindow(typeof(ExpertWatch))]
    [ProvideBindingPath]
    [ProvideOptionPage(typeof(BlueprintsOptionPage), "Expert Debug", "Blueprints", 0, 0, true)]
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

    [Guid("E77FB104-A860-4B35-A46D-BE33E3616FD4")]
    public class BlueprintsOptionPage : DialogPage
    {
        private string blueprints;
        public string Blueprints {
            get { return blueprints; }
            set { blueprints = value; OnOptionChanged(); }
        }


        protected override IWin32Window Window {
            get {
                ToolsOptionsBlueprintsUserControl page = new ToolsOptionsBlueprintsUserControl();
                page.BlueprintsOptionPage = this;
                page.Initialize();
                return page;
            }
        }

        public event OptionChanged OptionChangedEvent;
        private void OnOptionChanged() {
            OptionChangedEvent?.Invoke(blueprints);
        }
        public delegate void OptionChanged(string blueprints);
    }
}
